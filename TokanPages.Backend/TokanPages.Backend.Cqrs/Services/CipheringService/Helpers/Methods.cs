namespace TokanPages.Backend.Cqrs.Services.CipheringService.Helpers;

using System;
using System.Text;
using System.Collections.Generic;

public static class Methods
{
    /// <summary>
    /// Encode a byte array using BCrypt's slightly-modified
    /// Base64 encoding scheme. Note that this is _not_ compatible
    /// with the standard MIME-Base64 encoding.
    /// </summary>
    /// <param name="arrayToDecode">
    /// The byte array to encode
    /// </param>
    /// <param name="length">
    /// The number of bytes to encode
    /// </param>
    /// <returns>
    /// A Base64-encoded string
    /// </returns>
    public static string EncodeBase64(IReadOnlyList<byte> arrayToDecode, int length) 
    {
        if (length <= 0 || length > arrayToDecode.Count) 
            throw new ArgumentOutOfRangeException(nameof(length), length, null);
            
        var stringBuilder = new StringBuilder(length * 2);

        for (var offset = 0; offset < length;) 
        {
            var countPrimary = arrayToDecode[offset++] & 0xff;
            stringBuilder.Append(Arrays.FTableForBase64Encoding[(countPrimary >> 2) & 0x3f]);
            countPrimary = (countPrimary & 0x03) << 4;
            
            if (offset >= length) 
            {
                stringBuilder.Append(Arrays.FTableForBase64Encoding[countPrimary & 0x3f]);
                break;
            }
            
            var countSecondary = arrayToDecode[offset++] & 0xff;
            countPrimary |= (countSecondary >> 4) & 0x0f;
            stringBuilder.Append(Arrays.FTableForBase64Encoding[countPrimary & 0x3f]);
            countPrimary = (countSecondary & 0x0f) << 2;
            
            if (offset >= length) 
            {
                stringBuilder.Append(Arrays.FTableForBase64Encoding[countPrimary & 0x3f]);
                break;
            }
            
            countSecondary = arrayToDecode[offset++] & 0xff;
            countPrimary |= (countSecondary >> 6) & 0x03;
            stringBuilder.Append(Arrays.FTableForBase64Encoding[countPrimary & 0x3f]);
            stringBuilder.Append(Arrays.FTableForBase64Encoding[countSecondary & 0x3f]);
        }

        return stringBuilder.ToString();
    }

    /// <summary>Decode a string encoded using BCrypt's Base64 scheme to a
    /// byte array. Note that this is _not_ compatible with the standard
    /// MIME-Base64 encoding.
    /// </summary>
    /// <param name="stringToDecode">
    /// The string to decode
    /// </param>
    /// <param name="maximumLength">
    /// The maximum number of bytes to decode
    /// </param>
    /// <returns>
    /// An array containing the decoded bytes
    /// </returns>
    public static byte[] DecodeBase64(string stringToDecode, int maximumLength) 
    {
        var bytes = new List<byte>(Math.Min(maximumLength, stringToDecode.Length));

        if (maximumLength <= 0) 
            throw new ArgumentOutOfRangeException(nameof(maximumLength), maximumLength, null);

        for (int offset = 0, stringLength = stringToDecode.Length, length = 0; offset < stringLength - 1 && length < maximumLength; ) 
        {
            var countPrimary = Char64(stringToDecode[offset++]);
            var countSecondary = Char64(stringToDecode[offset++]);
            if (countPrimary == -1 || countSecondary == -1) 
                break;

            bytes.Add((byte)((countPrimary << 2) | ((countSecondary & 0x30) >> 4)));
            if (++length >= maximumLength || offset >= stringToDecode.Length) 
                break;

            var countTertiary = Char64(stringToDecode[offset++]);
            if (countTertiary == -1) 
                break;

            bytes.Add((byte)(((countSecondary & 0x0f) << 4) | ((countTertiary & 0x3c) >> 2)));
            if (++length >= maximumLength || offset >= stringToDecode.Length) 
                break;

            var countQuaternary = Char64(stringToDecode[offset++]);
            bytes.Add((byte)(((countTertiary & 0x03) << 6) | countQuaternary));

            ++length;
        }

        return bytes.ToArray();
    }

    /// <summary>
    /// Perform the central password hashing step in the bcrypt scheme.
    /// </summary>
    /// <param name="password">
    /// The password to hash.
    /// </param>
    /// <param name="salt">
    /// The binary salt to hash with the password.
    /// </param>
    /// <param name="logRounds">
    /// The binary logarithm of the number of rounds of hashing to apply.
    /// </param>
    /// <returns>
    /// An array containing the binary hashed password.
    /// </returns>
    public static byte[] CryptRaw(IReadOnlyList<byte> password, IReadOnlyList<byte> salt, int logRounds) 
    {
        var data = new uint[Arrays.CryptCipherText.Length];
        Arrays.CryptCipherText.CopyTo(data, 0);

        var dataLength = data.Length;
        if (logRounds is < 4 or > 31) 
            throw new ArgumentOutOfRangeException(nameof(logRounds), logRounds, null);

        var rounds = 1 << logRounds;
        if (salt.Count != Constants.CryptSaltLength) 
            throw new ArgumentException(@"Invalid salt length.", nameof(salt));

        InitializeBlowfishKey();
        EnhancedKeySchedule(salt, password);

        for (var index = 0; index < rounds; index++) 
        {
            Key(password);
            Key(salt);
        }

        for (var index = 0; index < 64; index++) 
        {
            for (var innerIndex = 0; innerIndex < dataLength >> 1; innerIndex++) 
            {
                Encipher(data, innerIndex << 1);
            }
        }

        var result = new byte[dataLength * 4];
        for (int index1 = 0, index2 = 0; index1 < dataLength; index1++) 
        {
            result[index2++] = (byte)((data[index1] >> 24) & 0xff);
            result[index2++] = (byte)((data[index1] >> 16) & 0xff);
            result[index2++] = (byte)((data[index1] >> 8) & 0xff);
            result[index2++] = (byte)(data[index1] & 0xff);
        }

        return result;
    }
        
    /// <summary>Look up the 3 bits base64-encoded by the specified
    /// character, range-checking against the conversion table.
    /// </summary>
    /// <param name="base64EncodedValue">
    /// The Base64-encoded value
    /// </param>
    /// <returns>
    /// The decoded value of <c>x</c>
    /// </returns>
    private static int Char64(char base64EncodedValue) 
    {
        int value = base64EncodedValue;
        return value < 0 || value > Arrays.FTableForBase64Decoding.Length 
            ? -1 
            : Arrays.FTableForBase64Decoding[value];
    }
        
    /// <summary>
    /// Blowfish encipher a single 64-bit block encoded as two 32-bit halves.
    /// </summary>
    /// <param name="block">
    /// An array containing the two 32-bit half blocks.
    /// </param>
    /// <param name="offset">
    /// The position in the array of the
    /// blocks.
    /// </param>
    private static void Encipher(IList<uint> block, int offset) 
    {
        uint index = 0;
        var length = block[offset]; 
        var record = block[offset + 1];

        length ^= Arrays.ExpandedBlowfishKeyPrimary[0];
        while (index <= Constants.BlowfishNumRounds - 2) 
        {
            // Feistel substitution on left word
            var number = Arrays.ExpandedBlowfishKeySecondary[(length >> 24) & 0xff];
            number += Arrays.ExpandedBlowfishKeySecondary[0x100 | ((length >> 16) & 0xff)];
            number ^= Arrays.ExpandedBlowfishKeySecondary[0x200 | ((length >> 8) & 0xff)];
            number += Arrays.ExpandedBlowfishKeySecondary[0x300 | (length & 0xff)];
            record ^= number ^ Arrays.ExpandedBlowfishKeyPrimary[++index];

            // Feistel substitution on right word
            number = Arrays.ExpandedBlowfishKeySecondary[(record >> 24) & 0xff];
            number += Arrays.ExpandedBlowfishKeySecondary[0x100 | ((record >> 16) & 0xff)];
            number ^= Arrays.ExpandedBlowfishKeySecondary[0x200 | ((record >> 8) & 0xff)];
            number += Arrays.ExpandedBlowfishKeySecondary[0x300 | (record & 0xff)];
            length ^= number ^ Arrays.ExpandedBlowfishKeyPrimary[++index];
        }
        
        block[offset] = record ^ Arrays.ExpandedBlowfishKeyPrimary[Constants.BlowfishNumRounds + 1];
        block[offset + 1] = length;
    }

    /// <summary>
    /// Cyclically extract a word of key material.
    /// </summary>
    /// <param name="data">
    /// The string to extract the data from.
    /// </param>
    /// <param name="offset">
    /// The current offset into data.
    /// </param>
    /// <returns>
    /// The next work of material from data.
    /// </returns>
    private static uint StreamToWord(IReadOnlyList<byte> data, ref int offset) 
    {
        uint word = 0;

        for (var index = 0; index < 4; index++) 
        {
            word = (word << 8) | data[offset];
            offset = (offset + 1) % data.Count;
        }

        return word;
    }

    /// <summary>
    /// Initialize the Blowfish key schedule.
    /// </summary>
    private static void InitializeBlowfishKey() 
    {
        Arrays.ExpandedBlowfishKeyPrimary = new uint[Arrays.FInitialKeyContentSchedule.Length];
        Arrays.FInitialKeyContentSchedule.CopyTo(Arrays.ExpandedBlowfishKeyPrimary, 0);
        Arrays.ExpandedBlowfishKeySecondary = new uint[Arrays.FKeys.Length];
        Arrays.FKeys.CopyTo(Arrays.ExpandedBlowfishKeySecondary, 0);
    }

    /// <summary>
    /// Key the Blowfish cipher.
    /// </summary>
    /// <param name="key">
    /// An array containing the key.
    /// </param>
    private static void Key(IReadOnlyList<byte> key) 
    {
        uint[] block = { 0, 0 };
        var primaryKeyLength = Arrays.ExpandedBlowfishKeyPrimary.Length; 
        var secondaryKeyLength = Arrays.ExpandedBlowfishKeySecondary.Length;

        var offset = 0;
        for (var index = 0; index < primaryKeyLength; index++) 
        {
            Arrays.ExpandedBlowfishKeyPrimary[index] ^= StreamToWord(key, ref offset);
        }

        for (var index = 0; index < primaryKeyLength; index += 2) 
        {
            Encipher(block, 0);
            Arrays.ExpandedBlowfishKeyPrimary[index] = block[0];
            Arrays.ExpandedBlowfishKeyPrimary[index + 1] = block[1];
        }

        for (var index = 0; index < secondaryKeyLength; index += 2) 
        {
            Encipher(block, 0);
            Arrays.ExpandedBlowfishKeySecondary[index] = block[0];
            Arrays.ExpandedBlowfishKeySecondary[index + 1] = block[1];
        }
    }

    /// <summary>
    /// Perform the "enhanced key schedule" step described by Provos
    /// and Mazieres in "A Future-Adaptable Password Scheme"
    /// (http://www.openbsd.org/papers/bcrypt-paper.ps).
    /// </summary>
    /// <param name="data">
    /// Salt information.
    /// </param>
    /// <param name="key">
    /// Password information.
    /// </param>
    private static void EnhancedKeySchedule(IReadOnlyList<byte> data, IReadOnlyList<byte> key) 
    {
        uint[] block = { 0, 0 };
        var primaryKeyLength = Arrays.ExpandedBlowfishKeyPrimary.Length;
        var secondaryKeyLength = Arrays.ExpandedBlowfishKeySecondary.Length;

        var keyOffset = 0;
        for (var index = 0; index < primaryKeyLength; index++) 
        {
            Arrays.ExpandedBlowfishKeyPrimary[index] ^= StreamToWord(key, ref keyOffset);
        }

        var dataOffset = 0;
        for (var index = 0; index < primaryKeyLength; index += 2) 
        {
            block[0] ^= StreamToWord(data, ref dataOffset);
            block[1] ^= StreamToWord(data, ref dataOffset);
            Encipher(block, 0);
            Arrays.ExpandedBlowfishKeyPrimary[index] = block[0];
            Arrays.ExpandedBlowfishKeyPrimary[index + 1] = block[1];
        }

        for (var index = 0; index < secondaryKeyLength; index += 2) 
        {
            block[0] ^= StreamToWord(data, ref dataOffset);
            block[1] ^= StreamToWord(data, ref dataOffset);
            Encipher(block, 0);
            Arrays.ExpandedBlowfishKeySecondary[index] = block[0];
            Arrays.ExpandedBlowfishKeySecondary[index + 1] = block[1];
        }
    }
}
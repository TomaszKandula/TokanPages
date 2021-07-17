namespace TokanPages.Backend.Cqrs.Services.CipheringService.Helpers
{
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
        /// <param name="AArrayToDecode">
        /// The byte array to encode
        /// </param>
        /// <param name="ALength">
        /// The number of bytes to encode
        /// </param>
        /// <returns>
        /// A Base64-encoded string
        /// </returns>
        public static string EncodeBase64(IReadOnlyList<byte> AArrayToDecode, int ALength) 
        {
            if (ALength <= 0 || ALength > AArrayToDecode.Count) 
                throw new ArgumentOutOfRangeException(nameof(ALength), ALength, null);
            
            var LStringBuilder = new StringBuilder(ALength * 2);

            for (var LOffset = 0; LOffset < ALength;) 
            {
                var LCountPrimary = AArrayToDecode[LOffset++] & 0xff;
                LStringBuilder.Append(Arrays.FTableForBase64Encoding[(LCountPrimary >> 2) & 0x3f]);
                LCountPrimary = (LCountPrimary & 0x03) << 4;
            
                if (LOffset >= ALength) 
                {
                    LStringBuilder.Append(Arrays.FTableForBase64Encoding[LCountPrimary & 0x3f]);
                    break;
                }
            
                var LCountSecondary = AArrayToDecode[LOffset++] & 0xff;
                LCountPrimary |= (LCountSecondary >> 4) & 0x0f;
                LStringBuilder.Append(Arrays.FTableForBase64Encoding[LCountPrimary & 0x3f]);
                LCountPrimary = (LCountSecondary & 0x0f) << 2;
            
                if (LOffset >= ALength) 
                {
                    LStringBuilder.Append(Arrays.FTableForBase64Encoding[LCountPrimary & 0x3f]);
                    break;
                }
            
                LCountSecondary = AArrayToDecode[LOffset++] & 0xff;
                LCountPrimary |= (LCountSecondary >> 6) & 0x03;
                LStringBuilder.Append(Arrays.FTableForBase64Encoding[LCountPrimary & 0x3f]);
                LStringBuilder.Append(Arrays.FTableForBase64Encoding[LCountSecondary & 0x3f]);
            }

            return LStringBuilder.ToString();
        }

        /// <summary>Decode a string encoded using BCrypt's Base64 scheme to a
        /// byte array. Note that this is _not_ compatible with the standard
        /// MIME-Base64 encoding.
        /// </summary>
        /// <param name="AStringToDecode">
        /// The string to decode
        /// </param>
        /// <param name="AMaximumLength">
        /// The maximum number of bytes to decode
        /// </param>
        /// <returns>
        /// An array containing the decoded bytes
        /// </returns>
        public static byte[] DecodeBase64(string AStringToDecode, int AMaximumLength) 
        {
            var LBytes = new List<byte>(Math.Min(AMaximumLength, AStringToDecode.Length));

            if (AMaximumLength <= 0) 
                throw new ArgumentOutOfRangeException(nameof(AMaximumLength), AMaximumLength, null);

            for (int LOffset = 0, LStringLength = AStringToDecode.Length, LLength = 0; LOffset < LStringLength - 1 && LLength < AMaximumLength; ) 
            {
                var LCountPrimary = Char64(AStringToDecode[LOffset++]);
                var LCountSecondary = Char64(AStringToDecode[LOffset++]);
                if (LCountPrimary == -1 || LCountSecondary == -1) 
                    break;

                LBytes.Add((byte)((LCountPrimary << 2) | ((LCountSecondary & 0x30) >> 4)));
                if (++LLength >= AMaximumLength || LOffset >= AStringToDecode.Length) 
                    break;

                var LCountTertiary = Char64(AStringToDecode[LOffset++]);
                if (LCountTertiary == -1) 
                    break;

                LBytes.Add((byte)(((LCountSecondary & 0x0f) << 4) | ((LCountTertiary & 0x3c) >> 2)));
                if (++LLength >= AMaximumLength || LOffset >= AStringToDecode.Length) 
                    break;

                var LCountQuaternary = Char64(AStringToDecode[LOffset++]);
                LBytes.Add((byte)(((LCountTertiary & 0x03) << 6) | LCountQuaternary));

                ++LLength;
            }

            return LBytes.ToArray();
        }

        /// <summary>
        /// Perform the central password hashing step in the bcrypt scheme.
        /// </summary>
        /// <param name="APassword">
        /// The password to hash.
        /// </param>
        /// <param name="ASalt">
        /// The binary salt to hash with the password.
        /// </param>
        /// <param name="ALogRounds">
        /// The binary logarithm of the number of rounds of hashing to apply.
        /// </param>
        /// <returns>
        /// An array containing the binary hashed password.
        /// </returns>
        public static byte[] CryptRaw(IReadOnlyList<byte> APassword, IReadOnlyList<byte> ASalt, int ALogRounds) 
        {
            var LData = new uint[Arrays.FBCryptCipherText.Length];
            Arrays.FBCryptCipherText.CopyTo(LData, 0);

            var LDataLength = LData.Length;
            if (ALogRounds is < 4 or > 31) 
                throw new ArgumentOutOfRangeException(nameof(ALogRounds), ALogRounds, null);

            var LRounds = 1 << ALogRounds;
            if (ASalt.Count != Constants.BCRYPT_SALT_LENGTH) 
                throw new ArgumentException(@"Invalid salt length.", nameof(ASalt));

            InitializeBlowfishKey();
            EnhancedKeySchedule(ASalt, APassword);

            for (var LIndex = 0; LIndex < LRounds; LIndex++) 
            {
                Key(APassword);
                Key(ASalt);
            }

            for (var LIndex = 0; LIndex < 64; LIndex++) 
            {
                for (var LInnerIndex = 0; LInnerIndex < LDataLength >> 1; LInnerIndex++) 
                {
                    Encipher(LData, LInnerIndex << 1);
                }
            }

            var LResult = new byte[LDataLength * 4];
            for (int LIndex1 = 0, LIndex2 = 0; LIndex1 < LDataLength; LIndex1++) 
            {
                LResult[LIndex2++] = (byte)((LData[LIndex1] >> 24) & 0xff);
                LResult[LIndex2++] = (byte)((LData[LIndex1] >> 16) & 0xff);
                LResult[LIndex2++] = (byte)((LData[LIndex1] >> 8) & 0xff);
                LResult[LIndex2++] = (byte)(LData[LIndex1] & 0xff);
            }

            return LResult;
        }
        
        /// <summary>Look up the 3 bits base64-encoded by the specified
        /// character, range-checking against the conversion table.
        /// </summary>
        /// <param name="ABase64EncodedValue">
        /// The Base64-encoded value
        /// </param>
        /// <returns>
        /// The decoded value of <c>x</c>
        /// </returns>
        private static int Char64(char ABase64EncodedValue) 
        {
            int LValue = ABase64EncodedValue;
            return LValue < 0 || LValue > Arrays.FTableForBase64Decoding.Length 
                ? -1 
                : Arrays.FTableForBase64Decoding[LValue];
        }
        
        /// <summary>
        /// Blowfish encipher a single 64-bit block encoded as two 32-bit halves.
        /// </summary>
        /// <param name="ABlock">
        /// An array containing the two 32-bit half blocks.
        /// </param>
        /// <param name="AOffset">
        /// The position in the array of the
        /// blocks.
        /// </param>
        private static void Encipher(IList<uint> ABlock, int AOffset) 
        {
            uint LIndex = 0;
            var LLength = ABlock[AOffset]; 
            var LRecord = ABlock[AOffset + 1];

            LLength ^= Arrays.ExpandedBlowfishKeyPrimary[0];
            while (LIndex <= Constants.BLOWFISH_NUM_ROUNDS - 2) 
            {
                // Feistel substitution on left word
                var LNumber = Arrays.ExpandedBlowfishKeySecondary[(LLength >> 24) & 0xff];
                LNumber += Arrays.ExpandedBlowfishKeySecondary[0x100 | ((LLength >> 16) & 0xff)];
                LNumber ^= Arrays.ExpandedBlowfishKeySecondary[0x200 | ((LLength >> 8) & 0xff)];
                LNumber += Arrays.ExpandedBlowfishKeySecondary[0x300 | (LLength & 0xff)];
                LRecord ^= LNumber ^ Arrays.ExpandedBlowfishKeyPrimary[++LIndex];

                // Feistel substitution on right word
                LNumber = Arrays.ExpandedBlowfishKeySecondary[(LRecord >> 24) & 0xff];
                LNumber += Arrays.ExpandedBlowfishKeySecondary[0x100 | ((LRecord >> 16) & 0xff)];
                LNumber ^= Arrays.ExpandedBlowfishKeySecondary[0x200 | ((LRecord >> 8) & 0xff)];
                LNumber += Arrays.ExpandedBlowfishKeySecondary[0x300 | (LRecord & 0xff)];
                LLength ^= LNumber ^ Arrays.ExpandedBlowfishKeyPrimary[++LIndex];
            }
        
            ABlock[AOffset] = LRecord ^ Arrays.ExpandedBlowfishKeyPrimary[Constants.BLOWFISH_NUM_ROUNDS + 1];
            ABlock[AOffset + 1] = LLength;
        }

        /// <summary>
        /// Cyclically extract a word of key material.
        /// </summary>
        /// <param name="AData">
        /// The string to extract the data from.
        /// </param>
        /// <param name="AOffset">
        /// The current offset into data.
        /// </param>
        /// <returns>
        /// The next work of material from data.
        /// </returns>
        private static uint StreamToWord(IReadOnlyList<byte> AData, ref int AOffset) 
        {
            uint LWord = 0;

            for (var LIndex = 0; LIndex < 4; LIndex++) 
            {
                LWord = (LWord << 8) | AData[AOffset];
                AOffset = (AOffset + 1) % AData.Count;
            }

            return LWord;
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
        /// <param name="AKey">
        /// An array containing the key.
        /// </param>
        private static void Key(IReadOnlyList<byte> AKey) 
        {
            uint[] LBlock = { 0, 0 };
            var LPrimaryKeyLength = Arrays.ExpandedBlowfishKeyPrimary.Length; 
            var LSecondaryKeyLength = Arrays.ExpandedBlowfishKeySecondary.Length;

            var LOffset = 0;
            for (var LIndex = 0; LIndex < LPrimaryKeyLength; LIndex++) 
            {
                Arrays.ExpandedBlowfishKeyPrimary[LIndex] = Arrays.ExpandedBlowfishKeyPrimary[LIndex] ^ StreamToWord(AKey, ref LOffset);
            }

            for (var LIndex = 0; LIndex < LPrimaryKeyLength; LIndex += 2) 
            {
                Encipher(LBlock, 0);
                Arrays.ExpandedBlowfishKeyPrimary[LIndex] = LBlock[0];
                Arrays.ExpandedBlowfishKeyPrimary[LIndex + 1] = LBlock[1];
            }

            for (var LIndex = 0; LIndex < LSecondaryKeyLength; LIndex += 2) 
            {
                Encipher(LBlock, 0);
                Arrays.ExpandedBlowfishKeySecondary[LIndex] = LBlock[0];
                Arrays.ExpandedBlowfishKeySecondary[LIndex + 1] = LBlock[1];
            }
        }

        /// <summary>
        /// Perform the "enhanced key schedule" step described by Provos
        /// and Mazieres in "A Future-Adaptable Password Scheme"
        /// (http://www.openbsd.org/papers/bcrypt-paper.ps).
        /// </summary>
        /// <param name="AData">
        /// Salt information.
        /// </param>
        /// <param name="AKey">
        /// Password information.
        /// </param>
        private static void EnhancedKeySchedule(IReadOnlyList<byte> AData, IReadOnlyList<byte> AKey) 
        {
            uint[] LBlock = { 0, 0 };
            var LPrimaryKeyLength = Arrays.ExpandedBlowfishKeyPrimary.Length;
            var LSecondaryKeyLength = Arrays.ExpandedBlowfishKeySecondary.Length;

            var LKeyOffset = 0;
            for (var LIndex = 0; LIndex < LPrimaryKeyLength; LIndex++) 
            {
                Arrays.ExpandedBlowfishKeyPrimary[LIndex] = Arrays.ExpandedBlowfishKeyPrimary[LIndex] ^ StreamToWord(AKey, ref LKeyOffset);
            }

            var LDataOffset = 0;
            for (var LIndex = 0; LIndex < LPrimaryKeyLength; LIndex += 2) 
            {
                LBlock[0] ^= StreamToWord(AData, ref LDataOffset);
                LBlock[1] ^= StreamToWord(AData, ref LDataOffset);
                Encipher(LBlock, 0);
                Arrays.ExpandedBlowfishKeyPrimary[LIndex] = LBlock[0];
                Arrays.ExpandedBlowfishKeyPrimary[LIndex + 1] = LBlock[1];
            }

            for (var LIndex = 0; LIndex < LSecondaryKeyLength; LIndex += 2) 
            {
                LBlock[0] ^= StreamToWord(AData, ref LDataOffset);
                LBlock[1] ^= StreamToWord(AData, ref LDataOffset);
                Encipher(LBlock, 0);
                Arrays.ExpandedBlowfishKeySecondary[LIndex] = LBlock[0];
                Arrays.ExpandedBlowfishKeySecondary[LIndex + 1] = LBlock[1];
            }
        }
    }
}
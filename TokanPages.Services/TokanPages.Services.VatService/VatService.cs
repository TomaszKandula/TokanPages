using System.Text.RegularExpressions;
using FluentValidation.Results;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.VatService.Models;

namespace TokanPages.Services.VatService;

public class VatService : IVatService
{
    /// <summary>
    /// Checks passed VAT number. It validates Polish VAT number (detailed check) and other European countries VAT numbers.
    /// </summary>
    /// <param name="request">Object with VAT number to check with additional parameters.</param>
    /// <returns>FluentValidation result (empty if no errors found).</returns>
    public ValidationResult ValidateVatNumber(VatValidationRequest request)
    {
        if (string.IsNullOrEmpty(request.VatNumber))
            return SetValidationResult(nameof(request.VatNumber), nameof(ErrorCodes.VAT_NUM_INCORRECT_LENGTH), ErrorCodes.VAT_NUM_INCORRECT_LENGTH);

        if (IsShortPolishVatNumber(request.VatNumber))
            return SetValidationResult(nameof(request.VatNumber), nameof(ErrorCodes.VAT_NUM_LENGTH_NINE), ErrorCodes.VAT_NUM_LENGTH_NINE);

        var polishVatNumberPattern = request.VatNumberPatterns
            .Where(pattern => pattern.CountryCode == "PL")
            .Select(pattern => pattern.Pattern)
            .FirstOrDefault() ?? string.Empty;

        var checkPrefix = Regex.Match(request.VatNumber, "^[A-Za-z]{2}");
        switch (checkPrefix.Success)
        {
            case true when checkPrefix.Value == "PL":
                return ValidatePolishVatNumber(request.VatNumber, polishVatNumberPattern, request.Options.CalculateCheckSum, request.Options.CheckZeros);
            case false:
            {
                var prefixedVatNumber = $"PL{request.VatNumber}";
                return ValidatePolishVatNumber(prefixedVatNumber, polishVatNumberPattern, request.Options.CalculateCheckSum, request.Options.CheckZeros);
            }
        }

        var europeanVatNumberPattern = request.VatNumberPatterns
            .Where(pattern => pattern.CountryCode != "PL")
            .Select(pattern => pattern.Pattern);

        foreach (var item in europeanVatNumberPattern)
        {
            var matchResult = Regex.Match(request.VatNumber, item, RegexOptions.IgnoreCase);
            if (matchResult.Success)
                return new ValidationResult(new List<ValidationFailure>());
        }

        return SetValidationResult(nameof(request.VatNumber), nameof(ErrorCodes.VAT_NUM_INCORRECT_FORMAT), ErrorCodes.VAT_NUM_INCORRECT_FORMAT);
    }

    /// <summary>
    /// Returns object with validation error.
    /// </summary>
    /// <param name="propertyName">Property name that is checked.</param>
    /// <param name="errorCode">Error code.</param>
    /// <param name="errorMessage">Error message if validation fails.</param>
    /// <returns>FluentValidation result.</returns>
    private static ValidationResult SetValidationResult(string propertyName, string errorCode, string errorMessage)
    {
        var result = new ValidationResult(new List<ValidationFailure>());
        var failure = new ValidationFailure(propertyName, errorMessage)
        {
            ErrorCode = errorCode
        };

        result.Errors.Add(failure);
        return result;
    }

    /// <summary>
    /// We allow VAT number with two character prefix (12), two character prefix and dash separators (15),
    /// for further checks.
    /// </summary>
    /// <param name="prefixedVatNumber"></param>
    /// <param name="calculateCheckSum"></param>
    /// <param name="checkZeros"></param>
    /// <param name="vatNumberPattern"></param>
    /// <returns></returns>
    private static ValidationResult ValidatePolishVatNumber(string prefixedVatNumber, string vatNumberPattern, bool calculateCheckSum = true, bool checkZeros = true)
    {
        int[] allowedLength = { 12, 15 };

        if (!allowedLength.Contains(prefixedVatNumber.Length))
            return SetValidationResult(nameof(prefixedVatNumber), nameof(ErrorCodes.VAT_NUM_INCORRECT_LENGTH), ErrorCodes.VAT_NUM_INCORRECT_LENGTH);

        var bareVatNumber = prefixedVatNumber
            .Replace("PL", string.Empty)
            .Replace("-", string.Empty);

        var pollutedVatNumber = prefixedVatNumber.Contains('-') 
            ? prefixedVatNumber.Replace("PL", string.Empty) 
            : prefixedVatNumber;

        if (bareVatNumber.All(letter => letter == bareVatNumber[0]))
            return SetValidationResult(nameof(bareVatNumber), nameof(ErrorCodes.VAT_NUM_ALL_DIGITS_ARE_SAME), ErrorCodes.VAT_NUM_ALL_DIGITS_ARE_SAME);

        if (bareVatNumber.Any(char.IsLetter))
            return SetValidationResult(nameof(bareVatNumber), nameof(ErrorCodes.VAT_NUM_INCORRECT_SIGN), ErrorCodes.VAT_NUM_INCORRECT_SIGN);

        if (!Regex.IsMatch(pollutedVatNumber, vatNumberPattern, RegexOptions.IgnoreCase))
            return SetValidationResult(nameof(pollutedVatNumber), nameof(ErrorCodes.VAT_NUM_INCORRECT_FORMAT), ErrorCodes.VAT_NUM_INCORRECT_FORMAT);

        if (checkZeros) 
        {
            var firstChar = bareVatNumber[0];
            var thirdChar = bareVatNumber[2];
            if (firstChar == '0' || thirdChar == '0')
                return SetValidationResult(nameof(bareVatNumber), nameof(ErrorCodes.VAT_NUM_ZERO_AT_FIRST_OR_THIRD_POSSITION), ErrorCodes.VAT_NUM_ZERO_AT_FIRST_OR_THIRD_POSSITION);
        }

        if (!calculateCheckSum) 
            return new ValidationResult(new List<ValidationFailure>());

        int[] weights = { 6, 5, 7, 2, 3, 4, 5, 6, 7 };
        var checkSum = CalculateCheckSum(bareVatNumber, weights);
        var controlNum = checkSum % 11;
        var lastChar = (int)char.GetNumericValue(bareVatNumber[9]);

        return controlNum != lastChar 
            ? SetValidationResult(nameof(bareVatNumber), nameof(ErrorCodes.VAT_NUM_INCORRECT_CHECK_SUM), ErrorCodes.VAT_NUM_INCORRECT_CHECK_SUM) 
            : new ValidationResult(new List<ValidationFailure>());
    }

    /// <summary>
    /// Calculates check sum for Polish VAT number.
    /// </summary>
    /// <param name="vatNumber">Fully qualified VAT number.</param>
    /// <param name="weights">List of weights.</param>
    /// <returns>Calculated check sum.</returns>
    private static int CalculateCheckSum(string vatNumber, IReadOnlyList<int> weights)
    {
        var checkSum = 0;
        for (var index = 0; index < vatNumber.Length - 1; index++)
        {
            checkSum += weights[index] * int.Parse(vatNumber[index].ToString());
        }
            
        return checkSum;
    }

    /// <summary>
    /// Check if given VAT number complies with Polish VAT number format.
    /// </summary>
    /// <param name="vatNumber">Fully qualified VAT number.</param>
    /// <returns>True or False</returns>
    private static bool IsShortPolishVatNumber(string vatNumber)
    {
        var value = vatNumber.ToUpper();
        return value.Length == 9 && value.All(char.IsDigit)
               || value.Length == 11 && value.StartsWith("PL") && value[2..].All(char.IsDigit);
    }
}
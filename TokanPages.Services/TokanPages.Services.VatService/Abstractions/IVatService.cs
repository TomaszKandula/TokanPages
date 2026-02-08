using FluentValidation.Results;
using TokanPages.Services.VatService.Models;

namespace TokanPages.Services.VatService.Abstractions;

public interface IVatService
{
    ValidationResult ValidateVatNumber(VatValidationRequest request);
}
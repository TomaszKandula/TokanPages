using TokanPages.Backend.Application.Revenue.Commands;
using TokanPages.Revenue.Dto.Subscriptions;

namespace TokanPages.Revenue.Controllers.Mappers;

/// <summary>
/// Subscriptions mapper.
/// </summary>
public static class SubscriptionsMapper
{
    /// <summary>
    /// Map request DTO to a given command.
    /// </summary>
    /// <param name="model">Input model</param>
    /// <returns>Command object.</returns>
    public static AddSubscriptionCommand MapToAddSubscriptionCommand(AddSubscriptionDto model) => new()
    {
        UserId = model.UserId,
        SelectedTerm = model.SelectedTerm,
        UserCurrency = model.UserCurrency,
        UserLanguage = model.UserLanguage
    };

    /// <summary>
    /// Map request DTO to a given command.
    /// </summary>
    /// <param name="model">Input model.</param>
    /// <returns>Command object.</returns>
    public static RemoveSubscriptionCommand MapToRemoveSubscriptionCommand(RemoveSubscriptionDto model) => new()
    {
        UserId = model.UserId
    };

    /// <summary>
    /// Map request DTO to a given command.
    /// </summary>
    /// <param name="model">Input model.</param>
    /// <returns>Command object.</returns>
    public static UpdateSubscriptionCommand MapToUpdateSubscriptionCommand(UpdateSubscriptionDto model) => new()
    {
        UserId = model.UserId,
        ExtOrderId = model.ExtOrderId,
        AutoRenewal = model.AutoRenewal,
        Term = model.Term,
        TotalAmount = model.TotalAmount,
        CurrencyIso = model.CurrencyIso
    };
}
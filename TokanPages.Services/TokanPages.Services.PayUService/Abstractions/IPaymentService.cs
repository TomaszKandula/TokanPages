using TokanPages.Services.PayUService.Models;
using TokanPages.Services.PayUService.Models.Sections;

namespace TokanPages.Services.PayUService.Abstractions;

public interface IPaymentService
{
    /// <summary>
    /// Returns bearer token for given client ID and client secret values.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Bearer access token.</returns>
    Task<AuthorizationOutput> GetAuthorization(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns allowed payment methods for logged user.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of payment methods.</returns>
    Task<PaymentMethodsOutput> GetPaymentMethods(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns order details for given order ID. It includes payments status.
    /// </summary>
    /// <param name="orderId">Order ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Order details.</returns>
    Task<OrderDetailsOutput> GetOrderDetails(string orderId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns transaction details for given order ID. It includes detailed information on card.
    /// </summary>
    /// <param name="orderId">Order ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Transaction details.</returns>
    Task<OrderTransactionsOutput> GetOrderTransactions(string orderId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns card token for further use.
    /// </summary>
    /// <param name="input">Input data incl. card info: number, maturity, cvv.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Masked card and card token.</returns>
    Task<GenerateCardTokenOutput> GenerateCardToken(GenerateCardTokenInput input, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates order for payment with selected payment method.
    /// This method should be used for transparent integration.
    /// </summary>
    /// <remarks>
    /// Response object do not return payment status. Payment status can be obtained via notification system.
    /// </remarks>
    /// <param name="input">Order details, it includes (among others) product details, payment method, etc.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Request response.</returns>
    Task<PostOrderOutput> PostOrder(PostOrderInput input, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates order for payment with selected payment method.
    /// This method should be used for non-transparent integration.
    /// </summary>
    /// <remarks>
    /// Response object do not return payment status. Payment status can be obtained via notification system.
    /// </remarks>
    /// <param name="input">Order details, it includes (among others) product details, payment method, etc.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Raw content from the server.</returns>
    Task<string> PostOrderDefault(PostOrderInput input, CancellationToken cancellationToken = default);
}
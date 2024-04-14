using TokanPages.Backend.Application.Revenue.Commands;
using TokanPages.Backend.Application.Revenue.Queries;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Attributes;
using TokanPages.Revenue.Controllers.Mappers;
using TokanPages.Revenue.Dto.Payments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TokanPages.Revenue.Controllers.Api;

/// <summary>
/// API endpoints definitions for payment capability.
/// </summary>
[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class PaymentsController : ApiBaseController
{
    /// <inheritdoc />
    public PaymentsController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Returns bearer access token from payment provider (PayU). It can be used to query external API.
    /// </summary>
    /// <returns>Access token details.</returns>
    [HttpGet]
    [AuthorizeUser(Roles.EverydayUser)]
    [ProducesResponseType(typeof(GetAuthorizationQueryResult), StatusCodes.Status200OK)]
    public async Task<GetAuthorizationQueryResult> GetAuthorization()
        => await Mediator.Send(new GetAuthorizationQuery());

    /// <summary>
    /// Returns list of available payment methods from payment provider (PayU).
    /// </summary>
    /// <returns>List of available payment methods.</returns>
    [HttpGet]
    [AuthorizeUser(Roles.EverydayUser)]
    [ProducesResponseType(typeof(GetPaymentMethodsQueryResults), StatusCodes.Status200OK)]
    public async Task<GetPaymentMethodsQueryResults> GetPaymentMethods()
        => await Mediator.Send(new GetPaymentMethodsQuery());

    /// <summary>
    /// Returns details of given order by its ID.
    /// </summary>
    /// <param name="orderId">Order ID created when requesting new payment.</param>
    /// <returns>Order details.</returns>
    [HttpGet]
    [AuthorizeUser(Roles.EverydayUser)]
    [ProducesResponseType(typeof(GetOrderDetailsQueryResult), StatusCodes.Status200OK)]
    public async Task<GetOrderDetailsQueryResult> GetOrderDetails([FromQuery] string orderId)
        => await Mediator.Send(new GetOrderDetailsQuery { OrderId = orderId });

    /// <summary>
    /// Returns transaction details associated with the given order ID.
    /// </summary>
    /// <param name="orderId">Order ID created when requesting new payment.</param>
    /// <returns>Transaction details.</returns>
    [HttpGet]
    [AuthorizeUser(Roles.EverydayUser)]
    [ProducesResponseType(typeof(GetOrderTransactionsQueryResult), StatusCodes.Status200OK)]
    public async Task<GetOrderTransactionsQueryResult> GetOrderTransactions([FromQuery] string orderId)
        => await Mediator.Send(new GetOrderTransactionsQuery { OrderId = orderId });

    /// <summary>
    /// Allows to order payment for given products/services. 
    /// </summary>
    /// <remarks>
    /// Use it for transparent integration.
    /// </remarks>
    /// <param name="payload">Payment details.</param>
    /// <returns>Payment response details.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CreatePaymentCommandResult),StatusCodes.Status200OK)]
    public async Task<CreatePaymentCommandResult> CreatePayment([FromBody] CreatePaymentDto payload) 
        => await Mediator.Send(PaymentsMapper.MapToCreatePaymentCommand(payload));

    /// <summary>
    /// Allows to order payment for given products/services. 
    /// </summary>
    /// <remarks>
    /// Use it for non-transparent integration.
    /// </remarks>
    /// <param name="payload">Payment details.</param>
    /// <returns>Raw server response.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(string),StatusCodes.Status200OK)]
    public async Task<string> CreatePaymentDefault([FromBody] CreatePaymentDefaultDto payload) 
        => await Mediator.Send(PaymentsMapper.MapToCreatePaymentDefaultCommand(payload));

    /// <summary>
    /// Allows to post notification to the system on an ongoing payment status.
    /// To be used by payment provider. 
    /// </summary>
    /// <remarks>
    /// This endpoint is created specifically for PayU payment provider.
    /// </remarks>
    /// <param name="payload">Notification details.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Unit),StatusCodes.Status200OK)]
    public async Task<Unit> PostNotification([FromBody] PostNotificationDto payload)
        => await Mediator.Send(PaymentsMapper.MapToPostNotificationCommand(payload));
}
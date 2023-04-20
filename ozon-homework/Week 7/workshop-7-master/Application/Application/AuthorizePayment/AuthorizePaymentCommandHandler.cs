using MediatR;
using NanoPaymentSystem.Application.PaymentProvider;
using NanoPaymentSystem.Application.Repository;
using NanoPaymentSystem.Domain;

namespace NanoPaymentSystem.Application.Application.AuthorizePayment;

internal sealed class AuthorizePaymentCommandHandler : IRequestHandler<AuthorizePaymentCommand, AuthorizePaymentResult>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPaymentProvider _paymentProvider;

    public AuthorizePaymentCommandHandler(IPaymentRepository paymentRepository, IPaymentProvider paymentProvider)
    {
        _paymentRepository = paymentRepository;
        _paymentProvider = paymentProvider;
    }

    /// <inheritdoc />
    public async Task<AuthorizePaymentResult> Handle(AuthorizePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.GetPaymentById(request.Id, cancellationToken);

        if (payment.Status != PaymentStatus.New)
        {
            throw new PaymentIncorrectStatusException();
        }

        payment.BankCard = new CardInfo(
            request.CardNumber.Substring(0, 6),
            request.CardNumber.Substring(request.CardNumber.Length - 4, 4),
            request.CardExpirationMonth,
            request.CardExpirationYear);

        payment.Status = PaymentStatus.Processing;

        await _paymentRepository.UpdatePayment(payment, cancellationToken);

        var authResult = await _paymentProvider.Authorize(new AuthorizePaymentRequest(
                payment.Id.ToString(),
                payment.Amount,
                payment.CurrencyCode,
                new CardData(
                    cardNumber: request.CardNumber,
                    expirationYear: request.CardExpirationYear, 
                    expirationMonth: request.CardExpirationMonth)),
            cancellationToken);

        payment.Message = authResult.Message;

        if (authResult.IsSuccess)
        {
            payment.Status = PaymentStatus.Authorized;
            payment.ProviderPaymentId = authResult.ProviderPaymentId;
        }
        else
        {
            payment.Status = PaymentStatus.Rejected;
        }

        await _paymentRepository.UpdatePayment(payment, cancellationToken);

        return new AuthorizePaymentResult(authResult.IsSuccess, authResult.Message);
    }
}

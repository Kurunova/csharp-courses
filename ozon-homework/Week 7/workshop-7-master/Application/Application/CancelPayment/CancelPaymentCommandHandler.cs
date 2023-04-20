using MediatR;
using NanoPaymentSystem.Application.PaymentProvider;
using NanoPaymentSystem.Application.Repository;
using NanoPaymentSystem.Domain;

namespace NanoPaymentSystem.Application.Application.CancelPayment;

internal sealed class CancelPaymentCommandHandler : IRequestHandler<CancelPaymentCommand, CancelPaymentResult>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPaymentProvider _paymentProvider;

    public CancelPaymentCommandHandler(IPaymentRepository paymentRepository, IPaymentProvider paymentProvider)
    {
        _paymentRepository = paymentRepository;
        _paymentProvider = paymentProvider;
    }

    /// <inheritdoc />
    public async Task<CancelPaymentResult> Handle(CancelPaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.GetPaymentById(request.Id, cancellationToken);

        if (payment.Status != PaymentStatus.Authorized)
        {
            throw new PaymentIncorrectStatusException();
        }

        var cancelResult = await _paymentProvider.Cancel(new CancelPaymentRequest(payment.ProviderPaymentId!), cancellationToken);

        payment.Message = cancelResult.Message;

        if (cancelResult.IsSuccess)
        {
            payment.Status = PaymentStatus.Cancelled;
        }

        await _paymentRepository.UpdatePayment(payment, cancellationToken);

        return new CancelPaymentResult(cancelResult.IsSuccess, cancelResult.Message);
    }
}

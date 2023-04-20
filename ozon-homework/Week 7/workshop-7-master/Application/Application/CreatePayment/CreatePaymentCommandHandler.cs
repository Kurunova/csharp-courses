using MediatR;
using NanoPaymentSystem.Application.Repository;
using NanoPaymentSystem.Domain;

namespace NanoPaymentSystem.Application.Application.CreatePayment;

internal sealed class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, CreatePaymentResult>
{
    private readonly IPaymentRepository _paymentRepository;

    public CreatePaymentCommandHandler(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    /// <inheritdoc />
    public async Task<CreatePaymentResult> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        var payment = new Payment
        {
            Amount = request.Amount,
            ClientId = request.ClientId,
            OrderId = request.OrderId,
            CurrencyCode = request.CurrencyCode,
            Status = PaymentStatus.New,
            Id = id,
        };

        await _paymentRepository.SavePayment(payment, cancellationToken);

        return new CreatePaymentResult(id.ToString());
    }
}

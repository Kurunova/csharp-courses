using MediatR;
using NanoPaymentSystem.Application.Repository;
using NanoPaymentSystem.Domain;

namespace NanoPaymentSystem.Application.Application.QueryPayment;

internal sealed class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, Payment>
{
    private readonly IPaymentRepository _paymentRepository;

    public GetPaymentByIdQueryHandler(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    /// <inheritdoc />
    public Task<Payment> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
    {
        return _paymentRepository.GetPaymentById(request.PaymentId, cancellationToken);
    }
}
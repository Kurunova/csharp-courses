namespace NanoPaymentSystem.Domain;

public sealed class Payment
{
    public Guid Id { get; set; }

    public string ClientId { get; set; } = string.Empty;

    public string OrderId { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string CurrencyCode { get; set; } = string.Empty;

    public PaymentStatus Status { get; set; }

    public string? Message { get; set; }

    public CardInfo? BankCard { get; set; }

    public string? ProviderPaymentId { get; set; }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("walker_payment")]
public class WalkerPayment
{
    [Column("walker_payment_id")]
    [Key]
    public int WalkerPaymentId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("booking_id")]
    public int BookingId { get; set; }

    [Column("amount")]
    public decimal Amount { get; set; }

    [Column("status")]
    public string? Status { get; set; }

    [Column("payment_date")]
    public DateTime PaymentDate { get; set; }

}
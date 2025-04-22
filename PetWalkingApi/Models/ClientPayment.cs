using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("client_payment")]
public class ClientPayment
{
    [Column("client_payment_id")]
    [Key]
    public int ClientPaymentId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("booking_id")]
    public int BookingId { get; set; }

    [Column("amount")]
    public decimal Amount { get; set; }

    [Column("payment_method")]
    public string? PaymentMethod { get; set; }

    [Column("status")]
    public string? Status { get; set; }

    [Column("payment_date")]
    public DateTime PaymentDate { get; set; }

}
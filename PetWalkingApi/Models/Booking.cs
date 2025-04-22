using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("booking")]
public class Booking
{
    [Column("booking_id")]
    [Key]
    public int BookingId { get; set; }

    [Column("calendar_id")]
    public int CalendarId { get; set; }

    [Column("status")]
    public string? Status { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }

    [Column("pet_id")]
    public int PetId { get; set; }

}
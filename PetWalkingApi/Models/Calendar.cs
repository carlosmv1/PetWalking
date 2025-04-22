using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("calendar")]
public class Calendar
{
    [Column("calendar_id")]
    [Key]
    public int CalendarId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("status")]
    public string? Status { get; set; }

}
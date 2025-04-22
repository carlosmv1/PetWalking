using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("availability_slot")]
public class AvailabilitySlot
{
    [Column("availability_slot_id")]
    [Key]
    public int AvailabilitySlotId { get; set; }

    [Column("calendar_id")]
    public int CalendarId { get; set; }

    [Column("start_time")]
    public DateTime StartTime { get; set; }

    [Column("end_time")]
    public DateTime EndTime { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }

}
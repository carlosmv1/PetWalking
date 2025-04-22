using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("zone")]
public class Zone
{
    [Column("zone_id")]
    [Key]
    public int ZoneId { get; set; }

    [Column("name")]
    public string? Name { get; set; }

}
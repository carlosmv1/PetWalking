using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("user")]
public class User
{
    [Column("user_id")]
    [Key]
    public int UserId { get; set; }

    [Column("user_type_id")]
    public int UserTypeId { get; set; }

    [Column("user_name")]
    public string? UserName { get; set; }

    [Column("password")]
    public string? Password { get; set; }

    [Column("first_name")]
    public string? FirstName { get; set; }

    [Column("last_name")]
    public string? LastName { get; set; }

    [Column("address")]
    public string? Address { get; set; }

    [Column("city")]
    public string? City { get; set; }

    [Column("phone")]
    public string? Phone { get; set; }

    [Column("alt_phone")]
    public string? AltPhone { get; set; }

    [Column("email")]
    public string? Email { get; set; }

    [Column("status")]
    public string? Status { get; set; }

    [Column("zone_id")]
    public int ZoneId { get; set; }

    [Column("image")]
    public string? Image { get; set; }
}
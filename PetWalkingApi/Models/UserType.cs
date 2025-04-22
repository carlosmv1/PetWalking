using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("user_type")]
public class UserType
{
    [Column("user_type_id")]
    [Key]
    public int UserTypeId { get; set; }

    [Column("type")]
    public string? Type { get; set; }

    [Column("description")]
    public string? Description { get; set; }

}
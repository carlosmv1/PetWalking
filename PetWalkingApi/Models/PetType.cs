using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("pet_type")]
public class PetType
{
    [Column("pet_type_id")]
    [Key]
    public int PetTypeId { get; set; }

    [Column("type_name")]
    public string? TypeName { get; set; }

}
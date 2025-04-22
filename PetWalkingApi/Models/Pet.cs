using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("pet")]
public class Pet
{
    [Column("pet_id")]
    [Key]
    public int PetId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("name")]
    public string? Name { get; set; }

    [Column("gender")]
    public string? Gender { get; set; }

    [Column("observations")]
    public string? Observations { get; set; }

    [Column("pet_type_id")]
    public int PetTypeId { get; set; }

    [Column("pet_breed_id")]
    public int PetBreedId { get; set; }

    [Column("status")]
    public string? Status { get; set; }

    [Column("image")]
    public string? Image { get; set; }
}
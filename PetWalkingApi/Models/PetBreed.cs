using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("pet_breed")]
public class PetBreed
{
    [Column("pet_breed_id")]
    [Key]
    public int PetBreedId { get; set; }

    [Column("breed_name")]
    public string? BreedName { get; set; }

    [Column("pet_type_id")]
    public int PetTypeId { get; set; }

}
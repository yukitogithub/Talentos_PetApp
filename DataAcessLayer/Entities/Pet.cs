using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Pet
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [EnumDataType(typeof(PetType))]
        public PetType Type { get; set; } //Dog Cat Perro Gato
        public DateTime Birthday { get; set; }
        //[EnumDataType(typeof(Breed))]
        public string Breed { get; set; }
        public string ImageUrl { get; set; }
        [EnumDataType(typeof(Sex))]
        public Sex Sex { get; set; } //F or M Female Male Macho Hembra

        //[ForeignKey("User")]
        public int? UserId { get; set; }
        public User? User { get; set; }
    }

    public enum PetType
    {
        Dog,
        Cat,
        Gato,
        Perro
    }

    public enum Breed
    {
        Akita
        ,AmericanBobtail
        ,AmericanWirehair
        ,AustralianMist
        ,Beagle
        ,Bengal
        ,Birman
        ,Bloodhound
        ,Bombay
        ,BorderCollie
        ,Boxer
        ,BritishShorthair
        ,Brittany
        ,Bulldog
        ,Burmese
        ,CavalierKingCharlesSpaniel
        ,Chartreux
        ,Collie
        ,Dalmatian
        ,DevonRex
        ,Doberman
        ,EgyptianMau
        ,Golden
        ,HavanaBrown
        ,Havanese
        ,Himalayan
        ,Korat
        ,MaineCoon
        ,Mastiff
        ,Munchkin
        ,Newfoundland
        ,Normal
        ,Oriental
        ,Papillon
        ,Pomeranian
        ,Poodle
        ,Ragamuffin
        ,Ragdoll
        ,RhodesianRidgeback
        ,Rottweiler
        ,RussianBlue
        ,ScottishTerrier
        ,SelkirkRex
        ,Serengeti
        ,ShihTzu
        ,Siamese
        ,Siberian
        ,SiberianHusky
        ,Singapura
        ,TurkishAngora
        ,TurkishVan
    }

    public enum Sex
    {
        Male,
        Female
    }
}

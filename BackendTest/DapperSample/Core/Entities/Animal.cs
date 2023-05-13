using System;
using System.Text.Json.Serialization;

namespace STGenetics.Core.Entities
{
    public class Animal
    {
        public int AnimalId { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public DateTime BirthDate { get; set; }
        public string Sex { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; }

        public Animal(int animalId, string name, string breed, DateTime birthDate, string sex, double price, bool status)
        {
            AnimalId = animalId;
            Name = name;
            Breed = breed;
            BirthDate = birthDate;
            Sex = sex;
            Price = price;
            Status = status;
        }

    }

    public class AnimalSexByQuantity
    {
        public string Sex { get; set; }
        public double Quantity { get; set; }
    }
}

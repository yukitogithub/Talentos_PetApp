using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class SeedRandomData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string[] petNames = new string[] { "Rex", "Bella", "Max", "Lucy", "Charlie", "Molly", "Buddy", "Daisy", "Rocky", "Maggie", "Jack", "Sophie", "Toby", "Bailey", "Coco", "Lola", "Teddy", "Stella", "Chloe", "Penny", "Zoe", "Lily", "Oscar", "Rosie", "Leo", "Ruby", "Zeus", "Sadie", "Bentley", "Roxy", "Milo", "Abby", "Jasper", "Ginger", "Harley", "Luna", "Jake", "Zoey", "Duke", "Belle", "Oliver", "Princess", "Koda", "Missy", "Bruno", "Holly", "Cooper", "Katie", "Riley", "Maddie" };
            string[] petTypes = new string[] { "Dog", "Cat" };
            string[] dogBreeds = new string[] { "Bulldog", "Beagle", "Rottweiler", "Boxer", "Poodle", "Siberian Husky", "Dalmatian", "Doberman", "Great Dane", "Shih Tzu", "Boston Terrier", "Pomeranian", "Havanese", "Cavalier King Charles Spaniel", "Shetland Sheepdog", "Brittany", "English Springer Spaniel", "Mastiff", "Bichon Frise", "Collie", "Weimaraner", "Newfoundland", "Border Collie", "Basset Hound", "Rhodesian Ridgeback", "West Highland White Terrier", "Chesapeake Bay Retriever", "Bullmastiff", "Bichon Frise", "Papillon", "Scottish Terrier", "Saint Bernard", "Bloodhound", "Bull Terrier", "Pekingese", "Akita" };
            string[] catBreeds = new string[] { "Persian", "Maine Coon", "Siamese", "Bengal", "Ragdoll", "Sphynx", "Abyssinian", "British Shorthair", "Scottish Fold", "Burmese", "Russian Blue", "Norwegian Forest", "Devon Rex", "Oriental", "Cornish Rex", "Turkish Van", "Manx", "Balinese", "Himalayan", "Exotic Shorthair", "Siberian", "Tonkinese", "Bombay", "Egyptian Mau", "American Bobtail", "Havana Brown", "Birman", "Chartreux", "Somali", "Japanese Bobtail", "Turkish Angora", "Korat", "American Curl", "Selkirk Rex", "American Wirehair", "LaPerm", "Ocicat", "Singapura", "Colorpoint Shorthair", "Cymric", "American Shorthair", "Ragamuffin", "Munchkin", "Snowshoe", "Pixiebob", "Australian Mist", "Nebelung", "European Burmese", "Javanese", "Serengeti" };
            string[] petSexs = new string[] { "Female", "Male" };

            for (int i = 0; i < 75; i++)
            {
                Random random = new Random();
                var petName = petNames[random.Next(petNames.Length)];
                var petType = petTypes[random.Next(petTypes.Length)];
                var petBreed = petType == "Dog" ? dogBreeds[random.Next(dogBreeds.Length)] : catBreeds[random.Next(catBreeds.Length)];
                var petSex = petSexs[random.Next(petSexs.Length)];
                var petBirthday = DateTime.Now.AddYears(-2);
                var petImageUrl = Guid.NewGuid().ToString() + ".jpg";
                int userId = i + 1;

                migrationBuilder.InsertData(
                   table: "Pets",
                   columns: new[] { "Id", "Name", "Description", "Type", "Birthday", "Breed", "ImageUrl", "Sex", "UserId" },
                   values: new object[] { i + 7, petName, $"Pet {i}", petType, petBirthday, petBreed, petImageUrl, petSex, userId }
                );
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

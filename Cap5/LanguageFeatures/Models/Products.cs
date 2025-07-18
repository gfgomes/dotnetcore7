namespace LanguageFeatures.Models
{
    public class Product {

        // public required string Name { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal? Price { get; set; }

        public bool NameBeginsWithS => Name.Length > 0 && Name[0] == 'S';

        public static Product?[] GetProducts()
        {

            Product kayak = new Product
            {
                Name = "Kayak",
                Price = 275M
            };

            Product lifejacket = new Product
            {
                Name = "Lifejacket",
                Price = 48.95M
            };

            return new Product?[] { kayak, lifejacket, null };
        }

    }
}
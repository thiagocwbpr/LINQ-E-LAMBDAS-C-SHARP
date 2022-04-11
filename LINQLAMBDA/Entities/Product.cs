using System.Globalization;

namespace LINQLAMBDA.Entities {
    internal class Product {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; } 
        public Category Category { get; set; }
        public override string ToString() {
            return "ID: "
                + ID.ToString()
                + " - Name: " + Name
                + " - Price: " + Price.ToString("F2", CultureInfo.InvariantCulture)
                + " - Category : " + Category.Name
                + " - Category : " + Category.Tier;
        }
    }
}

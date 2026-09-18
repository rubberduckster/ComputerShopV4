using System.Xml.Linq;

namespace ElectronicShopXML
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Load the XML file into memory
            XDocument document = XDocument.Load("products.xml");

            IEnumerable<XElement> products = document.Descendants("Product");

            // Filtering

            // Find products in the Computer category
            // Convert the XML Category value to a string for comparison
            var computers = products.Where(product => (string?)product.Element("Category") == "Computer");

            Console.WriteLine("=== Computers ===");

            foreach (XElement product in computers)
            {
                Console.WriteLine($"{product.Element("Name")?.Value} - {product.Element("Price")?.Value} kr.");
            }

            // Find products above 5000 kr.
            // Convert the XML Price value to decimal for numeric comparison
            var expensiveProducts = products.Where(product => (decimal?)product.Element("Price") > 5000m);

            Console.WriteLine("\n=== Products above 5000 kr. ===");

            foreach (XElement product in expensiveProducts)
            {
                Console.WriteLine($"{product.Element("Name")?.Value} - {product.Element("Price")?.Value} kr.");
            }

            // Find products between 1000 and 5000 kr.
            var midRangeProducts = products.Where(product => (decimal?)product.Element("Price") >= 1000m && (decimal?)product.Element("Price") <= 5000m);

            Console.WriteLine("\n=== Products between 1000 and 5000 kr. ===");

            foreach (XElement product in midRangeProducts)
            {
                Console.WriteLine($"{product.Element("Name")?.Value} - {product.Element("Price")?.Value} kr.");
            }

            // Find accessories above 1000 kr.
            var expensiveAccessories = products.Where(product => (string?)product.Element("Category") == "Tilbehør" && (decimal?)product.Element("Price") > 1000m);

            Console.WriteLine("\n=== Accessories above 1000 kr. ===");

            foreach (XElement product in expensiveAccessories)
            {
                Console.WriteLine($"{product.Element("Name")?.Value} - {product.Element("Price")?.Value} kr.");
            }

            // Find products where the Name contains "Gaming"
            var gamingProducts = products.Where(product => ((string?)product.Element("Name"))?.Contains("Gaming") == true);

            Console.WriteLine("\n=== Gaming Products ===");

            foreach (XElement product in gamingProducts)
            {
                Console.WriteLine($"{product.Element("Name")?.Value} - {product.Element("Price")?.Value} kr.");
            }

            // CRUD

            // Create
            // Build a new Product element
            XElement newProduct = new XElement("Product",
                new XElement("Name", "Gaming Keyboard"),
                new XElement("Category", "Tilbehør"),
                new XElement("Price", 1200));

            document.Root?.Add(newProduct);
            document.Save("products.xml");

            Console.WriteLine("\n=== CREATED PRODUCT ===");
            Console.WriteLine($"{newProduct.Element("Name")?.Value} - {newProduct.Element("Category")?.Value} - {newProduct.Element("Price")?.Value} kr.");

            // Update
            // Find the Gaming Keyboard product
            XElement? productToUpdate = document.Descendants("Product").FirstOrDefault(product => (string?)product.Element("Name") == "Gaming Keyboard");

            if (productToUpdate != null)
            {
                // Change the Price element's value
                productToUpdate.Element("Price")!.Value = "1350";
                // Save the updated XML to the file
                document.Save("products.xml");

                Console.WriteLine("\n=== UPDATED PRODUCT ===");
                Console.WriteLine($"{productToUpdate.Element("Name")?.Value} - {productToUpdate.Element("Category")?.Value} - {productToUpdate.Element("Price")?.Value} kr.");
            }

            // Delete
            // Find the Gaming Keyboard product
            XElement? productToDelete = document.Descendants("Product").FirstOrDefault(product => (string?)product.Element("Name") == "Gaming Keyboard");

            if (productToDelete != null)
            {
                // Remove the Product element from the XML document
                productToDelete.Remove();
                // Save the deletion to the XML file
                document.Save("products.xml");

                Console.WriteLine("\n=== DELETED PRODUCT ===");
                Console.WriteLine("Gaming Keyboard was deleted.");
                Console.WriteLine("Gaming Keyboard was deleted.");

                Console.WriteLine("\n=== PRODUCTS AFTER DELETE ===");

                foreach (XElement product in document.Descendants("Product"))
                {
                    Console.WriteLine($"{product.Element("Name")?.Value} - {product.Element("Category")?.Value} - {product.Element("Price")?.Value} kr.");
                }
            }
        }
    }
}

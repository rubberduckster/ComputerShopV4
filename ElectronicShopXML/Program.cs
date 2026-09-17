using System.Xml.Linq;

namespace ElectronicShopXML
{
    internal class Program
    {
        static void Main(string[] args)
        {
            XDocument document = XDocument.Load("products.xml");

            IEnumerable<XElement> products = document.Descendants("Product");

            var computers = products.Where(product =>(string?)product.Element("Category") == "Computer");
        }
    }
}

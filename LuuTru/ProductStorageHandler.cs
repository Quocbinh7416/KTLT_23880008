using KTLT_23880008.Entity;
using KTLT_23880008.Utils;
using Newtonsoft.Json;
using System.Text.Json;

namespace KTLT_23880008.LuuTru {
    public class ProductStorageHandler {
        public static string GetProductDetailsJson(Object product) {
            return JsonConvert.SerializeObject(product);
        }

        public static ProductList RetrieveProducts() {
            Product[]? products = null;
            string dir = Environment.CurrentDirectory;
            string combined = Path.Combine(dir, "Resources", ResoucesTypes.PRODUCT.FileName);
            using (FileStream fs = new FileStream(combined, FileMode.Open, FileAccess.ReadWrite))
            using (StreamReader sr = new(fs)) {
                while (!sr.EndOfStream) {
                    string s = sr.ReadToEnd();
                    if (s != null && s != string.Empty) {
                        products = JsonConvert.DeserializeObject<Product[]>(s);
                    }
                }

            }
            if (products != null) {
                return new ProductList(products.Length, products);
            } else { 
                return new ProductList(); 
            }

        }

        public static bool StoreData(String data) {
            bool isValid = false;
            string dir = Environment.CurrentDirectory;
            string combined = Path.Combine(dir, "Resources", ResoucesTypes.PRODUCT.FileName);
            using (FileStream fs = new FileStream(combined, FileMode.Create, FileAccess.Write)) {
                StreamWriter sw = new(fs);
                sw.WriteLine(data);
                sw.Flush();
                isValid = true;
            }
            return isValid;
        }

        public static bool StoreProductList(ProductList productList) {
            string data = GetProductDetailsJson(productList.Products);
            return StoreData(data);
        }
    }
}

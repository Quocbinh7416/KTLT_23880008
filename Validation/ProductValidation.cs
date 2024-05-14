using KTLT_23880008.Entity;
using KTLT_23880008.LuuTru;

namespace KTLT_23880008.Validation {
    public class ProductValidation {
        public static bool IsDuplicateId(int id) {
            bool isValid = false;
            Product[] products = ProductStorageHandler.RetrieveProducts().Products;
            foreach (Product product in products) {
                if (product.Id == id) {
                    isValid = true;
                    break;
                }
            }
            return isValid;
        }
        public static bool IsExistProductName(string name) {
            bool isValid = false;
            Product[] products = ProductStorageHandler.RetrieveProducts().Products;
            foreach (Product product in products) {
                if (product.Name.Equals(name)) {
                    isValid = true;
                    break;
                }
            }
            return isValid;
        }

        public static bool FindById(int id, Product[] products, out Product product) {
            product = default;
            bool isValid = false;
            foreach (var item in products) {
                if (item.Id == id) {
                    isValid = true;
                    product = item;
                    break;
                }
            }
            return isValid;
        }

        public static bool FindByName(string name, Product[] products, out Product product) {
            product = default;
            bool isValid = false;
            foreach (var item in products) {
                if (item.Name.Equals(name)) {
                    isValid = true;
                    product = item;
                    break;
                }
            }
            return isValid;
        }


        public static bool IsValidProductPrice(double price) {
            return price > 0;
        }
    }
}

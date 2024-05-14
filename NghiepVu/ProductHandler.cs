using KTLT_23880008.Entity;
using KTLT_23880008.LuuTru;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace KTLT_23880008.NghiepVu {
    public class ProductHandler {

        static ProductList currentProducts = ProductStorageHandler.RetrieveProducts();
        private static Product EditedProduct;

        public static Product GetEditedProduct() {
            return EditedProduct;
        }

        public static void SetEditedProduct(Product value) {
            EditedProduct = value;
        }


        public static Product[] GetProducts() {
            return currentProducts.Products;
        }

        private static Product[] GetValidList(Product[] products) {
            int ValidLength = 0;
            for (int i = 0; i < products.Length; i++) {
                if (!products[i].Equals(default(Product))) {
                    ValidLength++;
                }
            }
            Product[] result = new Product[ValidLength];
            int j = 0;
            for (int i = 0; i < products.Length; i++) {
                if (!products[i].Equals(default(Product))) {
                    result[j++] = products[i];
                }
            }
            return result;
        }
        public static void ReloadProducts() {
            currentProducts = ProductStorageHandler.RetrieveProducts();
        }

        public static bool AddProduct(Product product) {
            // Xử lý thêm sản phẩm
            bool isValid = currentProducts.AddProduct(product);
            if (!isValid) {
                return isValid;
            }
            // Lưu trữ sản phẩm
            isValid = ProductStorageHandler.StoreProductList(currentProducts);

            // Chỉnh sửa loại hàng
            if (isValid) {
                CategoryHandler.AddProductIdForCategory(product.Id, product.Category);
            }
            return isValid;
        }
        public static bool EditProduct(int productId, Product product, Product oldProduct) {
            // Chỉnh sửa loại hàng
            bool isValid = CategoryHandler.EditProductIdForCategory(productId, oldProduct.Category, product.Category);
            isValid &= OrderHandler.EditProductForOrder(oldProduct, product);

            if (isValid) {
                // Xử lý chỉnh sản phẩm
                isValid = currentProducts.EditProduct(productId, product);
            }

            if (!isValid) {
                return isValid;
            }

            isValid = ProductStorageHandler.StoreProductList(currentProducts);
            return isValid;
        }
        public static bool DeleteProduct(int productId) {
            // Xử lý chỉnh sản phẩm
            bool isValid = currentProducts.DeleteProductByProductId(productId, out string deletedCategory);
            if (!isValid) {
                return isValid;
            }
            // Chỉnh sửa loại hàng
            if (isValid) {
                CategoryHandler.DeleteProductIdForCategory(productId, deletedCategory);
            }
            isValid = ProductStorageHandler.StoreProductList(currentProducts);
            return isValid;
        }

        public static Product FindProductArrayById(int productId) {
            Product product = new Product();
            for (int i = 0; i < currentProducts.length; i++) {
                if (currentProducts.Products[i].Id == productId) {
                    product = currentProducts.Products[i];
                    break;
                }
            }
            return product;
        }
        public static Product[] FindProductArrayById(int productId, Product[] products) {
            Product[] result = new Product[products.Length];
            for (int i = 0; i < products.Length; i++) {
                if (products[i].Id.ToString().Contains(productId.ToString())) {
                    result[i] = products[i];
                }
            }
            return GetValidList(result);
        }
        public static Product[] FindProductByName(string name, Product[] products) {
            Product[] result = new Product[products.Length];
            for (int i = 0; i < products.Length; i++) {
                if (products[i].Name.Contains(name)) {
                    result[i] = products[i];
                }
            }
            return GetValidList(result);
        }
        public static Product[] FindProductByCompany(string company, Product[] products) {
            Product[] result = new Product[products.Length];
            for (int i = 0; i < products.Length; i++) {
                if (products[i].ProductCompany.Contains(company)) {
                    result[i] = products[i];
                }
            }
            return GetValidList(result);
        }
        public static Product[] FindProductByManufacturedDate(DateOnly manufacturedDate, Product[] products) {
            Product[] result = new Product[products.Length];
            for (int i = 0; i < products.Length; i++) {
                if (products[i].ManufacturedDate.Equals(manufacturedDate)) {
                    result[i] = products[i];
                }
            }
            return GetValidList(result);
        }
        public static Product[] FindProductByExpiredDate(DateOnly expiredDate, Product[] products) {
            Product[] result = new Product[products.Length];
            for (int i = 0; i < products.Length; i++) {
                if (products[i].ExpiredDate.Equals(expiredDate)) {
                    result[i] = products[i];
                }
            }
            return GetValidList(result);
        }
        public static Product[] FindProductByCategory(string Category, Product[] products) {
            Product[] result = new Product[products.Length];
            for (int i = 0; i < products.Length; i++) {
                if (products[i].Category.Contains(Category)) {
                    result[i] = products[i];
                }
            }
            return GetValidList(result);
        }
        public static Product[] FindProductByPrice(double Price, Product[] products) {
            Product[] result = new Product[products.Length];
            for (int i = 0; i < products.Length; i++) {
                if (products[i].Price.Equals(Price)) {
                    result[i] = products[i];
                }
            }
            return GetValidList(result);
        }

        internal static void UpdateCategoryNameForListProducts(int[] productId, string newCategory) {
            Product[] products = currentProducts.Products;
            for (int i = 0; i < productId.Length; i++) {
                for(int j = 0; j < products.Length; j++) {
                    if (products[j].Id == productId[i]) {
                        products[j].Category = newCategory;
                    }
                }
            }
            currentProducts.Products = products; 
            bool isValid = ProductStorageHandler.StoreProductList(currentProducts);
        }
    }
}

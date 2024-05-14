using System.Xml.Linq;

namespace KTLT_23880008.Entity {
    public struct ProductList {
        public Product[] Products;
        public int length;

        public ProductList(int length, Product[] products) : this() {
            this.length = length;
            Products = products;
        }

        public int GetValidList() { 
            int ValidLength = 0;
            for(int i = 0; i < length; i++) {
                if (!Products[i].Equals(default(Product))) {
                    ValidLength++;
                }
            }
            return ValidLength; 
        }

        public bool AddProduct(Product product) {
            try {
                length++;
                Product[] NewProducts = new Product[length];
                for (int i = 0; i < length - 1; i++) {
                    NewProducts[i] = Products[i];
                }
                NewProducts[NewProducts.Length - 1] = product;
                Products = NewProducts;
                return true;
            } catch (Exception ex) {
                return false;
            }

        }

        public bool EditProduct(int productId, Product product) {
            try {
                int index = FindIndex(productId);
                if (index < 0 || index > length - 1) {
                    return false;
                }
                Products[index] = product;
                return true;
            } catch (Exception ex) {
                return false;
            }
        }
        

        public bool DeleteProductByProductId(int productId, out string category) {
            category = null;
            try {
                int index = FindIndex(productId);
                if (index < 0 || index > length - 1) {
                    return false;
                }
                length--;
                Product[] NewProducts = new Product[length];
                for (int i = 0; i < Products.Length; i++) {
                    if (i < index) {
                        NewProducts[i] = Products[i];
                    } else if (i > index) {
                        NewProducts[i-1] = Products[i];
                    } else {
                        category = Products[i].Category;
                    }
                }
                Products = NewProducts;
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        public int FindIndex(int productId) {
            int index = -1;
            for(int j = 0; j < length; j++) {
                if (Products[j].Id == productId) {
                    index = j; break;
                }
            }
            return index;
        }
    }
}

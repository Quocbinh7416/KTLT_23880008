using Newtonsoft.Json;
using System;
using System.Reflection;

namespace KTLT_23880008.Entity {
    public struct Category {
        public int Id;
        public string Name;
        public int[] ProductIds;

        public Category(string categoryName) : this() {
            this.Id = 1;
            this.Name = categoryName;
            this.ProductIds = Array.Empty<int>();
        }

        public Category(string categoryName, int[] productIds) : this(categoryName) {
            this.Id = 1;
            this.Name = categoryName;
            this.ProductIds = productIds;
        }

        public string GetProductIds() {
            if(ProductIds == null || ProductIds.Length == 0) {
                return string.Empty;
            }
            return JsonConvert.SerializeObject(ProductIds);
        }

        internal bool AddProductId(int productId) {
            try {
                int length = ProductIds.Length + 1;
                int[] NewProductIds = new int[length];
                for (int i = 0; i < length - 1; i++) {
                    NewProductIds[i] = ProductIds[i];
                }
                NewProductIds[NewProductIds.Length - 1] = productId;
                ProductIds = NewProductIds;
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        internal bool DeleteProductId(int productId) {
            try {
                int length = ProductIds.Length - 1;
                int[] NewProductIds = new int[length];
                int j = 0;
                for (int i = 0; i < ProductIds.Length; i++) {
                    if (ProductIds[i] != productId) {
                        NewProductIds[j++] = ProductIds[i];
                    }
                }
                ProductIds = NewProductIds;
                return true;
            } catch (Exception ex) {
                return false;
            }
        }
    }
}

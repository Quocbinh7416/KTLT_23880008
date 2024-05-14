using KTLT_23880008.Entity;
using KTLT_23880008.LuuTru;
using System;
using System.Reflection;

namespace KTLT_23880008.NghiepVu {
    public class CategoryHandler {
        public static CategoryList CategoryList = CategoryStorageHandler.RetrieveCategories();
        public static Product[] products = ProductStorageHandler.RetrieveProducts().Products;



        public static Category[] GetCategoryList() {
            return CategoryList.Categories;
        }
        public static string[] GetCategoryNameList() {
            if(CategoryList.CategoryNames.Length == 0) {
                string[] result = new string[1];
                result[0] = "Mặc định";
                AddCategory("Mặc định");
                ReloadCategories();
                return result;
            }
            return CategoryList.CategoryNames;
        }

        public static void ReloadCategories() {
            CategoryList = CategoryStorageHandler.RetrieveCategories();
        }

        public static bool AddCategory(string category) {
            CategoryList.AddCategory(category);
            return CategoryStorageHandler.StoreCategory(CategoryList);
        }

        public static bool AddCategory(string category, int[] productIds) {
            CategoryList.AddCategory(category, productIds);
            return CategoryStorageHandler.StoreCategory(CategoryList);
        }

        public static bool AddCategory(Category category) {
            CategoryList.AddCategory(category);
            return CategoryStorageHandler.StoreCategory(CategoryList);
        }

        public static bool EditCategory(Category oldCategory, Category newCategory) {
            CategoryList.EditCategory(oldCategory, newCategory);
            int[] productId = oldCategory.ProductIds;
            ProductHandler.UpdateCategoryNameForListProducts(productId, newCategory.Name);
            return CategoryStorageHandler.StoreCategory(CategoryList);
        }

        public static bool DeleteCategory(Category oldCategory) {

            CategoryList.DeleteCategoryByName(oldCategory.Name);

            return CategoryStorageHandler.StoreCategory(CategoryList);
        }

        public static bool AddProductIdForCategory(int productId, string category) {
            int index = CategoryList.FindIndexByName(category);
            CategoryList.AddProductIdForCategory(productId, index);
            return CategoryStorageHandler.StoreCategory(CategoryList);


        }

        public static bool EditProductIdForCategory(int productId, string oldCategory, string newCategory) {
            if(oldCategory.Equals(newCategory)) return true;
            int oldIndex = CategoryList.FindIndexByName(oldCategory);
            int newIndex = CategoryList.FindIndexByName(newCategory);

            CategoryList.DeleteProductIdForCategory(productId, oldIndex);
            CategoryList.AddProductIdForCategory(productId, newIndex);

            return CategoryStorageHandler.StoreCategory(CategoryList);

        }

        public static bool DeleteProductIdForCategory(int productId, string oldCategory) {
            int index = CategoryList.FindIndexByName(oldCategory);
            CategoryList.DeleteProductIdForCategory(productId, index);
            return CategoryStorageHandler.StoreCategory(CategoryList);

        }

        public static Category[] FindCategoryById(int categoryId, Category[] categories) {
            Category[] result = new Category[categories.Length];
            for (int i = 0; i < categories.Length; i++) {
                if (categories[i].Id.Equals(categoryId)) {
                    result[i] = categories[i];
                }
            }
            return GetValidList(result);

        }

        public static Category[] FindCategoryByName(string name, Category[] categories) {
            Category[] result = new Category[categories.Length];
            for (int i = 0; i < categories.Length; i++) {
                if (categories[i].Name.Contains(name)) {
                    result[i] = categories[i];
                }
            }
            return GetValidList(result);

        }

        private static Category[] GetValidList(Category[] categories) {
            int ValidLength = 0;
            for (int i = 0; i < categories.Length; i++) {
                if (!categories[i].Equals(default(Category))) {
                    ValidLength++;
                }
            }
            Category[] result = new Category[ValidLength];
            int j = 0;
            for (int i = 0; i < categories.Length; i++) {
                if (!categories[i].Equals(default(Category))) {
                    result[j++] = categories[i];
                }
            }
            return result;
        }
    }
}

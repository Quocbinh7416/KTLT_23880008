using System.Security.Cryptography;

namespace KTLT_23880008.Entity {
    public struct CategoryList {

        public Category[] Categories;
        public string[] CategoryNames;
        public int Length;

        public CategoryList(int Length, Category[] Categories) : this() {
            this.Length = Length;
            this.Categories = Categories;
            this.CategoryNames = new string[Length];
            for(int i = 0; i < Length; i++) {
                CategoryNames[i] = Categories[i].Name;
            }
        }

        public int GetValidList() {
            int ValidLength = 0;
            for (int i = 0; i < Length; i++) {
                if (!Categories[i].Equals(default(Category))) {
                    ValidLength++;
                }
            }
            return ValidLength;
        }

        public bool AddCategory(Category Category) {
            try {
                Length++;
                Category[] NewCategories = new Category[Length];
                for (int i = 0; i < Length - 1; i++) {
                    NewCategories[i] = Categories[i];
                }
                NewCategories[NewCategories.Length - 1] = Category;
                Categories = NewCategories;
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        public bool AddCategory(string CategoryName) {
            try {
                Length++;
                Category[] NewCategories = new Category[Length];
                for (int i = 0; i < Length - 1; i++) {
                    NewCategories[i] = Categories[i];
                }
                NewCategories[NewCategories.Length - 1] = new Category(CategoryName);
                Categories = NewCategories;
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        internal bool AddCategory(string category, int[] productIds) {
            try {
                Length++;
                Category[] NewCategories = new Category[Length];
                for (int i = 0; i < Length - 1; i++) {
                    NewCategories[i] = Categories[i];
                }
                NewCategories[NewCategories.Length - 1] = new Category(category, productIds);
                Categories = NewCategories;
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        public bool EditCategory(Category oldCategory, Category newCategory) {
            try {
                int index = FindIndexByName(oldCategory.Name);
                if (index < 0 || index > Length - 1) {
                    return false;
                }
                Categories[index].Name = newCategory.Name;
                return true;
            } catch (Exception ex) {
                return false;
            }
        }


        public bool DeleteCategoryByName(string name) {
            try {
                int index = FindIndexByName(name);
                if (index < 0 || index > Length - 1) {
                    return false;
                }
                Length--;
                if(Length == 0) {
                    Categories = new Category[Length];
                    return true;
                }
                Category[] NewProducts = new Category[Length];
                for (int i = 0; i < Categories.Length; i++) {
                    if (i < index) {
                        NewProducts[i] = Categories[i];
                    } else if (i > index) {
                        NewProducts[i - 1] = Categories[i];
                    }
                }
                Categories = NewProducts;
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        public int FindIndexByName(string name) {
            int index = -1;
            for (int j = 0; j < Length; j++) {
                if (Categories[j].Name.Equals(name)) {
                    index = j; break;
                }
            }
            return index;
        }

        internal void AddProductIdForCategory(int productId, int index) {
            if(index >= 0 && index < Length) {
                Category category = Categories[index];
                category.AddProductId(productId);
                Categories[index] = category;
            }
        }

        internal void DeleteProductIdForCategory(int productId, int index) {
            if (index >= 0 && index < Length) {
                Category category = Categories[index];
                category.DeleteProductId(productId);
                Categories[index] = category;
            }
        }
    }
}

using KTLT_23880008.Entity;
using KTLT_23880008.LuuTru;
using KTLT_23880008.NghiepVu;
using System.Xml.Linq;

namespace KTLT_23880008.Validation {
    public class CategoryValidation {

        public static bool IsExistedId(int id, Category[] Categories) {
            bool isValid = false;
            foreach (Category Category in Categories) {
                if (Category.Id.Equals(id)) {
                    isValid = true;
                    break;
                }
            }
            return isValid;
        }
        public static bool IsExistedProductId(Category Category) {
            return Category.ProductIds.Length > 0;
        }
        public static bool IsExistedId(int id, Category[] Categories, out Category category) {
            category = default;
            bool isValid = false;
            foreach (Category Category in Categories) {
                if (Category.Id.Equals(id)) {
                    isValid = true;
                    category = Category;
                    break;
                }
            }
            return isValid;
        }
        public static bool IsDuplicateName(string name, Category[] Categories) {
            bool isValid = false;
            foreach (Category Category in Categories) {
                if (Category.Name.Equals(name)) {
                    isValid = true;
                    break;
                }
            }
            return isValid;
        }

        public static bool FindByName(string name, Category[] Categories, out Category Category) {
            Category = default;
            bool isValid = false;
            foreach (var item in Categories) {
                if (item.Name.Equals(name)) {
                    isValid = true;
                    Category = item;
                    break;
                }
            }
            return isValid;
        }
    }
}

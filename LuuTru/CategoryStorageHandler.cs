using KTLT_23880008.Entity;
using Newtonsoft.Json;

namespace KTLT_23880008.LuuTru {
    public class CategoryStorageHandler {
        private static string GetCategoryDetailsJson(Object product) {
            return JsonConvert.SerializeObject(product);
        }

        private static bool StoreData(String data) {
            bool isValid = false;
            string dir = Environment.CurrentDirectory;
            string combined = Path.Combine(dir, "Resources", ResoucesTypes.CATEGORY.FileName);
            using (FileStream fs = new FileStream(combined, FileMode.Create, FileAccess.Write)) {
                StreamWriter sw = new(fs);
                sw.WriteLine(data);
                sw.Flush();
                isValid = true;
            }
            return isValid;
        }

        public static CategoryList RetrieveCategories() {
            Category[]? CategoryNames = null;
            string dir = Environment.CurrentDirectory;
            string combined = Path.Combine(dir, "Resources", ResoucesTypes.CATEGORY.FileName);
            using (FileStream fs = new FileStream(combined, FileMode.Open, FileAccess.ReadWrite))
            using (StreamReader sr = new(fs)) {
                while (!sr.EndOfStream) {
                    string s = sr.ReadToEnd();
                    if (s != null && s != string.Empty) {
                        CategoryNames = JsonConvert.DeserializeObject<Category[]>(s);
                    }
                }
            }

            if (CategoryNames != null) {
                return new CategoryList(CategoryNames.Length, CategoryNames);
            } else {
                return new CategoryList();
            }
        }

        public static bool StoreCategory(CategoryList CategoryList) {
            string data = GetCategoryDetailsJson(CategoryList.Categories);
            return StoreData(data);
        }
    }
}

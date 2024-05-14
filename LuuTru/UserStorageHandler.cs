using KTLT_23880008.Entity;
using Newtonsoft.Json;

namespace KTLT_23880008.LuuTru {
    public class UserStorageHandler {
        private static string GetUserDetailsJson(Object product) {
            return JsonConvert.SerializeObject(product);
        }

        private static bool StoreData(String data) {
            bool isValid = false;
            string dir = Environment.CurrentDirectory;
            string combined = Path.Combine(dir, "Resources", ResoucesTypes.USER.FileName);
            using (FileStream fs = new FileStream(combined, FileMode.Create, FileAccess.Write)) {
                StreamWriter sw = new(fs);
                sw.WriteLine(data);
                sw.Flush();
                isValid = true;
            }
            return isValid;
        }

        public static UserList RetrieveUsers() {
            User[]? UserNames = null;
            string dir = Environment.CurrentDirectory;
            string combined = Path.Combine(dir, "Resources", ResoucesTypes.USER.FileName);
            using (FileStream fs = new FileStream(combined, FileMode.Open, FileAccess.ReadWrite))
            using (StreamReader sr = new(fs)) {
                while (!sr.EndOfStream) {
                    string s = sr.ReadToEnd();
                    if (s != null && s != string.Empty) {
                        UserNames = JsonConvert.DeserializeObject<User[]>(s);
                    }
                }
            }

            if (UserNames != null) {
                return new UserList(UserNames.Length, UserNames);
            } else {
                return new UserList();
            }
        }

        public static bool StoreUser(UserList UserList) {
            string data = GetUserDetailsJson(UserList.Users);
            return StoreData(data);
        }
    }
}

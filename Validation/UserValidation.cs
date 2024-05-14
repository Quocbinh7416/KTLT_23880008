using KTLT_23880008.Entity;
using KTLT_23880008.LuuTru;

namespace KTLT_23880008.Validation {
    public class UserValidation {

        public static bool FindByUserName(string userName, User[] Users, out User User) {
            User = default;
            bool isValid = false;
            foreach (var item in Users) {
                if (item.UserName.Equals(userName)) {
                    isValid = true;
                    User = item;
                    break;
                }
            }
            return isValid;
        }
        public static bool CheckValidPassword(string Password) {
            return Password.Length >= 3;
        }
    }
}

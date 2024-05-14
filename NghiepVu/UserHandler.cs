using KTLT_23880008.Entity;
using KTLT_23880008.LuuTru;
using System;
using System.Reflection;

namespace KTLT_23880008.NghiepVu {
    public class UserHandler {
        public static UserList UserList = UserStorageHandler.RetrieveUsers();

        public static User[] GetUserList() {
            return UserList.Users;
        }

        public static void ReloadUsers() {
            UserList = UserStorageHandler.RetrieveUsers();
        }

        public static bool AddUser(User User) {
            UserList.AddUser(User);
            return UserStorageHandler.StoreUser(UserList);
        }
        public static bool Login(User User) {
            return false;
        }


        public static User[] FindUserByName(string name, User[] Users) {
            User[] result = new User[Users.Length];
            for (int i = 0; i < Users.Length; i++) {
                if (Users[i].UserName.Contains(name)) {
                    result[i] = Users[i];
                }
            }
            return GetValidList(result);

        }

        private static User[] GetValidList(User[] Users) {
            int ValidLength = 0;
            for (int i = 0; i < Users.Length; i++) {
                if (!Users[i].Equals(default(User))) {
                    ValidLength++;
                }
            }
            User[] result = new User[ValidLength];
            int j = 0;
            for (int i = 0; i < Users.Length; i++) {
                if (!Users[i].Equals(default(User))) {
                    result[j++] = Users[i];
                }
            }
            return result;
        }
    }
}

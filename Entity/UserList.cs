using System.Xml.Linq;

namespace KTLT_23880008.Entity {
    public struct UserList {
        public User[] Users;
        public int Length;

        public UserList(int length, User[] users) : this() {
            this.Length = length;
            Users = users;
        }

        public int GetValidList() { 
            int ValidLength = 0;
            for(int i = 0; i < Length; i++) {
                if (!Users[i].Equals(default(User))) {
                    ValidLength++;
                }
            }
            return ValidLength; 
        }

        public bool AddUser(User User) {
            try {
                Length++;
                User[] NewUsers = new User[Length];
                for (int i = 0; i < Length - 1; i++) {
                    NewUsers[i] = Users[i];
                }
                NewUsers[NewUsers.Length - 1] = User;
                Users = NewUsers;
                return true;
            } catch (Exception ex) {
                return false;
            }

        }

        public bool EditUser(string Name, User User) {
            try {
                int index = FindIndexByName(Name);
                if (index < 0 || index > Length - 1) {
                    return false;
                }
                Users[index] = User;
                return true;
            } catch (Exception ex) {
                return false;
            }
        }
        

        public bool DeleteUserByUserName(string Name) {
            try {
                int index = FindIndexByName(Name);
                if (index < 0 || index > Length - 1) {
                    return false;
                }
                Length--;
                User[] NewUsers = new User[Length];
                for (int i = 0; i < Users.Length; i++) {
                    if (i < index) {
                        NewUsers[i] = Users[i];
                    } else if (i > index) {
                        NewUsers[i-1] = Users[i];
                    } else {
                    }
                }
                Users = NewUsers;
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        public int FindIndexByName(string Name) {
            int index = -1;
            for(int j = 0; j < Length; j++) {
                if (Users[j].UserName == Name) {
                    index = j; break;
                }
            }
            return index;
        }
    }
}

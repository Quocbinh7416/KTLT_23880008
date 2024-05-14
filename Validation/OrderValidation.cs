using KTLT_23880008.Entity;
using KTLT_23880008.LuuTru;

namespace KTLT_23880008.Validation {
    public class OrderValidation {
        public static bool IsDuplicateId(int id) {
            bool isValid = false;
            Order[] Orders = OrderStorageHandler.RetrieveOrders().Orders;
            foreach (Order Order in Orders) {
                if (Order.Id == id) {
                    isValid = true;
                    break;
                }
            }
            return isValid;
        }

        public static bool FindById(int id, Order[] Orders, out Order Order) {
            Order = default;
            bool isValid = false;
            foreach (var item in Orders) {
                if (item.Id == id) {
                    isValid = true;
                    Order = item;
                    break;
                }
            }
            return isValid;
        }


        public static bool IsValidOrderPrice(double price) {
            return price > 0;
        }
    }
}

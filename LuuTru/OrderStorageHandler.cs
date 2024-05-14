using KTLT_23880008.Entity;
using Newtonsoft.Json;

namespace KTLT_23880008.LuuTru {
    public class OrderStorageHandler {
        private static string GetOrderDetailsJson(Object data) {
            return JsonConvert.SerializeObject(data);
        }

        private static bool StoreData(String data) {
            bool isValid = false;
            string dir = Environment.CurrentDirectory;
            string combined = Path.Combine(dir, "Resources", ResoucesTypes.ORDER.FileName);
            using (FileStream fs = new FileStream(combined, FileMode.Create, FileAccess.Write)) {
                StreamWriter sw = new(fs);
                sw.WriteLine(data);
                sw.Flush();
                isValid = true;
            }
            return isValid;
        }

        public static OrderList RetrieveOrders() {
            Order[]? Orders = null;
            string dir = Environment.CurrentDirectory;
            string combined = Path.Combine(dir, "Resources", ResoucesTypes.ORDER.FileName);
            using (FileStream fs = new FileStream(combined, FileMode.Open, FileAccess.ReadWrite))
            using (StreamReader sr = new(fs)) {
                while (!sr.EndOfStream) {
                    string s = sr.ReadToEnd();
                    if (s != null && s != string.Empty) {
                        Orders = JsonConvert.DeserializeObject<Order[]>(s);
                    }
                }
            }

            if (Orders != null && Orders.Length > 0) {
                return new OrderList(Orders.Length, Orders);
            } else {
                return new OrderList();
            }
        }

        public static bool StoreOrder(OrderList OrderList) {
            string data = GetOrderDetailsJson(OrderList.Orders);
            return StoreData(data);
        }
    }
}

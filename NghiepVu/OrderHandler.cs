using KTLT_23880008.Entity;
using KTLT_23880008.LuuTru;
using System.Linq;

namespace KTLT_23880008.NghiepVu {
    public class OrderHandler {
        static OrderList currentOrders = OrderStorageHandler.RetrieveOrders();
        private static Order EditedOrder;

        public static Order GetEditedOrder() {
            return EditedOrder;
        }

        public static void SetEditedOrder(Order value) {
            EditedOrder = value;
        }

        public static OrderList GetOrderList() {
            return currentOrders;
        }

        public static Order[] GetOrders() {
            return currentOrders.Orders;
        }

        public static Order[] GetSellOrders() {
            return currentOrders.SellOrders;
        }

        public static Order[] GetBuyOrders() {
            return currentOrders.BuyOrders;
        }

        private static Order[] GetValidList(Order[] Orders) {
            int ValidLength = 0;
            for (int i = 0; i < Orders.Length; i++) {
                if (!Orders[i].Equals(default(Order))) {
                    ValidLength++;
                }
            }
            Order[] result = new Order[ValidLength];
            int j = 0;
            for (int i = 0; i < Orders.Length; i++) {
                if (!Orders[i].Equals(default(Order))) {
                    result[j++] = Orders[i];
                }
            }
            return result;
        }

        public static void ReloadOrders() {
            currentOrders = OrderStorageHandler.RetrieveOrders();
        }

        public static bool AddOrder(Order order) {
            bool isValid = currentOrders.AddOrder(order);
            if (!isValid) {
                return isValid;
            }
            isValid = StockHandler.AddOrder(order);
            if (!isValid) {
                return isValid;
            }
            return OrderStorageHandler.StoreOrder(currentOrders);
        }

        public static bool EditOrder(int id, Order Order) {
            bool isValid = currentOrders.EditOrder(id, Order);
            if (!isValid) {
                return isValid;
            }
            isValid = OrderStorageHandler.StoreOrder(currentOrders);
            if (!isValid) {
                return isValid;
            }
            
            return StockHandler.ReloadOrderData();
        }

        public static bool DeleteOrderById(int id) {
            bool isValid = currentOrders.DeleteOrderByOrderId(id, out Order order);
            if (!isValid) {
                return isValid;
            }
            isValid = StockHandler.DeleteOrder(order);
            if (!isValid) {
                return isValid;
            }
            return OrderStorageHandler.StoreOrder(currentOrders);
        }

        public static Order[] FindOrderArrayById(int OrderId, Order[] Orders) {
            Order[] result = new Order[Orders.Length];
            for (int i = 0; i < Orders.Length; i++) {
                if (Orders[i].Id.ToString().Contains(OrderId.ToString())) {
                    result[i] = Orders[i];
                }
            }
            return GetValidList(result);
        }
        public static Order[] FindOrderByProductName(string ProductName, Order[] Orders) {
            Order[] result = new Order[Orders.Length];
            for (int i = 0; i < Orders.Length; i++) {
                if (Orders[i].ProductName.Contains(ProductName)) {
                    result[i] = Orders[i];
                }
            }
            return GetValidList(result);
        }
        public static Order[] FindOrderByCreatedDate(DateOnly CreatedDate, Order[] Orders) {
            Order[] result = new Order[Orders.Length];
            for (int i = 0; i < Orders.Length; i++) {
                if (Orders[i].OrderCreatedDate.Equals(CreatedDate)) {
                    result[i] = Orders[i];
                }
            }
            return GetValidList(result);
        }
        public static Order[] FindOrderByOrderType(string OrderType, Order[] Orders) {
            Order[] result = new Order[Orders.Length];
            for (int i = 0; i < Orders.Length; i++) {
                if (Orders[i].GetOrderType().Equals(OrderType)) {
                    result[i] = Orders[i];
                }
            }
            return GetValidList(result);
        }
        public static Order[] FindOrderByQuantityOver(int Quantity, Order[] Orders) {
            Order[] result = new Order[Orders.Length];
            for (int i = 0; i < Orders.Length; i++) {
                if (Orders[i].Quantity >= Quantity) {
                    result[i] = Orders[i];
                }
            }
            return GetValidList(result);
        }
        public static Order[] FindOrderByTotalPriceOver(double TotalPrice, Order[] Orders) {
            Order[] result = new Order[Orders.Length];
            for (int i = 0; i < Orders.Length; i++) {
                if (Orders[i].TotalPrice >= TotalPrice) {
                    result[i] = Orders[i];
                }
            }
            return GetValidList(result);
        }

        public static bool EditProductForOrder(Product oldProduct, Product newProduct) {
            if(!oldProduct.Equals(newProduct)) {
                for (int i = 0; i < currentOrders.Orders.Length; i++) {
                    if (currentOrders.Orders[i].ProductName.Equals(oldProduct.Name)) {
                        Order order = currentOrders.Orders[i];
                        if(order.ProductName != newProduct.Name) {
                            order.ProductName = newProduct.Name;
                        }
                        if(order.ProductPrice != newProduct.Price) {
                            order.ProductPrice = newProduct.Price;
                            order.TotalPrice = order.ProductPrice * order.Quantity;
                        }
                        if (!order.ExpiredDate.Equals(newProduct.ExpiredDate)) {
                            order.ExpiredDate = newProduct.ExpiredDate;
                        }
                        currentOrders.Orders[i] = order;
                    }
                }
            }
            bool isValid = OrderStorageHandler.StoreOrder(currentOrders);
            if(!isValid) {
                return false;
            }
            return StockHandler.ReloadOrderData();
        }
    }
}

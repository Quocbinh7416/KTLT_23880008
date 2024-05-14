namespace KTLT_23880008.Entity {
    public struct OrderList {
        public Order[] Orders;
        public Order[] SellOrders;
        public Order[] BuyOrders;
        public int Length;
        public double TotalPrice;
        public double BuyPrice;
        public double SellPrice;

        public OrderList() {
            Length = 0;
            Orders = Array.Empty<Order>();
            SellOrders = Array.Empty<Order>();
            BuyOrders = Array.Empty<Order>();
            TotalPrice = 0;
            SellPrice = 0;
            BuyPrice = 0;
        }
        public OrderList(int length, Order[] orders) : this() {
            Length = length;
            Orders = orders;
            int buyCount = GetBuyOrderCounts(out int sellCount);
            SellOrders = new Order[sellCount];
            BuyOrders = new Order[buyCount];
            int b = 0;
            int s = 0;
            for (int i = 0; i < Orders.Length; i++) {
                if (Orders[i].OrderType.Equals(OrderType.BUY)) {
                    BuyOrders[b++] = Orders[i];
                    BuyPrice += Orders[i].TotalPrice;
                } else {
                    SellOrders[s++] = Orders[i];
                    SellPrice += Orders[i].TotalPrice;
                }
            }
            TotalPrice = BuyPrice + SellPrice;
        }

        public int GetBuyOrderCounts(out int sellTotalCount) {
            sellTotalCount = 0;
            int buyTotalCount = 0;
            if (Orders == null || Orders.Length == 0) {
                return buyTotalCount;
            }
            for (int i = 0; i < Orders.Length; i++) {
                if (Orders[i].OrderType.Equals(OrderType.BUY)) {
                    buyTotalCount++;
                } else {
                    sellTotalCount++;
                }
            }
            return buyTotalCount;
        }

        public bool AddOrder(Order Order) {
            try {
                Length++;
                Order[] NewOrders = new Order[Length];
                for (int i = 0; i < Length - 1; i++) {
                    NewOrders[i] = Orders[i];
                }
                NewOrders[NewOrders.Length - 1] = Order;
                Orders = NewOrders;
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        public bool EditOrder(int OrderId, Order Order) {
            try {
                int index = FindIndex(OrderId);
                if (index < 0 || index > Length - 1) {
                    return false;
                }
                Orders[index] = Order;
                return true;
            } catch (Exception ex) {
                return false;
            }
        }


        public bool DeleteOrderByOrderId(int OrderId, out Order order) {
            order = default(Order);
            try {
                int index = FindIndex(OrderId);
                if (index < 0 || index > Length - 1) {
                    return false;
                }
                Length--;
                Order[] NewOrders = new Order[Length];
                for (int i = 0; i < Orders.Length; i++) {
                    if (i < index) {
                        NewOrders[i] = Orders[i];
                    } else if (i > index) {
                        NewOrders[i - 1] = Orders[i];
                    } else {
                        order = Orders[i];
                    }
                }
                Orders = NewOrders;
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        public int FindIndex(int OrderId) {
            int index = -1;
            for (int j = 0; j < Length; j++) {
                if (Orders[j].Id == OrderId) {
                    index = j; break;
                }
            }
            return index;
        }
    }
}

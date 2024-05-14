using KTLT_23880008.Entity;
using KTLT_23880008.LuuTru;
using System;
using System.Reflection;

namespace KTLT_23880008.NghiepVu {
    public class StockHandler {
        public static StockList CurrentStocksList = StockStorageHandler.RetrieveStocks();

        public static Stock[] GetStockList() {
            if (CurrentStocksList.Length > 0) {
                return CurrentStocksList.Stocks;
            }
            return new Stock[0];
        }
        public static Stock[] GetExpiredStockList() {
            return GetValidList(CurrentStocksList.GetExpiredStockList());
        }
        public static Stock GetStockByProductName(string ProductName) {
            if (CurrentStocksList.Length > 0) {
                foreach (Stock s in CurrentStocksList.Stocks) {
                    if (s.ProductName.Equals(ProductName)) {
                        return s;
                    }
                }
            }
            return default;
        }

        public static void ReloadStocks() {
            CurrentStocksList = StockStorageHandler.RetrieveStocks();
        }

        public static bool AddNewStock(Stock Stock) {
            CurrentStocksList.AddNewStock(Stock);
            return StockStorageHandler.StoreStock(CurrentStocksList);
        }

        public static bool AddProductForStock(string ProductName, int quantity) {
            CurrentStocksList.AddProductForStock(ProductName, quantity);
            return StockStorageHandler.StoreStock(CurrentStocksList);
        }
        public static bool SubtractProductForStock(string ProductName, int quantity) {
            CurrentStocksList.SubtractProductForStock(ProductName, quantity);
            return StockStorageHandler.StoreStock(CurrentStocksList);
        }

        public static bool AddOrder(Order order) {
            Stock stock;
            int index = CurrentStocksList.FindIndexByProductName(order.ProductName, out stock);
            if (index < 0) {
                if (order.OrderType.Equals(OrderType.BUY)) {
                    stock.ProductName = order.ProductName;
                    stock.ExpiredDate = order.ExpiredDate;
                    stock.Quantity = order.Quantity;
                    return AddNewStock(stock);
                } else {
                    return false;
                }
            } else {
                if (order.OrderType.Equals(OrderType.BUY)) {
                    return AddProductForStock(order.ProductName, order.Quantity);
                } else {
                    return SubtractProductForStock(order.ProductName, order.Quantity);
                }
            }
        }

        public static bool DeleteOrder(Order order) {
            int index = CurrentStocksList.FindIndexByProductName(order.ProductName, out Stock stock);
            if (index < 0) {
                return false;
            } else {
                if (order.OrderType.Equals(OrderType.BUY)) {
                    return SubtractProductForStock(order.ProductName, order.Quantity);
                } else {
                    return AddProductForStock(order.ProductName, order.Quantity);
                }
            }
        }

        public static bool ReloadOrderData() {
            CurrentStocksList.Length = 0;
            CurrentStocksList.Stocks = new Stock[0];
            StockStorageHandler.StoreStock(CurrentStocksList);

            OrderList currentOrders = OrderStorageHandler.RetrieveOrders();
            bool isValid = false;
            for(int i = 0; i < currentOrders.Length; i++) {
                isValid = AddOrder(currentOrders.Orders[i]);
                if (!isValid) {
                    break;
                }
            }
            return isValid;
        }

        private static Stock[] GetValidList(Stock[] Stocks) {
            int ValidLength = 0;
            for (int i = 0; i < Stocks.Length; i++) {
                if (!Stocks[i].Equals(default(Stock))) {
                    ValidLength++;
                }
            }
            Stock[] result = new Stock[ValidLength];
            int j = 0;
            for (int i = 0; i < Stocks.Length; i++) {
                if (!Stocks[i].Equals(default(Stock))) {
                    result[j++] = Stocks[i];
                }
            }
            return result;
        }
    }
}

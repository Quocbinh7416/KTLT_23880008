using System.Xml.Linq;

namespace KTLT_23880008.Entity {
    public struct StockList {
        public Stock[] Stocks;
        public int Length;

        public StockList(int length, Stock[] Stocks) : this() {
            this.Length = length;
            this.Stocks = Stocks;
        }

        public int GetValidList() { 
            int ValidLength = 0;
            for(int i = 0; i < Length; i++) {
                if (!Stocks[i].Equals(default(Stock))) {
                    ValidLength++;
                }
            }
            return ValidLength; 
        }
        public Stock[] GetExpiredStockList() {
            if (Length > 0) {
                Stock[] expiredList = new Stock[Length];
                int i = 0;
                foreach (Stock s in Stocks) {
                    if (s.ExpiredDate.CompareTo(DateOnly.FromDateTime(DateTime.Now)) < 0) {
                        expiredList[i++] = s;
                    }
                }
                return expiredList;
            }
            return new Stock[0];
        }

        public bool AddNewStock(Stock Stock) {
            try {
                Length++;
                Stock[] NewStocks = new Stock[Length];
                for (int i = 0; i < Length - 1; i++) {
                    NewStocks[i] = Stocks[i];
                }
                NewStocks[NewStocks.Length - 1] = Stock;
                Stocks = NewStocks;
                return true;
            } catch (Exception ex) {
                return false;
            }

        }

        public bool EditStock(string Name, Stock Stock) {
            try {
                int index = FindIndexByProductName(Name);
                if (index < 0 || index > Length - 1) {
                    return false;
                }
                Stocks[index] = Stock;
                return true;
            } catch (Exception ex) {
                return false;
            }
        }
        

        public bool DeleteStockByProductName(string Name) {
            try {
                int index = FindIndexByProductName(Name);
                if (index < 0 || index > Length - 1) {
                    return false;
                }
                Length--;
                Stock[] NewStocks = new Stock[Length];
                for (int i = 0; i < Stocks.Length; i++) {
                    if (i < index) {
                        NewStocks[i] = Stocks[i];
                    } else if (i > index) {
                        NewStocks[i-1] = Stocks[i];
                    } else {
                    }
                }
                Stocks = NewStocks;
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        public int FindIndexByProductName(string Name) {
            int index = -1;
            for(int j = 0; j < Length; j++) {
                if (Stocks[j].ProductName == Name) {
                    index = j; break;
                }
            }
            return index;
        }

        public int FindIndexByProductName(string Name, out Stock stock) {
            stock = default(Stock);
            int index = -1;
            for(int j = 0; j < Length; j++) {
                if (Stocks[j].ProductName == Name) {
                    index = j;
                    stock = Stocks[j];
                    break;
                }
            }
            return index;
        }

        internal bool AddProductForStock(string productName, int quantity) {
            try {
                int index = FindIndexByProductName(productName, out Stock stock);
                if (index < 0 || index > Length - 1) {
                    return false;
                }
                stock.Quantity += quantity;
                Stocks[index] = stock;
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        internal bool SubtractProductForStock(string productName, int quantity) {
            try {
                int index = FindIndexByProductName(productName, out Stock stock);
                if (index < 0 || index > Length - 1) {
                    return false;
                }
                if(stock.Quantity >= quantity) {
                    stock.Quantity -= quantity;
                    Stocks[index] = stock;
                    return true;
                } else {
                    return false;
                }
            } catch (Exception ex) {
                return false;
            }
        }
    }
}

using KTLT_23880008.Entity;
using Newtonsoft.Json;

namespace KTLT_23880008.LuuTru {
    public class StockStorageHandler {
        private static string GetStockDetailsJson(Object data) {
            return JsonConvert.SerializeObject(data);
        }

        private static bool StoreData(String data) {
            bool isValid = false;
            string dir = Environment.CurrentDirectory;
            string combined = Path.Combine(dir, "Resources", ResoucesTypes.STOCK.FileName);
            using (FileStream fs = new FileStream(combined, FileMode.Create, FileAccess.Write)) {
                StreamWriter sw = new(fs);
                sw.WriteLine(data);
                sw.Flush();
                isValid = true;
            }
            return isValid;
        }

        public static StockList RetrieveStocks() {
            Stock[]? Stocks = null;
            string dir = Environment.CurrentDirectory;
            string combined = Path.Combine(dir, "Resources", ResoucesTypes.STOCK.FileName);
            using (FileStream fs = new FileStream(combined, FileMode.Open, FileAccess.ReadWrite))
            using (StreamReader sr = new(fs)) {
                while (!sr.EndOfStream) {
                    string s = sr.ReadToEnd();
                    if (s != null && s != string.Empty) {
                        Stocks = JsonConvert.DeserializeObject<Stock[]>(s);
                    }
                }
            }

            if (Stocks != null && Stocks.Length > 0) {
                return new StockList(Stocks.Length, Stocks);
            } else {
                return new StockList();
            }
        }

        public static bool StoreStock(StockList StockList) {
            string data = GetStockDetailsJson(StockList.Stocks);
            return StoreData(data);
        }
    }
}

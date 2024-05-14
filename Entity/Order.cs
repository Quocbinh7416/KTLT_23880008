namespace KTLT_23880008.Entity {
    public struct Order {
        public int Id;
        public string ProductName;
        public double ProductPrice;
        public DateOnly ExpiredDate;
        public DateOnly OrderCreatedDate;
        public OrderType OrderType;
        public int Quantity;
        public double TotalPrice;

        public string GetOrderType() {
            return OrderType.Value;
        }

        public readonly string GetCreatedDate() {
            return OrderCreatedDate.ToString("yyyy-MM-dd");
        }

        public readonly string GetExpiredDate() {
            return ExpiredDate.ToString("yyyy-MM-dd");
        }
    }
}

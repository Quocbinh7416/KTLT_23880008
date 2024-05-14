namespace KTLT_23880008.Entity {
    public struct OrderType {
        public string Value { get; private set; }
        public OrderType(string value) { this.Value = value; }

        public static OrderType BUY { get { return new OrderType("Nhập hàng"); } }
        public static OrderType SELL { get { return new OrderType("Bán hàng"); } }

        public static OrderType getOrderByValue(string value) {
            if (value.Equals(OrderType.BUY.Value)) {
                return OrderType.BUY;
            } else {
                return OrderType.SELL;
            }
        }
    }
}

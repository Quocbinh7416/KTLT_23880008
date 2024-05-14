namespace KTLT_23880008.Entity {
    public struct ResoucesTypes {
        private ResoucesTypes(string value) { FileName = value; }
        public string FileName { get; private set; }

        public static ResoucesTypes PRODUCT { get { return new ResoucesTypes("Products.json"); } }
        public static ResoucesTypes CATEGORY { get { return new ResoucesTypes("Categories.json"); } }
        public static ResoucesTypes ORDER { get { return new ResoucesTypes("Orders.json"); } }
        public static ResoucesTypes USER { get { return new ResoucesTypes("Users.json"); } }
        public static ResoucesTypes STOCK { get { return new ResoucesTypes("Stock.json"); } }

        public override string ToString() {
            return FileName;
        }
    }
}

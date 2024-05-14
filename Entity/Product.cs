namespace KTLT_23880008.Entity {
    public struct Product {
        public int Id;
        public string Name;
        public string ProductCompany;
        public DateOnly ManufacturedDate;
        public DateOnly ExpiredDate;
        public string Category;
        public double Price;

        public Product(string id, string name, string productCompany, string manufacturedDate, 
            string expiredDate, string category, string price) : this() {
            this.Id = int.Parse(id);
            this.Name = name;
            this.ProductCompany = productCompany;
            this.ManufacturedDate = DateOnly.Parse(manufacturedDate);
            this.ExpiredDate = DateOnly.Parse(expiredDate);
            this.Category = category;
            this.Price = double.Parse(price);
        }

        public Product(Product product) : this() {
            this.Id = product.Id;
            this.Name = product.Name;
            this.ProductCompany = product.ProductCompany;
            this.ManufacturedDate = product.ManufacturedDate;
            this.ExpiredDate = product.ExpiredDate;
            this.Category = product.Category;
            this.Price = product.Price;
        }

        public readonly string GetManufacturedDate() {
            return ManufacturedDate.ToString("yyyy-MM-dd");
        }
        public readonly string GetExpiredDate() {
            return ExpiredDate.ToString("yyyy-MM-dd");
        }

        public void SetData(Product product) {
            this.Id = product.Id;
            this.Name = product.Name;
            this.ProductCompany = product.ProductCompany;
            this.ManufacturedDate = product.ManufacturedDate;
            this.ExpiredDate = product.ExpiredDate;
            this.Category = product.Category;
            this.Price = product.Price;
        }
    }
}

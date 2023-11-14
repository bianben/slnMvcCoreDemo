namespace prjMvcCoreDemo.Models
{
    public class CProductWrap
    {
        public CProductWrap() 
        {
            _product = new TProduct();
        }
        private TProduct _product;
        public TProduct product
        {
            get { return _product; }
            set { _product = value; }
        }
        public int FId
        {
            get { return _product.FId; }
            set { this.product.FId = value; }
        }

        public string? FName
        {
            get { return _product.FName; }
            set { this.product.FName = value; }
        }

        public int? FQty
        {
            get { return _product.FQty; }
            set { this.product.FQty = value; }
        }

        public decimal? FCost
        {
            get { return _product.FCost; }
            set { this.product.FCost = value; }
        }

        public decimal? FPrice
        {
            get { return _product.FPrice; }
            set { this.product.FPrice = value; }
        }

        public string? FImagePath
        {
            get { return _product.FImagePath; }
            set { this.product.FImagePath = value; }
        }

        public IFormFile photo { get; set; }
    }
}

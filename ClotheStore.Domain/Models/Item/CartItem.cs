namespace ClotheStore.Domain.Models.Item
{
    public class CartItem
    {
        public Guid CartItemId { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public IEnumerable<Size> Size { get; set; }
        public IEnumerable<Customization> Customization { get; set; }

        //public Item()
        //{
        //    OptionSize = new SizeSet
        //    {
        //        S = new ProductSize(TShirtSize.S, 0),
        //        M = new ProductSize(TShirtSize.M, 0),
        //        L = new ProductSize(TShirtSize.L, 0),
        //        XL = new ProductSize(TShirtSize.XL, 0),
        //        XXL = new ProductSize(TShirtSize.XXL, 0),
        //        XXXL = new ProductSize(TShirtSize.XXXL, 0)
        //    };
        //}
    }
}

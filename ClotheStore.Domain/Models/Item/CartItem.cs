namespace ClotheStore.Domain.Models.Item
{
    public class CartItem
    {
        public Guid CartItemId { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public string? Name { get; set; }
        public int SAmount { get; set; }
        public int MAmount { get; set; }
        public int LAmount { get; set; }
        public int XLAmount { get; set; }
        public int XXLAmount { get; set; }
        public int XXXLAmount { get; set; }
        //public IEnumerable<Customization>? Customization { get; set; }
    }
}

namespace ClotheStore.Application.ViewModels
{
    public class CartVM
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
        public IEnumerable<CartItemVM> Items { get; set; }
    }
}

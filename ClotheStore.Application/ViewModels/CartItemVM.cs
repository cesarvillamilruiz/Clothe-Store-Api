namespace ClotheStore.Application.ViewModels
{
    public class CartItemVM
    {
        public Guid CartItemId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public IEnumerable<SizeVM> InventorySet { get; set; }
        public IEnumerable<CustomizationVM> Customization { get; set; }
    }
}

namespace ClotheStore.Application.ViewModels
{
    public class DesignVM
    {
        public Guid DesignId { get; set; }
        public IEnumerable<string> ProductId { get; set; }
        public string Name { get; set; }
        public IEnumerable<CustomizationVM> Customizations { get; set; }
    }
}

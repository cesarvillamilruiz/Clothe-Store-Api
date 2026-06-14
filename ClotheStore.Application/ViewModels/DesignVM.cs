using ClotheStore.Domain.Enum.Design;

namespace ClotheStore.Application.ViewModels
{
    public class DesignVM
    {
        public Guid DesignId { get; set; }
        public List<Guid> ProductId { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public string? ColorName { get; set; }
        public List<CustomizationVM>? Customizations { get; set; }
    }
}

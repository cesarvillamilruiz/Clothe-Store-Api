using ClotheStore.Domain.Enum.Design;

namespace ClotheStore.Domain.Models.Design
{
    public class Design
    {
        public Guid DesignId { get; set; }
        public Guid UserId { get; set; }
        public List<Guid> ProductId { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public string? ColorName { get; set; }
        public List<Customization.Customization> Customizations { get; set; }
    }
}

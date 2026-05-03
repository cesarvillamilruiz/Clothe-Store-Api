namespace ClotheStore.Domain.Models.Design
{
    public class Design
    {
        public Guid DesignId { get; set; }
        public Guid UserId { get; set; }
        public IEnumerable<string> ProductId { get; set; }
        public string Name { get; set; }
        public IEnumerable<ClotheStore.Domain.Models.Customization.Customization> Customizations { get; set; }
    }
}

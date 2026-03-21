namespace ClotheStore.Domain.Models.Design
{
    public class Design
    {
        public Guid DesignId { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public IEnumerable<Customization> Customizations { get; set; }
    }
}

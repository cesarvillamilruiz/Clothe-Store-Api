namespace ClotheStore.Domain.Models.Option
{
    public class OptionProduct
    {
        public Guid OptionProductId { get; set; }
        public Guid ColorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double BasePrice { get; set; }
    }
}

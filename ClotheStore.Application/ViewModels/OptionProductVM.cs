namespace ClotheStore.Application.ViewModels
{
    public class OptionProductVM
    {
        public Guid OptionProductId { get; set; }
        public Guid ColorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double BasePrice { get; set; }
    }
}

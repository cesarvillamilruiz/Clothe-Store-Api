namespace ClotheStore.Domain.Models.Color
{
    public class OptionColor
    {
        public Guid OptionColorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Hexadecimal { get; set; }
        public string ComponentName { get; set; }
    }
}

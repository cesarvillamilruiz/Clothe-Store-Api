using ClotheStore.Domain.Enum.Cart;

namespace ClotheStore.Application.ViewModels
{
    public class CustomizationVM
    {
        public Guid CustomizationId { get; set; }
        public Guid CartItemId { get; set; }
        public string Text { get; set; }
        public int ZIndex { get; set; }
        public bool IsHorizontalInverted { get; set; }
        public bool IsVerticalInverted { get; set; }
        public string FontFamily { get; set; }
        public Guid FontColorId { get; set; }
        public int FontSize { get; set; }
        public string OutlineFontColorId { get; set; }
        public Guid DesignId { get; set; }
        public string ImageUrl { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int TopDistance { get; set; }
        public int LeftDistance { get; set; }
        public int Arch { get; set; }
        public string ImageType { get; set; }
        public CustomizationType Type { get; set; }
        public bool IsFrontLocation { get; set; }
    }
}

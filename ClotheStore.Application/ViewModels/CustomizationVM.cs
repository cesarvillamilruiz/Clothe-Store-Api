namespace ClotheStore.Application.ViewModels
{
    public class CustomizationVM
    {
        // Customization
        public Guid CartItemId { get; set; }
        public bool IsHorizontalInverted { get; set; }
        public bool IsVerticalInverted { get; set; }
        public bool IsFrontLocation { get; set; }
        public int ZIndex { get; set; }
        public int TopDistance { get; set; }
        public int LeftDistance { get; set; }
        public string Type { get; set; } = string.Empty;

        // Image
        public string? BlobName { get; set; }
        public string? BlobUrl { get; set; }
        public string? ImageType { get; set; } // upload | preview | final | predesign
        public string? Category { get; set; }

        // CustomizationImage
        public int? Width { get; set; }
        public int? Height { get; set; }

        // Text
        public Guid? FontId { get; set; }
        public string? Text { get; set; }
        public int? FontSize { get; set; }
        public Guid? FontColorId { get; set; }
        public Guid? OutlineFontColorId { get; set; }
        public int? Arch { get; set; }
    }
}

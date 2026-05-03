namespace ClotheStore.Domain.Models.Customization
{
    public class Customization
    {
        // Customization
        public Guid CustomizationId { get; set; }
        public Guid DesignId { get; set; }
        public bool IsHorizontalInverted { get; set; }
        public bool IsVerticalInverted { get; set; }
        public bool IsFrontLocation { get; set; }
        public int ZIndex { get; set; }
        public int TopDistance { get; set; }
        public int LeftDistance { get; set; }
        public string Type { get; set; } = string.Empty;
        public int? Width { get; set; }
        public int? Height { get; set; }

        // Image
        public string? BlobName { get; set; }
        public string? BlobUrl { get; set; }
        public string? ImageType { get; set; } // upload | preview | final | predesign
        public string? Category { get; set; }        

        // Text
        public Guid? FontId { get; set; }
        public string? Text { get; set; }
        public int? FontSize { get; set; }
        public Guid? FontColorId { get; set; }
        public Guid? OutlineFontColorId { get; set; }
        public int? Arch { get; set; }   
    }
}

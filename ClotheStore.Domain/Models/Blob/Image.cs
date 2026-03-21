namespace ClotheStore.Domain.Models.Blob
{
    public class Image
    {
        public Guid ImageId { get; set; }
        public string BlobName { get; set; }
        public string Container { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; }
    }
}

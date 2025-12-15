namespace ClotheStore.Domain.Models.Tracking
{
    public class SiteInteraction
    {
        #region Properties
        public int LogId { get; set; }               // Primary Key
        public Guid UserId { get; set; }             // Foreign Key to User table
        public string Action { get; set; }           // Action name (max 100 chars)
        public string Description { get; set; }      // Detailed description
        public DateTime CreatedAt { get; set; }
        #endregion Properties
    }
}

namespace ClotheStore.Domain.Models.ContactPreference
{
    public class ContactPreference
    {
        public Guid ContactPreferenceId { get; set; }

        public Guid UserId { get; set; }

        public bool IsCommunicationByCall { get; set; }

        public bool IsCommunicationBySms { get; set; }

        public bool IsCommunicationByWhatsapp { get; set; }

        public bool IsCommunicationByEmail { get; set; }

        public bool IsPromotionByCall { get; set; }

        public bool IsPromotionBySms { get; set; }

        public bool IsPromotionByWhatsapp { get; set; }

        public bool IsPromotionByEmail { get; set; }

        public string Phone { get; set; } = string.Empty;
    }
}

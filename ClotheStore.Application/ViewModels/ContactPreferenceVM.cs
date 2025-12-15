namespace ClotheStore.Application.ViewModels
{
    public class ContactPreferenceVM
    {
        public Guid ContactPreferenceId { get; set; }

        public bool IsCommunicationByCall { get; set; }

        public bool IsCommunicationBySms { get; set; }

        public bool IsCommunicationByWhatsapp { get; set; }

        public bool IsCommunicationByEmail { get; set; }

        public bool IsPromotionByCall { get; set; }

        public bool IsPromotionBySms { get; set; }

        public bool IsPromotionByWhatsapp { get; set; }

        public bool IsPromotionByEmail { get; set; }

        public string Phone { get; set; }
    }
}

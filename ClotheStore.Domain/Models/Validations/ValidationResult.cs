namespace ClotheStore.Domain.Models.Validations
{
    public class ValidationResult
    {
        #region Properties
        public bool IsValid { get; set; }
        public string Message { get; set; }
        #endregion
    }
}
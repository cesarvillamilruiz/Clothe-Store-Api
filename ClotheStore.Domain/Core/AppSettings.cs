namespace ClotheStore.Domain.Core
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; } = new ConnectionStrings();
        public AzureStorage AzureStorage { get; set; } = new AzureStorage();
        public AzureAdB2C AzureAdB2C { get; set; } = new AzureAdB2C();
    }

    public class ConnectionStrings
    {
        public string Default { get; set; } = "";
    }

    public class AzureStorage
    {
        public string ConnectionString { get; set; }
    }

    public class AzureAdB2C
    {
        public string Instance { get; set; } = "";
        public string Domain { get; set; } = "";
        public string SignUpSignInPolicyId { get; set; } = "";
    }
}
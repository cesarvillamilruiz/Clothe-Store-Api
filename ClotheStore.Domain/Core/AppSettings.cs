namespace ClotheStore.Domain.Core
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; } = new ConnectionStrings();
        public AzureStorage AzureStorage { get; set; } = new AzureStorage();
    }

    public class ConnectionStrings
    {
        public string Default { get; set; } = "";
    }

    public class AzureStorage
    {
        public string ConnectionString { get; set; }
    }
}
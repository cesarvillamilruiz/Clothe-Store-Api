namespace ClotheStore.Domain.Core
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; } = new ConnectionStrings();
    }

    public class ConnectionStrings
    {
        public string Default { get; set; } = "";
    }
}
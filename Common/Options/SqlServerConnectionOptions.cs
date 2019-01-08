namespace SchoolSOA.Common.Options
{
    public class SqlServerConnectionOptions
    {
        public string Server { get; set; }

        public string Database { get; set; }

        public string UserId { get; set; }

        public string Password { get; set; }

        public string ConnectionString => $"Server='{Server}';Database='{Database}';User Id='{UserId}';Password='{Password}'";
    }
}
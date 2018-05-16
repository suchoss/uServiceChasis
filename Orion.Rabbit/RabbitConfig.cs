namespace Orion.Rabbit
{
    /// <summary>
    /// Used for parsing appsettings.json file.
    /// </summary>
    public class RabbitConfig
    {
        public string ConnectionString { get; set; }
        public string SubscriptionId { get; set; }
    }
}

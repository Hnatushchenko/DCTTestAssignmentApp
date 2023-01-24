namespace CoinGecko
{
    public class ApiEndPoints
    {
        public static readonly Uri BaseApiEndPoint = new Uri("https://api.coingecko.com/api/v3/");
        public const string CoinsList = "coins/list";
        public const string Ping = "ping";
        public const string CoinsMarkets = "coins/markets";
        public static string AllDataByCoinId(string id) => "coins/" + id; 
    }
}
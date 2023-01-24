namespace CoinGecko
{
    public class ApiEndPoints
    {
        public static readonly Uri BaseApiEndPoint = new Uri("https://api.coingecko.com/api/v3/");
        public static readonly Uri CoinsList = new Uri("coins/list");
        public static string AllDataByCoinId(string id) => "coins/" + id; 
    }
}
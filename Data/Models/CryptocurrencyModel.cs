using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace CryptoCurrencyDemoProject.Data.Models
{
    [BsonIgnoreExtraElements]
    public class CryptocurrencyModel
    {
        [BsonId]
        [BsonElement("Id")]
        public string Id { get; set; }
        [BsonElement("rank")]
        public int Rank { get; set; }
        [BsonElement("symbol")]
        public string Symbol { get; set; } = "Unknown";
        [BsonElement("name")]
        public string Name { get; set; } = "Unknown";
        [BsonElement("supply")] 
        public decimal? Supply { get; set; } = 0;
        [BsonElement("maxsupply")] 
        public decimal? MaxSupply { get; set; } = 0;
        [BsonElement("marketcapusd")] 
        public decimal? MarketCapUsd { get; set; } = 0;
        [BsonElement("volumeusd24hr")] 
        public decimal? VolumeUsd24Hr { get; set; } = 0;
        [BsonElement("priceusd")] 
        public decimal? PriceUsd { get; set; } = 0;
        [BsonElement("changepercent24hr")]
        public decimal? ChangePercent24Hr { get; set; } = 0;
        [BsonElement("vwap24hr")]
        public decimal? Vwap24Hr { get; set; } = 0;
    }
}

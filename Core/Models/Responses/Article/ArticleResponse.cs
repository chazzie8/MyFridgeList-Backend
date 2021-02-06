using System;

namespace MyFridgeListWebapi.Core.Models.Responses.Article
{
    public sealed class ArticleResponse
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
        public int Amount { get; set; }
        public DateTime Expirydate { get; set; }
        public DateTime Timestamp { get; set; }
        public string Expirystatus { get; set; }
    }
}

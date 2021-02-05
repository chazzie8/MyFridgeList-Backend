using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFridgeListWebapi.Core.Models.Responses.Article
{
    public sealed class EditArticleResponse
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
        public int Amount { get; set; }
        public DateTime Expirydate { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

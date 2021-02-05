using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFridgeListWebapi.Core.Models.Responses.DashboardArticle
{
    public sealed class DashboardArticleResponse
    {
        public Guid Id { get; set; }
        public Guid FridgeId { get; set; }
        public string ArticleName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Amount { get; set; }
    }
}

using System;
using System.Text.Json.Serialization;
using MediatR;
using MyFridgeListWebapi.Core.Models.Responses.Article;

namespace MyFridgeListWebapi.Application.Articles.Commands.Edit
{
    public sealed class EditArticleCommand : IRequest<EditArticleResponse>
    {
        public Guid UserId { get; set; }
        [JsonIgnore]
        public Guid FridgeId { get; set; }
        [JsonIgnore]
        public Guid ArticleId { get; set; }
        public string Label { get; set; }
        public int Amount { get; set; }
        public DateTime ExpiryDate { get; set; }
        [JsonIgnore]
        public DateTime Timestamp { get; set; }
    }
}

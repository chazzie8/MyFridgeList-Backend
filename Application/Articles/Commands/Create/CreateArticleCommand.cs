using System;
using System.Text.Json.Serialization;
using MediatR;
using MyFridgeListWebapi.Core.Models.Responses.Article;

namespace MyFridgeListWebapi.Application.Articles.Commands.Create
{
    public sealed class CreateArticleCommand : IRequest<CreateArticleResponse>
    {
        [JsonIgnore]
        public Guid FridgeId { get; set; }
        public string Label { get; set; }
        public int Amount { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}

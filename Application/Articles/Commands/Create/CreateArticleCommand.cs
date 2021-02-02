using MediatR;
using MyFridgeListWebapi.Core.Models.Responses.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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

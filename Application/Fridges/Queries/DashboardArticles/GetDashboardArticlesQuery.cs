using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using MediatR;
using MyFridgeListWebapi.Core.Models.Responses.DashboardArticle;

namespace MyFridgeListWebapi.Application.Fridges.Queries.DashboardArticles
{
    public sealed class GetDashboardArticlesQuery : IRequest<IEnumerable<DashboardArticleResponse>>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}

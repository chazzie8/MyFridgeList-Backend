﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyFridgeListWebapi.Application.Articles.Commands.Delete
{
    public sealed class DeleteArticleCommand
    {
        [JsonIgnore]
        public Guid FridgeId { get; set; }
        [JsonIgnore]
        public Guid ArticleId { get; set; }
    }
}

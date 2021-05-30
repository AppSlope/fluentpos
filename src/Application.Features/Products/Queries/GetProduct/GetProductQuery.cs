using FluentPOS.Application.Abstractions.Queries;
using FluentPOS.Shared.ViewModels.Products;
using MediatR;
using System;

namespace FluentPOS.Application.Features.Products.Queries.GetProduct
{
    public class GetProductQuery : IRequest<GetProductViewModel>, ICacheableQuery
    {
        public int Id { get; set; }
        public bool BypassCache { get; set; }
        public string CacheKey => $"{nameof(GetProductQuery)}-{Id}";
        public TimeSpan? SlidingExpiration { get; set; }
    }
}

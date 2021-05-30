using AutoMapper;
using FluentPOS.Application.Abstractions.Queries;
using FluentPOS.Domain.Entities;
using FluentPOS.Shared.ViewModels.Products;
using FluentPOS.Shared.Wrapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Application.Features.Products.Queries.GetProduct
{
    public class GetProductQuery : IRequest<Result<ProductViewModel>>, ICacheableQuery
    {
        public GetProductQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
        public bool BypassCache { get; set; }
        public string CacheKey => $"{nameof(GetProductQuery)}-{Id}";
        public TimeSpan? SlidingExpiration { get; set; }
    }
    internal class GetProductQueryHandler : IRequestHandler<GetProductQuery, Result<ProductViewModel>>
    {
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<Result<ProductViewModel>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            Product product = new() { Id = request.Id, Barcode = "MKS1994", Name="iPhone 11S", Price = 9999, Unit = "PC", LocaleName = "Dummy Data", ImageUrl = "someimage.jpg", Description = "Phone" };
            var data = _mapper.Map<ProductViewModel>(product);
            return Result<ProductViewModel>.Success(data);
        }
    }
}

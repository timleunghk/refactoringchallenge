using AutoMapper;
using RefactoringChallenge.Business.DTO;
using RefactoringChallenge.Data.Enities;

namespace RefactoringChallenge.Business.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderDetail, OrderDetailResponse>().ReverseMap();
            CreateMap<Order, OrderResponse>().ReverseMap();
        }
    }
}

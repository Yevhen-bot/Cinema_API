using AutoMapper;
using Cinema_API.DTOs;
using DataAccess.Entity;

namespace Cinema_API.Profiles
{
    public class AllProfiles : Profile
    {
        public AllProfiles() {
            CreateMap<Film, GetFilmModel>();
            CreateMap<CreateFilmModel, Film>();
            CreateMap<UpdateFilmModel, Film>();

            CreateMap<Hall, GetUpdateHallModel>().ReverseMap();
            CreateMap<CreateHallModel, Hall>();

            CreateMap<Session, GetUpdateSessionModel>().ReverseMap();
            CreateMap<CreateSessionModel, Session>();

            CreateMap<Ticket, GetUpdateTicketModel>().ReverseMap();
            CreateMap<CreateTicketModel, Ticket>();

            CreateMap<StatusSession, GetStatusSessionModel>();
            CreateMap<CreateStatusSessionModel, StatusSession>();

            CreateMap<StatusTicket, GetStatusTicketModel>();
            CreateMap<CreateStatusTicketModel, StatusTicket>();

            CreateMap<User, GetUpdateUserModel>().ReverseMap();
            CreateMap<CreateUserModel, User>();

            CreateMap<Sale, GetUpdateSaleModel>().ReverseMap();
            CreateMap<CreateSaleModel, Sale>();

            CreateMap<CreateDiscountModel, Discount>();
            CreateMap<Discount, GetDiscountModel>();

            CreateMap<CreateRegularDiscountModel, RegularDiscount>();
            CreateMap<RegularDiscount, GetRegularDiscountModel>();
        }
    }
}

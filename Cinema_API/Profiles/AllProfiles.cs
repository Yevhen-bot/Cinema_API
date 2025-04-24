using AutoMapper;
using Cinema_API.DTOs;
using Cinema_API.Models;
using DataAccess.Entity;

namespace Cinema_API.Profiles
{
    public class AllProfiles : Profile
    {
        public AllProfiles() {
            CreateMap<Film, GetFilmModel>();
            CreateMap<CreateFilmModel, Film>();

            CreateMap<Session, SessionModel>();
            CreateMap<SessionModel, Session>();

            CreateMap<Hall, HallModel>();
            CreateMap<HallModel, Hall>();

            CreateMap<Ticket, TicketModel>();
            CreateMap<TicketModel, Ticket>();

            CreateMap<StatusSession, StatusSessionModel>();
            CreateMap<StatusSessionModel, StatusSession>();

            CreateMap<StatusTicket, StatusTicketModel>();
            CreateMap<StatusTicketModel, StatusTicket>();

            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();

            CreateMap<Sale, SaleModel>();
            CreateMap<SaleModel, Sale>();

            CreateMap<Discount, DiscountModel>();
            CreateMap<DiscountModel, Discount>();

            CreateMap<RegularDiscount, RegularDiscountModel>();
            CreateMap<RegularDiscountModel, RegularDiscount>();
        }
    }
}

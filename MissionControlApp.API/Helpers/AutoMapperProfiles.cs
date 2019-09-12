using System.Linq;
using AutoMapper;
using MissionControlApp.API.Dtos;
using MissionControlApp.API.Models;

namespace MissionControlApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
                .ForMember(dest => dest.PhotoUrl, opt => {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(dest => dest.Age, opt => {
                    opt.MapFrom(d => d.DateOfBirth.CalculateAge());
                });
            CreateMap<User, UserForDetailedDto>()
                .ForMember(dest => dest.PhotoUrl, opt => {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(dest => dest.Age, opt => {
                    opt.MapFrom(d => d.DateOfBirth.CalculateAge());
                });

            CreateMap<MissionTeam, MissionTeamToReturnDto>()
                .ForMember(m => m.Username, opt => opt
                    .MapFrom(u => u.User.UserName))
                .ForMember(m => m.Gender, opt => opt
                    .MapFrom(u => u.User.Gender))
                .ForMember(m => m.Age, opt => opt
                    .MapFrom(u => u.User.DateOfBirth.CalculateAge()))
                .ForMember(m => m.KnownAs, opt => opt
                    .MapFrom(u => u.User.KnownAs))
                .ForMember(m => m.Employee, opt => opt
                    .MapFrom(u => u.User.Employee))
                .ForMember(m => m.JobTitle, opt => opt
                    .MapFrom(u => u.User.JobTitle))
                .ForMember(m => m.PhotoUrl, opt => opt
                    .MapFrom(u => u.User.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(m => m.Created, opt => opt
                    .MapFrom(u => u.User.Created))
                .ForMember(m => m.LastActive, opt => opt
                    .MapFrom(u => u.User.LastActive));
            CreateMap<Accelerator, MissionAcceleratorToReturnDto>();
            CreateMap<Platform, PlatformToReturnDto>();
            CreateMap<BusinessFunction, BusinessFunctionToReturnDto>();
            CreateMap<Industry, IndustryToReturnDto>();
            CreateMap<Mission, MissionToReturnDto>();
            CreateMap<MissionAccelerator, MissionAcceleratorToReturnDto>();
            CreateMap<MissionForCreateDto, Mission>();
            CreateMap<MissionAssessmentForCreateDto, MissionAssessment>();
            CreateMap<Photo, PhotosForDetailedDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<MissionAssessmentForUpdateDto, MissionAssessment>();
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<MessageForCreationDto, Message>().ReverseMap();
            CreateMap<Message, MessageToReturnDto>()
                .ForMember(m => m.SenderPhotoUrl, opt => opt
                    .MapFrom(u => u.Sender.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(m => m.RecipientPhotoUrl, opt => opt
                    .MapFrom(u => u.Recipient.Photos.FirstOrDefault(p => p.IsMain).Url));
        }
    }
}
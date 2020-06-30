using H2020.IPMDecisions.EML.Core.Dtos;
using H2020.IPMDecisions.EML.Core.Models;

namespace H2020.IPMDecisions.EML.Core.Profiles
{
    public class EmailingListContactProfile : MainProfile
    {
        public EmailingListContactProfile()
        {
            // Dtos to Models
            CreateMap<EmailingListContactDto, SendGridEmailingListContact>()
                .ForMember(dest => dest.Email,
                        opt => opt.MapFrom(src => src.Email.ToLower()))
                .ForMember(dest => dest.First_Name,
                    opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Last_Name,
                    opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore());
        }
    }
}
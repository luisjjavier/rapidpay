using API.Models;
using AutoMapper;
using Core.AppUsers;

namespace API.Profiles
{
    /// <summary>
    /// AutoMapper profile for mapping between <see cref="UserDto"/> and <see cref="AppUser"/>.
    /// </summary>
    public class UserProfiles : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfiles"/> class.
        /// </summary>
        public UserProfiles()
        {
            // Map CardDto to Card and vice versa
            CreateMap<UserDto, AppUser>().ReverseMap();
        }
    }

}

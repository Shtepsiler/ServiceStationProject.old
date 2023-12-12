using ServiceStation.BLL.DTO.Requests;
using ServiceStation.BLL.DTO.Responses;
using System.Threading.Tasks;

namespace ServiceStation.BLL.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<JwtResponse> SignInAsync(ClientSignInRequest request);

        Task<JwtResponse> SignUpAsync(ClientSignUpRequest request);


    //    Task SignUpWihtoutjvtAsync(ClientSignUpRequest request);
    }
}

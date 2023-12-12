using Application.DTOs.Requests;
using Application.DTOs.Respponces;
using Application.Operations.Managers.Commands;
using MediatR;

namespace Application.Interfaces
{
    public interface IIdentityService
    {
        IMediator Mediator { get; }

        Task<JwtResponse> SignInAsync(SingInCommand request);

        Task<JwtResponse> SignUpAsync(SingUpCommand request);

    }
}
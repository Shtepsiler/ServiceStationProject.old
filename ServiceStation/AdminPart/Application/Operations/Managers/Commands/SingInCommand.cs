using Application.DTOs.Respponces;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exeptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operations.Managers.Commands
{
    public class SingInCommand : IRequest<JwtResponse>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
    public class SingInHandler : IRequestHandler<SingInCommand, JwtResponse>
    {
        private readonly UserManager<Manager> userManager;
        private readonly ITokenService tokenService;
        public SingInHandler(IServiceStationDContext context, UserManager<Manager> userManager, ITokenService tokenService)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
        }



        async Task<JwtResponse> IRequestHandler<SingInCommand, JwtResponse>.Handle(SingInCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email)
          ?? throw new NotFoundException(
              $"{nameof(Manager)} with user email {request.Email} not found.");

            if (!await userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new NotFoundException("Incorrect username or password.");
            }


            var jwtToken = tokenService.BuildToken(user);
            return new() { Id = user.Id, Token = tokenService.SerializeToken(jwtToken) };




        }
    }

}

using Application.DTOs.Respponces;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Operations.Managers.Commands
{
    public class SingUpCommand : IRequest<JwtResponse>
    {
        public string UserName { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }



    }
    public class SingUpHandler : IRequestHandler<SingUpCommand, JwtResponse>
    {
        private readonly IServiceStationDContext context;
        private readonly UserManager<Manager> userManager;
        private readonly ITokenService tokenService;
        public SingUpHandler(IServiceStationDContext context, UserManager<Manager> userManager, ITokenService tokenService)
        {
            this.context = context;
            this.userManager = userManager;
            this.tokenService = tokenService;
        }



        async Task<JwtResponse> IRequestHandler<SingUpCommand, JwtResponse>.Handle(SingUpCommand request, CancellationToken cancellationToken)
        {

            Manager manager = new Manager()
            {
                UserName = request.UserName,
                Email = request.Email,
                FullName = request.FullName
            };



            var signUpResult = await userManager.CreateAsync(manager, request.Password);

            if (!signUpResult.Succeeded)
            {
                string errors = string.Join("\n",
                    signUpResult.Errors.Select(error => error.Description));

                throw new ArgumentException(errors);
            }

            await context.SaveChangesAsync();
            var newClient = await userManager.FindByNameAsync(request.UserName);



            var jwtToken = tokenService.BuildToken(manager);
            return new() { Id = manager.Id, Token = tokenService.SerializeToken(jwtToken) };




        }
    }

}

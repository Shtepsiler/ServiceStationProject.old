
using Application.Interfaces;
using Domain.Entities;
using Domain.Exeptions;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Operations.Jobs.Commands
{
    public record UpdateJobCommand : IRequest
    {
        public int Id { get; set; }
        public int? ManagerId { get; set; }
        public int ModelId { get; set; }
        public string? Status { get; set; }
        public int ClientId { get; set; }
        public int? MechanicId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
    }

    public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand>
    {
        private readonly IServiceStationDContext _context;

        public UpdateJobCommandHandler(IServiceStationDContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Jobs
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Job), request.Id);
            }

            entity.ManagerId = request.ManagerId;
            entity.ModelId = request.ModelId;
            entity.Status = request.Status;
            entity.ClientId = request.ClientId;
            entity.MechanicId = request.MechanicId;
            entity.IssueDate = request.IssueDate;
            entity.FinishDate = request.FinishDate;
            entity.Description = request.Description;
            entity.Price = request.Price;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
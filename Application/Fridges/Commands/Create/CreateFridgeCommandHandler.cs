using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Data.Entities;
using MyFridgeListWebapi.Core.Models.Responses.Fridge;

namespace MyFridgeListWebapi.Application.Fridges.Commands.Create
{
    public sealed class CreateFridgeCommandHandler : IRequestHandler<CreateFridgeCommand, CreateFridgeResponse>
    {
        private readonly DatabaseContext _dbContext;

        public CreateFridgeCommandHandler(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public async Task<CreateFridgeResponse> Handle(CreateFridgeCommand request, CancellationToken cancellationToken)
        {
            var fridge = new Fridge
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Name = request.Name,
            };

            _dbContext.Fridges.Add(fridge);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateFridgeResponse
            {
                Id = fridge.Id,
                Name = fridge.Name,
            };
        }
    }
}

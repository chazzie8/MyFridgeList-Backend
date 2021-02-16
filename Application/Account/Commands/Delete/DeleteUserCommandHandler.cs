using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Models.Responses.Account;

namespace MyFridgeListWebapi.Application.Account.Commands.Delete
{
    public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
    {
        private readonly DatabaseContext _dbContext;

        public DeleteUserCommandHandler(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }
        public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Where(x => x.Id == request.UserId)
                .FirstOrDefaultAsync();

            _dbContext.Users.Remove(user);

            await _dbContext.SaveChangesAsync();

            return new DeleteUserResponse();
        }
    }
}



using Domain.User;
using Domain.User.ValueObjects;

namespace Application.UseCaseUser.Create
{
    public record UserCreatedEvent
    {
        public UserId Id { get; set; }

        public FirstName UserName { get; set; } 


        public Email UserEmail { get; set; } 



    }
}


using Domain.Primitives;
using Domain.User.ValueObjects;
using Domain.Users;



namespace Domain.User
{
    public sealed class User : Entity
    {
        public UserId Id { get; private set; } 
        public FirstName FirstName { get; private set; }
        public LastName LastName { get; private set; }
        public Email Email { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public Address Address { get; private set; }


        private User()
        {

        }

        public static User Create(FirstName firstname, LastName lastName, Email email, PhoneNumber phoneNumber, Address address
        )
        {
            var user = new User
            {
                Id = new UserId(Guid.NewGuid()),
                FirstName = firstname,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                Address = address

            };
              

            user.Raise(new UserCreatedDomainEvent(user.Id));

            return user;
        }

    }
}

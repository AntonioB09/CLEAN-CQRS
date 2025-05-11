
using Domain.Primitives;
using Domain.User.ValueObjects;
using Domain.UserEvent;
using System.Xml.Linq;

namespace Domain.User;


    #region "Atribute user"
    public sealed class User : Entity
    {
        public UserId Id { get; private set; } 
        public FirstName FirstName { get; private set; }
        public LastName LastName { get; private set; }
        public Email Email { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public Address Address { get; private set; }
    #endregion

        private User()
        {

        }
    #region Factory User
    public static User Create(FirstName firstname, LastName lastName, 
            Email email, PhoneNumber phoneNumber, Address address)
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
              
            user.Raise(new UserCreatedDomainEvent(  user.Id, 
                                                    user.FirstName,
                                                    user.LastName,
                                                    user.Email,
                                                    user.PhoneNumber,
                                                    user.Address
                                                    ));

            return user;
        }
    #endregion

    #region Metodo Update user
    public void UpdateUser(UserId userId, FirstName newFirstname, LastName newLastName,
                               Email newEmail, PhoneNumber newPhoneNumber, Address newAddress)
        {   
            Id = userId;
            FirstName = newFirstname;
            LastName = newLastName;
            Email = newEmail;
            PhoneNumber = newPhoneNumber;
            Address = newAddress;

           Raise(new UserUpdatedDomainEvent(
                                      Id,
                                      FirstName,
                                      LastName,
                                      Email,
                                      PhoneNumber,
                                      Address
                                      ));

        }
    #endregion
}


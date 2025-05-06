using Domain.Shared;
using Domain.User.ValueObjects;
using Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.User;

    public sealed record UserUpdatedEvent(
    FirstName FirstName,
    LastName LastName,
    Email Email,
    PhoneNumber PhoneNumber,
    Address Address
    ) : IDomainEvent;




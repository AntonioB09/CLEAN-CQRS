using Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCaseUser;
public sealed class ReadUserNotFoundException : Exception
{
    public ReadUserNotFoundException(Guid userId)
        : base($"The user with the identifier {userId} was not found")
    {

    }
}

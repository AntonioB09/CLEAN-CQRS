
using Application.UseCaseUser.Create;
using Application.UseCaseUser.GetById;
using Domain.Shared;
using Domain.User;
using MediatR;


namespace CleanCQRS.Controllers;
public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/users", async (CreateUserCommand command, ISender sender) =>
        {
            Result<UserId> result = await sender.Send(command);

            return result.IsSuccess 
            ? Results.Ok(result.Value)
            : Results.BadRequest(result.Error.Description); 
        });


       
        app.MapGet("api/users/{userId}", async (Guid userId, ISender sender) =>
        {
            var query = new GetUserByIdQuery(new UserId(userId));

            Result<UserResponse> result = await sender.Send(query);

            return result.IsSuccess 
            ? Results.Ok(result.Value) 
            : Results.BadRequest(result.Error.Description);

        });
       

    }
}

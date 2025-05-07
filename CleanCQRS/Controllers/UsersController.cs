
using Application.UseCaseUser;
using Application.UseCaseUser.Create;
using Application.UseCaseUser.GetAll;
using Application.UseCaseUser.GetById;
using Application.UseCaseUser.Update;
using Domain.Shared;
using Domain.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;


namespace CleanCQRS.Controllers;
public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/user", async (CreateUserCommand command, ISender sender) =>
        {
            Result<UserId> result = await sender.Send(command);

            return result.IsSuccess 
            ? Results.Ok(result.Value)
            : Results.BadRequest(result.Error.Description); 
        });

        app.MapPut("api/updateUser/{userId}", async (Guid userId, [FromBody] UpdateUserRequest request, ISender sender) =>
        {
            var command = new UpdateUserCommand(
                          new UserId(userId),
                          request.FirstName,
                          request.LastName,
                          request.Email,
                          request.PhoneNumber,
                          request.Country,
                          request.State,
                          request.City,
                          request.Street,
                          request.ZipCode);

            Result<User> result = await sender.Send(command);

            return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.BadRequest(result.Error.Description);
        });

        app.MapGet("api/allUsers", async ( ISender sender) =>
        {

            var query = new GetAllUserQuery();
            Result<IReadOnlyList<UserResponse>> result = await sender.Send(query);

            return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.BadRequest(result.Error.Description);

        });

        app.MapGet("api/user/{userId}", async (Guid userId, ISender sender) =>
        {
            var query = new GetUserByIdQuery(userId);
            Result<UserResponse> result = await sender.Send(query);

            return result.IsSuccess
            ? Results.Ok(result.Value) 
            : Results.BadRequest(result.Error.Description);

        });



       

    }
}

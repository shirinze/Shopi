
using MediatR;
using Shopi.Application.ViewModels;

namespace Shopi.Application.Queries.User.GetById;

public record GetUserByIdQuery(int id):IRequest<UserViewModel>;

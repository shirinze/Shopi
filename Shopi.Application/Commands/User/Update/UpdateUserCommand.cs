using MediatR;

namespace Shopi.Application.Commands.User.Update;

public record UpdateUserCommand(int id,string name,string lastName,string phone):IRequest;

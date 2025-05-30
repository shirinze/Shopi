using MediatR;

namespace Shopi.Application.Commands.User.Create;

public record CreateUserCommand(string name,string lastName,string phone):IRequest;


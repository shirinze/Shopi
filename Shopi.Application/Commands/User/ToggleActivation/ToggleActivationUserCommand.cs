using MediatR;

namespace Shopi.Application.Commands.User.ToggleActivation;

public record ToggleActivationUserCommand(int Id):IRequest;

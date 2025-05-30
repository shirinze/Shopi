using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopi.Application.Commands.User.Delete;

public record DeleteUserCommand(int id):IRequest;


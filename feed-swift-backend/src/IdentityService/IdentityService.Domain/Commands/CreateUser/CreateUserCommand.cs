using IdentityService.Application.DTOs.User;
using IdentityService.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IdentityService.Domain.Commands.CreateUser;

public class CreateUserCommand: IRequest<bool>
{
    private readonly CreateUserDto _createUserDto;
    public CreateUserCommand(CreateUserDto createUserDto)
    {
        _createUserDto = createUserDto;
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        
        public CreateUserCommandHandler(IUserRepository userRepository,IUnitOfWork unitOfWork, ILogger<CreateUserCommandHandler> logger)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        
        
        public Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
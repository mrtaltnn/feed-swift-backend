using IdentityService.Application.DTOs.User;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Interfaces.Repositories;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using BC = BCrypt.Net.BCrypt;

namespace IdentityService.Application.Commands.CreateUser;

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
        private readonly IMapper _mapper;
        
        public CreateUserCommandHandler(IUserRepository userRepository,IUnitOfWork unitOfWork, ILogger<CreateUserCommandHandler> logger, IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        
        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userDto= request._createUserDto;

            if (await _userRepository.IsExistByEmailAsync(userDto.Email))
            {
                _logger.LogError("User already exists");
                return false;
            }

            var userEntity = _mapper.Map<User>(userDto);
            
            userEntity.SetPassword(BC.HashPassword(userDto.Password));
            
            await _userRepository.InsertAsync(userEntity);
            return await _unitOfWork.CommitAsync();
        }
    }
}
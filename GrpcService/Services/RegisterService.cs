using System.Threading.Tasks;
using BLL.Services;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using SignUp;

namespace GrpcService.Services
{
	public class RegisterService : Register.RegisterBase
	{
		private UserService userService;
		private readonly ILogger<RegisterService> _logger;

		public RegisterService(ILogger<RegisterService> logger)
		{
			userService = new UserService();
			_logger = logger;
		}

		public override Task<Correct> SendData(RegInfo request, ServerCallContext context)
		{
			return Task.FromResult(new Correct()
			{
				Correct_ = userService.CreateNewUser(new BLL.DTO.UserDTO()
				{
					Login = request.Login,
					Email = request.Email
				}, request.Password)
			});
		}

		public override Task<Correct> CheckLogin(Login request, ServerCallContext context)
		{
			return Task.FromResult(new Correct()
			{
				Correct_ = !userService.CheckLogin(request.Login_)
			});
		}
	}
}

using AutoMapper;
using BLL.Services;
using Grpc.Core;
using SignIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTO;

namespace GrpcService.Services
{
	public class LoginService : Loginer.LoginerBase
	{
		private UserService userService;
		private Mapper mapper;

		public LoginService()
		{
			userService = new UserService();
			mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, User>()));
		}

		public override Task<User> Login(LoginInfo request, ServerCallContext context)
		{
			return Task.Run(() =>
			{
				UserDTO userDTO = userService.LoginUser(request.Login, request.Password);
				if (userDTO == null)
					return new User();
				return mapper.Map<UserDTO, User>(userDTO);
			});
		}
	}
}

using AutoMapper;
using BLL.DTO;
using DAL;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
	public interface IUserService
    {
        bool CreateNewUser(UserDTO newUser, string pass);
        IEnumerable<UserDTO> GetAllUsers();
        UserDTO Find(int id);
        UserDTO Find(string login);
    }

    public class UserService : IUserService
    {
        private UnitOfWork repositories;
        private IMapper mapper;

        public UserService()
        {
            repositories = new UnitOfWork();
            IConfigurationProvider configuration = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<User, UserDTO>();
                    cfg.CreateMap<UserDTO,User>();
                });

            mapper = new Mapper(configuration);
        }

        public bool CreateNewUser(UserDTO newUser, string pass)
        {
			try
			{
                User user = mapper.Map<User>(newUser);
                user.Password = pass;
                return repositories.UserRepository.InsertCheck(user);
            }
			catch (System.Exception)
			{
                return false;
            }

        }

        public UserDTO Find(int id)
        {
            var result = repositories.UserRepository.GetByID(id);
            return mapper.Map<UserDTO>(result);
        }

        public UserDTO LoginUser(string login, string pass)
        {
            var result = repositories.UserRepository.Get(us => us.Login == login && us.Password == pass).FirstOrDefault();
            return mapper.Map<UserDTO>(result);
        }

        public bool CheckLogin(string login)
        {
            return repositories.UserRepository.Get(us => us.Login == login).FirstOrDefault() != null;
        }

        public UserDTO Find(string login)
        {
            var result = repositories.UserRepository.Get(us => us.Login == login).FirstOrDefault();
		
            return mapper.Map<UserDTO>(result);
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            var result = repositories.UserRepository.Get();
            return mapper.Map<IEnumerable<UserDTO>>(result);
        }
    }
}

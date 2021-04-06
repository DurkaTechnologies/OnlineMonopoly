using AutoMapper;
using BLL.DTO;
using DAL;
using System.Collections.Generic;

namespace BLL
{
	public interface IUserService
    {
        void CreateNewUser(UserDTO newUser);
        IEnumerable<UserDTO> GetAllUsers();
        UserDTO Find(int id);
        UserDTO Find(string login);
    }
    class UserService : IUserService
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

        public void CreateNewUser(UserDTO newUser)
        {
            repositories.UserRepository.Insert(mapper.Map<User>(newUser));
        }

        public UserDTO Find(int id)
        {
            var result = repositories.BranchRepository.GetByID(id);
            return mapper.Map<UserDTO>(result);
        }

        public UserDTO Find(string login)
        {
            var result = repositories.UserRepository.Get(us => us.Login == login);
            return mapper.Map<UserDTO>(result);
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            var result = repositories.UserRepository.Get();
            return mapper.Map<IEnumerable<UserDTO>>(result);
        }
    }
}

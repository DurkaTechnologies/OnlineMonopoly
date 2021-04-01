using AutoMapper;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public int Rating { get; set; }
        public int Money { get; set; }
    }
    public interface IUserService
    {
        void CreateNewUser(UserDTO newUser);
        IEnumerable<UserDTO> GetAllUsers();
    }
    class UserService : IUserService
    {
        UnitOfWork repositories;
        IMapper mapper;
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

        public IEnumerable<UserDTO> GetAllUsers()
        {
            var result = repositories.UserRepository.Get();
            return mapper.Map<IEnumerable<UserDTO>>(result);
        }
    }
}

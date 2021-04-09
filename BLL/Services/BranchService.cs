using AutoMapper;
using BLL.DTO;
using DAL;
using DAL.Entities;
using System.Collections.Generic;

namespace BLL.Services
{
	public interface IBranchService
    {
        void CreateNewBranch(BranchDTO newBranch);
        IEnumerable<BranchDTO> GetAllBranches();
        void DeleteBranch(BranchDTO branch);
        BranchDTO Find(int id);
        BranchDTO Find(string name);
    }

    public class BranchService : IBranchService
    {
        private UnitOfWork repositories;
        private IMapper mapper;

        public BranchService()
        {
            repositories = new UnitOfWork();
            IConfigurationProvider configuration = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<RentSetting, RentSettingsDTO>();
                    cfg.CreateMap<Branch, BranchDTO>();

                    cfg.CreateMap<RentSettingsDTO, RentSetting>();
                    cfg.CreateMap<BranchDTO, Branch>();
                });

            mapper = new Mapper(configuration);
        }

        public void CreateNewBranch(BranchDTO newBranch)
        {
            repositories.BranchRepository.Insert(mapper.Map<Branch>(newBranch));
        }

        public void DeleteBranch(BranchDTO newBranch)
        {
            repositories.BranchRepository.Delete(mapper.Map<Branch>(newBranch));
        }

        public BranchDTO Find(int id)
        {
            var result = repositories.BranchRepository.GetByID(id);
            return mapper.Map<BranchDTO>(result);
        }

        public BranchDTO Find(string name)
        {
            var result = repositories.BranchRepository.Get(br => br.Name == name);
            return mapper.Map<BranchDTO>(result);
        }

        public IEnumerable<BranchDTO> GetAllBranches()
        {
            var result = repositories.BranchRepository.Get();
            return mapper.Map<IEnumerable<BranchDTO>>(result);
        }
    }
}

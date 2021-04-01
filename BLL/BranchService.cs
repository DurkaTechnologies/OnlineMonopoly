using AutoMapper;
using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BranchDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int pledge { get; set; }
        public int Buyout { get; set; }
        public int Price { get; set; }
        public int Upgrade { get; set; }
        public virtual RentSettingsDTO RentSetting { get; set; }
    }
    public class RentSettingsDTO
    {
        public int Id { get; set; }
        public int StartCost { get; set; }
        public float FirstCoef { get; set; }
        public int SecondCoef { get; set; }
        public bool BybranchQuantity { get; set; }
    }
    public interface IBranchService
    {
        void CreateNewBranch(BranchDTO newUser);
        IEnumerable<BranchDTO> GetAllBranches();
    }
    class BranchService : IBranchService
    {
        UnitOfWork repositories;
        IMapper mapper;
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

        public IEnumerable<BranchDTO> GetAllBranches()
        {
            var result = repositories.BranchRepository.Get();
            return mapper.Map<IEnumerable<BranchDTO>>(result);
        }
    }
}

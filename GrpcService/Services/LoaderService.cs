using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using GameLoader;
using BLL.Services;
using Google.Protobuf.WellKnownTypes;
using BLL.DTO;
using System.Collections.ObjectModel;
using AutoMapper;
using Google.Protobuf.Collections;
using static GameLoader.Branch.Types;

namespace GrpcService.Services
{
	public class LoaderService : Loader.LoaderBase
	{
		private BranchService branchService;
		private Mapper mapper;

		public LoaderService()
		{
			branchService = new BranchService();

			IConfigurationProvider configuration = new MapperConfiguration(
			
			cfg =>
			{
				cfg.CreateMap<RentSetting, RentSettingDTO>();
				cfg.CreateMap<BranchType, BranchTypeDTO>();
				cfg.CreateMap<Branch, BranchDTO>();

				cfg.CreateMap<RentSettingDTO, RentSetting>();
				cfg.CreateMap<BranchTypeDTO, BranchType>();
				cfg.CreateMap<BranchDTO, Branch>();
			});

			mapper = new Mapper(configuration);
		}

		public override Task<Branches> LoadBranches(Empty e, ServerCallContext context)
		{
			return Task.Run(() =>
			{
				IEnumerable<BranchDTO> banchesDTO = branchService.GetAllBranches();
				Branches branches = new Branches();

				foreach (var item in banchesDTO)
					branches.Collection.Add(mapper.Map<BranchDTO, Branch>(item));

				return branches;
			});
		}
	}
}

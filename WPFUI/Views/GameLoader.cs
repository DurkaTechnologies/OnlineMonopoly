using AutoMapper;
using BLL.DTO;
using GameLoader;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using static GameLoader.Branch.Types;

namespace WPFUI.Views
{
	static class GamePageLoader
	{
		static HttpClientHandler httpHandler;
		static GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:5001");
		static Loader.LoaderClient client;
		static Mapper mapper;

		static GamePageLoader()
		{
			httpHandler = new HttpClientHandler();

			httpHandler.ServerCertificateCustomValidationCallback =
					HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

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

		public static ICollection<BranchDTO> GetBranches()
		{
			Connect();

			Branches branches = client.LoadBranches(new Empty());
			ICollection<BranchDTO> collection = new Collection<BranchDTO>();

			foreach (var item in branches.Collection)
				collection.Add(mapper.Map<Branch, BranchDTO>(item));

			Close();

			return collection;
		}

		private static void Connect()
		{
			channel = GrpcChannel.ForAddress("https://localhost:5001",
					new GrpcChannelOptions { HttpHandler = httpHandler });

			client = new Loader.LoaderClient(channel);
		}

		private static void Close()
		{
			channel.ShutdownAsync();
			client = null;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandsClassLibrary
{
	[Serializable]
	public enum CommandType
	{
		UserData,
		GameData
	}

	[Serializable]
	public enum UserCommandType
	{
		Connect,
		Disconnect
	}

	[Serializable]
	public enum GameActType
	{
		None,
		Move,
		OpenAuction,
		DropGameDices,
		BuyBranch,
		UpgradeBranch,
		Chance,
		MoveToJail,
		ExitFromJail,
		OpenOrder,
		SellUpgrade,
		BranchPledge,
		BuyOutBranch,
		TakeLoan,
		Surrender
	}

	public interface IClientCommand
	{
		CommandType CommandType { get; set; }
	}

	public interface IUserCommand
	{
		UserCommandType UserCommandType { get; set; }
	}

	public interface IGameActCommand
	{
		GameActType GameActType { get; set; }
	}
}

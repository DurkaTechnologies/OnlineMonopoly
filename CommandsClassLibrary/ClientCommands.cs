using CommandsClassLibrary.CommandViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandsClassLibrary
{
	///
	/// Класи, який містить інформацію, яка передається від клієнта на сервер //
	///

	/// Used for send connect or disconnect data to server
	[Serializable]
	public class ClientUserDataCommand : IClientCommand, IUserCommand
	{
		public ClientUserDataCommand()
		{

		}

		public ClientUserDataCommand(string login, string pass)
		{
			CommandType = CommandType.UserData;
			Login = login;
			Password = pass;
			UserCommandType = UserCommandType.Connect;
		}

		public ClientUserDataCommand(string login)
		{
			CommandType = CommandType.UserData;
			Login = login;
			UserCommandType = UserCommandType.Disconnect;
		}

		public string Login { get; set; }

		public string Password { get; set; }

		public CommandType CommandType { get; set; }

		public UserCommandType UserCommandType { get; set; }
	}

	/// Summary:
	///		Base class for game commands
	///		
	///		Use for send commands: 
	///		Drop game dices
	///		Surrender
	///		Move To Jail
	///		Exit From Jail
	///		Take Loan
	[Serializable]
	public class ClientGameCommand : IClientCommand, IGameActCommand
	{
		public ClientGameCommand()
		{
			CommandType = CommandType.GameData;
			UserId = 0;
		}

		public ClientGameCommand(int userId, GameActType gameActType = GameActType.None)
		{
			CommandType = CommandType.GameData;
			GameActType = gameActType;
			UserId = userId;
		}

		public CommandType CommandType { get; set; }

		public GameActType GameActType { get; set; }

		public int UserId { get; set; }
	}

	[Serializable]
	public class ClientMoveActCommand : ClientGameCommand
	{
		public ClientMoveActCommand() : base()
		{
			GameActType = GameActType.Move;
		}

		public ClientMoveActCommand(int userId, int moveValue) : base(userId, GameActType.Move)
		{
			MoveValue = moveValue;
		}

		public int MoveValue { get; set; }
	}

	[Serializable]
	public class ClientOpenAuctionCommand : ClientGameCommand
	{
		public ClientOpenAuctionCommand() : base()
		{
			GameActType = GameActType.OpenAuction;
		}

		public ClientOpenAuctionCommand(int userId, int branchId, int cost) : base(userId, GameActType.OpenAuction)
		{
			BranchId = branchId;
			Cost = cost;
		}

		public int BranchId { get; set; }
		public int Cost { get; set; }
	}

	///		Use for send commands: 
	///		Buy Branch
	///		Upgrade Branch
	///		Sell Upgrade
	///		Branch Pledge
	///		Buy Out Branch
	[Serializable]
	public class ClientBranchCommandAct : ClientGameCommand
	{
		public ClientBranchCommandAct() : base()
		{
			GameActType = GameActType.BuyBranch;
		}

		public ClientBranchCommandAct(int userId, int branchId, GameActType gameActType) : base(userId, gameActType)
		{
			BranchId = branchId;
		}

		public int BranchId { get; set; }
	}
	
	[Serializable]
	public class ClientChanceCommand : ClientGameCommand
	{
		public ClientChanceCommand() : base()
		{
			GameActType = GameActType.Chance;
		}

		public ClientChanceCommand(int userId, int chanceId) : base(userId, GameActType.Chance)
		{
			ChanceId = chanceId;
		}

		public int ChanceId { get; set; }
	}

	[Serializable]
	public class ClientOpenOrderCommand : ClientGameCommand
	{
		public ClientOpenOrderCommand() : base()
		{
			GameActType = GameActType.OpenOrder;
		}

		public ClientOpenOrderCommand(int userId, Order userOrder, Order opponentOrder) : base(userId, GameActType.OpenOrder)
		{
			UserOrder = userOrder;
			OpponentOrder = opponentOrder;
		}

		public Order UserOrder { get; set; }

		public Order OpponentOrder { get; set; }
	}
}

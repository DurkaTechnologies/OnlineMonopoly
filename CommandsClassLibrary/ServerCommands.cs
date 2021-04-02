using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandsClassLibrary
{
	///
	/// класи, який містить інформацію, яка передається від сервера на клієнт
	/// 


	[Serializable]
	public class ServerUserDataCommand : IClientCommand
	{

		public ServerUserDataCommand()
		{

		}

		public ServerUserDataCommand(string login)
		{
			Login = login;
			CommandType = CommandType.UserData;
		}
		public string Login { get; set; }

		public CommandType CommandType { get; set; }

		//public IEnumerable<PlayerDTO> { get; set; }
	}

	[Serializable]
	public class ServerGameCommand : IClientCommand, IGameActCommand
	{

		public ServerGameCommand()
		{
			CommandType = CommandType.GameData;
			UserId = 0;
		}

		public ServerGameCommand(int userId, GameActType gameActType = GameActType.None)
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
	public class ServerDropDicesCommand : ServerGameCommand
	{
		public ServerDropDicesCommand() : base()
		{
			GameActType = GameActType.DropGameDices;
		}

		public ServerDropDicesCommand(int userId, int firts, int second) : base(userId, GameActType.DropGameDices)
		{
			First = firts;
			Second = second;
		}

		public int First { get; set; }
		public int Second { get; set; }
	}
}

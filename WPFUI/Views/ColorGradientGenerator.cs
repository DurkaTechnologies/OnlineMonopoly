using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xaml;

namespace WPFUI.Views
{
	static class ColorManager
	{
		#region Fields

		public static int ColorMax = 255;

		private static List<Color> colors;
		private static List<Color> secondColors;

		#endregion

		static ColorManager()
		{
			colors = GetColorsFromServer("http://durkaftpserver.cf/Resources/ColorsLib/playerColors.txt");
			secondColors = GetColorsFromServer("http://durkaftpserver.cf/Resources/ColorsLib/branchColors.txt");

			//colors.Add(GetColorFromHtml("#161a1b"));
			//colors.Add(GetColorFromHtml("#cd3847"));
			//colors.Add(GetColorFromHtml("#2494e2"));
			//colors.Add(GetColorFromHtml("#8bcb5a"));
			//colors.Add(GetColorFromHtml("#8f59b5"));
			//colors.Add(GetColorFromHtml("#bf84e8"));

			//secondColors.Add(GetColorFromHtml("#ec87c1"));
			//secondColors.Add(GetColorFromHtml("#da4553"));
			//secondColors.Add(GetColorFromHtml("#e0b439"));
			//secondColors.Add(GetColorFromHtml("#37bc9d"));
			//secondColors.Add(GetColorFromHtml("#7f1f0f"));
			//secondColors.Add(GetColorFromHtml("#4b89dc"));
			//secondColors.Add(GetColorFromHtml("#8cc152"));
			//secondColors.Add(GetColorFromHtml("#4fc1e9"));
			//secondColors.Add(GetColorFromHtml("#7f1f0f"));
			//secondColors.Add(GetColorFromHtml("#656d78"));

			//UploadFTPFile("dice.png");
		}

		//public static void UploadFTPFile(string fname)
		//{
		//	FileInfo fileInf = new FileInfo(fname);
		//	string uri = "http://durkaftpserver.cf/Resources/ColorsLib/" + fileInf.Name;
		//	FtpWebRequest reqFTP;

		//	// Create FtpWebRequest object from the Uri provided
		//	reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(
		//		 "ftp://" + "ipaddress//Data//" + fileInf.Name));

		//	// Provide the WebPermission Credintials
		//	reqFTP.Credentials = new NetworkCredential("username",
		//										  "password");

		//	// By default KeepAlive is true, where the control connection is 
		//	// not closed after a command is executed.
		//	reqFTP.KeepAlive = false;

		//	// Specify the command to be executed.
		//	reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

		//	// Specify the data transfer type.
		//	reqFTP.UseBinary = true;

		//	Image img 

		//	// Notify the server about the size of the uploaded file
		//	//reqFTP.ContentLength = fileInf.Length; ???
		//	using (var img = Image.FromStream(image))
		//	{
		//		img.Save(adduser.User_Id + ".jpg", ImageFormat.Jpeg);
		//	}
		//}

		public static List<Color> GetColorsFromServer(string filePass)
		{
			List<Color> colors = new List<Color>();

			string file = "";
			WebClient request = new WebClient();
			string url = filePass;

			try
			{
				byte[] newFileData = request.DownloadData(url);
				file = System.Text.Encoding.UTF8.GetString(newFileData);

				//string zalypa = "zalypa";
				//newFileData = System.Text.Encoding.UTF8.GetBytes(zalypa);

				//request.Credentials = new NetworkCredential(@"durkaftpserver\$durkaftpserver", "vl4EoeKt0np7yy3uLJXSlYk7QFGBdlilaeYt1Kh9czaJQCXDyjaikGfryNi1");
				//request = FtpWebRequest.Create("http://durkaftpserver.cf/Resources/ColorsLib/dice.png");
				//request.UploadFile("http://durkaftpserver.cf/Resources/ColorsLib/dice.png", "../../../../dice.png");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

			if (String.IsNullOrEmpty(file))
				return null;

			List<JsonColor> jColors = JsonConvert.DeserializeObject<List<JsonColor>>(file);

			foreach (JsonColor item in jColors)
				colors.Add(GetColorFromHtml(item.Value));

			return colors;
		}

		#region Methods

		public static Color GetColorFromHtml(string htmlColor)
		{
			System.Drawing.Color color = System.Drawing.ColorTranslator.FromHtml(htmlColor);
			return Color.FromArgb(color.A, color.R, color.G, color.B);
		}

		public static Color GetColorFromLib(int id = 0)
		{
			if (id < 0 || id > colors.Count - 1)
				return colors[0];

			return colors[id];
		}

		public static Color GetSecondColorFromLib(int id = 0)
		{
			if (id < 0 || id > secondColors.Count - 1)
				return secondColors[0];

			return secondColors[id];
		}

		public static SolidColorBrush GetColorBrushFromLib(int id = 0)
		{
			return new SolidColorBrush(GetColorFromLib(id));
		}

		public static SolidColorBrush GetSecondBrushFromLib(int id = 0)
		{
			return new SolidColorBrush(GetSecondColorFromLib(id));
		}

		public static SolidColorBrush GenerateDarkerColor(Color color)
		{
			Color newColor = new Color();

			newColor.A = (byte)(ColorMax);
			newColor.R = (byte)(color.R - 10);
			newColor.G = (byte)(color.G - 10);
			newColor.B = (byte)(color.B - 10);

			return new SolidColorBrush(newColor);
		}

		public static LinearGradientBrush GenerateGradient(Color color, double angle)
		{
			GradientStop first = new GradientStop() { Offset = 0 };
			GradientStop second = new GradientStop() { Offset = 1 };

			Color firstColor = color;
			Color secondColor = new Color();

			secondColor.A = (byte)(255);
			secondColor.R = (byte)(color.R + 35);
			secondColor.G = (byte)(color.G + 48);
			secondColor.B = (byte)(color.B + 25);

			first.Color = firstColor;
			second.Color = secondColor;

			return new LinearGradientBrush(
						   new GradientStopCollection(
						   new List<GradientStop>()
						   { first, second }), angle);
		}

		#endregion
	}

	public class JsonColor
	{
		[JsonProperty("value")]
		public string Value { get; set; }
	}
}

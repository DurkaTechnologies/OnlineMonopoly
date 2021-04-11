using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UIWPF.Commands;

namespace WPFUI.UserControls
{
	/// <summary>
	/// Interaction logic for OfferControl.xaml
	/// </summary>
	public partial class OfferControl : UserControl, INotifyPropertyChanged
	{
		#region Fields

		private UserDTO leftUser;
		private UserDTO rightUser;

		private ICollection<BranchDTO> leftUserBranches = new ObservableCollection<BranchDTO>();
		private ICollection<BranchDTO> rightUserBranches = new ObservableCollection<BranchDTO>();

		private int leftUserMoney;
		private int rightUserMoney;

		private int allRightMoney;
		private int allLeftMoney;

		private Command confirmOfferCommand;

		#endregion

		#region Proporties

		public ICommand ConfirmOfferCommand => confirmOfferCommand;

		public UserDTO LeftUser
		{
			get => leftUser;
			set
			{
				leftUser = value;
				OnPropertyChanged();
			}
		}

		public UserDTO RightUser
		{
			get => rightUser;
			set
			{
				rightUser = value;
				OnPropertyChanged();
			}
		}

		public IEnumerable<BranchDTO> LeftUserBranches => leftUserBranches;
		public IEnumerable<BranchDTO> RightUserBranches => rightUserBranches;

		public int LeftUserMoney
		{
			get => leftUserMoney;
			set
			{
				leftUserMoney = value;
				OnPropertyChanged();
			}
		}

		public int RightUserMoney
		{
			get => rightUserMoney;
			set
			{
				rightUserMoney = value;
				OnPropertyChanged();
			}
		}


		public int AllLeftMoney
		{
			get => allLeftMoney;
			set
			{
				allLeftMoney = value;
				OnPropertyChanged();
			}
		}

		public int AllRightMoney
		{
			get => allRightMoney;
			set
			{
				allRightMoney = value;
				OnPropertyChanged();
			}
		}

		#endregion

		public OfferControl()
		{
			InitializeComponent();
			InitializePropertyChanged();
			LayoutRoot.DataContext = this;
			confirmOfferCommand = new DelegateCommand(ConfirmOffer, ConfirmCanExectute);

			LeftUser = new UserDTO() { Login = "Shiza", Image = "https://cdn.discordapp.com/attachments/821379755743903764/830777911350394890/unknown.png" };
			RightUser = new UserDTO() { Login = "Onizuka", Image = "https://cdn.discordapp.com/attachments/821379755743903764/830045208569839616/GTO.png" };

			AddLeftUserElement(new BranchDTO() { Name = "SoftServe", Price = 666, Image = "pack://application:,,,/Resources/IT/softserve.png" });
			AddLeftUserElement(new BranchDTO() { Name = "Step", Price = 666, Image = "pack://application:,,,/Resources/IT/step.png" });

			AddRightUserElement(new BranchDTO() { Name = "Riven", Price = 666, Image = "pack://application:,,,/Resources/Drinks/riven.png" });

		}

		#region Methods

		private bool ConfirmCanExectute()
		{
			if (AllLeftMoney == AllRightMoney)
				return true;

			return false;
		}

		public void ConfirmOffer()
		{
			Offer offer = new Offer();

			offer.LeftUserMoney = this.LeftUserMoney;
			offer.RightUserMoney = this.RightUserMoney;
			offer.LeftUserBranhces = this.LeftUserBranches;
			offer.RightUserBranhces = this.RightUserBranches;
		}

		public void AddLeftUserElement(BranchDTO branch)
		{
			leftUserBranches.Add(branch);
			UpdateAllLeftMoney();
		}

		public void AddRightUserElement(BranchDTO branch)
		{
			rightUserBranches.Add(branch);
			UpdateAllRightMoney();
		}

		#endregion

		#region INotify

		private void InitializePropertyChanged()
		{
			PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName.Equals(nameof(LeftUserMoney)))
					UpdateAllLeftMoney();

				if (args.PropertyName.Equals(nameof(RightUserMoney)))
					UpdateAllRightMoney();

				if (args.PropertyName.Equals(nameof(AllLeftMoney)) ||
					args.PropertyName.Equals(nameof(AllRightMoney)))
					confirmOfferCommand.RaiseCanExecuteChanged();
			};
		}

		private void UpdateAllLeftMoney()
		{
			AllLeftMoney = LeftUserMoney;

			foreach (var item in leftUserBranches)
				AllLeftMoney += item.Price;
		}

		private void UpdateAllRightMoney()
		{
			AllRightMoney = RightUserMoney;

			foreach (var item in rightUserBranches)
				AllRightMoney += item.Price;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		#endregion
	}

	public struct Offer
	{
		public int LeftUserMoney { get; set; }
		public int RightUserMoney { get; set; }

		public IEnumerable<BranchDTO> LeftUserBranhces { get; set; }
		public IEnumerable<BranchDTO> RightUserBranhces { get; set; }
	}
}

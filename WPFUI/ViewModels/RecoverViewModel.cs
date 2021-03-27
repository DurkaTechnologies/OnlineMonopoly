using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIWPF.Commands;
using UIWPF.ViewModels;

namespace WPFUI.ViewModels
{
    class RecoverViewModel : BaseViewModel
    {
        public RecoverViewModel()
        {
            InitializeCommands();
        }
        private string mail;
        private Command recoverCommand;
        private Command checkCommand;
        private static RecoverViewModel instance;

        string server = "smtp.gmail.com"; // sets the server address
        int port = 587; //sets the server port
        private string key = "";
        public static RecoverViewModel GetInstance()
        {
            if (instance == null)
                instance = new RecoverViewModel();
            return instance;
        }
        public string Mail
        {
            get => mail;
            set
            { 
                mail = value;
                OnPropertyChanged();
            }
        }
        private string text= "E-Mail";
        public string Text
        {
            get => text;
            set 
            {
                text = value;
                OnPropertyChanged();
            }
        }
        private void InitializeCommands()
        {
            recoverCommand = new DelegateCommand(Recover, ()=>true);
            checkCommand = new DelegateCommand(Check, () => true);
        }
        public ICommand RecoverCommand => recoverCommand;
        public ICommand CheckCommand => checkCommand;

        private void Recover()
        {
            Random random = new Random();
            for (int i = 0; i < 10; i++)
                key += (Char)random.Next(33, 100);
            MailMessage message = new MailMessage("prodoq@gmail.com", mail, "recover Password", "Hello, u can recover your password, entered your key: "+key);
            message.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient(server, port);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("prodoq@gmail.com", "r4e3w2q1");
            client.SendAsync(message, "blabla");
            Navigation.Navigation.Navigate(Navigation.Navigation.SecondRecoverPageAlies, null);
            Text = "Enter key";
            Mail = "";
        }
        private void Check()
        {
            if (key == Mail)
                Text = "Complited";
            else
                Text = "False";
        }
    }
}

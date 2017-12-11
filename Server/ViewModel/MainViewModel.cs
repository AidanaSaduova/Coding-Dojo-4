using GalaSoft.MvvmLight;
using CD4_Server.Communication;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System;
using Server;

namespace CD4_Server.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private Communication.Server server;
        private const int port = 10100;
        private const string ip = "127.0.0.1";
        private bool isConnected = false;
        // private DataHandler dHandler;

        // properties
        public RelayCommand StartBtnClickCmd { get; set; }
        public RelayCommand StopBtnClickCmd { get; set; }
        public RelayCommand DropClientBtnClickCmd { get; set; }
        public RelayCommand SaveToLogBtnClickCmd { get; set; }
        public ObservableCollection<string> Users { get; set; }
        public ObservableCollection<string> Messages { get; set; }
        public string SelectedUser { get; set; }
        public int NoOfReceivedMessages
        {
            get { return Messages.Count; }

        }
        public MainViewModel()
        {
            Messages = new ObservableCollection<string>();
            Users = new ObservableCollection<string>();

            StartBtnClickCmd = new RelayCommand(
                () =>
                {
                    server = new Communication.Server(ip, port, UpdateGuiWithNewMessage);
                    server.StartAccepting();
                    isConnected = true;
                },
                () => { return !isConnected; });

            //set command for stop button
            StopBtnClickCmd = new RelayCommand(
                //action for execute
                () =>
                {
                    server.StopAccepting();
                    isConnected = false;
                },
                () => { return isConnected; });

            //init command for drop button with can execute statement
            DropClientBtnClickCmd = new RelayCommand(
                () =>
                {
                    server.DisconnectSpecificClient(SelectedUser);
                    //remove from gui listbox
                    Users.Remove(SelectedUser);
                },
                () => { return (SelectedUser != null); });
        }




        private void UpdateGuiWithNewMessage(string message)
        {
            //switch thread to gui to write to gui
            App.Current.Dispatcher.Invoke(() =>
            {
                string name = message.Split(':')[0];
                if (!Users.Contains(name))
                {//not in list=>add it
                    Users.Add(name);
                }
                //writes message
                Messages.Add(message);
                // do this to inform the gui update of the received message counter!
                RaisePropertyChanged("NoOfReceivedMesssages");
            });



        }
    }
}
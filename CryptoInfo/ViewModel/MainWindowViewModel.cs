using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CryptoInfo.Infrastructure.Commands;
using CryptoInfo.ViewModel.Base;

namespace CryptoInfo.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region Window header

        /// <summary>Title of window</summary>
        private string _Title = "Top 10 cryptocurrencies"; 

        /// <summary>Title of window</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        #region Programm status

        /// <summary>Program status</summary>
        private string _Status = "Well done";

        /// <summary>Program status</summary>
        public string Status
        {
            get => _Status; 
            set => Set(ref _Status, value);
        }

        #endregion

        #region Commands

        #region CloseApplicationCommand

        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecute(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        #endregion

        #endregion
        public MainWindowViewModel()
        {
            #region Commands

            CloseApplicationCommand =
                new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);



            #endregion
        }
    }
}

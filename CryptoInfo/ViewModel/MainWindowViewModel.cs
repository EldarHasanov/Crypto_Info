using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Input;
using CryptoInfo.Infrastructure.Commands;
using CryptoInfo.Models;
using CryptoInfo.ViewModel.Base;
using System.Text.Json;
using System.Windows.Media;
using CryptoInfo.DAL;
using CryptoInfo.Infrastructure.Commands.Base;

namespace CryptoInfo.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase, ICryptoList
    {
        private const string coincap_assets_url = @"http://api.coincap.io/v2/assets";
        public List<Cryptocurrency> Cryptocurrencies { get; set; }

        #region Title

        private object _title = "CryptoInfo";

        public object Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public HomeViewModel HomeVM
        {
            get; 
            set;
        }

        public InfoWindowView InfoVM
        {
            get; 
            set;
        }

        #region Current View

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
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

        



        private bool CanHomeViewCommand(object p) => true;
        private void OnHomeViewCommand(object p)
        {
            CurrentView = HomeVM;
        }

        private bool CanInfoViewCommand(object p) => true;
        private void OnInfoViewCommand(object p)
        {
            InfoVM = new InfoWindowView();
            CurrentView = InfoVM;
        }
        #endregion

        #region ChangeView

        public BaseCommand HomeViewCommand { get; set; }

        public BaseCommand InfoViewCommand { get; set; }

        #endregion

        #endregion
        public MainWindowViewModel()
        {
            #region Commands

            CloseApplicationCommand =
                new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);


            HomeViewCommand =
                new LambdaCommand(OnHomeViewCommand, CanHomeViewCommand);

            InfoViewCommand = 
                new LambdaCommand(OnInfoViewCommand, CanInfoViewCommand);

            


            #endregion

            #region HTTP requests

            Cryptocurrencies = GetCryptocurrenciesList();


            #endregion

            #region Home View

            HomeVM = new HomeViewModel();
                                                                                                
            InfoVM = new InfoWindowView();
            CurrentView = HomeVM;



            #endregion
        }

        public List<Cryptocurrency> GetCryptocurrenciesList()
        {
            try
            {
                var client = new HttpClient();
                if (client != null)
                {
                    var responseMessage = client.GetAsync(coincap_assets_url).Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var jsonStringResult = responseMessage.Content.ReadAsStringAsync().Result;

                        AssetsRootObjectWithList rootobject = JsonSerializer.Deserialize<AssetsRootObjectWithList>(jsonStringResult);

                         var Crypt = rootobject.data.ToList();
                         return Crypt;
                    }
                    else
                    {
                        throw new FieldAccessException(responseMessage.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            throw new NotImplementedException();
        }
    }
}

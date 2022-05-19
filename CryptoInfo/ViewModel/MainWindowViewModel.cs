using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public HomeViewModel HomeVM { get; set; }

        public InfoWindowView InfoVM { get; set; }

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

        #region Window header

        /// <summary>Title of window</summary>
        private string _Title = "CryptInfo"; 

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

            HomeViewCommand = new LambdaCommand(o =>
            {
                CurrentView = HomeVM;
            });

            InfoViewCommand = new LambdaCommand(o =>
            {
                CurrentView = InfoVM;
            });

            #endregion

            #region HTTP requests

            Cryptocurrencies = GetCryptocurrenciesList();


            #endregion

            #region Home View

            HomeVM = new HomeViewModel();
            InfoVM = new InfoWindowView("bitcoin");
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
                        throw new FieldAccessException("Something wrong with url");
                    }
                }
            }
            catch (Exception e)
            {
                _Status = e.Message;
            }
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Input;
using CryptoInfo.DAL;
using CryptoInfo.Infrastructure.Commands;
using CryptoInfo.Models;
using CryptoInfo.ViewModel.Base;

namespace CryptoInfo.ViewModel
{
    internal class InfoWindowView : ViewModelBase, ICryptoList
    {
        private const string coincap_assets_url = @"http://api.coincap.io/v2/assets";

        private Cryptocurrency _cryptocurrencie;

        public Cryptocurrency Cryptocurrencie 
        {
            get { return _cryptocurrencie; }
            set
            {
                _cryptocurrencie = value;
                _cryptocurrencie.priceUsd = value.priceUsd.Substring(0, 8) + " USD";
                OnPropertyChanged();
            }
        }

        #region Crypt Id
        private string _cryptId;

        public string CryptId
        {
            get { return _cryptId; }
            set
            {
                _cryptId = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public ICommand SearchCryptocurencyCommand { get; }
        private bool CanSearchCryptocurencyCommand(object p) => true;
        private void OnSearchCryptocurencyCommand(object p)
        {
            var Crypts = GetCryptocurrenciesList(true);

            Cryptocurrency result = Crypts.Find(element => element.name == Search);

            if (result == null)
            {
                CryptId = "FalseName";
                Cryptocurrencie = GetCryptocurrenciesList()[0];
            }
            else
            {
                Cryptocurrencie = result;
            }
        }

        public ICommand GoToCommand { get; }
        private bool CanGoToCommand(object p) => true;
        private void OnGoToCommand(object p)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = Cryptocurrencie.explorer,
                UseShellExecute = true
            });
        }

        #endregion

        private string _search;

        public string Search
        {
            get => _search;
            set => Set(ref _search, value);
        }

        public InfoWindowView()
        {
            SearchCryptocurencyCommand =
                new LambdaCommand(OnSearchCryptocurencyCommand, CanSearchCryptocurencyCommand);

            GoToCommand =
                new LambdaCommand(OnGoToCommand, CanGoToCommand);

            Cryptocurrencie = GetCryptocurrenciesList()[0];
        }

        #region Interface realisation

        public List<Cryptocurrency> GetCryptocurrenciesList()
        {
            var client = new HttpClient();
            if (client != null)
            {
                var responseMessage = client.GetAsync(coincap_assets_url + "/bitcoin").Result;
                if (CryptId != null)
                {
                    responseMessage = client.GetAsync(coincap_assets_url + "/" + CryptId).Result;
                }

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonStringResult = responseMessage.Content.ReadAsStringAsync().Result;

                    AssetsRootObjectById rootobject = JsonSerializer.Deserialize<AssetsRootObjectById>(jsonStringResult);

                    var listOfCryptocurrencies = new List<Cryptocurrency>();
                    listOfCryptocurrencies.Add(rootobject.data);

                    return listOfCryptocurrencies;
                }
                else
                {
                    Cryptocurrency errCryptocurrencie = new Cryptocurrency();
                    errCryptocurrencie.symbol = "Wrong ID";
                    errCryptocurrencie.name = "use correct ID";
                    errCryptocurrencie.rank = "0";
                    errCryptocurrencie.priceUsd = "000000000";

                    var listOfCryptocurrencies = new List<Cryptocurrency>();
                    listOfCryptocurrencies.Add(errCryptocurrencie);

                    return listOfCryptocurrencies;
                }
            }
            throw new NotImplementedException();
        }

        public List<Cryptocurrency> GetCryptocurrenciesList(bool fullList)
        {
            if (!fullList)
            {
                return GetCryptocurrenciesList();
            }

            var client = new HttpClient();
            if (client != null)
            {
                var responseMessage = client.GetAsync(coincap_assets_url).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonStringResult = responseMessage.Content.ReadAsStringAsync().Result;

                    AssetsRootObjectWithList rootobject = JsonSerializer.Deserialize<AssetsRootObjectWithList>(jsonStringResult);

                    return rootobject.data.ToList();
                }
            }
            throw new NotImplementedException();
        }

        #endregion


    }
}

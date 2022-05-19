using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CryptoInfo.DAL;
using CryptoInfo.Models;
using CryptoInfo.ViewModel.Base;

namespace CryptoInfo.ViewModel
{
    internal class InfoWindowView : ViewModelBase, ICryptoList
    {
        private const string coincap_assets_url = @"http://api.coincap.io/v2/assets";

        public Cryptocurrency Cryptocurrencie { get; set; }

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

        public InfoWindowView(string cryptId)
        {
            CryptId = cryptId;
            Cryptocurrencie = GetCryptocurrenciesList()[0];
        }

        public InfoWindowView()
        {
            CryptId = "bitcoin";
            Cryptocurrencie = GetCryptocurrenciesList()[0];
            Cryptocurrencie.priceUsd = Cryptocurrencie.priceUsd.Substring(0, 8) + " USD";
        }

        #region Interface realisation

        public List<Cryptocurrency> GetCryptocurrenciesList()
        {
            var client = new HttpClient();
            if (client != null)
            {
                var responseMessage = client.GetAsync(coincap_assets_url + "/" + CryptId).Result;
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
                    throw new FieldAccessException("Something wrong with url");
                }
            }
            throw new NotImplementedException();
        }

        #endregion
    }
}

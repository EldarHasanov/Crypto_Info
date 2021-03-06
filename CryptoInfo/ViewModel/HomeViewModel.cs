using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using CryptoInfo.DAL;
using CryptoInfo.Infrastructure.Commands;
using CryptoInfo.Models;
using CryptoInfo.ViewModel.Base;

namespace CryptoInfo.ViewModel
{
    internal class HomeViewModel : ViewModelBase, ICryptoList
    {
        private const string coincap_assets_url = @"http://api.coincap.io/v2/assets";
        public List<Cryptocurrency> TopTenCryptocurrencies { get; set; }

        public RelayCommand SelectCryptocurencyCommand { get; set; }

        public HomeViewModel()
        {
            TopTenCryptocurrencies = GetCryptocurrenciesList();
        }

        public List<Cryptocurrency> GetCryptocurrenciesList()
        {
            var client = new HttpClient();
            if (client != null)
            {
                var responseMessage = client.GetAsync(coincap_assets_url).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonStringResult = responseMessage.Content.ReadAsStringAsync().Result;

                    AssetsRootObjectWithList rootobject = JsonSerializer.Deserialize<AssetsRootObjectWithList>(jsonStringResult);

                    var listOfCryptocurrencies = rootobject.data.Take(10).ToList();
                    foreach (var element in listOfCryptocurrencies)
                    {
                        element.priceUsd = element.priceUsd.Substring(0, 8) + " USD";
                        element.changePercent24Hr = element.changePercent24Hr.Substring(0, 5) + " %";
                    }

                    return listOfCryptocurrencies;
                }
                else
                {
                    throw new FieldAccessException("Something wrong with url");
                }
            }
            throw new NotImplementedException();


        }
    }
}

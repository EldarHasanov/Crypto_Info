using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoInfo.Models;

namespace CryptoInfo.ViewModel
{
    internal class HomeViewModel
    {
        public List<Cryptocurrency> TopTenCryptocurrencies { get; set; }

        public HomeViewModel(List<Cryptocurrency> topTenCryptocurrencies)
        {
            TopTenCryptocurrencies = topTenCryptocurrencies;
            foreach (var element in TopTenCryptocurrencies)
            {
                element.priceUsd = element.priceUsd.Substring(0, 8) + " USD";
                element.changePercent24Hr = element.changePercent24Hr.Substring(0, 5) + " %";
            }
        }

        public HomeViewModel()
        {
            TopTenCryptocurrencies = new List<Cryptocurrency>();
        }
    }
}

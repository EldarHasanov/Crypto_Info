using System.Collections.Generic;
using CryptoInfo.Models;

namespace CryptoInfo.DAL
{
    internal interface ICryptoList
    {
        public List<Cryptocurrency> GetCryptocurrenciesList();
    }
}

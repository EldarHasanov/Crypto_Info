using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CryptoInfo.Models;

namespace CryptoInfo.DAL
{
    internal interface ICryptoList
    {
        public List<Cryptocurrency> GetCryptocurrenciesList();
    }
}

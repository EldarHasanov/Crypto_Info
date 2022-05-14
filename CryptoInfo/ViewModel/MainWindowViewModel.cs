using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoInfo.ViewModel.Base;

namespace CryptoInfo.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private string _Title = "Top 10 cryptocurrencies"; 

        /// <summary>Title of window</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
    }
}

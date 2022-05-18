﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;
using CryptoInfo.Infrastructure.Commands;
using CryptoInfo.Models;
using CryptoInfo.ViewModel.Base;
using System.Text.Json;

namespace CryptoInfo.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private const string coincap_assets_url = @"http://api.coincap.io/v2/assets";
        public List<Cryptocurrency> TopTenCryptocurrencies { get; set; }

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

            #region HTTP requests

            #region TopTenCryptocurrencies

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

                        TopTenCryptocurrencies = rootobject.data.Take(10).ToList();
                        foreach (var element in TopTenCryptocurrencies)
                        {
                            element.priceUsd = element.priceUsd.Substring(0, 10) + " USD";
                            element.changePercent24Hr = element.changePercent24Hr.Substring(0, 10) + " %";
                        }
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

            #endregion


            #endregion
        }
    }
}

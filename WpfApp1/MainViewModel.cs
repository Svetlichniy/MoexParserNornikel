using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using WpfApp1.Services;
using WpfApp1.Models;
using System.Linq;
using System.Net;
using System.Windows;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    public class MainViewModel : NotifyInterface
    {
        IDialogService dialogService;
        //public ObservableCollection<ResultData> Data { get; set; }

        // команда открытия файла
        private RelayCommand openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                  (openCommand = new RelayCommand(obj =>
                  {
                  }));
            }
        }


        public MainViewModel()
        {

        }
    }
}

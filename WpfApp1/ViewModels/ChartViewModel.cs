using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using WpfApp1.Models;
using WpfApp1.Services;

namespace WpfApp1.ViewModels
{
    public class ChartViewModel : NotifyInterface
    {
        const string url = "https://iss.moex.com/cs/engines/stock/markets/shares/boardgroups/57/securities/GMKN.hs?callback=jQuery111209416957654225013_1578137803283&s1.type=candles&interval=31&candles=500&indicators=&_=1578137805091";
        public ObservableCollection<Point> Data { get; set; }

        public List<Person> People { get; set; }
        public string text 
        {
            get 
            {
                return "234";
            }
            set 
            {
                RaisePropertyChangedEvent("");
            }
        }


        private RelayCommand requestCommand;
        public RelayCommand RequestCommand
        {
            get
            {
                return requestCommand ??
                  (requestCommand = new RelayCommand(obj =>
                  {
                      DispatcherHelper.DoAsync(()=> Data.Clear());
                      var MoexNornikel = new MoexData(url);
                      string FixData = MoexNornikel.GetWebData();

                      var currentMouth = DateTime.Now;
                      foreach (var sub in FixData.Split(' ').Reverse().Take(30))
                      {
                          string Mouth = currentMouth.ToString("MM-yyyy");
                          string Value = sub.Split(',')[4];
                          DispatcherHelper.DoAsync(()=> Data.Add(new Point(Mouth, double.Parse(Value.Replace(".", ",")))) );

                          currentMouth = currentMouth.AddMonths(-1);
                      }

                  }));
            }
        }


        public ChartViewModel()
        {
            Data = new ObservableCollection<Point>();

            People = new List<Person>()
            {
                new Person { Name = "David", Height = 180 },
                new Person { Name = "Michael", Height = 170 },
                new Person { Name = "Steve", Height = 160 },
                new Person { Name = "Joel", Height = 182 }
            };
        }
    }
}

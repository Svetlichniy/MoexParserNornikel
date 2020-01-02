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

namespace WpfApp1
{
    public class MainViewModel : INotifyPropertyChanged
    {
        IFileService fileService;
        IDialogService dialogService;
        public ObservableCollection<ResultData> Data { get; set; }

        // команда открытия файла
        private RelayCommand openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                  (openCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          #region FileOpen
                          /*if (dialogService.OpenFileDialog() == true)
                          {
                              var data = fileService.Open(dialogService.FilePath);
                              Data.Clear();
                              foreach (var d in data.Models)
                                  Data.Add(d);
                              dialogService.ShowMessage("Файл открыт");
                          }*/
                          #endregion

                          DispatcherHelper.DoAsync(() =>
                          {
                              if (Data.Count() != 0)
                                  Data.Clear();
                          });
                          
                          string url = "https://dom.gosuslugi.ru/ppa/api/rest/services/ppa/public/organizations/searchByOrg?pageIndex=1&elementsPerPage=50";
                          var cookies = new CookieContainer();
                          #region Init cookies
                          cookies.Add(new Uri(url), new Cookie("_ym_d", "1571241984"));
                          cookies.Add(new Uri(url), new Cookie("_ym_isad", "2"));
                          cookies.Add(new Uri(url), new Cookie("_ym_uid", "1571241984433917606"));
                          cookies.Add(new Uri(url), new Cookie("_ym_visorc_28362746", "w"));
                          cookies.Add(new Uri(url), new Cookie("dtCookie", "1$CD313654B4A7952BEDAD481C1F560406"));
                          cookies.Add(new Uri(url), new Cookie("JSESSIONID", "ksVgR1+HsJxBo6-bPGW9iv6t"));
                          cookies.Add(new Uri(url), new Cookie("route_pafo-reports", "c74cc883b187536d03cd9d27dbb214c6"));
                          cookies.Add(new Uri(url), new Cookie("route_pafo-saiku", "3eed4a51ebe9ecbd752731228dd302a5"));
                          cookies.Add(new Uri(url), new Cookie("route_rest", "e7ef7e619dcd1dea17bea6eea59032ab"));
                          cookies.Add(new Uri(url), new Cookie("route_suim", "9cec376355a82e1c2092d0f01142afd1"));
                          cookies.Add(new Uri(url), new Cookie("suimSessionGuid", "35db6e53-ed74-4980-8781-9e01ebc0888c"));
                          cookies.Add(new Uri(url), new Cookie("timezone", "3"));
                          cookies.Add(new Uri(url), new Cookie("userSelectedLanguage", "ru"));
                          #endregion

                          var data = fileService.GetPostWebData(url, cookies);
                          foreach (var d in data.Models)
                          {
                              var company = fileService.GetWebData(
                              string.Format("https://dom.gosuslugi.ru/ppa/api/rest/services/ppa/public/organizations/orgByGuid?organizationGuid={0}", d.guid), cookies);
                              DispatcherHelper.DoAsync(() =>
                              {
                                  Data.Add(
                                      new ResultData()
                                      {
                                          fullName = company.fullName,
                                          inn = company.inn,
                                          kpp = company.kpp,
                                          ogrn = company.ogrn,
                                          orgAddress = company.orgAddress,
                                          orgEmail = company.orgEmail,
                                          url = company.url,
                                          factualAddress = company.factualAddress.formattedAddress
                                      }
                                  );
                              });
                          }

                          dialogService.ShowMessage("Done!");
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }

        // команда сохранения файла
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.SaveFileDialog() == true)
                          {
                              fileService.Save(dialogService.FilePath, Data.ToList());
                              dialogService.ShowMessage("Файл сохранен");
                          }
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }

        public MainViewModel(IDialogService dialogService, IFileService fileService)
        {
            Data = new ObservableCollection<ResultData>();
            
            this.dialogService = dialogService;
            this.fileService = fileService;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

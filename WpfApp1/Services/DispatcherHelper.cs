using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace WpfApp1.Services
{
    static public class DispatcherHelper
    {
        public static void DoAsync(Action act)
        {
            Application.Current.Dispatcher.Invoke(act);
        }
    }
}

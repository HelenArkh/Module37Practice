using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp
{
    public partial class ResultAlarmPage : ContentPage
    {
        public ResultAlarmPage(DateTime date, TimeSpan time)
        {
            InitializeComponent();
            GetContent(date, time);
        }

        public void GetContent(DateTime date, TimeSpan time)
        {
            //var datePickerText = new Label { Text = $"{date}", Margin = new Thickness(0, 20, 0, 0) };

            string formatDate = date.ToString("dd.MM");
            string formatTime = time.ToString("t");

            var alarmText = new Label
            {
                Text = $"{formatDate} в {formatTime}",
                //VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 30, 0, 0)
            };

            // Добавляем всё на страницу
            stackLayout.Children.Add(alarmText);
        }

    }
}
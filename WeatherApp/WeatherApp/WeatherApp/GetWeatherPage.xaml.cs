using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp
{
    public partial class GetWeatherPage : ContentPage
    {
        public GetWeatherPage()
        {
            InitializeComponent();

            weatherButton.Clicked += Weather_Click;
            alarmButton.Clicked += Alarm_Click;
        }

        private async void Weather_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WeatherPage());
        }

        private async void Alarm_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AlarmPage());
        }
    }
}
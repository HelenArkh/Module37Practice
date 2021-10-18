using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp
{
    public partial class AlarmPage : ContentPage
    {
        public DateTime date;
        public TimeSpan time;

        public AlarmPage()
        {
            InitializeComponent();
            GetContent();
        }

        public void GetContent()
        {
            // Создаем виджет выбора даты
            var datePicker = new DatePicker
            {
                Format = "D",
                // Диапазон дат: +/- неделя
                MaximumDate = DateTime.Now.AddDays(7),
                MinimumDate = DateTime.Now,
            };

            var datePickerText = new Label { Text = "Дата будильника ", Margin = new Thickness(0, 20, 0, 0) };

            // Добавляем всё на страницу
            stackLayout.Children.Add(datePickerText);
            stackLayout.Children.Add(datePicker);

            // Виджет выбора времени.
            var timePickerText = new Label { Text = "Время будильника ", Margin = new Thickness(0, 20, 0, 0) };
            var timePicker = new TimePicker
            {
                Time = new TimeSpan(13, 0, 0)
            };

            stackLayout.Children.Add(timePickerText);
            stackLayout.Children.Add(timePicker);

            // Установим текст текущего значения переключателя Stepper
            var stepperText = new Label
            {
                Text = "Громкость сигнала будильника: 50%",
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 30, 0, 0)
            };
            // Установим сам переключатель
            Stepper stepper = new Stepper
            {
                Minimum = 0,
                Maximum = 100,
                Increment = 1,
                Value = 50,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            // Добавим в разметку
            stackLayout.Children.Add(stepperText);
            stackLayout.Children.Add(stepper);

            // Создаем заголовок для переключателя
            var switchHeader = new Label { Text = "Не повторять каждый день", HorizontalOptions = LayoutOptions.Center, Margin = new Thickness(0, 5, 0, 0) };
            stackLayout.Children.Add(switchHeader);

            // Создаем переключатель
            Switch switchControl = new Switch
            {
                IsToggled = false,
                HorizontalOptions = LayoutOptions.Center,
                ThumbColor = Color.DodgerBlue,
                OnColor = Color.LightSteelBlue,
            };
            stackLayout.Children.Add(switchControl);

            Button saveButton = new Button { Text = "Сохранить", BackgroundColor = Color.Silver, Margin = new Thickness(0, 5, 0, 0) };

            stackLayout.Children.Add(saveButton);

            // Регистрируем обработчик события выбора даты
            datePicker.DateSelected += (sender, e) => DateSelectedHandler(sender, e, datePickerText);
            // Регистрируем обработчик события выбора времени
            timePicker.PropertyChanged += (sender, e) => TimeChangedHandler(sender, e, timePickerText, timePicker);
            // Регистрируем обработчик события выбора громкости
            stepper.ValueChanged += (sender, e) => VolumeChangedHandler(sender, e, stepperText);
            // Регистрируем обработчик события переключения
            switchControl.Toggled += (sender, e) => SwitchHandler(sender, e, switchHeader);
            // Регистрируем обработчик события сохранения
            saveButton.Clicked += (sender, e) => SaveButton_Click(sender, e);
        }

        public DateTime DateSelectedHandler(object sender, DateChangedEventArgs e, Label datePickerText)
        {
            // При срабатывании выбора - будет меняться информационное сообщение.
            datePickerText.Text = "Запустится " + e.NewDate.ToString("dd/MM/yyyy");

            return date = e.NewDate;
        }

        public TimeSpan TimeChangedHandler(object sender, PropertyChangedEventArgs e, Label timePickerText, TimePicker timePicker)
        {
            // Обновляем текст сообщения, когда появляется новое значение времени
            if (e.PropertyName == "Time")
                timePickerText.Text = "В " + timePicker.Time;
            return time = timePicker.Time;
        }

        /// <summary>
        /// Обработчик изменения громкости
        /// </summary>
        private void VolumeChangedHandler(object sender, ValueChangedEventArgs e, Label header)
        {
            header.Text = String.Format("Громкость: {0:F1} %", e.NewValue);
        }

        /// <summary>
        /// Обработка переключателя
        /// </summary>
        public void SwitchHandler(object sender, ToggledEventArgs e, Label header)
        {
            if (!e.Value)
            {
                header.Text = "Не повторять каждый день";
                return;
            }

            header.Text = "Повторять каждый день";
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResultAlarmPage(date, time));
        }
    }
}
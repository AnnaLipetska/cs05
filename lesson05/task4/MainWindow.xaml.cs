#define Config

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using task4.Settings;

namespace task4
{
    //Создайте приложение WPF Application, в главном окне которого поместите любой текст.Также,
    //должно быть окно настроек (можно реализовать с помощью TabControl). Пользователь может
    //изменять настройки.При повторном запуске приложения настройки должны оставаться
    //прежними.Реализуйте два варианта (в одном приложении или двух разных): 1) сохранение
    //настроек в конфигурационном файле; 2) сохранение настроек в реестре.
    //В окне настроек реализуйте следующие опции: цвет фона, цвет текста, размер шрифта, стиль
    //шрифта, а также кнопку «Сохранить». Для выбора цвета воспользуйтесь ColorPicker-ом по
    //примеру задания из Урока №3.
    public partial class MainWindow : Window
    {
        ISetting settings;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FontFamilySelector.ItemsSource = Fonts.SystemFontFamilies.OrderBy(x => x.Source);

            var numbers = new List<int>();
            for (int i = 8; i < 23; i++)
            {
                numbers.Add(i);
            }
            FontSizeSelector.ItemsSource = numbers;

#if Config
            settings = new ConfigSetting();
#else
            settings = new RegSetting();
#endif
            SetSettings();
        }

        #region Properties

        public Brush CurrentBackgroundBrush
        {
            get { return (Brush)GetValue(CurrentBackgroundBrushProperty); }
            set { SetValue(CurrentBackgroundBrushProperty, value); }
        }
        public static readonly DependencyProperty CurrentBackgroundBrushProperty =
            DependencyProperty.Register("CurrentBackgroundBrush", typeof(Brush), typeof(MainWindow), new UIPropertyMetadata(Brushes.AliceBlue));

        public Brush CurrentForegroundBrush
        {
            get { return (Brush)GetValue(CurrentForegroundBrushProperty); }
            set { SetValue(CurrentForegroundBrushProperty, value); }
        }
        public static readonly DependencyProperty CurrentForegroundBrushProperty =
            DependencyProperty.Register("CurrentForegroundBrush", typeof(Brush), typeof(MainWindow), new UIPropertyMetadata(Brushes.Black));

        public int CurrentFontSize
        {
            get { return (int)GetValue(CurrentFontSizeProperty); }
            set { SetValue(CurrentFontSizeProperty, value); }
        }
        public static readonly DependencyProperty CurrentFontSizeProperty =
            DependencyProperty.Register("CurrentFontSize", typeof(int), typeof(MainWindow), new PropertyMetadata(11));

        public FontFamily CurrentFontFamily
        {
            get { return (FontFamily)GetValue(CurrentFontFamilyProperty); }
            set { SetValue(CurrentFontFamilyProperty, value); }
        }
        public static readonly DependencyProperty CurrentFontFamilyProperty =
            DependencyProperty.Register("CurrentFontFamily", typeof(FontFamily), typeof(MainWindow), new UIPropertyMetadata(Fonts.SystemFontFamilies.FirstOrDefault()));

        #endregion

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            settings.SavedBackgroundBrush = CurrentBackgroundBrush;
            settings.SavedForegroundBrush = CurrentForegroundBrush;
            settings.SavedFontSize = CurrentFontSize;
            settings.SavedFontFamily = CurrentFontFamily;

            settings.SaveSettings();

            MessageBox.Show("Настройки успешно сохранены.");
        }

        private void SetSettings()
        {
            CurrentBackgroundBrush = settings.SavedBackgroundBrush;
            CurrentForegroundBrush = settings.SavedForegroundBrush;
            CurrentFontSize = settings.SavedFontSize;
            CurrentFontFamily = settings.SavedFontFamily;
        }
    }
}

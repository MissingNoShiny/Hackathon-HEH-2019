using System;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Interop;

namespace Gooboi
{
    /*ACRYLIC BLUR is a UWP component unavailable from the WPF framework currently.
     ABE_start > ABE_end = code than mimic this visual effect. It comes from Rafael Rivera's GitHub.
     * https://github.com/riverar/sample-win32-acrylicblur */

    public partial class PanelWindow : Window
    {
        [DllImport("user32.dll")] /*ABE_start*/
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
        
        private uint _blurOpacity;
        public double BlurOpacity
        {
            get { return _blurOpacity; }
            set { _blurOpacity = (uint)value; EnableBlur(); }
        }

        private uint _blurBackgroundColor = 0x550000; /*ABE_end*/

        MainWindow mainWindow;

        public PanelWindow(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            Owner = mainWindow;
            DataContext = this;
            InitializeComponent();
            this.Show();
            this.Theme();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Window_Position;
            timer.Interval = TimeSpan.FromSeconds(0.00001);
            timer.Start();
            EnableBlur();
            WindowState = mainWindow.WindowState;
            if (mainWindow.Admin)
                admin_button.Content = "Utilisateur";
        }
        /*BUTTONS CLICK*/
        private void Admin_Button(object sender, RoutedEventArgs e)
        {
            mainWindow.Admin_Mode();
            Owner.Focus();
            this.Close();
        }        
        private void About_Button(object sender, RoutedEventArgs e)
        {
            SettingsWindow window = new SettingsWindow();
            window.Owner = this;
            window.WindowState = this.WindowState;
            window.Content_Display(1);
            window.Show();
        }
        private void Quit_Button(object sender, RoutedEventArgs e)
        {
            Owner.Close();
        }
        private void Settings_Button(object sender, RoutedEventArgs e)
        {
            SettingsWindow window = new SettingsWindow();
            window.Owner = this;
            window.WindowState = this.WindowState;
            window.Content_Display(0);
            window.Show();           
        }

        /*TIMER MAKES THE WINDOW FOLLOW THE MAINWINDOW*/
        private void Window_Position(object sender, EventArgs e)
        {
                this.Top = Owner.Top;
                this.Left = Owner.Left - 300;
                this.Height = Owner.Height;
                var mainWindow = (Application.Current.MainWindow as MainWindow);
                this.WindowState = mainWindow.WindowState;
                window_background.Height = Height;
            if (WindowState == WindowState.Maximized)
            {
                mainWindow.Display_Fullscreen(true);
                page_title.Margin = new Thickness(25, 20, 75, 0);
                page_content.Visibility = Visibility.Collapsed;
            }
            else
            {
                mainWindow.Display_Fullscreen(false);
                page_title.Margin = new Thickness(25, 50, 75, 0);
                page_content.Visibility = Visibility.Visible;
            }
            
        }        
        /*APPLYING THEME*/
        public void Theme()
        {
            if (Gooboi.Properties.Settings.Default.Theme == "Light")
            {
                window_background.Background = new SolidColorBrush(Color.FromArgb(44,192, 192, 192));
                window_background.Opacity = 0.1;
                page_title.Foreground = new SolidColorBrush(Colors.Black);
                window_titlebar.BorderBrush = new SolidColorBrush(Colors.White);
                window_titlebar.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                window_titlebar.Opacity = 0.6;
                admin_button.Foreground = new SolidColorBrush(Colors.Black);
                admin_button.Background = new SolidColorBrush(Color.FromRgb(192, 192, 192));
                quit_button.Foreground = new SolidColorBrush(Colors.Black);
                quit_button.Background = new SolidColorBrush(Color.FromRgb(192, 192, 192));
                about_button.Foreground = new SolidColorBrush(Colors.Black);
                about_button.Background = new SolidColorBrush(Color.FromRgb(192, 192, 192));
                page_content.Foreground = new SolidColorBrush(Colors.Black);
                settings_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/settings_button_light.png")));
            }
            else
            {
                window_background.Background = new SolidColorBrush(Color.FromRgb(21, 21, 21));
                window_background.Opacity = 0.1;
                page_title.Foreground = new SolidColorBrush(Colors.White);
                window_titlebar.BorderBrush = new SolidColorBrush(Color.FromArgb(77,77, 77, 77));
                window_titlebar.Background = new SolidColorBrush(Color.FromRgb(36, 36, 36));
                window_titlebar.Opacity = 0.6;
                admin_button.Foreground = new SolidColorBrush(Colors.White);
                admin_button.Background = new SolidColorBrush(Color.FromRgb(36, 36, 36));
                quit_button.Foreground = new SolidColorBrush(Colors.White);
                quit_button.Background = new SolidColorBrush(Color.FromRgb(36, 36, 36));
                about_button.Foreground = new SolidColorBrush(Colors.White);
                about_button.Background = new SolidColorBrush(Color.FromRgb(36, 36, 36));
                page_content.Foreground = new SolidColorBrush(Colors.White);
                settings_button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/settings_button.png")));
            }
        }        
        public void EnableBlur() /*ABE_start*/
        {            
            var windowHelper = new WindowInteropHelper(this);
            var accent = new AccentPolicy();
            accent.AccentState = AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND;
            accent.GradientColor = 2013265920;
            var accentStructSize = Marshal.SizeOf(accent);
            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);
            var data = new WindowCompositionAttributeData();
            data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
            data.SizeOfData = accentStructSize;
            data.Data = accentPtr;
            SetWindowCompositionAttribute(windowHelper.Handle, ref data);
            Marshal.FreeHGlobal(accentPtr);
        }
        internal void DisableBlur()
        {            
            var windowHelper = new WindowInteropHelper(this);
            var accent = new AccentPolicy();
            accent.AccentState = AccentState.ACCENT_DISABLED;
            accent.GradientColor = 2013265920;
            var accentStructSize = Marshal.SizeOf(accent);
            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);
            var data = new WindowCompositionAttributeData();
            data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
            data.SizeOfData = accentStructSize;
            data.Data = accentPtr;
            SetWindowCompositionAttribute(windowHelper.Handle, ref data);
            Marshal.FreeHGlobal(accentPtr);
        }
        /*DISABLE Touch Cursor*/
        protected override void OnPreviewTouchDown(TouchEventArgs e)
        {
            base.OnPreviewTouchDown(e);
            Cursor = Cursors.None;
        }
        protected override void OnPreviewTouchMove(TouchEventArgs e)
        {
            base.OnPreviewTouchMove(e);
            Cursor = Cursors.None;
        }
        protected override void OnGotMouseCapture(MouseEventArgs e)
        {
            base.OnGotMouseCapture(e);
            Cursor = Cursors.None;
        }
        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);
            if (e.StylusDevice == null)
                Cursor = Cursors.Arrow;
        }
    }
    
    internal enum AccentState
    {
        ACCENT_DISABLED = 0,
        ACCENT_ENABLE_GRADIENT = 1,
        ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
        ACCENT_ENABLE_BLURBEHIND = 3,
        ACCENT_ENABLE_ACRYLICBLURBEHIND = 4,
        ACCENT_INVALID_STATE = 5
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct AccentPolicy
    {
        public AccentState AccentState;
        public uint AccentFlags;
        public uint GradientColor;
        public uint AnimationId;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowCompositionAttributeData
    {
        public WindowCompositionAttribute Attribute;
        public IntPtr Data;
        public int SizeOfData;
    }

    internal enum WindowCompositionAttribute
    {
        WCA_ACCENT_POLICY = 19
    }    //ABE_end         
}

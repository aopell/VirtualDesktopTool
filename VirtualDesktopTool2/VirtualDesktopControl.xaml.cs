using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Timers;
using WindowsDesktop;
using IO = System.IO;
using Microsoft.Win32;

namespace VirtualDesktopTool2
{
    /// <summary>
    /// Interaction logic for VirtualDesktopControl.xaml
    /// </summary>
    public partial class VirtualDesktopControl : UserControl
    {
        public string ImagePath
        {
            get => (string)GetValue(ImagePathProperty);
            set => SetValue(ImagePathProperty, value);
        }

        public static readonly DependencyProperty ImagePathProperty =
            DependencyProperty.Register("ImagePath", typeof(string),
                                        typeof(VirtualDesktopControl), new PropertyMetadata("Icons\\desktop.ico", ImagePathChanged));
        private static void ImagePathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            using Icon ico = Icon.ExtractAssociatedIcon((string)e.NewValue);
            ((VirtualDesktopControl)d).IconImage.Source = Imaging.CreateBitmapSourceFromHIcon(ico.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        public string SelectedImagePath
        {
            get => (string)GetValue(SelectedImagePathProperty);
            set => SetValue(SelectedImagePathProperty, value);
        }

        public static readonly DependencyProperty SelectedImagePathProperty =
            DependencyProperty.Register("SelectedImagePath", typeof(string),
                                        typeof(VirtualDesktopControl), new PropertyMetadata("Icons\\desktop-selected.ico", SelectedImagePathChanged));

        private static void SelectedImagePathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            using Icon ico = Icon.ExtractAssociatedIcon((string)e.NewValue);
            ((VirtualDesktopControl)d).SelectedIconImage.Source = Imaging.CreateBitmapSourceFromHIcon(ico.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        public string Number
        {
            get => (string)GetValue(NumberProperty);
            set => SetValue(NumberProperty, value);
        }

        public static readonly DependencyProperty NumberProperty =
            DependencyProperty.Register("Number", typeof(string),
                                        typeof(VirtualDesktopControl), new PropertyMetadata("0", NumberChanged));

        private static void NumberChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((VirtualDesktopControl)d).DesktopNumber.Content = e.NewValue;
            ((VirtualDesktopControl)d).DesktopName.IsEnabled = int.TryParse((string)e.NewValue, out int _);
            ((VirtualDesktopControl)d).UpdateVisuals();
        }


        public VirtualDesktopControl()
        {
            InitializeComponent();

            VirtualDesktop.Renamed += VirtualDesktop_Renamed;
            VirtualDesktop.Created += VirtualDesktop_Created;
            VirtualDesktop.Destroyed += VirtualDesktop_Destroyed;
            VirtualDesktop.CurrentChanged += VirtualDesktop_CurrentChanged;
        }

        private void VirtualDesktop_CurrentChanged(object sender, VirtualDesktopChangedEventArgs e)
        {
            UpdateVisuals();
        }

        private void VirtualDesktop_Destroyed(object sender, VirtualDesktopDestroyEventArgs e)
        {
            UpdateVisuals();
        }

        private void VirtualDesktop_Created(object sender, VirtualDesktop e)
        {
            UpdateVisuals();
        }

        private void VirtualDesktop_Renamed(object sender, VirtualDesktopRenamedEventArgs e)
        {
            UpdateVisuals();
        }


        public void UpdateVisuals()
        {
            UpdateBackgroundColor();
            UpdateDesktopName();
        }

        private void UpdateBackgroundColor()
        {
            try
            {
                int num = int.Parse(Number);
                var desks = VirtualDesktop.GetDesktops();
                if (num - 1 == Array.IndexOf(desks, VirtualDesktop.Current))
                {
                    MainGrid.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(16, 107, 158));
                }
                else
                {
                    MainGrid.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(42, 42, 42));
                }
            }
            catch (Exception ex)
            {
                MainGrid.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(42, 42, 42));
            }
        }

        private void UpdateDesktopName()
        {
            try
            {
                int num = int.Parse(Number);
                var desks = VirtualDesktop.GetDesktops();
                if (num <= desks.Length)
                {
                    DesktopName.IsEnabled = true;
                    DesktopName.Text = string.IsNullOrEmpty(desks[num - 1].Name) ? $"Desktop {num}" : desks[num - 1].Name;
                }
                else
                {
                    DesktopName.IsEnabled = false;
                    DesktopName.Text = $"Desktop {Number}";
                }
            }
            catch (Exception ex)
            {
                DesktopName.Text = $"Desktop {Number}";
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                ImagePath = ofd.FileName;
            }
        }

        private void SelectedImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                SelectedImagePath = ofd.FileName;
            }
        }

        private void DesktopName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                VirtualDesktop.GetDesktops()[int.Parse(Number) - 1].Name = DesktopName.Text;
            }
            catch
            {

            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}

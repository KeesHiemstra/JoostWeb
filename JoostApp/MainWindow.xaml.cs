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

namespace JoostApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			//Make the menu width depending to the width of the window
			menuMain.Width = this.Width;
		}

		private void OpenAbout(object sender, RoutedEventArgs e)
		{
			string msg = $"Application: Joost Appplication\n" +
				$"Author: Kees Hiemstra\n" +
				$"Version: 0.0.1";
			MessageBox.Show(msg, "About");
		}

		private void ExitApplication(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}

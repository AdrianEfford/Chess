using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using System.Windows.Threading;

namespace Chess
{
	/// <summary>
	/// Interaction logic for MainMenu.xaml
	/// </summary>
	public partial class MainMenu : UserControl
	{
		private readonly int _count = 0;

		public MainMenu()
		{
			InitializeComponent();
			
			var hardCodedGameModes = Enum.GetValues(typeof(GameMode));
			foreach (var gameMode in hardCodedGameModes)
			{
				//Create row
				var row = new RowDefinition { Height = new GridLength(1, GridUnitType.Star) };
				menuGrid.RowDefinitions.Add(row); //add row
				var gameModeString = new string(gameMode.ToString().SelectMany((c, i) => i > 0 && char.IsUpper(c) ? new[] { ' ', c } : new[] { c }).ToArray());

				Button thisButton = new Button
				{
					Content = gameModeString,
					Margin = new Thickness(0, 10, 10, 0),
					HorizontalAlignment = HorizontalAlignment.Right,
					VerticalAlignment = VerticalAlignment.Top,
					FontSize = 30d,
					Height = 50d,
					Width = 300d,
					Tag = gameMode
				};
				thisButton.Click += OnGameModeSelected;
				
				thisButton.SetValue(Grid.RowProperty, _count);
				menuGrid.Children.Add(thisButton);
				
				_count++;
			}
		}
		
		private void OnGameModeSelected(object sender, RoutedEventArgs e)
		{
			Model.GameMode = (GameMode)Enum.Parse(typeof(GameMode), ((Button)sender).Tag.ToString());
		}
	}
}

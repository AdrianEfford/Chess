using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Chess
{
	public class MainWindowViewModel : INotifyPropertyChanged
	{

		#region Singleton Pattern
		private static MainWindowViewModel _onlyInstanceOfThisClass;
		private MainWindowViewModel()
		{
			//private constructor
		}

		public static MainWindowViewModel GetInstance()
		{
			return _onlyInstanceOfThisClass ?? (_onlyInstanceOfThisClass = new MainWindowViewModel());
		}

		#endregion

		private int seconds;
		private int min;
		private int hours;
		public int Seconds
		{
			get { return seconds;}
			set
			{
				seconds = value;
				if (seconds % 60 == 0)
				{
					min++;
					seconds -= 60;
					if (min % 60 == 0)
					{
						hours++;
						min -= 60;
					}
				}
				OnPropertyChanged("TimeString");
			}
		}
		public string TimeString
		{
			get
			{
				return hours.ToString("D2") + ":" + min.ToString("D2") + ":" + seconds.ToString("D2");
			}
		}



		private EPieceColor turnColor;
		public EPieceColor TurnColor
		{
			get { return turnColor; }
			set
			{
				if (turnColor == value) return;

				turnColor = value;

				if (value == EPieceColor.White)
				{
					TurnImage = new BitmapImage(
						new Uri("pack://application:,,,/Chess;component/Resources/king_white.png"));
				}
				else
				{
					TurnImage = new BitmapImage(
						new Uri("pack://application:,,,/Chess;component/Resources/king_dark.png"));
				}
			}
		}

		private ImageSource turnImage = new BitmapImage(
			new Uri("pack://application:,,,/Chess;component/Resources/king_white.png"));

		public ImageSource TurnImage
		{
			get { return turnImage; }
			set {

				if (turnImage != value)
				{
					turnImage = value;
					OnPropertyChanged();
				}
			}
		}
		


		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
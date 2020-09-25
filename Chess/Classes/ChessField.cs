using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace Chess
{
	public class ChessField : INotifyPropertyChanged
	{
		public int X { get; protected set; }
		public int Y { get; protected set; }

		public bool Recheable
		{
			get;
			set;
		}

		private readonly SolidColorBrush _defaultFieldColor;

		public ChessField(int X, int Y, SolidColorBrush fieldColor)
		{
			this.X = X;
			this.Y = Y;
			ColorProperty = fieldColor;
			_defaultFieldColor = fieldColor;
			Recheable = false;
		}

		public void SetToDefault()
		{
			ColorProperty = _defaultFieldColor;
			Recheable = false;
		}

		private SolidColorBrush colorProperty;

		public SolidColorBrush ColorProperty
		{
			get
			{
				return colorProperty;
			}

			set
			{
				if (colorProperty == value) return;

				colorProperty = value;
				OnPropertyChanged();
			}
		}


		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
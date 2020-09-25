using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Chess
{
	public static class Helper
	{
		public static readonly int BoardSize = 8; //Board size
		public const int OneSecond = 1000; //ms


		#region Field Color

		public static readonly SolidColorBrush DarkerFieldsColor = new SolidColorBrush(Color.FromArgb(255, 209, 139, 71));
		public static readonly SolidColorBrush ClearerFieldsColor = new SolidColorBrush(Color.FromArgb(255, 255, 206, 158));
		


		#endregion

		#region Images

		public static readonly ImageSource EmptyImage = new BitmapImage(
			new Uri("pack://application:,,,/Chess;component/Resources/empty.png"));
		public static readonly ImageSource SuperPiece = new BitmapImage(
			new Uri("pack://application:,,,/Chess;component/Resources/super.png"));

		#endregion
	}
}
using System.ComponentModel;
using WindowsDisplayAPI;

namespace BlackWin
{
	public record ScreenInfo : INotifyPropertyChanged
	{
		private bool _isVisible;
		private string _displayName;

		public Form Form { get; set; }
		public Display Display { get; private init; }
		public string DevicePath { get; private init; }

		public bool IsVisible
		{
			get => _isVisible;
			set
			{
				_isVisible = value;

				if (Form != null)
				{
					Form.Visible = value;
				}
				
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_isVisible)));
			}
		}

		public ScreenInfo(Display display, string devicePath, string displayName, bool isVisible)
		{
			Display = display;
			DevicePath = devicePath;
			IsVisible = isVisible;
			_displayName = displayName;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public override string ToString() =>
			_displayName;
	}
}

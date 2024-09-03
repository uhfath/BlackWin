using System.ComponentModel;
using System.Data;
using System.Text.Json;
using System.Windows.Forms;
using WindowsDisplayAPI;
using WindowsDisplayAPI.DisplayConfig;

namespace BlackWin
{
	public partial class Form1 : Form
	{
		private bool? _isHovered;
		private bool _isInternalVisibleChange;

		public Form1(ScreenInfo currentScreenInfo, IEnumerable<ScreenInfo> screenInfos)
		{
			InitializeComponent();
			SetBounds(currentScreenInfo.Display.CurrentSetting.Position.X, currentScreenInfo.Display.CurrentSetting.Position.Y, currentScreenInfo.Display.CurrentSetting.Resolution.Width, currentScreenInfo.Display.CurrentSetting.Resolution.Height);

			foreach (var screenInfo in screenInfos.Where(s => s.DevicePath != currentScreenInfo.DevicePath))
			{
				screenInfo.PropertyChanged += OnScreenInfoVisibleChanged;
				checkedListBox1.Items.Add(screenInfo, screenInfo.IsVisible);
			}

			checkedListBox1.Dock = DockStyle.None;
			checkedListBox1.Height = (checkedListBox1.Items.Count + 1) * checkedListBox1.ItemHeight;
			checkedListBox1.Dock = DockStyle.Top;
		}

		private void OnScreenInfoVisibleChanged(object sender, PropertyChangedEventArgs eventArgs)
		{
			try
			{
				_isInternalVisibleChange = true;

				var screenInfo = sender as ScreenInfo;

				var index = Array.IndexOf(checkedListBox1.Items
					.Cast<ScreenInfo>()
					.ToArray(),
					screenInfo);

				checkedListBox1.SetItemChecked(index, screenInfo.IsVisible);
			}
			finally
			{
				_isInternalVisibleChange = false;
			}
		}

		private void UpdatePanelHover()
		{
			var isHovered = panel1.ClientRectangle.Contains(PointToClient(MousePosition));
			if (_isHovered != isHovered)
			{
				_isHovered = isHovered;
				var height = panel1.Height;

				panel1.AutoSize = isHovered;
				checkedListBox1.Visible = isHovered;
				button1.Visible = isHovered;
				panel1.Height = height;
			}
		}

		private void button1_Click(object sender, EventArgs eventArgs)
		{
			Application.Exit();
		}

		private void timer1_Tick(object sender, EventArgs eeventArgs)
		{
			UpdatePanelHover();
		}

		private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs eventArgs)
		{
			if (_isInternalVisibleChange)
			{
				return;
			}

			var screen = checkedListBox1.Items[eventArgs.Index] as ScreenInfo;
			screen.IsVisible = eventArgs.NewValue == CheckState.Checked;
		}
	}
}

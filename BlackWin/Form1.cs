using System.Data;
using System.Text.Json;
using System.Windows.Forms;
using WindowsDisplayAPI;

namespace BlackWin
{
	public partial class Form1 : Form
	{
		private const string SettingsFileName = "black_win.cfg";
		private static readonly string SettingsFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), SettingsFileName);

		private bool? _isHovered;

		private HashSet<string> GetSavedDisplays()
		{
			if (File.Exists(SettingsFilePath))
			{
				var displays = File.ReadAllLines(SettingsFilePath);
				return displays.ToHashSet(StringComparer.OrdinalIgnoreCase);
			}

			return null;
		}

		private void SaveDisplays(IEnumerable<string> displays)
		{
			File.WriteAllLines(SettingsFilePath, displays);
		}

		public Form1(Display currentDisplay)
		{
			InitializeComponent();
			SetBounds(currentDisplay.CurrentSetting.Position.X, currentDisplay.CurrentSetting.Position.Y, currentDisplay.CurrentSetting.Resolution.Width, currentDisplay.CurrentSetting.Resolution.Height);

			if (currentDisplay.IsGDIPrimary)
			{
				panel1.Visible = true;

				var adapters = WindowsDisplayAPI.DisplayConfig.PathDisplayTarget.GetDisplayTargets().ToArray();
				var displays = Display.GetDisplays().Where(d => !d.IsGDIPrimary);

				var savedDisplays = GetSavedDisplays() ?? displays.Select(d => d.DevicePath).ToHashSet(StringComparer.OrdinalIgnoreCase);

				foreach (var display in displays)
				{
					var adapter = adapters.Single(a => a.DevicePath == display.DevicePath);
					var isChecked = savedDisplays.Contains(display.DevicePath);

					var form = new Form1(display);
					form.Visible = isChecked;

					checkedListBox1.Items.Add(new ScreenInfo(form, display.DevicePath, adapter.FriendlyName), isChecked);
				}

				checkedListBox1.Dock = DockStyle.None;
				checkedListBox1.Height = (displays.Count() + 1) * checkedListBox1.ItemHeight;
				checkedListBox1.Dock = DockStyle.Top;

				timer1.Enabled = true;
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
			var screen = checkedListBox1.Items[eventArgs.Index] as ScreenInfo;
			var isChecked = eventArgs.NewValue == CheckState.Checked;
			screen.Form.Visible = isChecked;

			var displays = checkedListBox1.CheckedItems
				.Cast<ScreenInfo>()
				.Select(s => s.DevicePath)
				.ToHashSet(StringComparer.OrdinalIgnoreCase)
			;

			if (isChecked)
			{
				displays.Add(screen.DevicePath);
			}
			else
			{
				displays.Remove(screen.DevicePath);
			}

			SaveDisplays(displays);
		}

		private record ScreenInfo
		{
			private readonly string _devicePath;
			private readonly string _displayName;

			public Form Form { get; }
			public string DevicePath => _devicePath;

            public ScreenInfo(Form form, string devicePath, string displayName)
            {
				Form = form;
				_devicePath = devicePath;
				_displayName = displayName;
			}

			public override string ToString() =>
				_displayName;
		}
	}
}

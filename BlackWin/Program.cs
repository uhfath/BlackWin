using System.ComponentModel;
using System.Windows.Forms;
using WindowsDisplayAPI;

namespace BlackWin
{
	internal static class Program
	{
		private const string SettingsFileName = "black_win.cfg";
		private static readonly string SettingsFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), SettingsFileName);
		private static readonly IList<ScreenInfo> CurrentScreenInfos = new List<ScreenInfo>();

		private static HashSet<string> GetSavedDisplays()
		{
			if (File.Exists(SettingsFilePath))
			{
				var displays = File.ReadAllLines(SettingsFilePath);
				return displays.ToHashSet();
			}

			return null;
		}

		private static void SaveDisplays(IEnumerable<string> displays)
		{
			File.WriteAllLines(SettingsFilePath, displays);
		}

		[STAThread]
		private static void Main()
		{
			ApplicationConfiguration.Initialize();

			var adapters = WindowsDisplayAPI.DisplayConfig.PathDisplayTarget.GetDisplayTargets().ToArray();
			var displayAdapters = Display.GetDisplays()
				.Select(d => new
				{
					Display = d,
					Adapter = adapters.Single(a => a.DevicePath == d.DevicePath),
				})
				.ToArray()
			;

			var savedDisplays = GetSavedDisplays() ?? displayAdapters.Select(d => d.Display.DevicePath).ToHashSet();

			foreach (var display in displayAdapters)
			{
				var screenInfo = new ScreenInfo(display.Display, display.Adapter.DevicePath, display.Adapter.FriendlyName, savedDisplays.Contains(display.Adapter.DevicePath));
				screenInfo.PropertyChanged += OnScreenVisibleInfoChanged;

				CurrentScreenInfos.Add(screenInfo);
			}

			foreach (var screenInfo in CurrentScreenInfos)
			{
				screenInfo.Form = new Form1(screenInfo, CurrentScreenInfos);
			}

			Application.Run();
		}

		private static void OnScreenVisibleInfoChanged(object sender, PropertyChangedEventArgs eventArgs)
		{
			SaveDisplays(CurrentScreenInfos
				.Where(s => s.IsVisible)
				.Select(s => s.DevicePath));
		}
	}
}
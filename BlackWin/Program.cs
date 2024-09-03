using System.Windows.Forms;
using WindowsDisplayAPI;

namespace BlackWin
{
	internal static class Program
	{
		[STAThread]
		static void Main()
		{
			ApplicationConfiguration.Initialize();
			Application.Run(new Form1(Display.GetDisplays().Single(d => d.IsGDIPrimary)));
		}
	}
}
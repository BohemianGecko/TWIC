using XBoxAsMouse;

namespace TWIC;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

		var inputMonitor = new XBoxControllerAsMouse();
		inputMonitor.Start();
        inputMonitor.MouseModeEnabled = true;

        Application.Run(new OptionsScreen());
    }    
}
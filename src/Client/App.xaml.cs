namespace Client;

using Client.Api;
using System.Windows;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	public App()
	{
        ApiClient.Init("http://localhost:5094/api/v1/student");
    }
}

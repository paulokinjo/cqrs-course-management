using Client.Students;

namespace Client.Common;

internal sealed class MainViewModel
{
    public ViewModel ViewModel { get; }

	public MainViewModel()
	{
		ViewModel = new StudentListViewModel();
	}
}

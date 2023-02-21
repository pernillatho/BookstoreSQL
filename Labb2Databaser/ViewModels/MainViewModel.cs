using CommunityToolkit.Mvvm.ComponentModel;
using Labb2Databaser.Managers;

namespace Labb2Databaser.ViewModels;

public class MainViewModel : ObservableObject
{
    private readonly NavigationManager _navigationManager;
    private readonly DataManager _dataManager;

    public ObservableObject CurrentViewModel => _navigationManager.CurrentViewModel;

    public MainViewModel(NavigationManager navigationManager, DataManager dataManager)
    {
        _navigationManager = navigationManager;
        _dataManager = dataManager;

        _navigationManager.CurrentViewModelChanged += CurrentViewModelChanged;

    }

    private void CurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
}
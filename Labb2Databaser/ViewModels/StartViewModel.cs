using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labb2Databaser.Managers;
//using Labb2Databaser.Models;

namespace Labb2Databaser.ViewModels;

public class StartViewModel : ObservableObject
{
    private readonly NavigationManager _navigationManager;
    private readonly DataManager _dataManager;

    public StartViewModel(DataManager dataManager, NavigationManager navigationManager)
    {
        _dataManager = dataManager;
        _navigationManager = navigationManager;

        NavigateInventoryBalanceView = new RelayCommand(() => _navigationManager.CurrentViewModel =
            new InventoryViewModel(_dataManager, _navigationManager));
        NavigateAuthorsView = new RelayCommand(() => _navigationManager.CurrentViewModel =
            new AuthorViewModel(_dataManager, _navigationManager));
    }

    public IRelayCommand NavigateInventoryBalanceView { get; }
    public IRelayCommand NavigateAuthorsView { get; }

    

}
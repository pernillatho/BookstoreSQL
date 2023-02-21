using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labb2Databaser.Managers;
using Labb2Databaser.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using static System.Reflection.Metadata.BlobBuilder;

namespace Labb2Databaser.ViewModels;

public class InventoryViewModel : ObservableObject
{
    private readonly DataManager _dataManager;
    private readonly NavigationManager _navigationManager;

    public InventoryViewModel(DataManager dataManager, NavigationManager navigationManager)
    {
        _dataManager = dataManager;
        _navigationManager = navigationManager;
        MainMenuCommand = new RelayCommand(() =>
            _navigationManager.CurrentViewModel = new StartViewModel(_dataManager, _navigationManager));
        _storeList = _dataManager.LoadStores();
        _bookList = _dataManager.LoadBooks();
        AddSelectedBook = new RelayCommand(AddBooktoStores);
        RemoveSelectedBook = new RelayCommand(RemoveBookFromStores);
        UpdateInventoryOnSelectedBook = new RelayCommand(ChangeInventoryBalanceOnBooks);
    }

    public RelayCommand MainMenuCommand { get; }
    public RelayCommand RemoveSelectedBook { get; }
    public RelayCommand AddSelectedBook { get; }
    public RelayCommand UpdateInventoryOnSelectedBook { get; }

    private ObservableCollection<Store> _storeList;
    public ObservableCollection<Store> StoreList
    {
        get { return _storeList; }
        set { SetProperty(ref _storeList, value); }
    }

    private Store _selectedStore;
    public Store SelectedStore
    {
        get { return _selectedStore; }
        set
        {
            SetProperty(ref _selectedStore, value);
            SelectedStoreBooksInventoryBalances();
        }
    }

    private ObservableCollection<Book> _bookList;
    public ObservableCollection<Book> BookList
    {
        get { return _bookList; }
        set
        {
            SetProperty(ref _bookList, value);

        }
    }

    private Book _selectedBook;
    public Book SelectedBook
    {
        get { return _selectedBook; }
        set
        {
            SetProperty(ref _selectedBook, value);
        }
    }

    private ObservableCollection<InventoryBalance> _selectedStorebookList;

    public ObservableCollection<InventoryBalance> SelectedStoreBookList
    {
        get { return _selectedStorebookList; }
        set
        {
            SetProperty(ref _selectedStorebookList, value);
        }
    }

    private InventoryBalance _selectedBookInventoryBalance;
    public InventoryBalance SelectedBookInventoryBalance
    {
        get { return _selectedBookInventoryBalance; }
        set
        {
            _selectedBookInventoryBalance = value;
        }
    }

    private int _inputInventoryAmount;
    public int InputInventoryAmount
    {
        get { return _inputInventoryAmount; }
        set
        {
            _inputInventoryAmount = value;
        }
    }

    public void ChangeInventoryBalanceOnBooks()
    {
        var bookStoreDbContext = new BokhandelDbContext();

        if (SelectedStore == null || SelectedBookInventoryBalance == null || InputInventoryAmount == null)
            return;

        var result = bookStoreDbContext.InventoryBalances.FirstOrDefault
            (a => a.Isbn.Equals(SelectedBookInventoryBalance) && a.StoreId.Equals(SelectedStore.Id));

        if (result == null)
        {
            var updateBook = new InventoryBalance() { Isbn = SelectedBookInventoryBalance.Isbn, StoreId = SelectedStore.Id, Quantity = InputInventoryAmount };

            bookStoreDbContext.InventoryBalances.Update(updateBook);
            bookStoreDbContext.SaveChanges();
            SelectedStoreBooksInventoryBalances();
        }
    }

    public void AddBooktoStores()
    {
        var bookStoreDbContext = new BokhandelDbContext();

        if (SelectedStore == null || SelectedBook == null)
            return;
        
        var result = bookStoreDbContext.InventoryBalances.FirstOrDefault
            (r => r.Isbn.Equals(SelectedBook.Isbn) && r.StoreId.Equals(SelectedStore.Id));

        if (result == null)
        {
            var addBook = new InventoryBalance() { Isbn = SelectedBook.Isbn, StoreId = SelectedStore.Id };
            bookStoreDbContext.InventoryBalances.Add(addBook);
            bookStoreDbContext.SaveChanges();
            SelectedStoreBooksInventoryBalances();
        }
    }

    public void RemoveBookFromStores()
    {
        var bookStoreDbContext = new BokhandelDbContext();

        if (SelectedStore == null || SelectedBook == null)
            return;

        var result = bookStoreDbContext.InventoryBalances.FirstOrDefault
            (r => r.Isbn.Equals(SelectedBook.Isbn) && r.StoreId.Equals(SelectedStore.Id));

        if (result != null)
        {
            bookStoreDbContext.InventoryBalances.Remove(result);
            bookStoreDbContext.SaveChanges();
            SelectedStoreBooksInventoryBalances();
        }
    }

    public ObservableCollection<InventoryBalance> SelectedStoreBooksInventoryBalances()
    {
            var bookStoreDbContext = new BokhandelDbContext();

            SelectedStoreBookList = new ObservableCollection<InventoryBalance>
            (bookStoreDbContext.InventoryBalances.Include(b => b.IsbnNavigation)
                .Where(s => s.StoreId.Equals(SelectedStore.Id)));
                    return SelectedStoreBookList;
    }
}

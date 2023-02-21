using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media.Animation;
using Labb2Databaser.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Labb2Databaser.Managers;

public class DataManager
{
    
    public ObservableCollection<Store> StoreList { get; set; }

    public ObservableCollection<Store> LoadStores()
    {
        var bookStoreDbContext = new BokhandelDbContext();
        StoreList = new ObservableCollection<Store>(bookStoreDbContext.Stores.ToList());
        return StoreList;
    }

    public ObservableCollection<Book> BookList { get; set; }

    public ObservableCollection<Book> LoadBooks()
    {
        var bookStoreDbContext = new BokhandelDbContext();
        BookList = new ObservableCollection<Book>(bookStoreDbContext.Books.ToList());
        return BookList;
    }

    public ObservableCollection<Author> AuthorList { get; set; }

    public ObservableCollection<Author> LoadAuthor()
    {
        var bookStoreDbContext = new BokhandelDbContext();
        AuthorList = new ObservableCollection<Author>(bookStoreDbContext.Authors.ToList());
        return AuthorList;
    }

    public ObservableCollection<InventoryBalance> InventoryList { get; set; }

    public ObservableCollection<InventoryBalance> LoadInventoryBalances()
    {
        var bookStoreDbContext = new BokhandelDbContext();
        InventoryList = new ObservableCollection<InventoryBalance>(bookStoreDbContext.InventoryBalances);
        return InventoryList;
    }
}

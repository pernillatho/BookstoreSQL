using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labb2Databaser.Managers;
using Labb2Databaser.Models;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Windows.Media;

namespace Labb2Databaser.ViewModels;

public class AuthorViewModel : ObservableObject
{
    private readonly DataManager _dataManager;
    private readonly NavigationManager _navigationManager;

    public AuthorViewModel(DataManager dataManager, NavigationManager navigationManager)
    {
        _dataManager = dataManager;
        _navigationManager = navigationManager;
        MainMenuCommand = new RelayCommand(() =>
            _navigationManager.CurrentViewModel = new StartViewModel(_dataManager, _navigationManager));
        _authorList = _dataManager.LoadAuthor();
        _bookList = _dataManager.LoadBooks();
        AddNewAuthor = new RelayCommand(AddAuthor);
        RemoveAuthors = new RelayCommand(RemoveAuthor);
        EditExistAuthor = new RelayCommand(EditAuthor);
        AddNewBook = new RelayCommand(AddBooks);
        RemoveTitle = new RelayCommand(RemoveBooks);
        EditExistingBook = new RelayCommand(EditBook);
        ClearSelectedBook = new RelayCommand(ClearTextBoxBooks);
    }

    public RelayCommand MainMenuCommand { get; }
    public RelayCommand RemoveAuthors { get; }
    public RelayCommand AddNewAuthor { get; }
    public RelayCommand EditExistAuthor { get; }
    public RelayCommand AddNewBook { get; }
    public RelayCommand RemoveTitle { get; }
    public RelayCommand EditExistingBook { get; }
    public RelayCommand ClearSelectedBook { get;  }

    private ObservableCollection<Author> _authorList;
    public ObservableCollection<Author> AuthorList
    {
        get { return _authorList; }
        set { SetProperty(ref _authorList, value); }
    }

    private Author _selectedAuthor;
    public Author SelectedAuthor
    {
        get { return _selectedAuthor; }
        set
        {
            SetProperty(ref _selectedAuthor, value);
            if (SelectedAuthor != null)
            {
                FirstName = _selectedAuthor.FirstName;
                LastName = _selectedAuthor.LastName;
                DateBirth = _selectedAuthor.DateOfBirth;
            }
        }
    }

    private string _firstName;
    public string FirstName
    {
        get { return _firstName; }
        set
        {
            SetProperty(ref _firstName, value);
        }
    }

    private string _lastName;
    public string LastName
    {
        get { return _lastName; }
        set
        {
            SetProperty(ref _lastName, value);
        }
    }

    private DateTime? _dateBirth;
    public DateTime? DateBirth
    {
        get { return _dateBirth; }
        set
        {
            SetProperty(ref _dateBirth, value);
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
            IsReadOnly = false;
            if (SelectedBook != null)
            {
                Isbn = _selectedBook.Isbn;
                Title = _selectedBook.Title;
                Language = _selectedBook.Language;
                Price = _selectedBook.Price;
                ReleaseYear = _selectedBook.ReleaseYear;
                AuthorId = _selectedBook.AuthorId;
                IsEnabled = false;
            }
        }
    }

    private string _isbn;
    public string Isbn
    {
        get { return _isbn; }
        set
        {
            SetProperty(ref _isbn, value);
        }
    }

    private string _title;
    public string Title
    {
        get { return _title; }
        set
        {
            SetProperty(ref _title, value);
        }
    }

    private string _language;
    public string Language
    {
        get { return _language; }
        set
        {
            SetProperty(ref _language, value);
        }
    }

    private int _price;
    public int Price
    {
        get { return _price; }
        set
        {
            SetProperty(ref _price, value);
        }
    }

    private DateTime? _releaseYear;
    public DateTime? ReleaseYear
    {
        get { return _releaseYear; }
        set
        {
            SetProperty(ref _releaseYear, value);
        }
    }

    private int _authorId;
    public int AuthorId
    {
        get { return _authorId; }
        set
        {
            SetProperty(ref _authorId, value);
        }
    }

    private bool _isReadOnly = true;
    public bool IsReadOnly
    {
        get { return _isReadOnly; }
        set
        {
            SetProperty(ref _isReadOnly, value);
        }
    }

    private bool _isEnabled = true;

    public bool IsEnabled
    {
        get { return _isEnabled; }
        set
        {
            SetProperty(ref _isEnabled, value);
        }
    }

    public void ClearTextBoxBooks()
    {
        SelectedBook = null;
        Isbn = string.Empty;
        Title = string.Empty;
        Language = string.Empty;
        Price = 0;
        AuthorId = 0;
        DateBirth = DateTime.Today;
        IsReadOnly = true;
        IsEnabled = true;
    }

    public void ClearTextBoxAuthors()
    {
        SelectedAuthor = null;
        FirstName = string.Empty;
        LastName = string.Empty;
        DateBirth = DateTime.Today;
    }

    public void AddAuthor()
    {
        var bookStoreDbContext = new BokhandelDbContext();

        if (FirstName is null || LastName is null || DateBirth is null)
            return;

        var author = new Author() { DateOfBirth = _dateBirth, FirstName = _firstName, LastName = _lastName};
        bookStoreDbContext.Authors.Add(author);
        bookStoreDbContext.SaveChanges();
        AuthorList =_dataManager.LoadAuthor();

        ClearTextBoxAuthors();
    }

    public void RemoveAuthor()
    {
        var bookStoreDbContext = new BokhandelDbContext();

        if (SelectedAuthor is null)
            return;

        var authorExists = bookStoreDbContext.Authors.FirstOrDefault(a => a.Id == SelectedAuthor.Id);
        if(authorExists is null) return;

        bookStoreDbContext.Remove(authorExists);
        bookStoreDbContext.SaveChanges();
        AuthorList = _dataManager.LoadAuthor();
        
        ClearTextBoxAuthors();
    }

    public void EditAuthor()
    {
        var bookStoreDbContext = new BokhandelDbContext();

        if (SelectedAuthor is null || FirstName is null || LastName is null || DateBirth is null)
            return;

        var authorExists = bookStoreDbContext.Authors.FirstOrDefault(a => a.Id == SelectedAuthor.Id);

        if (authorExists != null)
        {
            authorExists.FirstName = _firstName;
            authorExists.LastName = _lastName; 
            authorExists.DateOfBirth = _dateBirth;

            bookStoreDbContext.SaveChanges();
            AuthorList = _dataManager.LoadAuthor();
            
            ClearTextBoxAuthors();
        }
    }

    public void AddBooks()
    {
        var bookStoreDbContext = new BokhandelDbContext();

        if (Isbn is null || Title is null || Language is null || Price.ToString() is null ||
            ReleaseYear.ToString() is null || AuthorId.ToString() is null || _isbn.Length != 13)
            return;

        var books = bookStoreDbContext.Books;
        foreach (var b in books)
        {
            if (b.Isbn == Isbn)
            {
                return;
            }
        }

        var result = bookStoreDbContext.InventoryBalances.FirstOrDefault
            (r => r.Isbn.Equals(_isbn));
        if (result != null)
            return;

        var book = new Book() { Isbn = _isbn, Title = _title, Language = _language, Price = _price, ReleaseYear = _releaseYear, AuthorId = _authorId};

        bookStoreDbContext.Books.Add(book);
        bookStoreDbContext.SaveChanges();
        BookList = _dataManager.LoadBooks();

        ClearTextBoxBooks();
    }

    public void RemoveBooks()
    {
        var bookStoreDbContext = new BokhandelDbContext();

        if (SelectedBook is null)
            return;

        var bookExists = bookStoreDbContext.Books.FirstOrDefault(a => a.Isbn == SelectedBook.Isbn);
        if (bookExists is null)
            return;
        
        bookStoreDbContext.Remove(bookExists);
        bookStoreDbContext.SaveChanges();
        BookList = _dataManager.LoadBooks();
        ClearTextBoxBooks();
    }

    public void EditBook()
    {
        var bookStoreDbContext = new BokhandelDbContext();

        if (SelectedBook is null)
            return;

        var bookExists = bookStoreDbContext.Books.FirstOrDefault(a => a.Isbn == SelectedBook.Isbn);
        
        if (bookExists != null)
        {
            if (bookExists.Isbn != Isbn)
                return;
            bookExists.Isbn = Isbn;
            bookExists.Title = Title;
            bookExists.Language = Language;
            bookExists.Price = Price;
            bookExists.ReleaseYear = ReleaseYear;
            bookExists.AuthorId = AuthorId;

            bookStoreDbContext.SaveChanges();
            BookList = _dataManager.LoadBooks();
        }
    }


}
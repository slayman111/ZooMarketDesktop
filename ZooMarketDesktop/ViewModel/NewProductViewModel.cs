using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using ZooMarketDesktop.Command;
using ZooMarketDesktop.Core;
using ZooMarketDesktop.Core.Converter;
using ZooMarketDesktop.DbService;
using ZooMarketDesktop.Model.Entity;
using ZooMarketDesktop.Model.Entity.Context;
using ZooMarketDesktop.View;

namespace ZooMarketDesktop.ViewModel;

internal class NewProductViewModel : BaseViewModel
{
    private string? _name;
    private string? _description;
    private Brand? _selectedBrand;
    private ObservableCollection<Brand>? _brands;
    private int _amount;
    private ProductType? _selectedProductType;
    private ObservableCollection<ProductType>? _productTypes;
    private decimal _price;
    private string? _filePath;
    private BitmapImage? _image;

    public string? Name { get => _name; set => SetProperty(ref _name, value); }
    public string? Description { get => _description; set => SetProperty(ref _description, value); }
    public Brand? SelectedBrand { get => _selectedBrand; set => SetProperty(ref _selectedBrand, value); }
    public ObservableCollection<Brand>? Brands { get => _brands; private set => SetProperty(ref _brands, value); }
    public int Amount { get => _amount; set => SetProperty(ref _amount, value); }

    public ProductType? SelectedProductType
    {
        get => _selectedProductType;
        set => SetProperty(ref _selectedProductType, value);
    }

    public ObservableCollection<ProductType>? ProductTypes
    {
        get => _productTypes;
        private set => SetProperty(ref _productTypes, value);
    }

    public decimal Price { get => _price; set => SetProperty(ref _price, value); }
    public string? FilePath { get => _filePath; set => SetProperty(ref _filePath, value); }

    public ICommand OpenDashboardWindowCommand { get; private set; }
    public ICommand AddProductCommand { get; private set; }
    public ICommand ChooseImageCommand { get; }

    public NewProductViewModel()
    {
        OpenDashboardWindowCommand = new DelegateCommand(OpenDashboardWindow);
        AddProductCommand = new DelegateCommand(AddProduct);
        ChooseImageCommand = new DelegateCommand(ChooseImage);

        FilePath = "Выберите файл";

        Task.Run(async () =>
        {
            await using var context = new ZooMarketDbContext();
            using var brandService = new CrudDbService<int, Brand>(context);
            using var productTypeService = new CrudDbService<int, ProductType>(context);

            Brands = new ObservableCollection<Brand>(await brandService.GetAllAsync());
            ProductTypes = new ObservableCollection<ProductType>(await productTypeService.GetAllAsync());
        });
    }

    private void ChooseImage(object obj)
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "Image files|*.bmp;*.jpg;*.png",
            FilterIndex = 1
        };

        if (openFileDialog.ShowDialog() is not true) return;

        FilePath = openFileDialog.FileName;
        _image = new BitmapImage(new Uri(openFileDialog.FileName));
    }

    private async void AddProduct(object obj)
    {
        await using var context = new ZooMarketDbContext();
        using var productService = new CrudDbService<int, Product>(context);

        await productService.CreateAsync(new Product
        {
            Name = Name ?? throw new Exception("Заполните имя"),
            Description = Description ?? throw new Exception("Заполните описание"),
            BrandId = SelectedBrand?.Id ?? throw new Exception("Выберите бренд"),
            Amount = Amount,
            ProductTypeId = SelectedProductType?.Id ?? throw new Exception("Выберите тип товара"),
            Price = Price,
            CreatedById = CurrentUser.User.Id,
            Image = ByteArrayToBitmapImageConverter.ToByteArray(_image)
        });

        MessageBoxService.Info("Товар добавлен");
    }

    private static void OpenDashboardWindow(object obj)
    {
        WindowManager.Open<DashboardWindow, NewProductWindow>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListaZakupowWaszkiewicz.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ListaZakupowWaszkiewicz.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Category> FilteredCategories { get; set; }
        public ObservableCollection<string> Stores { get; set; }

        private string selectedStore;
        public string SelectedStore
        {
            get => selectedStore;
            set
            {
                selectedStore = value;
                OnPropertyChanged();
                UpdateStoreFilter();
            }
        }

        public string NewCategoryName { get; set; }
        public string NewProductName { get; set; }
        public string NewProductUnit { get; set; }
        public int NewProductQuantity { get; set; } = 1;
        public string NewProductStore { get; set; } = "Wszystkie";
        public bool NewProductIsOptional { get; set; }
        public string SelectedCategoryForProduct { get; set; }

        public ICommand AddCategoryCommand { get; }
        public ICommand AddProductCommand { get; }

        public MainViewModel()
        {
            Categories = new ObservableCollection<Category>();
            FilteredCategories = new ObservableCollection<Category>();

            Stores = new ObservableCollection<string>()
            {
                "Wszystkie",
                "Biedronka",
                "Lidl",
                "Kaufland",
                "Zabka",
        "Intermarche",
                "Inne"
            };

            SelectedStore = "Wszystkie";

            AddCategoryCommand = new Command(() =>
            {
                if (!string.IsNullOrWhiteSpace(NewCategoryName))
                {
                    Categories.Add(new Category { Name = NewCategoryName });
                    NewCategoryName = "";
                    OnPropertyChanged(nameof(NewCategoryName));
                    Refresh();
                }
            });

            AddProductCommand = new Command(() =>
            {
                if (string.IsNullOrWhiteSpace(NewProductName) || string.IsNullOrWhiteSpace(SelectedCategoryForProduct))
                    return;

                Category cat = null;
                foreach (var c in Categories)
                    if (c.Name == SelectedCategoryForProduct)
                        cat = c;

                if (cat != null)
                {
                    cat.Products.Add(new Product
                    {
                        Name = NewProductName,
                        Unit = NewProductUnit,
                        Quantity = NewProductQuantity,
                        Store = NewProductStore,
                        IsOptional = NewProductIsOptional
                    });

                    // Reset formularza
                    NewProductName = "";
                    NewProductUnit = "";
                    NewProductQuantity = 1;
                    NewProductStore = "Wszystkie";
                    NewProductIsOptional = false;
                    OnPropertyChanged(nameof(NewProductName));
                    OnPropertyChanged(nameof(NewProductUnit));
                    OnPropertyChanged(nameof(NewProductQuantity));
                    OnPropertyChanged(nameof(NewProductStore));
                    OnPropertyChanged(nameof(NewProductIsOptional));

                    Refresh();
                }
            });
        }

        private void UpdateStoreFilter()
        {
            FilteredCategories.Clear();

            for (int i = 0; i < Categories.Count; i++)
            {
                Category original = Categories[i];
                Category newCat = new Category();
                newCat.Name = original.Name;

                for (int j = 0; j < original.Products.Count; j++)
                {
                    Product p = original.Products[j];
                    bool add = false;

                    if (SelectedStore == "Wszystkie")
                        add = true;
                    else if (p.Store == SelectedStore)
                        add = true;

                    if (add)
                        newCat.Products.Add(p);
                }

                List<Product> sorted = new List<Product>();
                for (int k = 0; k < newCat.Products.Count; k++)
                    if (!newCat.Products[k].IsBought)
                        sorted.Add(newCat.Products[k]);
                for (int k = 0; k < newCat.Products.Count; k++)
                    if (newCat.Products[k].IsBought)
                        sorted.Add(newCat.Products[k]);

                newCat.Products = sorted;

                if (newCat.Products.Count > 0)
                    FilteredCategories.Add(newCat);
            }
        }

        public void Refresh()
        {
            UpdateStoreFilter();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

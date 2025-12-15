using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListaZakupowWaszkiewicz.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ListaZakupowWaszkiewicz.ViewModels
{
    public class ProductItemViewModel : INotifyPropertyChanged
    {
        public Product Product { get; set; }
        private MainViewModel mainVM;
        private Category category;

        public ProductItemViewModel(Product product, Category cat, MainViewModel vm)
        {
            Product = product;
            category = cat;
            mainVM = vm;

            QuantityPlusCommand = new Command(() =>
            {
                Product.Quantity++;
                OnPropertyChanged(nameof(Product));
                mainVM.Refresh();
            });

            QuantityMinusCommand = new Command(() =>
            {
                if (Product.Quantity > 0)
                    Product.Quantity--;
                OnPropertyChanged(nameof(Product));
                mainVM.Refresh();
            });

            DeleteCommand = new Command(() =>
            {
                category.Products.Remove(Product);
                mainVM.Refresh();
            });
        }

        public ICommand QuantityPlusCommand { get; }
        public ICommand QuantityMinusCommand { get; }
        public ICommand DeleteCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

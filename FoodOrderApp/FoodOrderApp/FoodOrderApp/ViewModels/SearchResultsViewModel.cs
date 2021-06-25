using FoodOrderApp.Model;
using FoodOrderApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FoodOrderApp.ViewModels
{
    class SearchResultsViewModel : BaseViewModel
    {
        public ObservableCollection<FoodItem> FoodItemsByQuery { get; set; }

        private int _TotalFoodItems;
        public int TotalFoodItems
        {
            set
            {
                _TotalFoodItems = value;
                OnPropertyChanged();
            }

            get
            {
                return _TotalFoodItems;
            }
        }

        public SearchResultsViewModel(string searchText)
        {
            FoodItemsByQuery = new ObservableCollection<FoodItem>();
            GetFoodItemsByQuery(searchText);
        }

        private async void GetFoodItemsByQuery(string searchText)
        {
            var data = await new FoodItemService().GetFoodItemsByQueryAsync(searchText);
            FoodItemsByQuery.Clear();
            foreach (var item in data)
            {
                FoodItemsByQuery.Add(item);
            }
            TotalFoodItems = FoodItemsByQuery.Count;
        }
    }
}

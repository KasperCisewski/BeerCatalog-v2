﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using KatalogPiw.ViewModels;
using KatalogPiw.Models;
using System.Collections.ObjectModel;

namespace KatalogPiw.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowBeerListView : ContentPage
    {
        ShowBeerListViewModel vm;
        public ShowBeerListView()
        {
            vm = new ShowBeerListViewModel();
            BindingContext = vm;
            InitializeComponent();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (SearchBar.Text == null)
            {
                SearchBar.Text = "";
            }

            BeerList.ItemsSource = vm.FiltringBeers(SearchBar.Text, BreweryList.SelectedItem, TypeList.SelectedItem, Slider.Value);

        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {

            if (SearchBar.Text == null)
            {
                SearchBar.Text = "";
            }

            BeerList.ItemsSource = vm.FiltringBeers(SearchBar.Text, BreweryList.SelectedItem, TypeList.SelectedItem, Slider.Value);

        }

        private async void buttonClearList_Click(object sender,TextChangedEventArgs e)
        {
            //BeerList.BeginRefresh();
            //vm.ClearAllBearsInList();
            //BeerList.EndRefresh();
            vm = new ShowBeerListViewModel();
            BindingContext = vm;
            InitializeComponent();
         //   vm.ClearAllBearsInList();
        }

        private async void buttonSelectAllInList_Click(object sender, TextChangedEventArgs e)
        {
            if (SearchBar.Text == null)
            {
                SearchBar.Text = "";
            }

            vm.SelectAllBeers(SearchBar.Text, BreweryList.SelectedItem, TypeList.SelectedItem, Slider.Value);
        }

        private async void buttonCreatePriceListA_Click(object sender, TextChangedEventArgs e)
        {
            try
            {
                vm.CreatePriceListA();
            }

            catch (Syncfusion.Pdf.PdfException pdfException)
            {
                DisplayAlert("Error", "Problem z generowaniem PDF" + pdfException.Message.ToString(), "OK");
            }
            catch (Exception exception)
            {
                DisplayAlert("Error", "Problem z generowaniem PDF" + exception.Message.ToString(), "OK");
            }
        }

        private async void buttonCreatePriceListB_Click(object sender, TextChangedEventArgs e)
        {
            try
            {
                vm.CreatePriceListB();
            }

            catch (Syncfusion.Pdf.PdfException pdfException)
            {
                DisplayAlert("Error", "Problem z generowaniem PDF" + pdfException.Message.ToString(), "OK");
            }
            catch (Exception exception)
            {
                DisplayAlert("Error", "Problem z generowaniem PDF" + exception.Message.ToString(), "OK");
            }
        }

        private async void buttonCreatePriceListC_Click(object sender, TextChangedEventArgs e)
        {
            try
            {
                vm.CreatePriceListC();
            }

            catch (Syncfusion.Pdf.PdfException pdfException)
            {
                DisplayAlert("Error", "Problem z generowaniem PDF" + pdfException.Message.ToString(), "OK");
            }
            catch (Exception exception)
            {
                DisplayAlert("Error", "Problem z generowaniem PDF" + exception.Message.ToString(), "OK");
            }
        }

        private async void ClearTypeContext(object Sender, EventArgs e)
        {
            TypeList.SelectedItem = null;
        }

        private async void ClearBreweryContext(object sender, EventArgs e)
        {
            BreweryList.SelectedItem = null;
        }

        async void SelectType_ItemSelect(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {

                return;
            }

            if (SearchBar.Text == null)
            {
                SearchBar.Text = "";
            }

            BeerList.ItemsSource = vm.FiltringBeers(SearchBar.Text, BreweryList.SelectedItem, TypeList.SelectedItem, Slider.Value);

        }

        async void SelectBrewery_ItemSelect(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            if (SearchBar.Text == null)
            {
                SearchBar.Text = "";
            }

            BeerList.ItemsSource = vm.FiltringBeers(SearchBar.Text, BreweryList.SelectedItem, TypeList.SelectedItem, Slider.Value);

        }
    }
}
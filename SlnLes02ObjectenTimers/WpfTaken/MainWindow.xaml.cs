using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfTaken
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Stack<ListBoxItem> removedItems = new Stack<ListBoxItem>();

        public MainWindow()
        {
            InitializeComponent();
            cbPrioriteit.Items.Add("Laag");
            cbPrioriteit.Items.Add("Normaal");
            cbPrioriteit.Items.Add("Hoog");
            cbPrioriteit.SelectedIndex = 1;
            btnToevoegen.Click += BtnToevoegen_Click;
            btnReset.Click += BtnReset_Click;
            btnErase.Click += BtnErase_Click;
            txtTaak.TextChanged += TxtTaak_TextChanged;
            cbPrioriteit.SelectionChanged += CbPrioriteit_SelectionChanged;
            dpDate.SelectedDateChanged += DpDate_SelectedDateChanged;
            rb1.Checked += RadioButton_Checked;
            rb2.Checked += RadioButton_Checked;
            rb3.Checked += RadioButton_Checked;
        }

        private void BtnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem item = new ListBoxItem();
            item.Content = $"{txtTaak.Text}, {cbPrioriteit.SelectedItem}, {dpDate.SelectedDate}, {GetSelectedRadioButton()}";
            item.Background = GetPriorityColor();
            lbTaken.Items.Add(item);
            ResetForm();
            CheckForm();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            if (removedItems.Count > 0)
            {
                ListBoxItem item = removedItems.Pop();
                lbTaken.Items.Add(item);
            }
            CheckForm();
        }

        private void BtnErase_Click(object sender, RoutedEventArgs e)
        {
            if (lbTaken.SelectedItem != null)
            {
                ListBoxItem item = (ListBoxItem)lbTaken.SelectedItem;
                removedItems.Push(item);
                lbTaken.Items.Remove(item);
            }
            CheckForm();
        }

        private void TxtTaak_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblTaakError.Content = "";
            CheckForm();
        }

        private void CbPrioriteit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckForm();
        }

        private void DpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckForm();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            CheckForm();
        }

        private void CheckForm()
        {
            btnToevoegen.IsEnabled = !string.IsNullOrEmpty(txtTaak.Text) && cbPrioriteit.SelectedItem != null && dpDate.SelectedDate != null && GetSelectedRadioButton() != null;
        }

        private void ResetForm()
        {
            txtTaak.Text = "";
            cbPrioriteit.SelectedIndex = 1;
            dpDate.SelectedDate = null;
            rb1.IsChecked = false;
            rb2.IsChecked = false;
            rb3.IsChecked = false;
        }

        private string GetSelectedRadioButton()
        {
            if (rb1.IsChecked == true)
                return rb1.Content.ToString();
            else if (rb2.IsChecked == true)
                return rb2.Content.ToString();
            else if (rb3.IsChecked == true)
                return rb3.Content.ToString();
            else
                return null;
        }

        private SolidColorBrush GetPriorityColor()
        {
            if (cbPrioriteit.SelectedItem.ToString() == "Laag")
                return Brushes.Green;
            else if (cbPrioriteit.SelectedItem.ToString() == "Normaal")
                return Brushes.Yellow;
            else
                return Brushes.Red;
        }
    }
}

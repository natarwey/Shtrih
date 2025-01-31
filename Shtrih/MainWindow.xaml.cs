using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Shtrih.ShtrihDbContext;

namespace Shtrih
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Dictionary<int, List<int>> dictA = new Dictionary<int, List<int>>(10);
        Dictionary<int, List<int>> dictB = new Dictionary<int, List<int>>(10);
        Dictionary<int, List<int>> dictC = new Dictionary<int, List<int>>(10);
        Dictionary<int, List<string>> numGroup = new Dictionary<int, List<string>>(10);
        public MainWindow()
        {
            InitializeComponent();
            FillDict();
            LoadProducts();
        }
        public void FillDict()
        {
            dictA.Add(0, new List<int> { 3, 2, 1, 1 });
            dictA.Add(1, new List<int> { 2, 2, 2, 1 });
            dictA.Add(2, new List<int> { 2, 1, 2, 2 });
            dictA.Add(3, new List<int> { 1, 4, 1, 1 });
            dictA.Add(4, new List<int> { 1, 1, 3, 2 });
            dictA.Add(5, new List<int> { 1, 2, 3, 1 });
            dictA.Add(6, new List<int> { 1, 1, 1, 4 });
            dictA.Add(7, new List<int> { 1, 3, 1, 2 });
            dictA.Add(8, new List<int> { 1, 2, 1, 3 });
            dictA.Add(9, new List<int> { 3, 1, 1, 2 });

            dictB.Add(0, new List<int> { 1, 1, 2, 3 });
            dictB.Add(1, new List<int> { 1, 2, 2, 2 });
            dictB.Add(2, new List<int> { 2, 2, 1, 2 });
            dictB.Add(3, new List<int> { 1, 1, 4, 1 });
            dictB.Add(4, new List<int> { 2, 3, 1, 1 });
            dictB.Add(5, new List<int> { 1, 3, 2, 1 });
            dictB.Add(6, new List<int> { 4, 1, 1, 1 });
            dictB.Add(7, new List<int> { 2, 1, 3, 1 });
            dictB.Add(8, new List<int> { 3, 1, 2, 1 });
            dictB.Add(9, new List<int> { 3, 1, 1, 2 });

            dictC.Add(0, new List<int> { 3, 2, 1, 1 });
            dictC.Add(1, new List<int> { 2, 2, 2, 1 });
            dictC.Add(2, new List<int> { 2, 1, 2, 2 });
            dictC.Add(3, new List<int> { 1, 4, 1, 1 });
            dictC.Add(4, new List<int> { 1, 1, 3, 2 });
            dictC.Add(5, new List<int> { 1, 2, 3, 1 });
            dictC.Add(6, new List<int> { 1, 1, 1, 4 });
            dictC.Add(7, new List<int> { 1, 3, 1, 2 });
            dictC.Add(8, new List<int> { 1, 2, 1, 3 });
            dictC.Add(9, new List<int> { 3, 1, 1, 2 });

            numGroup.Add(0, new List<string> { "A", "A", "A", "A", "A", "A" });
            numGroup.Add(1, new List<string> { "A", "A", "B", "А", "В", "B" });
            numGroup.Add(2, new List<string> { "A", "A", "B", "B", "A", "B" });
            numGroup.Add(3, new List<string> { "A", "A", "B", "B", "B", "A" });
            numGroup.Add(4, new List<string> { "A", "B", "А", "A", "B", "B" });
            numGroup.Add(5, new List<string> { "A", "B", "B", "А", "A", "B" });
            numGroup.Add(6, new List<string> { "A", "B", "В", "B", "А", "A" });
            numGroup.Add(7, new List<string> { "A", "B", "A", "B", "A", "B" });
            numGroup.Add(8, new List<string> { "A", "B", "A", "B", "B", "A" });
            numGroup.Add(9, new List<string> { "A", "B", "В", "A", "B", "A" });
        }
        private void Load(string num)
        {
            barcodeCan.Children.Clear();
            char[] nums = num.Replace(" ", "").Replace(",", "").ToArray();
            if (nums.Length != 12) return;

            List<string> numGroupNum = numGroup[Convert.ToInt32(nums[0].ToString())];

            double leftMargin = 30;
            double barcodeHeight = 207;
            double labelTopMargin = barcodeHeight + 10;

            Rectangle startRect = CreateRectangle(222, 4, new Thickness(leftMargin, 10, 0, 0), Brushes.Black);
            barcodeCan.Children.Add(startRect);
            leftMargin += startRect.Width;

            Rectangle separatorRect = CreateRectangle(222, 2, new Thickness(leftMargin, 10, 0, 0), Brushes.White);
            barcodeCan.Children.Add(separatorRect);
            leftMargin += separatorRect.Width;
            for (int i = 0; i < 6; i++)
            {
                int numIndex = Convert.ToInt32(nums[i + 1].ToString());
                string group = numGroupNum[i];
                List<int> widths = group == "A" ? dictA[numIndex] : dictB[numIndex];

                foreach (int width in widths)
                {
                    Rectangle rect = CreateRectangle(barcodeHeight, 3 * width, new Thickness(leftMargin, 10, 0, 0), Brushes.Black);
                    barcodeCan.Children.Add(rect);
                    leftMargin += rect.Width;

                    Rectangle whiteRect = CreateRectangle(barcodeHeight, 3, new Thickness(leftMargin, 10, 0, 0), Brushes.White);
                    barcodeCan.Children.Add(whiteRect);
                    leftMargin += whiteRect.Width;
                }
                TextBlock label = new TextBlock
                {
                    Text = nums[i + 1].ToString(),
                    FontSize = 14,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(leftMargin - (3 * widths.Sum() + 3 * (widths.Count - 1)) / 2, labelTopMargin, 0, 0)
                };
                barcodeCan.Children.Add(label);
            }

            Rectangle middleRect = CreateRectangle(222, 4, new Thickness(leftMargin, 10, 0, 0), Brushes.Black);
            barcodeCan.Children.Add(middleRect);
            leftMargin += middleRect.Width;
            for (int i = 7; i < 12; i++)
            {
                int numIndex = Convert.ToInt32(nums[i].ToString());
                List<int> widths = dictC[numIndex];

                foreach (int width in widths)
                {
                    Rectangle rect = CreateRectangle(barcodeHeight, 3 * width, new Thickness(leftMargin, 10, 0, 0), Brushes.Black);
                    barcodeCan.Children.Add(rect);
                    leftMargin += rect.Width;

                    Rectangle whiteRect = CreateRectangle(barcodeHeight, 3, new Thickness(leftMargin, 10, 0, 0), Brushes.White);
                    barcodeCan.Children.Add(whiteRect);
                    leftMargin += whiteRect.Width;
                }
                TextBlock label = new TextBlock
                {
                    Text = nums[i].ToString(),
                    FontSize = 14,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(leftMargin - (3 * widths.Sum() + 3 * (widths.Count - 1)) / 2, labelTopMargin, 0, 0)
                };
                barcodeCan.Children.Add(label);
            }
            Rectangle endRect = CreateRectangle(222, 4, new Thickness(leftMargin, 10, 0, 0), Brushes.Black);
            barcodeCan.Children.Add(endRect);
        }

        private Rectangle CreateRectangle(double height, double width, Thickness margin, Brush fill)
        {
            return new Rectangle
            {
                Height = height,
                Width = width,
                Margin = margin,
                Fill = fill,
                SnapsToDevicePixels = true
            };
        }

        private void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtBox.Text.Length == 12)
                {
                    Load(txtBox.Text);
                    barcodeBorder.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Введите 12 цифр.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
        private void TxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtBox.Text = new string(txtBox.Text.Where(char.IsDigit).ToArray());

            if (txtBox.Text.Length > 12)
            {
                txtBox.Text = txtBox.Text.Substring(0, 12);
                txtBox.CaretIndex = 12;
            }
        }
        public class Product
        {
            public string nameProd { get; set; }
            public string barcodeNumber { get; set; }
        }

        private void LoadProducts()
        {
            try
            {
                using (var context = new Shtrih_MDKEntities())
                {
                    // Получаем данные из таблицы Products
                    var products = context.Product
                        .Select(p => new { p.Name, p.Shtrih_number })
                        .ToList();

                    // Привязываем данные к ComboBox
                    cmbProducts.ItemsSource = products;
                    cmbProducts.DisplayMemberPath = "Name";
                    cmbProducts.SelectedValuePath = "Shtrih_number";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
            }
        }

        private void CmbProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbProducts.SelectedValue != null)
            {
                string barcode = cmbProducts.SelectedValue.ToString();
                txtBox.Text = barcode;
            }
        }
    }
}

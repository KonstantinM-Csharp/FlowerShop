using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using FlowerShop.Data;
using FlowerShop.Data.Models;
using FlowerShop.Services;
using FlowerShop.UI.UC;
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
using System.Xml.Linq;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;
using Run = DocumentFormat.OpenXml.Wordprocessing.Run;
using Table = DocumentFormat.OpenXml.Wordprocessing.Table;
using TableCell = DocumentFormat.OpenXml.Wordprocessing.TableCell;
using TableRow = DocumentFormat.OpenXml.Wordprocessing.TableRow;

namespace FlowerShop.UI.Pg.ClientPgs
{
    /// <summary>
    /// Логика взаимодействия для CheckPg.xaml
    /// </summary>
    public partial class CheckPg : Page
    {
        public string Fullname {
            get;
            set;
        }
        public string Number { get; set; }
        public decimal CommonPrice { get; set; }
        public CheckPg()
        {
            InitializeComponent();
            LoadBasketItems();
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            LoadBasketItems();
            userDataTxtBlck.Text = AutorizationService.User.FirstName +" "+ AutorizationService.User.LastName;
            userPhoneTxtBlck.Text = AutorizationService.User.Phone;
            commonPriceTxtBlck.Text = AutorizationService.LastOrder.TotalAmount.ToString();
        }
        private void LoadBasketItems()
        {
            LVOrderItem.ItemsSource = null;
            if(AutorizationService.LastOrder!=null)
                     LVOrderItem.ItemsSource = FlowerMagicEntities.GetContext().OrderItems.Where(x=>x.Order.Id==AutorizationService.LastOrder.Id).ToList();
        }

        private void OkBttn_Click(object sender, RoutedEventArgs e)
        {
            BasketService.ClearBasket();
            VisibilityWindowsService.OpenCatalogPg();
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                // Создаем файл
                string filePath = "order.docx";

                using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(filePath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
                {
                    MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                    mainPart.Document = new Document();
                    Body body = new Body();

                    // Создаем таблицу
                    Table table = new Table();

                    // Создаем свойства таблицы (границы)
                    TableProperties tblProps = new TableProperties(
                        new TableBorders(
                            new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                            new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                            new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                            new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                            new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                            new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 }
                        )
                    );
                    table.AppendChild(tblProps);

                    // Добавляем заголовок
                    AddRowToTable(table, "Чек", 2);

                    // Добавляем дату и время заказа
                    AddRowToTable(table, $"Дата и время заказа: {DateTime.Now}", 2);

                    // Добавляем данные из TextBlock
                    AddRowToTable(table, "Магазин Магия цветов", 2);
                    AddRowToTable(table, "Покупатель:", userDataTxtBlck.Text);
                    AddRowToTable(table, "Телефон", userPhoneTxtBlck.Text);

                    // Добавляем заголовки столбцов для ListView
                    AddRowToTable(table, "Название", "Количество", "Стоимость");

                    // Добавляем данные из ListView
                    foreach (var item in LVOrderItem.Items)
                    {
                        if (LVOrderItem.ItemContainerGenerator.ContainerFromItem(item) is ListViewItem listViewItem)
                        {
                            CheckItem checkItem = FindVisualChild<CheckItem>(listViewItem);
                            if (checkItem != null)
                            {
                                var textBlocks = FindVisualChildren<TextBlock>(checkItem).ToList();
                                if (textBlocks.Count == 3)
                                {
                                    string bouquetName = textBlocks[0].Text;
                                    string bouquetCount = textBlocks[1].Text;
                                    string bouquetCost = textBlocks[2].Text;
                                    AddRowToTable(table, bouquetName, bouquetCount, bouquetCost);
                                }
                            }
                        }
                    }

                    // Добавляем итоговую сумму
                    AddRowToTable(table, "Итого:", commonPriceTxtBlck.Text);

                    body.Append(table);
                    mainPart.Document.Append(body);
                    mainPart.Document.Save();
                }

                MessageBox.Show("Документ сохранен как " + filePath);
                }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddRowToTable(Table table, string col1Text, string col2Text)
        {
            TableRow tr = new TableRow();

            TableCell tc1 = new TableCell(new Paragraph(new Run(new Text(col1Text))));
            TableCell tc2 = new TableCell(new Paragraph(new Run(new Text(col2Text))));

            tr.Append(tc1, tc2);
            table.Append(tr);
        }

        private void AddRowToTable(Table table, string col1Text, int colSpan = 1)
        {
            TableRow tr = new TableRow();
            TableCellProperties tcp = new TableCellProperties();
            if (colSpan > 1)
            {
                GridSpan gridSpan = new GridSpan() { Val = colSpan };
                tcp.Append(gridSpan);
            }

            TableCell tc = new TableCell(new Paragraph(new Run(new Text(col1Text))));
            tc.Append(tcp);
            tr.Append(tc);
            table.Append(tr);
        }

        private void AddRowToTable(Table table, string col1Text, string col2Text, string col3Text)
        {
            TableRow tr = new TableRow();

            TableCell tc1 = new TableCell(new Paragraph(new Run(new Text(col1Text))));
            TableCell tc2 = new TableCell(new Paragraph(new Run(new Text(col2Text))));
            TableCell tc3 = new TableCell(new Paragraph(new Run(new Text(col3Text))));

            // Устанавливаем свойства для ячеек таблицы
            TableCellProperties tcp1 = new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Auto });
            TableCellProperties tcp2 = new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Auto });
            TableCellProperties tcp3 = new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Auto });
            tc1.Append(tcp1);
            tc2.Append(tcp2);
            tc3.Append(tcp3);

            tr.Append(tc1, tc2, tc3);
            table.Append(tr);
        }

        // Вспомогательный метод для поиска дочернего элемента заданного типа
        public static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T typedChild)
                {
                    return typedChild;
                }

                T childOfChild = FindVisualChild<T>(child);
                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }
            return null;
        }

        // Вспомогательный метод для поиска всех дочерних элементов заданного типа
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                    if (child is T typedChild)
                    {
                        yield return typedChild;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}

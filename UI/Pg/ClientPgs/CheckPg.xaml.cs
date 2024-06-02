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

namespace FlowerShop.UI.Pg.ClientPgs
{
    /// <summary>
    /// Логика взаимодействия для CheckPg.xaml
    /// </summary>
    public partial class CheckPg : Page
    {
        public CheckPg()
        {
            InitializeComponent();
            LoadBasketItems();
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            LoadBasketItems();

        }
        private void LoadBasketItems()
        {
            LVOrderItem.ItemsSource = null;
            LVOrderItem.ItemsSource = BasketService.GetBasket();
        }

        private void OkBttn_Click(object sender, RoutedEventArgs e)
        {
            BasketService.ClearBasket();
            VisibilityWindowsService.OpenCatalogPg();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Создаем файл
            string filePath = "order.docx";

            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(filePath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = new Body();

                // Добавляем текст заголовка
                AddTextToBody(body, "Чек");

                // Проходим по каждому элементу ListView
                foreach (var item in LVOrderItem.Items)
                {
                    // Предполагаем, что TextBlock находится внутри UserControl
                    if (LVOrderItem.ItemContainerGenerator.ContainerFromItem(item) is ListViewItem listViewItem)
                    {
                        CheckItem checkItem = FindVisualChild<CheckItem>(listViewItem);
                        if (checkItem != null)
                        {
                            foreach (TextBlock textBlock in FindVisualChildren<TextBlock>(checkItem))
                            {
                                AddTextToBody(body, textBlock.Text);
                            }
                        }
                    }
                }

                mainPart.Document.Append(body);
            }

            MessageBox.Show("Документ сохранен как " + filePath);
        }

        private void AddTextToBody(Body body, string text)
        {
            Paragraph para = new Paragraph();
            Run run = new Run();
            run.Append(new Text(text));
            para.Append(run);
            body.Append(para);
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

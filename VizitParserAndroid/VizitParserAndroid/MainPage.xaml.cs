using HtmlAgilityPack;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VizitParserAndroid
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            FillListBox();
        }

        private void FillListBox()
        {
            string url = "https://www.vizit.ks.ua/novosti/";
            HtmlWeb webDoc = new HtmlWeb();
            HtmlDocument doc = webDoc.Load(url);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//h2[@class='entry-title']/a");
            if (nodes != null)
            {
                foreach (HtmlNode node in nodes)
                {
                    Label label = new Label();
                    label.Text = node.InnerText.Replace("&#8220;", "'").Replace("&#8221;", "'");
                    label.FontSize = 20;
                    label.Margin = new Thickness(5, 2, 5, 2);
                    label.HorizontalTextAlignment = TextAlignment.Center;
                    var forgetPassword_tap = new TapGestureRecognizer();
                    forgetPassword_tap.Tapped += (s, e) =>
                    {
                        Browser.OpenAsync(node.Attributes[0].Value, BrowserLaunchMode.SystemPreferred);
                    };
                    label.GestureRecognizers.Add(forgetPassword_tap);
                    if (node.InnerText.ToLower().Contains("вод"))
                    {
                        label.TextColor = Color.Red;
                    }
                    MainLayout.Children.Add(label);
                }
            }
            else
            {
                Label label = new Label();
                label.Text = "Не удалось получить заголовки новостей";
                label.TextColor = Color.Red;
                label.FontSize = 20;
                MainLayout.Children.Add(label);
            }
        }
    }
}
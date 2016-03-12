using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using Windows.Storage;
using CoreTweet;
using uniApp1.Class;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace uniApp1
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {

    internal Tokens tokens;
    Tweets data = new Tweets();
    public MainPage()
        {
            this.InitializeComponent();
      var reg = new uniApp1.Class.ClientData();
      tokens = data.getToken();
      nameChange();
      this.testFrame.Navigate(typeof(Pages.HomeFrame));
      //this.testFrame.Navigate(typeof(Pages.MainFrame));


      SystemNavigationManager.GetForCurrentView().BackRequested += (_, args) =>
      {
        if (testFrame.CanGoBack)
        {
          testFrame.GoBack();
          args.Handled = true;
        }
      };/**/
      //UpdateBackButtonState();
    }

    private void UpdateBackButtonState()
    {
      var rootFrame = (Frame)Window.Current.Content;
      SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = rootFrame.CanGoBack ?
          AppViewBackButtonVisibility.Visible :
          AppViewBackButtonVisibility.Collapsed;

    }

    private void nameChange()
    {
      var settings = ApplicationData.Current.RoamingSettings;
      titleBlock.Text = (string)settings.Values["ScreenName"];
    }

    private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
    {
      // 適切な移動先のページに移動し、新しいページを構成します。
      // このとき、必要な情報をナビゲーション・パラメータとして渡します
      this.testFrame.Navigate(typeof(Pages.HomeFrame));
      //this.testFrame.Navigate(typeof(Pages.MainFrame));
    }

    private void Grid_Tapped_1(object sender, TappedRoutedEventArgs e)
    {
      this.testFrame.Navigate(typeof(Pages.HomeFrame));
      //this.testFrame.Navigate(typeof(Pages.MainFrame));

    }

    private void Grid_Tapped_2(object sender, TappedRoutedEventArgs e)
    {

      this.testFrame.Navigate(typeof(SettingsFrame));

    }

    private void Grid_Tapped_3(object sender, TappedRoutedEventArgs e)
    {
      var settings = ApplicationData.Current.RoamingSettings;
      this.testFrame.Navigate(typeof(Pages.UserPage), (long?)settings.Values["UserId"]);

    }

    private void Grid_Tapped_4(object sender, TappedRoutedEventArgs e)
    {
      this.testFrame.Navigate(typeof(Pages.MentionPage));
    }

    private void Grid_Tapped_5(object sender, TappedRoutedEventArgs e)
    {
      this.testFrame.Navigate(typeof(Pages.SearchPage));
    }

    private void Grid_Tapped_6(object sender, TappedRoutedEventArgs e)
    {
      this.testFrame.Navigate(typeof(Pages.StreamingPage));
    }
  }
}

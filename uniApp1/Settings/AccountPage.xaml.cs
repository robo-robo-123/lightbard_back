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
using CoreTweet;
using Windows.Storage;
using uniApp1.Class;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace uniApp1.Settings
{
  /// <summary>
  /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
  /// </summary>
  public sealed partial class AccountPage : Page
  {
    OAuth.OAuthSession session;
    internal Tokens tokens;
    public AccountPage()
    {
      this.InitializeComponent();
      initAuthrize();

    }
    private void registButton_Tapped(object sender, TappedRoutedEventArgs e)
    {

    }

    private async void token_session()
    {
      var settings = ApplicationData.Current.RoamingSettings;

      session = await OAuth.AuthorizeAsync((string)settings.Values["ApiKey"]

          , (string)settings.Values["ApiSecret"]);

    }

    private async void initAuthrize()

    {

      try

      { 
        var settings = ApplicationData.Current.RoamingSettings;

        session = await OAuth.AuthorizeAsync((string)settings.Values["ApiKey"]

            , (string)settings.Values["ApiSecret"]);

        authWeb.Source = session.AuthorizeUri;


        //pinURITextBox.Text = session.AuthorizeUri.ToString();

        //pinTextBox.e.ClearValuer();

        // System.Diagnostics.Process.Start(session.AuthorizeUri.ToString());

      }

      catch (Exception ex)

      {
        pinTextBox.Text = "sippsi";
        // in this example we assume the parent of the UserControl is a Popup 
        Popup p = this.Parent as Popup;

        // close the Popup
        if (p != null) { p.IsOpen = false; }

        //Close();

      }

    }
/*
    private void cancelButton_Click(object sender, RoutedEventArgs e)
    {
      Properties.Settings.Default.AccessToken = null;

      Properties.Settings.Default.AccessTokenSecret = null;

      Properties.Settings.Default.ScreenName = null;

      Properties.Settings.Default.Save();

      var dialog = new ModernDialog1("ユーザ情報を削除しました");
      dialog.ShowDialog();
    }
*/
    private async void okButton_Click(object sender, RoutedEventArgs e)
    {
      if (string.IsNullOrEmpty(pinTextBox.Text)

          || System.Text.RegularExpressions.Regex.IsMatch(pinTextBox.Text, @"\D"))

      {

        //        MessageBox.Show("Type numeric characters");

       // var dialog = new ModernDialog1("Type numeric characters");
       // dialog.ShowDialog();

        //pinTextBox.Clear();

        return;

      }



      try

      {

        // PIN認証

        //MainWindow2 owner = (MainWindow2)this.Owner;
        //SettingWindow owner = (SettingWindow)this.Owner;

        tokens = await session.GetTokensAsync(pinTextBox.Text);

        // トークン保存

        var settings = ApplicationData.Current.RoamingSettings;
        settings.Values["AccessToken"] = tokens.AccessToken;
        settings.Values["AccessTokenSecret"] = tokens.AccessTokenSecret;
        settings.Values["ScreenName"] = tokens.ScreenName;
        settings.Values["UserId"] = tokens.UserId;

        // 表示調整

        //owner.updatescreennameLabel(owner.tokens.ScreenName);



        //  MessageBox.Show("verified: " + tokens.ScreenName);
        //  var dialog = new ModernDialog1("verified: " + tokens.ScreenName + "一旦このアプリを再起動してください。");
        //  dialog.ShowDialog();
        //  pinTextBox.Clear();

        // Close();

      }

      catch (Exception ex)

      {

        // やり直し

        //MessageBox.Show(ex.Message);
     //   var dialog = new ModernDialog1(ex.Message);
      //  dialog.ShowDialog();

      //  initAuthrize();

      }



    }

    private void startButton_Click(object sender, RoutedEventArgs e)
    {
      initAuthrize();
    }


  }
}

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
using MyToolkit;
using Windows.UI.Core;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace uniApp1
{
  /// <summary>
  /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
  /// </summary>
  public sealed partial class SettingsFrame : Page
  {
    OAuth.OAuthSession session;
    internal Tokens tokens;

    public SettingsFrame()
    {
      this.InitializeComponent();
      version();

      this.blockFrame.Navigate(typeof(Pages.BlockPage));
      release();

    }



private void BackButton_Click(object sender, RoutedEventArgs e)
    {
      if (rootPivot.SelectedIndex > 0)
      {
        // If not at the first item, go back to the previous one.
        rootPivot.SelectedIndex -= 1;
      }
      else
      {
        // The first PivotItem is selected, so loop around to the last item.
        rootPivot.SelectedIndex = rootPivot.Items.Count - 1;
      }
    }

    private void NextButton_Click(object sender, RoutedEventArgs e)
    {
      if (rootPivot.SelectedIndex < rootPivot.Items.Count - 1)
      {
        // If not at the last item, go to the next one.
        rootPivot.SelectedIndex += 1;
      }
      else
      {
        // The last PivotItem is selected, so loop around to the first item.
        rootPivot.SelectedIndex = 0;
      }
    }

    private void PivotItem_Tapped(object sender, TappedRoutedEventArgs e)
    {
      ///this.testFrame.Navigate(typeof(Settings.AccountPage));

    }

    private void registButton_Click(object sender, RoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(Settings.AccountPage));
    }

    private void version()
    {
      var versionInfo = Windows.ApplicationModel.Package.Current.Id.Version;
      string version = string.Format(
                         "{0}.{1}.{2}.{3}",
                         versionInfo.Major, versionInfo.Minor,
                         versionInfo.Build, versionInfo.Revision);
      versionBlock.Text = version;
    }
    private void release()
    {
      var str = "2015/12/02にVer.2を公開。\n2015/12/05に会話機能を追加";
      releaseBlock.Text = str;
    }
  }
}

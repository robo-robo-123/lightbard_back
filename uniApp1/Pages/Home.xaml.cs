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
using Windows.Storage;
using uniApp1.Class;
using CoreTweet;
using Windows.UI.Popups;
using Windows.Storage.Pickers;
using Windows.UI.Notifications;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace uniApp1.Pages
{
  /// <summary>
  /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
  /// </summary>
  public sealed partial class Home : Page
  {

    internal Tokens tokens;
    Tweets data = new Tweets();

    List<TweetClass.TweetInfo> tweet;
    List<TweetClass.TweetInfo> userTweet;

    List<TweetClass.UserInfo> user;
    List<TweetClass.UserInfo> userPro;
    public long? UserId { get; set; }
    TweetClass.TweetInfo item;
    public long? ReplyId { get; set; }

    public string userId { get; set; }
    public string ScreenName { get; set; }
    public string filename { get; set; }
    public FileInfo fileinfo { get; set; }

    FileOpenPicker openPicker = new FileOpenPicker();
    StorageFile file;
    public Home()
    {
      this.InitializeComponent();

      tokens = data.getToken();
      if (item == null)
      {
        tweetLoad();
      }


      //tweetLoad();
      var settings = ApplicationData.Current.RoamingSettings;
    }


    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      item = (TweetClass.TweetInfo)e.Parameter;
      
      if(item != null /*&& e.SourcePageType.Name == */)
      {
        UserId = item.UserId;
        userTweetLoad();
      }



    }

    //tweetをロードするのに使います
    private async void tweetLoad()
    {
      if (tokens != null)
      {
        tweet = new List<TweetClass.TweetInfo>();
        try
        {
          tweet = new List<TweetClass.TweetInfo>();


          foreach (var status in await tokens.Statuses.HomeTimelineAsync(count => 800))
          {
            data.Addtweet(tweet, status);
          }
          this.listView.ItemsSource = tweet;
        }
        catch (Exception ex)
        {
        }
      }
    }

    private async void userTweetLoad()
    {
      if (tokens != null)
      {
        tweet = new List<TweetClass.TweetInfo>();
        try
        {
          tweet = new List<TweetClass.TweetInfo>();


          foreach (var status in await tokens.Statuses.UserTimelineAsync(user_id => UserId, count => 800))
          {
            data.Addtweet(tweet, status);
          }
          this.listView.ItemsSource = tweet;
        }
        catch (Exception ex)
        {
        }
      }
    }

    private async void mentionLoad()
    {
      if (tokens != null)
      {
        tweet = new List<TweetClass.TweetInfo>();
        try
        {
          tweet = new List<TweetClass.TweetInfo>();


          foreach (var status in await tokens.Statuses.MentionsTimelineAsync(count => 800))
          {
            data.Addtweet(tweet, status);
          }
          this.listView.ItemsSource = tweet;
          
        }
        catch (Exception ex)
        {
        }
      }
    }

    //ロードボタンです．
    private void reloadButton_Click(object sender, RoutedEventArgs e)
    {
      if (item == null)
      {
        tweetLoad();
      }
      else
      {
        userTweetLoad();
      }
    }

    //リプライページに飛びます．※ツイートに関しては，下のツイートと共通化させたい．
    private void replyButton_Click(object sender, RoutedEventArgs e)
    {
      var item = this.listView.SelectedItem as TweetClass.TweetInfo;
      if (item == null)
      {
        return;
      }
      else
      {
        var item_send = this.listView.SelectedItem as TweetClass.TweetInfo;
        this.Frame.Navigate(typeof(ReplayPage), item_send);
        //rootFrame.Navigate(typeof(ReplayPage), item_send);
      }

    }

    private void toast(string text1)
    {
      // テンプレートのタイプを取得
      var template = ToastTemplateType.ToastText01;
      // テンプレートを取得（XMLDocument"
      var toastXml = ToastNotificationManager.GetTemplateContent(template);
      // textタグを取得（ここに文字列が入る）
      var textTag = toastXml.GetElementsByTagName("text").First();
      // 子要素に文字列を追加
      textTag.AppendChild(toastXml.CreateTextNode(text1));

      // Notifierを作成してShowメソッドで通知
      var notifier = ToastNotificationManager.CreateToastNotifier();
      notifier.Show(new ToastNotification(toastXml));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    
    //お気に入り．
    private async void favasync(long itemid)
    {
      try { 
     await tokens.Favorites.CreateAsync(id => itemid);
      //  favoriteBlock.Text = "'いいね'しました";
        
      }
      catch
      {
       // favoriteBlock.Text = "既に'いいね'しています";
      }
    }

    private void favoriteButton_Click(object sender, RoutedEventArgs e)
    {
      var item = this.listView.SelectedItem as TweetClass.TweetInfo;
      if (item == null)
      {
      //  favoriteBlock.Text = "未選択です";
        return;
      }
      else
      {
          favasync(item.Id);
        var tes = "'いいね'しました";
        toast(tes);
      }
    }

    //リツイート
    private async void retweetasync(long itemid)
    {
      try
      {
        await tokens.Statuses.RetweetAsync(id => itemid);
        //retweetBlock.Text = "リツイートしました";
       // var dlg = new MessageDialog("test", "リツイートしました");
       // await dlg.ShowAsync();
      }
      catch (Exception ex)
      {
        //retweetBlock.Text = "既にリツイートしています";
      }

    }

    private void retweetButton_Click(object sender, RoutedEventArgs e)
    {
      var item = this.listView.SelectedItem as TweetClass.TweetInfo;
      if (item == null)
      {
       // retweetBlock.Text = "未選択です";
        return;
      }
      else
      {
        retweetasync(item.Id);
        var tes = "リツイートしました";
        toast(tes);

      }
    }
    
    ///

    //userinfo
    private void userInfoCommand_Click(object sender, RoutedEventArgs e)
    {
      var item = this.listView.SelectedItem as TweetClass.TweetInfo;
      if (item == null)
      {
        return;
      }
      else
      {
        var item_send = this.listView.SelectedItem as TweetClass.TweetInfo;
        this.Frame.Navigate(typeof(UserPage), item_send.UserId);
        //rootFrame.Navigate(typeof(ReplayPage), item_send);
      }
    }

    private void profileImage_Tapped(object sender, TappedRoutedEventArgs e)
    {
      var item = this.listView.SelectedItem as TweetClass.TweetInfo;
      if (item == null)
      {
        return;
      }
      else
      {
        var item_send = this.listView.SelectedItem as TweetClass.TweetInfo;
        this.Frame.Navigate(typeof(UserPage), item_send.UserId);
        //rootFrame.Navigate(typeof(ReplayPage), item_send);
      }
  }

  private void listView_RightTapped(object sender, RightTappedRoutedEventArgs e)
    {
      FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);

    }

    private void MenuFlyoutItem_Tapped(object sender, TappedRoutedEventArgs e)
    {

    }

    private void userInfoItem_Tapped(object sender, TappedRoutedEventArgs e)
    {
      var item = this.listView.SelectedItem as TweetClass.TweetInfo;
      if (item == null)
      {
        return;
      }
      else
      {
        var item_send = this.listView.SelectedItem as TweetClass.TweetInfo;
        this.Frame.Navigate(typeof(UserPage), item_send.UserId);
        //rootFrame.Navigate(typeof(ReplayPage), item_send);
      }
    }

    private void TweetsList_Tapped(object sender, TappedRoutedEventArgs e)
    {
      FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
    }

    private void conveItem_Click(object sender, RoutedEventArgs e)
    {
      var item = this.listView.SelectedItem as TweetClass.TweetInfo;
      if (item == null)
      {
        return;
      }
      else
      {
        var item_send = this.listView.SelectedItem as TweetClass.TweetInfo;
        this.Frame.Navigate(typeof(ConvePage), item_send.Id);
        //rootFrame.Navigate(typeof(ReplayPage), item_send);
      }
    }

    private void tweetButton_Click(object sender, RoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(TweetPage));
    }
  }

  //ツイートに関して．画像も送れるようにしたい．将来的にはリプのページと共通化したい
  /*
  private async void tweetsend(string text)
  {
    try
    {
      await tokens.Statuses.UpdateAsync(status => text);
      tweetState.Text = "ツイート成功";
      tweetInputBox.Text = "";
    }
    catch
    {
      tweetState.Text = "ツイート失敗";
    }
  }
  */
  /*
  private void tweetSendButton_Click(object sender, RoutedEventArgs e)
  {
    tweetsend(tweetInputBox.Text);
  }
  */
  /*
  private async void filepickersync(StorageFile file)
  {
    file = await openPicker.PickSingleFileAsync();
  }
  */
  /*
  private void photoButtom_Click(object sender, RoutedEventArgs e)
  {
    // ファイルを開くダイアログ
    // using Windows.Storage.Pickers;


    // 表示モードはリスト形式
    //openPicker.ViewMode = PickerViewMode.List;

    // 表示モードはサムネイル形式
    openPicker.ViewMode = PickerViewMode.Thumbnail;

    // ピクチャーライブラリーが起動時の位置
    // その他候補はPickerLocationIdを参照
    // http://msdn.microsoft.com/en-us/library/windows/apps/windows.storage.pickers.pickerlocationid
    openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

    // jpg, jpeg, pngのファイル形式から選択
    openPicker.FileTypeFilter.Add(".jpg");
    openPicker.FileTypeFilter.Add(".jpeg");
    openPicker.FileTypeFilter.Add(".png");

    // ファイルオープンピッカーを起動する

    filepickersync(file);


    if (null != file)
    {
      tweetState.Text = file.Name;
      tweetInputBox.Text = file.Name;

      // FileNameText,FilePathTextはXASMLで定義されたTextBlockコントロール

      //var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
      //BitmapImage image = new BitmapImage();
      //image.SetSource(stream);

      // FileImageはXAMLで定義されたコントロール
      //sendImage1.Source = image;
    }
    else
    {

    }


  }*/


  /*
      private void tweetButton_Click(object sender, RoutedEventArgs e)
      {
        tweetState.Text = "";
      }
  */


  /*
  public void tweetMethod(string text, FileInfo filename = null, long? replyid = 0)
  {
    // getToken();
    try
    {
      if (filename == null)
      {
        tokens.Statuses.Update(status => text);
        dialog.ShowDialog();
      }
      else
      {
        //tokens.Statuses.UpdateWithMedia(status => text, media => filename);
        var mid = tokens.Media.Upload(media => filename);
        tokens.Statuses.Update(status => text, media_ids => mid);
        //, media_ids => mid, media_ids => mid, media_ids => mid
        //tokens.Media.Upload(media => fileinfo);
        dialog.ShowDialog();
      }
    }
    catch (Exception ex)
    {
      dialog.ShowDialog();
    }
  }
  */

  //ツイート処理はここまで．
}

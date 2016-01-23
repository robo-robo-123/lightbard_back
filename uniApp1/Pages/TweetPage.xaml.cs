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
using Windows.UI.Xaml.Media.Imaging;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Core;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace uniApp1.Pages
{
  /// <summary>
  /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
  /// </summary>
  public sealed partial class TweetPage : Page
  {
    internal Tokens tokens;

    public long? ReplyId { get; set; }
    public byte[] media2 { get; set; } //= null;
    TweetClass.TweetInfo item;

    public string userId { get; set; }
    public string ScreenName { get; set; }
    public string filename { get; set; }
    public MediaUploadResult mid { get; set; }
    public MediaUploadResult[] mids { get; set; }
    public Stream stream { get; set; }
    Tweets data = new Tweets();
    public byte[] bytes { get; set; }
    public byte[][] bytesarray { get; set; }
    public int filenum { get; set; }

    public byte[] bytes1 { get; set; }
    public byte[] bytes2 { get; set; }
    public byte[] bytes3 { get; set; }
    public byte[] bytes4 { get; set; }

    public TweetPage()
    {
      this.InitializeComponent();
      tokens = data.getToken();
      clear();

      SystemNavigationManager.GetForCurrentView().BackRequested += (_, args) =>
      {
        if (Frame.CanGoBack)
        {
          Frame.GoBack();
          args.Handled = true;
        }
      };

    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      item = (TweetClass.TweetInfo)e.Parameter;
      if (item != null)
      {
        //tweetState.Text = e.Content.ToString();
        userBlock.Text = "Reply to " + item.ScreenName;
        this.tweetInputBox.Text = item.ScreenName + " ";
      }
      else
      {
        userBlock.Text = "What's happen?";
      }
    }


    private void tweetSendButton_Click(object sender, RoutedEventArgs e)
    {
      tweetMethod(tweetInputBox.Text);
    }

    private async void photoButtom_Click(object sender, RoutedEventArgs e)
    {
      bytes1 = null;
      bytes2 = null;
      bytes3 = null;
      bytes4 = null;

      // ファイルを開くダイアログ
      // using Windows.Storage.Pickers;
      FileOpenPicker openPicker = new FileOpenPicker();

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
      IReadOnlyList<StorageFile> files = await openPicker.PickMultipleFilesAsync();

      if (null != files)
      {
        int n = 0;
        foreach (StorageFile file in files)
        {
          bytes = null;
          // FileNameText,FilePathTextはXASMLで定義されたTextBlockコントロール
          var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
          BitmapImage image = new BitmapImage();
          image.SetSource(stream);

          var reader = new DataReader(stream.GetInputStreamAt(0));
          bytes = new byte[stream.Size];
          await reader.LoadAsync((uint)stream.Size);
          reader.ReadBytes(bytes);

          filenum = files.Count;
          media2 = null;
          // FileImageはXAMLで定義されたコントロール
          switch (n)
          {
            case 0:
              sendImage1.Source = image;
              bytes1 = bytes;
              break;
            case 1:
              sendImage2.Source = image;
              bytes2 = bytes;
              break;
            case 2:
              sendImage3.Source = image;
              bytes3 = bytes;
              break;
            case 3:
              sendImage4.Source = image;
              bytes4 = bytes;
              break;
          }
          n++;
        }
        //uploadButton.IsEnabled = true;
      }
      else
      {
        tweetState.Text = "なにも選択できていません";
      }
    }

    private void tweetButton_Click(object sender, RoutedEventArgs e)
    {
      tweetState.Text = "";
    }

    public async void tweetMethod(string text)
    {
      // getToken();
      try
      {
        if (mids == null)
        {
          var x = await tokens.Statuses.UpdateAsync(status => text);
          tweetState.Text = "ツイート成功";
          clear();
      }
        else
        {
          Status s = await tokens.Statuses.UpdateAsync(
            status => text,
            media_ids => mids.Select(x => x.MediaId)
            );
          //初期化
          tweetState.Text = "ツイート成功";
          clear();
        }
      }
      catch (Exception ex)
      {
        //  tweetState.Text = ex.Message;
        // tweetState.Text = "送信エラー";
      }
    }

    private void clear()
    {
      tweetState.Text = "";
      tweetInputBox.Text = "";
      bytes = null;
      sendImage1.Source = null;
      sendImage2.Source = null;
      sendImage3.Source = null;
      sendImage4.Source = null;
      mid = null;
      mids = null;
      bytes1 = null;
      bytes2 = null;
      bytes3 = null;
      bytes4 = null;
     // uploadButton.IsEnabled = false;
    }

    private void clearButtom_Click(object sender, RoutedEventArgs e)
    {
      clear();
    }

    private async void uploadButton_Click(object sender, RoutedEventArgs e)
    {
      try
      {
        if (bytes1 != null && bytes2 == null && bytes3 == null && bytes4 == null)
        {
          mids = await Task.WhenAll(
            tokens.Media.UploadAsync(media => bytes1)
            );
          tweetState.Text = "upload success!!";
        }
        else if (bytes1 != null && bytes2 != null && bytes3 == null && bytes4 == null)
        {
          mids = await Task.WhenAll(
            tokens.Media.UploadAsync(media => bytes1),
            tokens.Media.UploadAsync(media => bytes2)
            );
          tweetState.Text = "upload success!!";
        }
        else if (bytes1 != null && bytes2 != null && bytes3 != null && bytes4 == null)
        {
          mids = await Task.WhenAll(
            tokens.Media.UploadAsync(media => bytes1),
            tokens.Media.UploadAsync(media => bytes2),
            tokens.Media.UploadAsync(media => bytes3)
            );
          tweetState.Text = "upload success!!";
        }
        else if (bytes1 != null && bytes2 != null && bytes3 != null && bytes4 != null)
        {
          mids = await Task.WhenAll(
            tokens.Media.UploadAsync(media => bytes1),
            tokens.Media.UploadAsync(media => bytes2),
            tokens.Media.UploadAsync(media => bytes3),
            tokens.Media.UploadAsync(media => bytes4)
            );
          tweetState.Text = "upload success!!";
        }
        else
        {
          tweetState.Text = "no image";
        }

        /*
        Status s = await tokens.Statuses.UpdateAsync(
          status => tweetInputBox.Text,
          media_ids => mids.Select(x => x.MediaId)
          );

        //初期化
        tweetState.Text = "ツイート成功";
        clear();
        */
        //  tweetState.Text = "upload success!!";


      }
      catch (Exception ex)
      {
        tweetState.Text = ex.Message;
      }

    }
  }
}

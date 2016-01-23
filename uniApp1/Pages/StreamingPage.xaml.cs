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
using uniApp1.Class;
// using System;
using Windows.UI.Core;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using CoreTweet;
using CoreTweet.Streaming;
using Windows.UI.Notifications;
using Windows.Storage;
//using CoreTweet.Rest;
//using CoreTweet.Streaming.Reactive;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace uniApp1.Pages
{
  /// <summary>
  /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
  /// </summary>
  public sealed partial class StreamingPage : Page
  {

    internal Tokens tokens;
    Tweets data = new Tweets();

    public StreamingPage()
    {
      this.InitializeComponent();
      tokens = data.getToken();


      //this.frame1.Navigate(typeof(Pages.Home));
      this.frame2.Navigate(typeof(Pages.MentionPage));




      // tokens.Streaming.UserAsObservable().Subscribe((StreamingMessage m) => test(m));

      streamingtest();


      //Console.WriteLine(m));

      //Thread.Sleep(30000);
      /*
      foreach (var m in tokens.Streaming.Filter(track: "茶")
            .OfType<StatusMessage>()
            .Select(x => x.Status)
            .Take(10))
        Console.WriteLine("お茶についてのツイート {0}: {1}", m.User.ScreenName, m.Text);


    }
    */
    }

    private async void streamingtest()
    {
      var stream = tokens.Streaming.UserAsObservable().Publish();



      //stream.OfType<FriendsMessage>().Subscribe(x => Console.WriteLine("フォロー中: " + string.Join(", ", x)));

      //stream.OfType<StatusMessage>().Subscribe(x => Console.WriteLine("{0}: {1}", x.Status.User.ScreenName, x.Status.Text));

      stream.OfType<StatusMessage>().Subscribe(x => test(x.Status.User.ScreenName + ":" + x.Status.Text + "\n"));
      stream.OfType<StatusMessage>().Subscribe(x => test2(x));
      //stream.OfType<EventMessage>().Subscribe(x => test3(x));

      var disposable = stream.Connect();
      //testBlock.Text = "接続中です";

      await Task.Delay(300 * 1000);
      disposable.Dispose();
      //testBlock.Text = "接続終了";


    }

    /*
    private void test3(EventMessage x)
    {
      
    }
    */

    private void test2(StatusMessage x)
    {
      var settings = ApplicationData.Current.RoamingSettings;
      var myname = (string)settings.Values["ScreenName"];

      if(myname == x.Status.User.ScreenName)
      {
        toast("ツイートができました！");
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

    private async void test(string x)

    {
      await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
      {

        testBlock.Text += x;
        //testBlock.Text = x.Status.User.ScreenName, x.Status.Text;
      });
    }
  }
}

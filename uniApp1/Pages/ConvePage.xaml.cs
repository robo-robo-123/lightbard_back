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
using uniApp1.Class;
using Windows.UI.Core;
using System.Collections.ObjectModel;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace uniApp1.Pages
{
  /// <summary>
  /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
  /// </summary>
  public sealed partial class ConvePage : Page
  {
    internal Tokens tokens;
    ObservableCollection<TweetClass.TweetInfo> tweet;
    Tweets data = new Tweets();
    TweetClass.TweetInfo item;
    public Status status { get; set; }
    public ConvePage()
    {
      this.InitializeComponent();
      tokens = data.getToken();

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
      //item = (TweetClass.TweetInfo)e.Parameter;
      var id  = (long?)e.Parameter;
      //replyBox.Text = item.ToString();
      //show();
      //loadTweet(item.Id);
      tweet = new ObservableCollection<TweetClass.TweetInfo>();
      conv(id);

    }



    private async void conv(long? Id)
    {
      status = await tokens.Statuses.ShowAsync(id => Id);
      data.Addtweet(tweet, status);
      try
      {
        //if (status.ExtendedEntities.UserMentions[0].Id != null)
        //{
        if (status.InReplyToStatusId != null)
        { 
          var rep_status = status.InReplyToStatusId;
          //checkBlock.Text = rep_status.ToString();
        conv(rep_status);
        }
        else
        {
          conveView.ItemsSource = tweet;
        }
      }
      catch(Exception ex) {
        testBlock.Text = ex.Message;
        return;
      }
/*
      if (status.ExtendedEntities.UserMentions[0].Id == null)
      {
        conveView.ItemsSource = tweet;
        return;
      }
      else
      {
        
        var rep_status = status.Entities.UserMentions[0].ScreenName;
        //tweet.Add(new TweetClass.TweetInfo
        //{
        testBlock.Text = rep_status;
        //}
 // );
      }
      */

    }

    private async void loadTweet(long? Id)
    {
      status = await tokens.Statuses.ShowAsync(id => Id);
      var tweet = new ObservableCollection<TweetClass.TweetInfo>();
      data.Addtweet(tweet, status);
      //tweet = data.replytweetinfo2(status);
      conveView.ItemsSource = tweet;
      //show(status);

    }

    private void conButtom_Click(object sender, RoutedEventArgs e)
    {
      //tweet = new List<TweetClass.TweetInfo>();
      //conv(item.Id);
    }
  }
}

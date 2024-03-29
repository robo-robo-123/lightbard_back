﻿using System;
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
using System.Collections.ObjectModel;
using System.Threading.Tasks;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace uniApp1.Pages
{
  /// <summary>
  /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
  /// </summary>
  public sealed partial class MentionPage : Page
  {

    internal Tokens tokens;
    Tweets data = new Tweets();

    ObservableCollection<TweetClass.TweetInfo> tweet;
    public long? UserId { get; set; }
    TweetClass.TweetInfo item;
    public long? ReplyId { get; set; }

    public string userId { get; set; }
    public string ScreenName { get; set; }
    public string filename { get; set; }
    public FileInfo fileinfo { get; set; }

    FileOpenPicker openPicker = new FileOpenPicker();

    public MentionPage()
    {
      this.InitializeComponent();
      tokens = data.getToken();
      mentionLoad();
      var settings = ApplicationData.Current.RoamingSettings;
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      item = (TweetClass.TweetInfo)e.Parameter;
    }

    //tweetをロードするのに使います
    private async void mentionLoad()
    {
      Task<ObservableCollection<TweetClass.TweetInfo>> mentionload = data.mentionload();
      try {
        this.listView.ItemsSource = await mentionload;
      }
      catch(Exception ex)
      {
        
      }
    }

    //ロードボタンです．
    private void reloadButton_Click(object sender, RoutedEventArgs e)
    {
      mentionLoad();
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

    //お気に入り．
    private void favoriteButton_Click(object sender, RoutedEventArgs e)
    {
      var item = this.listView.SelectedItem as TweetClass.TweetInfo;
      if (item == null)
      {
        favoriteBlock.Text = "未選択です";
        return;
      }
      else
      {
          data.like(item.Id);
      }
    }

    //リツイート
    private void retweetButton_Click(object sender, RoutedEventArgs e)
    {
      var item = this.listView.SelectedItem as TweetClass.TweetInfo;
      if (item == null)
      {
        return;
      }
      else
      {
        data.retweet(item.Id);
      }
    }

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

    private void TweetsList_Tapped(object sender, TappedRoutedEventArgs e)
    {
      FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
    }
  }
}

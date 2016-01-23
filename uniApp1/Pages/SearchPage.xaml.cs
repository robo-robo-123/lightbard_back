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
using Windows.Storage;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace uniApp1.Pages
{
  /// <summary>
  /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
  /// </summary>
  public sealed partial class SearchPage : Page
  {

    internal Tokens tokens;

    List<TweetClass.TweetInfo> tweet;
    List<TweetClass.TweetInfo> userTweet;

    List<TweetClass.UserInfo> user;
    List<TweetClass.UserInfo> userPro;
    public long? ReplyId { get; set; }

    public string userId { get; set; }
    public string ScreenName { get; set; }
    Tweets data = new Tweets();

    public SearchPage()
    {
      this.InitializeComponent();
      tokens = data.getToken();
    }

    private async void searchTweet()
    {
      if (tokens != null)
      {
        tweet = new List<TweetClass.TweetInfo>();
        try
        {
          string search_word = serchBox.Text;
          var result = await tokens.Search.TweetsAsync(count => 100, q => search_word);
          
          //foreach (var status in await tokens.Search.TweetsAsync(q => serchBox.Text, count => 200, lang => "ja"))
          foreach (var status in result)
          {
            data.Addtweet(tweet, status);
          }
          listView.ItemsSource = tweet;
        }
        catch (Exception ex)
        {
//          viewTextBox.Text = ex.Source;
        }
      }
    }

    private async void searchUser()
    {
      if (tokens != null)
      {
        user = new List<TweetClass.UserInfo>();
        try
        {
          string search_word = serchBox.Text;
          var result = await tokens.Users.SearchAsync(count => 100, q => search_word);

          //foreach (var status in await tokens.Search.TweetsAsync(q => serchBox.Text, count => 200, lang => "ja"))
          foreach (var status in result)
          {
            user.Add(new TweetClass.UserInfo
            {
              UserName = status.Name,
              UserId = status.Id,
              ScreenName = "@" + status.ScreenName,
              ProfileImageUrl = status.ProfileImageUrlHttps,
              FollowCount = status.FollowersCount,
              FavCount = status.FavouritesCount,
              FollowerCount = status.FriendsCount,
              Prof = status.Description

            });
          }
          listView.ItemsSource = user;
        }
        catch (Exception ex)
        {
          //          viewTextBox.Text = ex.Source;
        }
      }
    }

    private void searchButtom_Click(object sender, RoutedEventArgs e)
    {
      searchTweet();
    }

    private void userButton_Click(object sender, RoutedEventArgs e)
    {
      searchUser();
    }

    private async void searchTrend()
    {
      if (tokens != null)
      {
        try
        {
          string search_word = serchBox.Text;
          var result = await tokens.Trends.AvailableAsync();

          //foreach (var status in await tokens.Search.TweetsAsync(q => serchBox.Text, count => 200, lang => "ja"))

          listView.ItemsSource = result;
        }
        catch (Exception ex)
        {
          //          viewTextBox.Text = ex.Source;
        }
      }
    }

  }
}

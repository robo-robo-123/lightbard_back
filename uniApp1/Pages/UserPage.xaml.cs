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
using CoreTweet;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Core;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace uniApp1.Pages
{
  /// <summary>
  /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
  /// </summary>
  public sealed partial class UserPage : Page
  {
    internal Tokens tokens;
    TweetClass.TweetInfo item;
    Tweets data = new Tweets();
    public long? UserId { get; set; }
    public string name { get; set; }

    UserResponse showedUser;

    List<TweetClass.TweetInfo> tweet;
    List<TweetClass.TweetInfo> userTweet;

    List<TweetClass.UserInfo> user;
    List<TweetClass.UserInfo> userPro;
    List<TweetClass.UserInfo> userPro2;
    private DispatcherTimer mTimer;
    private double mTime = 0.0;


    public UserPage()
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

  //    mTimer = new DispatcherTimer();
  //    mTimer.Interval = TimeSpan.FromMilliseconds(100); //100ミリ秒間隔に設定
  //    mTimer.Tick += new EventHandler(TickTimer);


      // userInfo();

    }



    private void Button_Click(object sender, RoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(MainFrame));
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      //item = (TweetClass.TweetInfo)e.Parameter;
      //UserId = item.UserId;
      UserId = (long?)e.Parameter;
      showasync();
      //userInfo();
    }

    private async void showasync()
    {try
      {
        showedUser = await tokens.Users.ShowAsync(user_id => UserId);
      }
      catch
      {

      }
    }

    private void userInfo()
    {
     if (tokens != null)
      {
        userPro = new List<TweetClass.UserInfo>();
        try
        {

     //     showedUser =await tokens.Users.ShowAsync(user_id => name);


          userPro.Add(new TweetClass.UserInfo
          {
            UserName = showedUser.Name,
            UserId = showedUser.Id,
            ScreenName = "@" + showedUser.ScreenName,
            ProfileImageUrl = showedUser.ProfileImageUrlHttps,
            FollowCount = showedUser.FollowersCount,
            FavCount = showedUser.FavouritesCount,
            FollowerCount = showedUser.FriendsCount,
            Prof = showedUser.Description

          }
);

          this.userInfoView.ItemsSource = userPro;

          //userTimeline2();
        }

        catch (Exception ex)

        {

        }

      }

    }

    private async void tweetLoad()
    {
      userTLView.Visibility = Visibility.Collapsed;
      listView2.Visibility = Visibility.Visible;
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
          this.listView2.ItemsSource = tweet;
        }
        catch (Exception ex)
        {
        }
      }
    }


    private async void followTimeline()
    {
      listView2.Visibility = Visibility.Collapsed;
      userTLView.Visibility = Visibility.Visible;
      if (tokens != null)

      {
        //this.userTLView.Items.Clear();

        userPro2 = new List<TweetClass.UserInfo>();
        try

        {
          userPro2 = new List<TweetClass.UserInfo>();
          // var showedUser = tokens.Favorites.List(screen_name => ScreenName, count => 200);

          foreach (var status in await tokens.Friends.ListAsync(user_id => UserId, count => 200))

          {
            //data.AddInfo(userPro2, status);
            userPro2.Add(new TweetClass.UserInfo
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
          this.userTLView.ItemsSource = userPro2;
        }

        catch (Exception ex)

        {


        }

      }
    }


    private async void followerTimeline()
    {
      listView2.Visibility = Visibility.Collapsed;
      userTLView.Visibility = Visibility.Visible;
      if (tokens != null)

      {
        //this.userTLView.Items.Clear();

        userPro2 = new List<TweetClass.UserInfo>();
        try

        {
          userPro2 = new List<TweetClass.UserInfo>();
          // var showedUser = tokens.Favorites.List(screen_name => ScreenName, count => 200);

          foreach (var status in await tokens.Followers.ListAsync(user_id => UserId, count => 200))

          {
            //data.AddInfo(userPro2, status);
            userPro2.Add(new TweetClass.UserInfo
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
          this.userTLView.ItemsSource = userPro2;
        }

        catch (Exception ex)

        {


        }

      }
    }
    private void timeLineButton_Click(object sender, RoutedEventArgs e)
    {
      // showasync();
      //this.tweetFrame.Navigate(typeof(Home), item);
      tweetLoad();

    }

    private async void favLoad()
    {
      userTLView.Visibility = Visibility.Collapsed;
      listView2.Visibility = Visibility.Visible;
      if (tokens != null)
      {
        tweet = new List<TweetClass.TweetInfo>();
        try
        {
          tweet = new List<TweetClass.TweetInfo>();


          foreach (var status in await tokens.Favorites.ListAsync(user_id => UserId, count => 800))
          {
            data.Addtweet(tweet, status);
          }
          this.listView2.ItemsSource = tweet;
        }
        catch (Exception ex)
        {
        }
      }
    }

    private void followerButton_Click(object sender, RoutedEventArgs e)
    {
      followerTimeline();
    }

    private void fllowButton_Click(object sender, RoutedEventArgs e)
    {
      followTimeline();
    }

    private void favButton_Click(object sender, RoutedEventArgs e)
    {
      favLoad();
    }

    private void InfoButton_Click(object sender, RoutedEventArgs e)
    {
userInfo();
    }

    //リプライページに飛びます．※ツイートに関しては，下のツイートと共通化させたい．
    private void replyButton_Click(object sender, RoutedEventArgs e)
    {
      var item = this.listView2.SelectedItem as TweetClass.TweetInfo;
      if (item == null)
      {
        return;
      }
      else
      {
        var item_send = this.listView2.SelectedItem as TweetClass.TweetInfo;
        this.Frame.Navigate(typeof(ReplayPage), item_send);
        //rootFrame.Navigate(typeof(ReplayPage), item_send);
      }

    }

    //お気に入り．
    private async void favasync(long itemid)
    {
      try
      {
        await tokens.Favorites.CreateAsync(id => itemid);
        favoriteBlock.Text = "'いいね'しました";
      }
      catch
      {
        favoriteBlock.Text = "既に'いいね'しています";
      }
    }

    private void favoriteButton_Click(object sender, RoutedEventArgs e)
    {
      var item = this.listView2.SelectedItem as TweetClass.TweetInfo;
      if (item == null)
      {
        favoriteBlock.Text = "未選択です";
        return;
      }
      else
      {
        favasync(item.Id);
      }
    }

    //リツイート
    private async void retweetasync(long itemid)
    {
      try
      {
        await tokens.Statuses.RetweetAsync(id => itemid);
        retweetBlock.Text = "リツイートしました";
      }
      catch (Exception ex)
      {
        retweetBlock.Text = "既にリツイートしています";
      }

    }

    private void retweetButton_Click(object sender, RoutedEventArgs e)
    {
      var item = this.listView2.SelectedItem as TweetClass.TweetInfo;
      if (item == null)
      {
        retweetBlock.Text = "未選択です";
        return;
      }
      else
      {
        retweetasync(item.Id);
      }
    }

    //userinfo
    private void userInfoCommand_Click(object sender, RoutedEventArgs e)
    {
      var item = this.userTLView.SelectedItem as TweetClass.TweetInfo;
      if (item == null)
      {
        return;
      }
      else
      {
        var item_send = this.userTLView.SelectedItem as TweetClass.TweetInfo;
        this.Frame.Navigate(typeof(UserPage), item_send.UserId);
        //rootFrame.Navigate(typeof(ReplayPage), item_send);
      }
    }
  }
}

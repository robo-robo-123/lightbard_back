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
  public sealed partial class BlockPage : Page
  {

    internal Tokens tokens;
    TweetClass.TweetInfo item;
    Tweets data = new Tweets();
    public long? UserId { get; set; }
    public string name { get; set; }

    List<TweetClass.UserInfo> user;

    public BlockPage()
    {
      this.InitializeComponent();
      tokens = data.getToken();

      var settings = ApplicationData.Current.RoamingSettings;
      UserId = (long?)settings.Values["UserId"];


      muteInfo();
    }

    private async void muteInfo()
    {
      if (tokens != null)
      {


        user = new List<TweetClass.UserInfo>();
        try
        {

         
          foreach (var status in await tokens.Mutes.Users.ListAsync())

          {
            //data.AddInfo(userPro2, status);
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
          this.muteView.ItemsSource = user;
        }

        catch (Exception ex)

        {

        }

      }

    }
/*
    private async void blockInfo()
    {
      if (tokens != null)
      {


        user = new List<TweetClass.UserInfo>();
        try
        {


          foreach (var status in await tokens.Blocks.ListAsync())

          {
            //data.AddInfo(userPro2, status);
            user.Add(new TweetClass.UserInfo
            {
              UserName = status.Name,
              UserId = status.Id,
              ScreenName = "@" + status.ScreenName,
              ProfileImageUrl = status.ProfileImageUrlHttps.OriginalString,
              FollowCount = status.FollowersCount,
              FavCount = status.FavouritesCount,
              FollowerCount = status.FriendsCount,
              Prof = status.Description

            });

          }
          this.muteView.ItemsSource = user;
        }

        catch (Exception ex)

        {

        }

      }

    }
    */
    private void Button_Click(object sender, RoutedEventArgs e)
    {
      muteInfo();
    }

    private void blockButton_Click(object sender, RoutedEventArgs e)
    {
     // blockInfo();
    }
  }
}

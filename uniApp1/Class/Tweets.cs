using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreTweet;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using Windows.Storage;
using System.Collections.ObjectModel;
using Windows.UI.Notifications;


namespace uniApp1.Class
{
  class Tweets
  {
    
    internal Tokens tokens;
    ObservableCollection<TweetClass.TweetInfo> tweet;
    ObservableCollection<TweetClass.TweetInfo> reply;
    List<TweetClass.UserInfo> userPro;

    public Tweets()
    {
      SaveKey();
    }

    private void SaveKey()
    {
      var settings = ApplicationData.Current.RoamingSettings;
      settings.Values["ApiKey"] = "rXBbSNc4nf01jY2tYYhcQlLdQ";
      settings.Values["ApiSecret"] = "nSaEdTdvVuAHbv8torT1RzvdHRLdF0b6XiUCO5n5Ciq43gv3vs";
    }

    public CoreTweet.Tokens getToken()
    {
      var value = ApplicationData.Current.RoamingSettings;

      if (!string.IsNullOrEmpty((string)value.Values["AccessToken"])

&& !string.IsNullOrEmpty((string)value.Values["AccessTokenSecret"]))

      {

        tokens = Tokens.Create(

            (string)value.Values["ApiKey"]

            , (string)value.Values["ApiSecret"]

            , (string)value.Values["AccessToken"]

            , (string)value.Values["AccessTokenSecret"]
            );
      }
      return tokens;
    }

    //Tweet投稿関連
    public async Task<ObservableCollection<TweetClass.TweetInfo>> tweetload()
    {
      tweet = new ObservableCollection<TweetClass.TweetInfo>();
      foreach (var status in await tokens.Statuses.HomeTimelineAsync(count => 800))
      {
        Addtweet(tweet, status);
      }
      return tweet;
    }

    public async Task<ObservableCollection<TweetClass.TweetInfo>> mentionload()
    {
      tweet = new ObservableCollection<TweetClass.TweetInfo>();
      foreach (var status in await tokens.Statuses.MentionsTimelineAsync(count => 800))
      {
        Addtweet(tweet, status);
      }
      return tweet;
    }


  public void Addtweet(ObservableCollection<TweetClass.TweetInfo> tweet, Status status)
    {
      string con = status.Text;
      if (status.RetweetedStatus != null)
      {

        tweet.Add(new TweetClass.TweetInfo
        {
          UserName = status.RetweetedStatus.User.Name + " ",
          UserId = status.RetweetedStatus.User.Id,
          ScreenName = "@" + status.RetweetedStatus.User.ScreenName,
          ProfileImageUrl = status.RetweetedStatus.User.ProfileImageUrlHttps,
          Text = System.Net.WebUtility.HtmlDecode(status.RetweetedStatus.Text),
          Date = status.RetweetedStatus.CreatedAt.LocalDateTime.ToString(),
          //Date = status.RetweetedStatus.CreatedAt.ToString(),
          Id = status.RetweetedStatus.Id,
          //retUser = status.RetweetedStatus,
          Via = status.RetweetedStatus.Source,
          FavoriteCount = ", Like: " + status.FavoriteCount.ToString(),
          RetweetCount = "Retweet: " + status.RetweetCount.ToString(),

          //Url = m.Value.ToString(),
          RetweetUser = "Retweeted by @" + status.User.ScreenName,
          RetweetUserProfileImageUrl = status.User.ProfileImageUrlHttps,

          urls = status.RetweetedStatus.Entities.Urls,
          //ReplyId = status.RetweetedStatus.InReplyToStatusId

          //media = status.RetweetedStatus.ExtendedEntities.Media
          //arrayB.CopyTo(arrayA, 0)
          //media = status.RetweetedStatus.Entities.Media.CopyTo(media, 0)

        }
          );
      }
      else
      {

        tweet.Add(new TweetClass.TweetInfo
        {
          UserName = status.User.Name + " ",
          UserId = status.User.Id,
          ScreenName = "@" + status.User.ScreenName,
          ProfileImageUrl = status.User.ProfileImageUrlHttps,
          Text = System.Net.WebUtility.HtmlDecode(status.Text),
          Date = status.CreatedAt.LocalDateTime.ToString(),
          //Date = status.RetweetedStatus.CreatedAt.ToString(),
          Id = status.Id,
          //retUser = status.RetweetedStatus,
          //Url = m.Value.ToString(),
          FavoriteCount = ", Like: " + status.FavoriteCount.ToString(),
          RetweetCount = "Retweet: " + status.RetweetCount.ToString(),
          Via = status.Source,
          RetweetUser = null,
          urls = status.Entities.Urls,
          //ReplyId = status.InReplyToStatusId

        }
        );
      }
    }

    //通知関連
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

    //Like関連
    private async void favasync(long itemid)
    {
      try
      {
        await tokens.Favorites.CreateAsync(id => itemid);
      }
      catch
      {
      }
    }

    public void like(long id)
    {
      favasync(id);
      var tes = "'いいね'しました";
      toast(tes);
    }

    //リツイート
    private async void retweetasync(long itemid)
    {
      try
      {
        await tokens.Statuses.RetweetAsync(id => itemid);
      }
      catch (Exception ex)
      {
      }

    }

    public void retweet(long id)
    {
      retweetasync(id);
      var tes = "リツイートしました";
      toast(tes);
    }


    public void AddInfo(List<TweetClass.UserInfo> userPro, CoreTweet.User user/*=null, CoreTweet.*/)
    {
      userPro.Add(new TweetClass.UserInfo
      {
        UserName = user.Name,
        UserId = user.Id,
        ScreenName = "@" + user.ScreenName,
        ProfileImageUrl = user.ProfileImageUrlHttps,
        FollowCount = user.FollowersCount,
        FavCount = user.FavouritesCount,
        FollowerCount = user.FriendsCount,
        Prof = user.Description

      }
);
    }

    public ObservableCollection<TweetClass.TweetInfo> replytweetinfo(TweetClass.TweetInfo item)
    {
      //情報を引き出し、ここで画像を取得しよう。
      reply = new ObservableCollection<TweetClass.TweetInfo>();

      reply.Add(new TweetClass.TweetInfo
      {

        UserName = item.UserName,
        UserId = item.UserId,
        ScreenName = item.ScreenName,
        ProfileImageUrl = item.ProfileImageUrl,
        Text = System.Net.WebUtility.HtmlDecode(item.Text),
        Date = item.Date,
        Via = item.Via,
        FavoriteCount = item.FavoriteCount.ToString(),
        RetweetCount = item.RetweetCount.ToString()

      }
      );

      return reply;

    }

    public ObservableCollection<TweetClass.TweetInfo> replytweetinfo2(Status item)
    {
      //情報を引き出し、ここで画像を取得しよう。
      reply = new ObservableCollection<TweetClass.TweetInfo>();
      try
      {
        reply.Add(new TweetClass.TweetInfo
        {

          UserName = item.User.Name,
          UserId = item.User.Id,
          ScreenName = item.User.ScreenName,
          ProfileImageUrl = item.User.ProfileImageUrlHttps,
          Text = System.Net.WebUtility.HtmlDecode(item.Text),
          Date = item.CreatedAt.LocalDateTime.ToString(),
          Via = item.Source,
          FavoriteCount = item.FavoriteCount.ToString(),
          RetweetCount = item.RetweetCount.ToString(),
          //urls = item.Entities.Urls,
          //media = item.ExtendedEntities.Media
        }
        );
      }
      catch
      { }
        return reply;

    }
    /*
    public void tweetMethod(string text, FileInfo filename = null, long? replyid = 0)
    {
      // getToken();
      try
      {
        if (filename == null)
        {
          if (replyid == 0)
          {
            tokens.Statuses.Update(status => text);
            var dialog = new ModernDialog1("ツイートしました");
            dialog.ShowDialog();
          }
          else if (replyid != 0)
          {
            tokens.Statuses.Update(status => text, in_reply_to_status_id => replyid);
            var dialog = new ModernDialog1("リプライを送りました");
            dialog.ShowDialog();
          }
        }
        else
        {
          //tokens.Statuses.UpdateWithMedia(status => text, media => filename);
          var mid = tokens.Media.Upload(media => filename);
          tokens.Statuses.Update(status => text, media_ids => mid);
          //, media_ids => mid, media_ids => mid, media_ids => mid
          //tokens.Media.Upload(media => fileinfo);
          var dialog = new ModernDialog1("ツイートしました");
          dialog.ShowDialog();
        }
      }
      catch (Exception ex)
      {
        var dialog = new ModernDialog1("エラーが発生しました");
        dialog.ShowDialog();
      }
    }*/

  }
}

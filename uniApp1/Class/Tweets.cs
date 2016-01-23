using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreTweet;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using Windows.Storage;


namespace uniApp1.Class
{
  class Tweets
  {
    
    internal Tokens tokens;
    List<TweetClass.TweetInfo> tweet;
    List<TweetClass.TweetInfo> reply;
    List<TweetClass.UserInfo> userPro;

    public Tweets()
    {
      var settings = ApplicationData.Current.RoamingSettings;

      settings.Values["ApiKey"] = "rXBbSNc4nf01jY2tYYhcQlLdQ";
      settings.Values["ApiSecret"] = "nSaEdTdvVuAHbv8torT1RzvdHRLdF0b6XiUCO5n5Ciq43gv3vs";

    }

//    StringCollection nameList = (StringCollection)Properties.Settings.Default.mutewords;
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


    public async Task<List<TweetClass.TweetInfo>> tweetload()
    {
      //  tokens = getToken();

      tweet = new List<TweetClass.TweetInfo>();

      //      string[] names = nameList.Cast<string>().ToArray();
      /*      foreach(var str in names)
            {
              if (0 <= con.IndexOf(str))
              {
                return;
              }
            }
      */

      foreach (var status in await tokens.Statuses.HomeTimelineAsync(count => 800))

      {
        Addtweet(tweet, status);
        /*
      if (status.RetweetedStatus != null)
      {
        Regex http = new Regex(@"http://(?<domain>[\w\.]*)/(?<path>[\w\./]*)");
        Match m = http.Match(status.Text);



        tweet.Add(new TweetClass.TweetInfo
        {


          UserName = status.RetweetedStatus.User.Name + " ",
          UserId = status.RetweetedStatus.User.Id,
          ScreenName = "@" + status.RetweetedStatus.User.ScreenName,
          ProfileImageUrl = status.RetweetedStatus.User.ProfileImageUrlHttps.OriginalString,
          Text = status.RetweetedStatus.Text,
          //Date = status.RetweetedStatus.CreatedAt.ToString(),
          Id = status.RetweetedStatus.Id,
          //retUser = status.RetweetedStatus,
          Url = m.Value.ToString(),
          Via = status.RetweetedStatus.Source,
          RetweetUser = "Retweeted by @" + status.User.Name

        }
          );


      }
      else
      {
        Regex http = new Regex(@"http://(?<domain>[\w\.]*)/(?<path>[\w\./]*)");
        Match m = http.Match(status.Text);



        //  viewTextBox.AppendText(string.Format("{0}: {1}{2}"
        tweet.Add(new TweetClass.TweetInfo
        {

          UserName = status.User.Name + " ",
          UserId = status.User.Id,
          ScreenName = "@" + status.User.ScreenName,
          ProfileImageUrl = status.User.ProfileImageUrlHttps.OriginalString,
          Text = status.Text,
          //Date = status.RetweetedStatus.CreatedAt.ToString(),
          Id = status.Id,
          //retUser = status.RetweetedStatus,
          Url = m.Value.ToString(),
          Via = status.Source

        }
        );

      }*/



      }

      return tweet;


    }





    public void Addtweet(List<TweetClass.TweetInfo> tweet, Status status)
    {
      //status.Entities.
      string con = status.Text;
      /*
            if (0 < con.IndexOf(""))
            {
              return;
            }
      */


      //     string[] names = nameList.Cast<string>().ToArray();
      /*foreach(var str in nameList)
      {
        if (0 < con.IndexOf(str))
        {
          tweet.Add(new TweetClass.TweetInfo
          {UserName = str });

          return;
        }
      }*/



      if (status.RetweetedStatus != null)
      {
        Regex http = new Regex(@"http://(?<domain>[\w\.]*)/(?<path>[\w\./]*)");
        Match m = http.Match(status.Text);

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
        Regex http = new Regex(@"http://(?<domain>[\w\.]*)/(?<path>[\w\./]*)");
        Match m = http.Match(status.Text);



        //  viewTextBox.AppendText(string.Format("{0}: {1}{2}"
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
          
          //media_number = status.Entities.Media.Length,
          //media = status.ExtendedEntities.Media

        }
        );

      }


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

    public List<TweetClass.TweetInfo> replytweetinfo(TweetClass.TweetInfo item)
    {
      //情報を引き出し、ここで画像を取得しよう。
      reply = new List<TweetClass.TweetInfo>();

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

    public List<TweetClass.TweetInfo> replytweetinfo2(Status item)
    {
      //情報を引き出し、ここで画像を取得しよう。
      reply = new List<TweetClass.TweetInfo>();
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

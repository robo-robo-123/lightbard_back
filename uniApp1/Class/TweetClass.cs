using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreTweet;

namespace uniApp1.Class
{
  class TweetClass
  {

    public class TweetInfo
    {
      public string UserName { get; set; }
      public long? UserId { get; set; }
      public string Text { get; set; }
      public string ScreenName { get; set; }
      public long Id { get; set; }
      public long? ReplyId { get; set; }
      public string ProfileImageUrl { get; set; }
      public string Date { get; set; }
      public string Via { get; set; }
      // public Status retUser { get; set; }
      public string Url { get; set; }
      public string RetweetUser { get; set; }
      public string RetweetUserProfileImageUrl { get; set; }
      public string RetweetCount { get; set; }
      public string FavoriteCount { get; set; }
      public MediaEntity[] media { get; set; }
      public UrlEntity[] urls { get; set; }
      public int media_number { get; set; }
      public int RelativeTime { get; set; }
      
    }

    public class UserInfo
    {
      public string UserName { get; set; }
      public long? UserId { get; set; }
      public string Text { get; set; }
      public string ScreenName { get; set; }
      public long Id { get; set; }
      public string ProfileImageUrl { get; set; }
      public string Prof { get; set; }
      public string Date { get; set; }
      public string Via { get; set; }
      public int FollowCount { get; set; }
      public int FavCount { get; set; }
      public int TweetCount { get; set; }
      public int FollowerCount { get; set; }
      // public string ProfileImageUrl { get; set; }
    }

  }
}

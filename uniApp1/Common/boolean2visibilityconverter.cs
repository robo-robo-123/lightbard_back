using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace uniApp1.Common
{
  /// <summary>
  /// BooleanをVisibilityに変換する。
  /// </summary>
  public class Boolean2VisibilityConverter : IValueConverter
  {
    /// <summary>
    /// Trueをセットした場合、変換を逆転させる。TrueがCollapsedを返す。
    /// </summary>
    public bool IsReversed { get; set; }

    public object Convert(object value, Type typeName, object parameter, string language)
    {
      var val = System.Convert.ToBoolean(value);
      if (this.IsReversed)
      {
        val = !val;
      }
      if (val)
      {
        return Visibility.Visible;
      }
      return Visibility.Collapsed;
    }
    public object ConvertBack(object value, Type typeName, object parameter, string language)
    {
      throw new NotImplementedException();
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel; // ObservableCollection
using System.Runtime.CompilerServices; // CallerMemberName
using Windows.Storage; // ApplicationDataContainer, ApplicationData
using Newtonsoft.Json; // JsonConvert（JSON.NET）
using System.Collections.Specialized; // NotifyCollectionChangedEventArgs

namespace uniApp1.Class
{
  
  public class OptionSettings : Common.BindableBase
  {
    
    // プロセスで唯一のインスタンス
    private static OptionSettings _thisInstance = new OptionSettings();
    public static OptionSettings Current { get { return _thisInstance; } }

    // ApplicationDataContainerのインスタンス
    private ApplicationDataContainer _settings
                = ApplicationData.Current.LocalSettings;



    // 設定ファイルを読み書きするための汎用メソッド（Json.NETを利用）

    private T GetValue<T>(T defaultValue, [CallerMemberName] string key = "")
    {
      if (!_settings.Values.ContainsKey(key))
        return defaultValue;

      var json = (string)_settings.Values[key];
      return JsonConvert.DeserializeObject<T>(json);
    }

    private void SetValue<T>(T newValue,
                              [CallerMemberName] string key = "")
    {
      var json = JsonConvert.SerializeObject(newValue);
      _settings.Values[key] = json;

      base.OnPropertyChanged(key);
    }




    //



    // 単純な値（string）
    /*
    private const string PivotToggleSwtichDefault = "False";
    public string PivotToggleSwtich
    {
      get { if (GetValue<string>(PivotToggleSwtichDefault) == "True") { return "True"; } else { return "False"; } }
      set { SetValue<string>(value); }
    }
    */
    
    private const bool PivotToggleSwtichDefault = false;
    public bool PivotToggleSwtich
    {
      get { if (GetValue<bool>(PivotToggleSwtichDefault) == true) {  return true; } else {  return false; } }
      set { SetValue<bool>(value); }
    }
    

    /*
    public string ViewSwtich
    {
      get { if (GetValue<string>(PivotToggleSwtichDefault) == "True") { return "Visible"; } else { return "Collapsed"; } }
// set { SetValue<bool>(value); }
    }
    */


    // 単純な値（double）
    private const double FontSizeDefault = 18.0;
    public double FontSizeSetting
    {
      get { return GetValue<double>(FontSizeDefault); }
      set { SetValue<double>(value); }
    }


  
  }
  
}

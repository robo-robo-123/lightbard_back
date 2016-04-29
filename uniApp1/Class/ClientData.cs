using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
namespace uniApp1.Class
{
  class ClientData
  {
    public ClientData()
    {
      var settings = ApplicationData.Current.RoamingSettings;

      settings.Values["ApiKey"] = "rXBbSNc4nf01jY2tYYhcQlLdQ";
      settings.Values["ApiSecret"] = "nSaEdTdvVuAHbv8torT1RzvdHRLdF0b6XiUCO5n5Ciq43gv3vs";

    }

  }
}

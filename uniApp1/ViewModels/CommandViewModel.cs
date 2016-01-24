using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uniApp1.ViewModels
{
  public class CommandViewModel : Common.BindableBase
  {
    private Models.CommandModel Model { get; } = Models.CommandModel.Instance;



  }
}

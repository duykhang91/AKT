using System;
using System.Collections.Generic;
using System.Text;

namespace AKTTool.Common
{
  public enum ConcurrencyResolutionStrategy
  {
    /// <summary>
    /// Throws exception
    /// </summary>
    None,

    /// <summary>
    /// Uses database values
    /// </summary>
    DatabaseWin,

    /// <summary>
    /// Uses client values
    /// </summary>
    ClientWin
  }
}

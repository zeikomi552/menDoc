using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menDoc.Common.Helpers
{
    public class NewItemHolderHelper
    {
        static public readonly object NewItemPlaceholder =
            typeof(System.Windows.Controls.DataGrid)
            .GetProperty("NewItemPlaceholder", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
            .GetValue(null);
    }
}

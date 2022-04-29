using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomList
{
    public class ListEventArgs : EventArgs
    {
        public string Message { get; set; }

        public ListEventArgs(string message)
        {
            Message = message;
        }
    }
}

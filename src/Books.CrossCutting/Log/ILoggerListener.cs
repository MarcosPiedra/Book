using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.CrossCutting
{
    public interface ILoggerListener
    {
        void Init();
        void Close();
        void Write(string message);
    }
}

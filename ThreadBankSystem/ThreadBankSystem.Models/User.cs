using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadBankSystem.Models
{
    [Synchronization]
    public class User : ContextBoundObject
    {
        private object lockObject = new object();
        public string Fullname { get; set; }
        public int Sum { get; private set; } = 3000;

        public void AddSum(int sum)
        {
            Monitor.Enter(lockObject);
            try
            {

            }
            finally
            {
                Monitor.Exit(lockObject);
            }
        }

        public int WithDrawSum(int sum)
        {

        }

    }
}

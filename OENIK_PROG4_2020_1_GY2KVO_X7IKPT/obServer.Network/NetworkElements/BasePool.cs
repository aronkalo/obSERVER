using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obServer.Network.NetworkElements
{
    internal abstract class BasePool<T>
    {
        public BasePool()
        {
            this.pool = new Queue<T>();
        }
        private Queue<T> pool { get ; set; }

        public void AddPoolElement(T element)
        {
            pool.Enqueue(element);
        }

        virtual protected T GetPoolElement()
        {
            return pool.Dequeue();
        }

        public bool NotNullElement()
        {
            return pool.Count > 0;
        }
    }
}

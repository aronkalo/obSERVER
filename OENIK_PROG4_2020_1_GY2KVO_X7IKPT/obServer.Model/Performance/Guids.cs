using System;
using System.Collections.Generic;

namespace obServer.Model.Performance
{
    class Guids
    {

        private List<Guid> guidList;

        public Guids()
        {
            this.guidList = new List<Guid>();
        }

        public Guid[] Get()
        {
            if (guidList.Count > 0)
            {
                return guidList.ToArray();
            }
            return new Guid[0];
        }

        public IEnumerable<Guid> List()
        {
            return guidList;
        }

        public void Add(Guid id)
        {
            if (!guidList.Contains(id))
            {
                guidList.Add(id);
            }
        }

        public void Del(Guid id)
        {
            if (guidList.Contains(id))
            {
                guidList.Remove(id);
            }
        }

        public bool Got(Guid id)
        {
            return guidList.Contains(id) ? true : false;
        }
    }
}

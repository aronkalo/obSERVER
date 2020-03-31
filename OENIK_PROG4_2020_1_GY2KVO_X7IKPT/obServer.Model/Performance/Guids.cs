using System;
using System.Collections.Generic;
using System.Linq;

namespace obServer.Model.Performance
{
    class Guids
    {
        private Guid[] guids;

        public Guids()
        {
            guids = new Guid[0];
        }

        public Guid[] Get()
        {
            if (guids.Length > 0)
            {
                return guids;
            }
            return new Guid[0];
        }

        public IEnumerable<Guid> List()
        {
            return guids.ToList<Guid>();
        }

        public void Add(Guid id)
        {
            Guid[] helper = guids;
            guids = new Guid[helper.Length + 1];
            helper.CopyTo(guids, 0);
            guids[helper.Length] = id;
        }

        public void Del(Guid id)
        {
            if (guids.Contains(id))
            {
                guids = guids.Except(new List<Guid>{ id }).ToArray<Guid>();
            }
        }

        public bool Got(Guid id)
        {
            if (guids.Length > 0)
            {
                return false;
            }
            else
            {
                if (guids.Contains(id))
                {
                    return true;
                }
                return false;
            }
        }
    }
}

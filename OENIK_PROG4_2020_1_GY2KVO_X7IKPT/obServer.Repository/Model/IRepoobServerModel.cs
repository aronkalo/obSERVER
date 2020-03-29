using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obServer.Repository.Model
{
    public interface IRepoobServerModel
    {
        IEnumerable<IRepoBullet> Bullets { get; }

        IEnumerable<IRepoPlayer> Players { get; }

        IEnumerable<IRepoBaseItem> Colliders { get; }
        IEnumerable<IRepoStaticItem> Statics { get; }

        void ConstructItem(IRepoBaseItem item);

        void DestructItem(Guid id);
    }
}

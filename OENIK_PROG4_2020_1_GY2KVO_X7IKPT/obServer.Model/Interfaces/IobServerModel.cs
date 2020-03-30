using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obServer.Model.Interfaces
{
    public interface IobServerModel
    {
        IEnumerable<IBaseItem> AllItems { get; }
        IEnumerable<IBullet> Bullets { get; }

        IEnumerable<IPlayer> Players { get; }

        IEnumerable<IBaseItem> Colliders { get; }
        IEnumerable<IStaticItem> Statics { get; }

        void ConstructItem(IBaseItem item);

        void DestructItem(Guid id);
        Guid[] GetCloseItems(Guid id);
    }
}

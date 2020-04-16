using obServer.GameModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obServer.Repository.GameModel
{
    public interface IRepoobServerModel
    {
        IEnumerable<IBullet> Bullets { get; }

        IEnumerable<IPlayer> Players { get; }

        IEnumerable<IBaseItem> Colliders { get; }
        IEnumerable<IStaticItem> Statics { get; }

        void ConstructItem(IBaseItem item);

        void DestructItem(Guid id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obServer.Model.Interfaces
{
    public interface IobServerModel
    {
        IPlayer MyPlayer { get;  }
        IEnumerable<IBaseItem> AllItems { get; }
        IEnumerable<IBaseItem> Bullets { get; }
        IEnumerable<IBaseItem> Players { get; }
        IEnumerable<IBaseItem> Colliders { get; }
        IEnumerable<IBaseItem> Statics { get; }
        IEnumerable<IBaseItem> Weapons { get; }
        IStaticItem[] Map { get; }
        void ConstructItem(IBaseItem item);
        void DestructItem(Guid id);
        IEnumerable<IBaseItem> GetCloseItems(Guid id);
        void UpdateItem(Guid id, double xMove, double yMove, double width, double height, double rotation);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obServer.GameModel.Interfaces
{
    public interface IObServerModel
    {
        IPlayer MyPlayer { get; }
        IEnumerable<IBaseItem> AllItems { get; }
        IEnumerable<IBaseItem> Bullets { get; }
        IEnumerable<IBaseItem> Players { get; }
        IEnumerable<IBaseItem> Colliders { get; }
        IEnumerable<IBaseItem> Statics { get; }
        IEnumerable<IBaseItem> Weapons { get; }
        IStaticItem[] Map { get; }
        void ConstructItem(IBaseItem item);
        void DestructItem(Guid id);
        void Changed();
        void RemoveVisuals();
        void LoadItems(string fileName);
    }
}

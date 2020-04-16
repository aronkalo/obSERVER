//using obServer.GameModel;
//using obServer.GameModel.Interfaces;
//using System;
//using System.Collections.Generic;

//namespace obServer.Repository.GameModel
//{
//    public class RepoobServerModel
//    {
//        public RepoobServerModel(int width, int height)
//        {
//            model = new obServerModel(width, height); ;
//        }
//        private IobServerModel model;

//        public IEnumerable<IBullet> Bullets { get {return model.Bullets;} }

//        public IEnumerable<IPlayer> Players { get {return model.Players ;} }

//        //public IEnumerable<IBaseItem> Colliders { get { return model.Colliders; } }
       
//        public IEnumerable<IStaticItem> Statics { get { return model.Statics; } }

//        public void ConstructItem(IBaseItem item)
//        {
//            model.ConstructItem(item);
//        }

//        public void DestructItem(Guid id)
//        {
//            model.DestructItem(id);
//        }
//    }
//}

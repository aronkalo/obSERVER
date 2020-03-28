using obServer.Network.Structs;
using obServer.Repository.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace obServer.Logic
{
    public sealed class ClientLogic : BaseLogic
    {
        public ClientLogic()
        {
            gameClient = new RepoGameClient(serverPort, clientPort);
            gameClient.StartListening();
        }

        private const int serverPort = 3200;

        private const int clientPort = 4500;

        private IRepoGameClient gameClient;

        protected override void HandleRequests()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    Request r = gameClient.GetResponse();
                    switch (r.Operation)
                    {
                        case Operation.Connect:
                            HandleConnect(r);
                            break;
                        case Operation.Disconnect:
                            HandleDisconnect(r);
                            break;
                        case Operation.CheckServerAvaliable:
                            HandleReady(r);
                            break;
                        case Operation.SendObject:
                            HandleSendObject(r);
                            break;
                        case Operation.Remove:
                            HandleRemove(r);
                            break;
                        case Operation.Die:
                            HandleDie(r);
                            break;
                        case Operation.Hit:
                            HandleHit(r);
                            break;
                        case Operation.SendChatMessage:
                            HandleSendMessage(r);
                            break;
                        case Operation.Shoot:
                            HandleShoot(r);
                            break;
                        case Operation.Move:
                            HandleMove(r);
                            break;
                        case Operation.Pickup:
                            HandlePickup(r);
                            break;
                        default:
                            throw new Exception("Not implemented Exception");
                    }
                }
                catch (Exception) { }
            });
        }

        public Request DoAddPlayer()
        {
            Guid PlayerId = Guid.NewGuid();
            Player p = new Player(PlayerId, Physics.PlayerPhysics, 20, 20);
            MapInfo.AddObject(p);
            Player = p;
            return new Request()
            {
                Broadcast = true,
                Operation = UDPNetworking.GameOperation.SendObject,
                Parameters = $"{Player.Identity};{Player.Position.X};{Player.Position.Y};{Player.Width};{Player.Height};{Player.Angle}",
            };
        }

        public double[] GetPlayerParameters()
        {
            return new double[]
            {
                Player.Width,
                Player.Height,
                Player.Position.X,
                Player.Position.Y,
                Player.Angle,
                Player.Physics.MovementSpeed,
            };
        }

        public void RotatePlayer(double angle)
        {
            Player.Rotate(angle);
        }

        private void AddElement(BaseObject obj)
        {
            MapInfo.AddObject(obj);
        }

        private void SetObjectPositon(Guid id, double x , double y, double angle = 0 )
        {
            MapInfo.UpdateObject(id, x, y, angle);
        }

        public IEnumerable<Tuple<Guid, double[], GameObjectType>> GetUpdatedVisuals()
        {
            return MapInfo.SelectUpdatedVisuals();
        }

        public IEnumerable<Tuple<Guid, double[], GameObjectType>> GetDeletedVisuals()
        {
            return MapInfo.DeletedVisuals();
        }
        public void LoadMap()
        {
            foreach (var wall in SimpleMap.Walls)
            {
                MapInfo.AddObject(wall);
            }
            foreach (var weapon in SimpleMap.Weapons)
            {
                MapInfo.AddObject(weapon);
            }
        }

        
        internal Request[] DoShootWeapon()
        {
            if (Player.CurrentWeapon != null)
            {
                return DoShoot(Player.CurrentWeapon.Identity);
            }
            else
            {
                return new Request[0];
            }
        }

        //private void HandlePickup(Request response)
        //{
        //    string parameters = response.Parameters;
        //    string[] prefixes = parameters.Split(';');
        //    Guid id = Guid.Parse(prefixes[0]);
        //    Guid weapon = Guid.Parse(prefixes[1]);
        //    bool reply = Convert.ToBoolean(int.Parse(response.Reply));
        //    if (reply)
        //    {
        //        Weapon newWeapon = (Weapon)MapInfo.Objects.Values.Where(x => x.Identity == weapon).First();
        //        Player.ChangeWeapon(newWeapon);
        //    }
        //}

        //private void HandleHit(Request response)
        //{
        //    string parameters = response.Parameters;
        //    string[] prefixes = parameters.Split(';');
        //    Guid id = Guid.Parse(prefixes[0]);
        //    Guid bullet = Guid.Parse(prefixes[1]);
        //    int damage = int.Parse(prefixes[2]);
        //    if (MapInfo.Objects.Values.Where(x => x.Identity == id).Count() == 1 && MapInfo.Objects.Values.Where(x => x.Identity == bullet).Count() == 1)
        //    {
        //        BaseObject obj = MapInfo.Objects.Values.Where(x => x.Identity == id).FirstOrDefault();
        //        Bullet bulletObj = (Bullet)MapInfo.Objects.Values.Where(x => x.Identity == bullet).FirstOrDefault();
        //        bulletObj.DoDamage((Player)obj);
        //        MapInfo.RemoveObject(bullet);
        //    }
        //}

        //private void HandleDie(Request response)
        //{
        //    string parameters = response.Parameters;
        //    string[] prefixes = parameters.Split(';');
        //    Guid id = Guid.Parse(prefixes[0]);
        //    Guid bullet = Guid.Parse(prefixes[1]);
        //    int damage = int.Parse(prefixes[2]);
        //    if (MapInfo.Objects.Values.Where(x => x.Identity == id).Count() == 1 && MapInfo.Objects.Values.Where(x => x.Identity == bullet).Count() == 1)
        //    {
        //        MapInfo.RemoveObject(id);
        //        MapInfo.RemoveObject(bullet);
        //    }
        //}

        //private void HandleRemove(Request response)
        //{
        //    string parameters = response.Parameters;
        //    string[] prefixes = parameters.Split(';');
        //    Guid id = Guid.Parse(prefixes[0]);
        //    MapInfo.RemoveObject(id);
        //}

        //private void HandlePlaceObject(Request response)
        //{
        //    string parameters = response.Parameters;
        //    string[] prefixes = parameters.Split(';');
        //    Guid id = Guid.Parse(prefixes[0]);
        //    double xPos = double.Parse(prefixes[1]);
        //    double yPos = double.Parse(prefixes[2]);
        //    int width = int.Parse(prefixes[3]);
        //    int height = int.Parse(prefixes[4]);
        //    int angle = int.Parse(prefixes[5]);

        //}

        //private void HandleMove(Request oldestRequest)
        //{
        //    string parameters = oldestRequest.Parameters;
        //    string[] prefixes = parameters.Split(';');
        //    Guid id = Guid.Parse(prefixes[0]);
        //    double x = double.Parse(prefixes[1]);
        //    double y = double.Parse(prefixes[2]);
        //    double angle = double.Parse(prefixes[3]);
        //    bool reply = Convert.ToBoolean(int.Parse(oldestRequest.Reply));
        //    if (reply)
        //    {
        //        SetObjectPositon(id, x, y, angle);   
        //    }
        //}

        //private void HandleShoot(Request oldestRequest)
        //{
        //    string parameters = oldestRequest.Parameters;
        //    string[] prefixes = parameters.Split(';');
        //    Guid id = Guid.Parse(prefixes[0]);
        //    Guid bulletID = Guid.Parse(prefixes[1]);
        //    int Damage = int.Parse(prefixes[2]);
        //    bool reply = Convert.ToBoolean(int.Parse(oldestRequest.Reply));
        //    if (!reply)
        //    {
        //        MapInfo.RemoveObject(bulletID);
        //    }
        //}

        //private void HandleSendMessage(Request oldestRequest)
        //{
        //    throw new NotImplementedException();
        //}
        //private void HandleServerCheck(Request oldestRequest)
        //{
        //    if (oldestRequest.Reply == "0")
        //    {
        //        Online = false;
        //        Queue = true;
        //    }
        //    else
        //    {
        //        Online = true;
        //        Queue = false;
        //    }
        //}

        //private void HandleDisconnect(Request oldestRequest)
        //{
        //    throw new NotImplementedException();
        //}

        //private void HandleConnect(Request oldestRequest)
        //{
        //    Server.EndPoint.Address = IPAddress.Parse(oldestRequest.Reply.Split(';')[0]);
        //    Server.EndPoint.Port = int.Parse(oldestRequest.Reply.Split(';')[1]);
        //}

        //public Request[] DoMovePlayer(double x, double y, double angle)
        //{
        //    List<Request> r = new List<Request>();
        //    BaseObject[] colliders = MapInfo.CollidesWithAny(Player.Identity);
        //    bool collide = false;
        //    foreach (BaseObject collider in colliders)
        //    {
        //        VisualObject player = MapInfo.Visuals.Where(z => z.Identity == Player.Identity).FirstOrDefault();
        //        VisualObject other = MapInfo.Visuals.Where(z => z.Identity == collider.Identity).FirstOrDefault();
        //        if (CollidesWith(player.Primitive.RenderedGeometry, other.Primitive.RenderedGeometry, Player.Position, collider.Position))
        //        {
        //            if (collider.GetType() == typeof(Bullet))
        //            {
        //                r.Add(DoHit(Player.Identity, collider.Identity));
        //                break;
        //            }
        //            else
        //            {
        //                collide = true;
        //                break;
        //            }
        //        }
        //    }
        //    if (!collide)
        //    {
        //        r.Add(DoMove(Player.Identity, x, y, angle));
        //        return r.ToArray();
        //    }
        //    else
        //    {
        //        return new Request[0];
        //    }
        //}
        //private Request DoHit(Guid player, Guid bullet)
        //{
        //    return new Request();
        //}

        //private Request DoMove(Guid id, double x, double y, double angle = 0)
        //{
        //    SetObjectPositon(id, x, y, angle);
        //    return new Request()
        //    {
        //        Operation = UDPNetworking.GameOperation.Move,
        //        Parameters = $"{id};{x};{y};{angle}",
        //    };
        //}

        //private Request[] DoShoot(Guid weapon)
        //{
        //    List<Request> r = new List<Request>();
        //    if (Player.CurrentWeapon.Identity == weapon)
        //    {
        //        Guid id = Player.Identity;
        //        Bullet[] bullets =  Player.CurrentWeapon.DoShoot();

        //        foreach (var bullet in bullets)
        //        {
        //            r.Add(new Request()
        //            {
        //                Operation = UDPNetworking.GameOperation.Shoot,
        //                Parameters = $"{id};{weapon};{bullet.Identity};{bullet.Position.X};{bullet.Position.Y};{bullet.Angle}",
        //            });
        //            MapInfo.AddObject(bullet);
        //        }
        //        return r.ToArray();
        //    }
        //    throw new Exception("The player don't have weapon");
        //}

        //private Request DoPickup(Guid weapon)
        //{
        //    Guid id = Player.Identity;
        //    return new Request()
        //    {
        //        Operation = UDPNetworking.GameOperation.Pickup,
        //        Parameters = $"{id};{weapon}",
        //    };
        //}

        //private bool CollidesWith(Geometry firstObject, Geometry secondObject, Vector firstPosition, Vector secondPosition)
        //{
        //    var transform = new TranslateTransform(firstPosition.X, firstPosition.Y);
        //    var baseGeometry = Geometry.Combine(firstObject, firstObject, GeometryCombineMode.Union, transform);
        //    transform = new TranslateTransform(secondPosition.X, secondPosition.Y);
        //    var shapeGeometry = Geometry.Combine(secondObject, secondObject, GeometryCombineMode.Union, transform);
        //    var detail = baseGeometry.FillContainsWithDetail(shapeGeometry, 0, ToleranceType.Absolute);
        //    if (detail == IntersectionDetail.Intersects || detail == IntersectionDetail.FullyContains)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public void BulletFly(double deltaTime)
        //{
        //    var bullets = MapInfo.Bullets;
        //    if (bullets != null)
        //    {
        //        foreach (var bullet in bullets)
        //        {
        //            bullet.FlyForward(deltaTime);
        //        }
        //    }
        //}
    }
}

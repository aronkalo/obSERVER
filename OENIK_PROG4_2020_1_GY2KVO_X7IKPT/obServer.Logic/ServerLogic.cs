using obServer.Network.Structs;
using obServer.Repository.Network;
using System.Threading.Tasks;

namespace obServer.Logic
{
    public class ServerLogic : BaseLogic
    {
        public ServerLogic()
        {
            gameServer = new RepoGameServer();
        }

        private IRepoGameServer gameServer;

        protected override void HandleRequests()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    Request r = gameServer.GetRequest();
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

        protected override void HandleConnect(Request request)
        {
            base.HandleConnect(request);
        }

        protected override void HandleDie(Request request)
        {
            base.HandleDie(request);
        }

        protected override void HandleDisconnect(Request request)
        {
            base.HandleDisconnect(request);
        }

        protected override void HandleHit(Request request)
        {
            base.HandleHit(request);
        }

        protected override void HandleMove(Request request)
        {
            base.HandleMove(request);
        }

        protected override void HandlePickup(Request request)
        {
            base.HandlePickup(request);
        }

        protected override void HandleReady(Request request)
        {
            base.HandleReady(request);
        }

        protected override void HandleRemove(Request request)
        {
            base.HandleRemove(request);
        }

        protected override void HandleSendMessage(Request request)
        {
            base.HandleSendMessage(request);
        }

        protected override void HandleSendObject(Request request)
        {
            base.HandleSendObject(request);
        }

        protected override void HandleShoot(Request request)
        {
            base.HandleShoot(request);
        }
        //    public void LoadMap()
        //    {
        //        foreach (var wall in SimpleMap.Walls)
        //        {
        //            MapInfo.AddObject(wall);
        //        }
        //        foreach (var weapon in SimpleMap.Weapons)
        //        {
        //            MapInfo.AddObject(weapon);
        //        }
        //    }

        //    protected override void HandleRequests()
        //    {
        //        Task.Factory.StartNew(() =>
        //        {
        //            try
        //            {
        //                Request r = gameClient.GetResponse();
        //                switch (r.Operation)
        //                {
        //                    case Operation.Connect:
        //                        HandleConnect(r);
        //                        break;
        //                    case Operation.Disconnect:
        //                        HandleDisconnect(r);
        //                        break;
        //                    case Operation.CheckServerAvaliable:
        //                        HandleReady(r);
        //                        break;
        //                    case Operation.SendObject:
        //                        HandleSendObject(r);
        //                        break;
        //                    case Operation.Remove:
        //                        HandleRemove(r);
        //                        break;
        //                    case Operation.Die:
        //                        HandleDie(r);
        //                        break;
        //                    case Operation.Hit:
        //                        HandleHit(r);
        //                        break;
        //                    case Operation.SendChatMessage:
        //                        HandleSendMessage(r);
        //                        break;
        //                    case Operation.Shoot:
        //                        HandleShoot(r);
        //                        break;
        //                    case Operation.Move:
        //                        HandleMove(r);
        //                        break;
        //                    case Operation.Pickup:
        //                        HandlePickup(r);
        //                        break;
        //                    default:
        //                        throw new Exception("Not implemented Exception");
        //                }
        //            }
        //            catch (Exception) { }
        //        });
        //    }

        //    protected override void HandleConnect(Request request)
        //    {
        //        base.HandleConnect(request);
        //    }

        //    protected override void HandleDie(Request request)
        //    {
        //        base.HandleDie(request);
        //    }

        //    protected override void HandleDisconnect(Request request)
        //    {
        //        base.HandleDisconnect(request);
        //    }

        //    protected override void HandleHit(Request request)
        //    {
        //        base.HandleHit(request);
        //    }

        //    protected override void HandleMove(Request request)
        //    {
        //        base.HandleMove(request);
        //    }

        //    protected override void HandlePickup(Request request)
        //    {
        //        base.HandlePickup(request);
        //    }

        //    protected override void HandleReady(Request request)
        //    {
        //        base.HandleReady(request);
        //    }

        //    protected override void HandleRemove(Request request)
        //    {
        //        base.HandleRemove(request);
        //    }

        //    protected override void HandleSendMessage(Request request)
        //    {
        //        base.HandleSendMessage(request);
        //    }

        //    protected override void HandleSendObject(Request request)
        //    {
        //        base.HandleSendObject(request);
        //    }

        //    protected override void HandleShoot(Request request)
        //    {
        //        base.HandleShoot(request);
        //    }

        //    public Request SendMessage(string message)
        //    {
        //        return new Request()
        //        {
        //            Sender = server.EndPoint,
        //            Operation = UDPNetworking.GameOperation.SendChatMessage,
        //            Broadcast = true,
        //            Reply = "1",
        //        };
        //    }

        //    public Request PlaceObject(BaseObject obj)
        //    {
        //        MapInfo.AddObject(obj);  
        //        return new Request()
        //        {
        //            Sender = server.EndPoint,
        //            Operation = UDPNetworking.GameOperation.SendObject,
        //            Parameters = $"{obj.Identity};{obj.Position.X};{obj.Position.Y};{obj.Width};{obj.Height};{obj.Angle}",
        //            Broadcast = true,
        //            Reply = "1",
        //        };
        //    }

        //    private void SetObjectPositon(Guid id, double xPos, double yPos, double angle)
        //    {
        //        MapInfo.UpdateObject(id, xPos, yPos, angle);
        //    }

        //    public void CheckClient(Request request)
        //    {
        //        if (request.Sender != null)
        //        {
        //                if (ClientInfos.All(x => x.EndPoint.Address.ToString() != request.Sender.Address.ToString()))
        //                {
        //                    AddClient(new GameClientInfo()
        //                    {
        //                        EndPoint = request.Sender,
        //                        Id = Guid.NewGuid()
        //                    });
        //                }
        //        }
        //    }

        //    private void AddClient(GameClientInfo clientInfo)
        //    {
        //        ClientInfos.Add(clientInfo);
        //    }

        //    private void DeleteClient(Request request)
        //    {
        //        GameClientInfo clientInfo = ClientInfos.Where(x => x.EndPoint == request.Sender).First();
        //        ClientInfos.Remove(clientInfo);
        //    }

        //    public Request[] HandleRequest(Request request)
        //    {
        //        Task.Factory.StartNew(() => CheckClient(request));
        //        List<Request> replys = new List<Request>();
        //        switch (request.Operation)
        //        {
        //            case UDPNetworking.GameOperation.Connect:
        //                replys.Add(HandleConnection(request));
        //                break;
        //            case UDPNetworking.GameOperation.Disconnect:
        //                replys.Add(HandleDisconnection(request));
        //                break;
        //            case UDPNetworking.GameOperation.CheckServerAvaliable:
        //                replys.Add(HandleServerCheck(request));
        //                break;
        //            case UDPNetworking.GameOperation.SendChatMessage:
        //                replys.Add(HandleMessageSend(request));
        //                break;
        //            case UDPNetworking.GameOperation.Shoot:
        //                replys.Add(HandleShooting(request));
        //                break;
        //            case UDPNetworking.GameOperation.Move:
        //                replys.AddRange(HandleMovement(request));
        //                break;
        //            case UDPNetworking.GameOperation.SendObject:
        //                replys.Add(HandleObject(request));
        //                break;
        //            case UDPNetworking.GameOperation.Remove:
        //                replys.Add(HandleRemove(request));
        //                break;
        //            case UDPNetworking.GameOperation.Pickup:
        //                replys.Add(HandlePickup(request));
        //                break;
        //            case UDPNetworking.GameOperation.Die:
        //                replys.Add(HandleDie(request));
        //                break;
        //            default:
        //                throw new Exception("Not implemented Operation");
        //        }
        //        return replys.ToArray();
        //    }

        //    private Request HandlePickup(Request request)
        //    {
        //        string parameters = request.Parameters;
        //        string[] prefixes = parameters.Split(';');
        //        Guid id = Guid.Parse(prefixes[0]);
        //        Guid weapon = Guid.Parse(prefixes[1]);
        //        if (MapInfo.Objects.Keys.Where(x => x.Identity == weapon).Count() == 1)
        //        {
        //            Weapon weaponObj = (Weapon)MapInfo.Objects.Values.Where(x => x.Identity == weapon).First();
        //            if (weaponObj.AncestorPlayer == null)
        //            {
        //                if (MapInfo.Objects.Keys.Where(x => x.Identity == id).Count() == 1)
        //                {
        //                    Player player = (Player)MapInfo.Objects.Values.Where(x => x.Identity == weapon).First();
        //                    player.ChangeWeapon(weaponObj);
        //                    request.Reply = "1";
        //                    request.Broadcast = true;
        //                    return request;
        //                }
        //                else
        //                {
        //                    request.Reply = "0";
        //                    request.Broadcast = false;
        //                    return request;
        //                }
        //            }
        //            else
        //            {
        //                request.Reply = "0";
        //                request.Broadcast = false;
        //                return request;
        //            }
        //        }
        //        else
        //        {
        //            request.Reply = "0";
        //            request.Broadcast = false;
        //            return request;
        //        }
        //    }

        //    private Request HandleDie(Request request)
        //    {
        //        string parameters = request.Parameters;
        //        string[] prefixes = parameters.Split(';');
        //        Guid id = Guid.Parse(prefixes[0]);
        //        Guid bulletId = Guid.Parse(prefixes[1]);
        //        int Damage = int.Parse(prefixes[2]);
        //        var players = MapInfo.Players;
        //        if (players.Where(x => x.Identity == id).Count() == 1)
        //        {
        //            var bullets = MapInfo.Bullets;
        //            Player idObj = (Player)players.Where(x => x.Identity == id).First();
        //            Bullet bulletObj = (Bullet)bullets.Where(x => x.Identity == id).First();
        //            bulletObj.DoDamage(idObj);
        //            if (!idObj.Alive)
        //            {
        //                request.Broadcast = true;
        //                request.Reply = "1";
        //                return request;
        //            }
        //            else
        //            {
        //                request.Broadcast = true;
        //                request.Reply = "2";
        //                return request;
        //            }
        //        }
        //        else
        //        {
        //            request.Reply = "0";
        //            return request;
        //        }
        //    }

        //    private Request HandleRemove(Request request)
        //    {
        //        string parameters = request.Parameters;
        //        string[] prefixes = parameters.Split(';');
        //        Guid id = Guid.Parse(prefixes[0]);
        //        if (MapInfo.Objects.Keys.Where(x => x.Identity == id).Count() == 1)
        //        {
        //            MapInfo.RemoveObject(id);
        //            request.Reply = "1";
        //            request.Broadcast = true;
        //            return request;
        //        }
        //        else
        //        {
        //            request.Reply = "0";
        //            return request;
        //        }
        //    }
        //    private Request HandleObject(Request request)
        //    {
        //        string parameters = request.Parameters;
        //        string[] prefixes = parameters.Split(';');
        //        Guid id = Guid.Parse(prefixes[0]);
        //        double xPos = double.Parse(prefixes[1]);
        //        double yPos = double.Parse(prefixes[2]);
        //        int width = int.Parse(prefixes[3]);
        //        int height = int.Parse(prefixes[4]);
        //        int angle = int.Parse(prefixes[5]);

        //        if (MapInfo.Objects.Keys.Where(x => x.Identity == id).Count() == 1)
        //        {
        //            request.Reply = "0";
        //            return request;
        //        }
        //        MapInfo.AddObject(new Player(id, Physics.PlayerPhysics, new Vector(xPos, yPos), width, height) { Angle = angle });
        //        request.Broadcast = true;
        //        request.Reply = "1";
        //        return request; 
        //    }

        //    private Request[] HandleMovement(Request request)
        //    {
        //        List<Request> retvals = new List<Request>();
        //        string parameters = request.Parameters;
        //        string[] prefixes = parameters.Split(';');
        //        Guid id = Guid.Parse(prefixes[0]);
        //        Player baseObject = null;
        //        var players = MapInfo.Players;
        //        if (players.Where(r => r.Identity == id).Count() == 1)
        //        {
        //            baseObject = (Player)players.Where(c => c.Identity == id).FirstOrDefault();
        //        }
        //        else
        //        {
        //            throw new Exception("Not contained id.");
        //        }
        //        //Player baseObject = MapInfo.Players.Where(r => r.Identity == id).Count() == 1 ? MapInfo.Players.Where(c => c.Identity == id).FirstOrDefault() : throw new Exception("Not contained id.");
        //        double x = double.Parse(prefixes[1]);
        //        double y = double.Parse(prefixes[2]);
        //        double angle = double.Parse(prefixes[3]);
        //        BaseObject[] Colliders = MapInfo.CollidesWithAny(id);
        //        var visuals = MapInfo.Visuals;
        //        //Geometry baseGeometry = visuals.Where(curr => curr.Identity == id)
        //        //    .First().Primitive.RenderedGeometry;
        //        for (int index = 0; index < Colliders.Length; index++)
        //        {
        //            //Geometry currentGeometry = visuals.Where(curr => curr.Identity == Colliders[index].Identity)
        //            //.First().Primitive.RenderedGeometry;
        //            //var p = MapInfo.Players;
        //            //BaseObject currentObject = (Player)p.Where(c => c.Identity == id).FirstOrDefault();
        //            //if (CollidesWith(baseGeometry, currentGeometry, baseObject.Position, currentObject.Position) )
        //            //{
        //                if (Colliders[index].GetType() == typeof(Bullet))
        //                {
        //                    retvals.Add(MovementHit(baseObject, (Bullet)Colliders[index]));
        //                }
        //                else
        //                {
        //                    request.Reply = "0";
        //                    return retvals.ToArray();
        //                }
        //            //}
        //        }
        //        SetObjectPositon(id, x, y, angle);
        //        request.Reply = "1";
        //        request.Broadcast = true;
        //        retvals.Add(request);
        //        return retvals.ToArray();
        //    }

        //    private Request HandleShooting(Request request)
        //    {
        //        // $"{id};{weapon};{bullet};{StartPos.X};{StartPos.Y};{Direction.X};{Direction.Y};{speed}"

        //        string parameters = request.Parameters;
        //        string[] prefixes = parameters.Split(';');
        //        Guid id = Guid.Parse(prefixes[0]);
        //        Guid weapon = Guid.Parse(prefixes[1]);
        //        if (MapInfo.Objects.Keys.Where(x => x.Identity == weapon).Count() == 1)
        //        {
        //            Weapon shootWeapon = (Weapon)MapInfo.Objects.Values.Where(x => x.Identity == weapon).First();
        //            if (shootWeapon.AncestorPlayer.Identity == id)
        //            {
        //                Guid bullet = Guid.Parse(prefixes[2]);
        //                double xStart = double.Parse(prefixes[3]);
        //                double yStart = double.Parse(prefixes[4]);
        //                double angle = double.Parse(prefixes[5]);
        //                MapInfo.AddObject(new Bullet(bullet, new Facilities(true, true, shootWeapon.BulletSpeed),
        //                    new Vector(xStart, yStart), 1, 3, angle, shootWeapon));
        //                request.Broadcast = true;
        //                return request;
        //            }
        //            else
        //            {
        //                throw new Exception("Weapon not owned by player.");
        //            }
        //        }
        //        else
        //        {
        //            throw new Exception("Weapon not exists.");
        //        }
        //    }

        //    private Request HandleMessageSend(Request request)
        //    {
        //        if (request.Parameters == "1")
        //        {
        //            request.Reply = "1";
        //        }
        //        return request;
        //    }

        //    private Request HandleServerCheck(Request request)
        //    {
        //        request.Reply = "0";
        //        var client = ClientInfos.Where(x => x.EndPoint.ToString() == request.Sender.ToString()).FirstOrDefault();
        //        client.Ready = true;
        //        if (ClientInfos.Where(x=> x.EndPoint!=null).All(x => x.Ready))
        //        {
        //            request.Reply = "1";
        //            Online = true;
        //        }
        //        return request;
        //    }

        //    private Request HandleDisconnection(Request request)
        //    {
        //        DeleteClient(request);
        //        request.Broadcast = true;
        //        request.Reply = "1";
        //        return request;
        //    }

        //    private Request HandleConnection(Request request)
        //    {
        //        request.Reply = $"{server.EndPoint.Address.ToString()};{server.EndPoint.Port.ToString()}";
        //        return request;
        //    }
        //    private Request MovementHit(Player player, Bullet bullet)
        //    {
        //        return new Request()
        //        {
        //            Sender = server.EndPoint,
        //            Operation = Damage(player, bullet) ? UDPNetworking.GameOperation.Hit : UDPNetworking.GameOperation.Die,
        //            Parameters = $"{player.Identity};{bullet.Identity};{bullet.Damage}",
        //            Broadcast = true,
        //            Reply = "1",
        //        };
        //    }

        //    //  Ha valami ütközés van akkor elküldi az "eseményeket", ha nincs akkor mehet tovább a golyó.
        //    public Request[] GetBulletHits(double deltaTime)
        //    {
        //        List<Request> requests = new List<Request>();
        //        foreach (var bullet in MapInfo.Bullets)
        //        {
        //            BaseObject[] colliders = MapInfo.CollidesWithAny(bullet.Identity);
        //            bool canMove = true;
        //            foreach (var collider in colliders)
        //            {
        //                if (collider.GetType() == typeof(Player))
        //                {
        //                    requests.Add(PlayerHit((Player)collider, bullet));
        //                    canMove = false;
        //                }
        //                else if(collider.GetType() == typeof(Wall))
        //                {
        //                    canMove = false;
        //                    requests.Add(WallHit((Wall)collider, bullet));
        //                }
        //            }
        //            if (canMove)
        //            {
        //                bullet.FlyForward(deltaTime);
        //            }
        //        }
        //        return requests.ToArray();
        //    }

        //    private Request WallHit(Wall wall, Bullet bullet)
        //    {
        //        MapInfo.RemoveObject(bullet.Identity);
        //        return new Request()
        //        {
        //            Sender = server.EndPoint,
        //            Operation = UDPNetworking.GameOperation.Remove,
        //            Parameters = $"{bullet.Identity}",
        //            Broadcast = true,
        //            Reply = "1",
        //        };
        //    }

        //    private Request PlayerHit(Player player, Bullet bullet)
        //    {
        //        return new Request()
        //        {
        //            Sender = server.EndPoint,
        //            Operation = Damage(player, bullet) ? UDPNetworking.GameOperation.Hit : UDPNetworking.GameOperation.Die,
        //            Parameters = $"{player.Identity};{bullet.Identity};{bullet.Damage}",
        //            Broadcast = true,
        //            Reply = "1",
        //        };
        //    }

        //    private bool Damage(Player player, Bullet bullet)
        //    {
        //        bullet.DoDamage(player);
        //        if (player.Alive)
        //        {
        //            return true;
        //        }
        //        return false;
        //    }

        //    private bool CollidesWith(Geometry firstObject, Geometry secondObject, Vector firstPosition, Vector secondPosition)
        //    {
        //        var transform = new TranslateTransform(firstPosition.X, firstPosition.Y);
        //        var baseGeometry = Geometry.Combine(firstObject, firstObject, GeometryCombineMode.Union, transform);
        //        transform = new TranslateTransform(secondPosition.X, secondPosition.Y);
        //        var shapeGeometry = Geometry.Combine(secondObject, secondObject, GeometryCombineMode.Union, transform);
        //        if (baseGeometry.GetArea() > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
    }

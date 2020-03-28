using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TCPServer_Client.Gamespace.GameObjects;
using TCPServer_Client.Gamespace.GameObjects.Structs;

namespace TCPServer_Client.GameLogic
{
    public static class Physics
    {
        // Cant create Object from this class
        // Handles movement, mass and collision.
        /*
            On the Map we need to place Static objects
            And the Immobilized objects too
            So we will need methods for this task.
            Method List:
            Collides - returns List
            PlaceObject - returns void 
            private Calculate distance - returns double
             */
        
        /*
         Constants of the game Physics.
             */
        public static Facilities PlayerPhysics = new Facilities(true, true, 100);
        public static Facilities BulletPhysics = new Facilities(true, true, 0);
        public static Facilities WeaponPhysics = new Facilities(true, false, 0);
        public static Facilities BushPhysics = new Facilities(false, false, 0);
        public static Facilities WallPhysics = new Facilities(false, true, 0);
        public static Facilities LogPhysics = new Facilities(false, true, 0);

        public const int PistolaBulletSpeed = 20; 
        public const int ShotgunBulletSpeed = 15;
        public const int MachinegunBulletSpeed = 25;
        public const int PistolaBulletDamage = 10;
        public const int ShotgunBulletDamage = 10;
        public const int MachinegunBulletDamage = 15;
        private const int StrokeThick = 3;

        private static ImageBrush playerBrush = new ImageBrush(new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}\\textures\\player.png")));
        private static ImageBrush wallBrush = new ImageBrush(new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}\\textures\\wall.png")));
        private static ImageBrush logBrush = new ImageBrush(new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}\\textures\\log.png")));
        private static ImageBrush bushBrush = new ImageBrush(new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}\\textures\\bush.png")));
        private static ImageBrush weaponBrush = new ImageBrush(new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}\\textures\\weapon.png")));

        public static Ellipse playerVisual = new Ellipse() { Width = 20, Height=20, StrokeThickness = 3, Fill = playerBrush };
        public static Ellipse bullletVisual = new Ellipse() { Width = 3, Height=1, StrokeThickness = 3};
        public static Rectangle wallVisual(int width, int height)
        {
            return new Rectangle() { Width = width, Height = height, StrokeThickness = 3, Fill = wallBrush };
        }
        internal static Shape logVisual(int width, int height)
        {
            return new Ellipse() { Width = width, Height = height, StrokeThickness = 3, Fill = logBrush };
        }

        internal static Shape bushVisual(int width, int height)
        {
            return new Rectangle() { Width = width, Height = height, StrokeThickness = 3, Fill = bushBrush };
        }

        internal static Shape weaponVisual(int width, int height)
        {
            return new Rectangle() { Width = width, Height = height, StrokeThickness = 3, Fill = weaponBrush };
        }


        //Calculate of the collision of one object with any other
        /*
         0 - Store all object in array
         1 - Get the Shape of the object
         2 - Calculate collisions
         3 - Return if collide and save Collide Object(s)
         4 - For circles we have to use Pitagoras Theory 
         5 - For rectangles we have much to do
         6 - We need a good arch for many objects to be checked for collision
         */
    }
}

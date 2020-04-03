using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace obServer.ViewController.Render.Texture
{
    public class RenderCache
    {
        public RenderCache()
        {
            MapCache = new DrawingGroup();
            GroundCache = new DrawingGroup();
            MiddleCache = new DrawingGroup();
            TopCache = new DrawingGroup();
            UICache = new DrawingGroup();
        }
        public DrawingGroup MapCache { get; set; } 

        public DrawingGroup GroundCache { get; set; }

        public DrawingGroup MiddleCache { get; set; }

        public DrawingGroup TopCache { get; set; }

        public DrawingGroup UICache { get; set; }
    }
}

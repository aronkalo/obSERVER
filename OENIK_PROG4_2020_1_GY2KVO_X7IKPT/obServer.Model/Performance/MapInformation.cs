using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obServer.Model.Performance
{
    class MapInformation
    {
        public MapInformation(int width, int height)
        {
            map = new Guids[width, height];
            InitMap(width, height);
            ContainedElements = new Dictionary<Guid, int[]>();
        }

        private Guids[,] map;

        private Dictionary<Guid, int[]> ContainedElements;

        public void Add(Guid id, int xStart, int yStart, int width, int heigth)
        {
            var elements = ContainedElements.Where(x => x.Key == id);
            if (elements.Count() == 1)
            {
                Del(id);
                ContainedElements.Remove(id);
            }
            AddRange(id, xStart, yStart,  xStart + width, yStart + heigth);
            ContainedElements.Add(id, new int[] { xStart, yStart, width, heigth });
        }

        public void Del(Guid id)
        {
            var elements = ContainedElements.Where(x => x.Key == id);
            if (elements.Count() == 1)
            {
                int[] dim = elements.First().Value;
                ContainedElements.Remove(id);
                DelRange(id, dim[0], dim[1], dim[2], dim[3]);
            }
        }

        public IEnumerable<Guid> Collision(Guid id)
        {
            var elements = ContainedElements.Where(x => x.Key == id);
            if (elements.Count() == 1)
            {
                int[] dim = elements.First().Value;
                List<Guid> colliders = new List<Guid>();
                Action<int, int> act = new Action<int, int>((x, y) =>
                {
                    colliders.AddRange(map[x, y].List());
                });
                IterateRange(id, dim[0], dim[1], dim[2], dim[3], act);
                var dist = colliders.Distinct().Where(x => x != id);
                return dist;
            }
            throw new Exception("not contained id");
        }

        private void AddRange(Guid id, int xStart, int yStart, int width, int heigth)
        {
           Action<int, int> act = new Action<int, int>((x, y) =>
           {
               map[x, y].Add(id);
           });
           IterateRange(id, xStart, yStart, width, heigth, act);
        }

        private void DelRange(Guid id, int xStart, int yStart, int width, int heigth)
        {
            Action<int, int> act = new Action<int, int>((x, y) =>
            {
                map[x, y].Del(id);
            });
            IterateRange(id, xStart, yStart, width, heigth, act);
        }

        private void IterateRange(Guid id, int xStart, int yStart, int width, int heigth, Action<int, int> act)
        {
            int i = 0;
            for (int x = xStart; x < width -1; x++)
            {
                    for (int y = yStart; y < heigth - 1; y++)
                    {
                    act?.Invoke(x, y);
                    }
            }
        }
        private void InitMap(int width, int height)
        {
            for (int i = 0; i < width; i++)
            {
                for (int y = 0; y < height; y++)
                {
                    map[i, y] = new Guids();
                }
            }
        }

    }
}
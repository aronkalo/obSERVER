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
            AddRange(id, xStart, yStart, width, heigth);
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

        private void IterateRange(Guid id, int xStart, int yStart, int width, int heigth, Action<int,int> innerMethod)
        {
            Task[] tasks = new Task[width - xStart];
            for (int x = xStart; x < width; x++)
            {
                Task t = new Task(() =>
                {
                    for (int y = yStart; y < heigth; y++)
                    {
                        innerMethod?.Invoke(x,y);
                    }
                });
                tasks[x - xStart] = t;
                t.Start();
            }
            Task.WaitAll(tasks);
        }
        private void InitMap(int width, int height)
        {
            Action<int, int> act = new Action<int, int>((x, y) =>
            {
                map[x, y] = new Guids();
            });
            IterateRange(Guid.NewGuid(), 0, 0, width, height, act);
        }

    }
}
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Core
{
    //public class ObjectPool<T> where T : new()
    //{
    //    public delegate T NewDelegate();
    //    NewDelegate New;

    //    private readonly ConcurrentBag<T> items = new ConcurrentBag<T>();
    //    private int counter = 0;
    //    private int MAX;
    //    public void Release(T item)
    //    {
    //        Console.WriteLine("Release");
    //        if (counter < MAX)
    //        {
    //            items.Add(item);
    //            counter++;
    //        }
    //    }
    //    public T Get()
    //    {
    //        T item;
    //        if (items.TryTake(out item))
    //        {
    //            counter--;
    //            return item;
    //        }
    //        else
    //        {
    //            Debug.Assert(New != null);
    //            T obj = New();
    //            items.Add(obj);
    //            counter++;
    //            return obj;
    //        }
    //    }

    //    public ObjectPool(NewDelegate NewFunc, int max)
    //    {
    //        New = NewFunc;
    //        MAX = max;
    //    }
    //}

    public class ObjectPool<T> where T : class
    {
        private ConcurrentBag<T> _objects;
        private Func<T> _objectGenerator;

        public ObjectPool(Func<T> objectGenerator)
        {
            Debug.Assert(objectGenerator != null);
            _objects = new ConcurrentBag<T>();
            _objectGenerator = objectGenerator;
        }

        public T GetObject()
        {
            T item;
            if (_objects.TryTake(out item)) return item;
            return _objectGenerator();
        }

        public void PutObject(T item)
        {
            _objects.Add(item);
        }
    }
}

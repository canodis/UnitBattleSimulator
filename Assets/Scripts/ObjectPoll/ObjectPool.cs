using System;
using System.Collections.Generic;

public class ObjectPool<T> where T : class
{
    private Queue<T> pool = new Queue<T>();
    private Func<T> createFunc;
    private Action<T> activeAction;
    private Action<T> deactiveAction;

    public ObjectPool(Func<T> createFunc, Action<T> activeAction, Action<T> deactiveAction = null, int initialCapacity = 0)
    {
        this.createFunc = createFunc;
        this.deactiveAction = deactiveAction;
        this.activeAction = activeAction;
        for (int i = 0; i < initialCapacity; i++)
        {
            pool.Enqueue(createFunc());
        }
    }

    public T Get()
    {
        if (pool.Count == 0)
            pool.Enqueue(createFunc());
        T item = pool.Dequeue();
        activeAction?.Invoke(item);
        return item;
    }

    public void Return(T item)
    {
        deactiveAction?.Invoke(item);
        pool.Enqueue(item);
    }
}

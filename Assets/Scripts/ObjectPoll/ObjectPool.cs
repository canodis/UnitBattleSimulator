using System;
using System.Collections.Generic;

/// <summary>
/// Generic object pool for efficient object reuse.
/// </summary>
/// <typeparam name="T">Type of objects to be pooled (class).</typeparam>
public class ObjectPool<T> where T : class
{
    private Queue<T> _pool = new Queue<T>();
    private Func<T> _createFunc;
    private Action<T> _activeAction;
    private Action<T> _deactiveAction;

    /// <summary>
    /// Initializes a new instance of the ObjectPool class.
    /// </summary>
    /// <param name="createFunc">Function to create new objects when the pool is empty.</param>
    /// <param name="activeAction">Action to perform when an object is retrieved from the pool.</param>
    /// <param name="deactiveAction">Action to perform when an object is returned to the pool (optional).</param>
    /// <param name="initialCapacity">Initial capacity of the pool.</param>
    public ObjectPool(Func<T> createFunc, Action<T> activeAction, Action<T> deactiveAction = null, int initialCapacity = 0)
    {
        this._createFunc = createFunc;
        this._deactiveAction = deactiveAction;
        this._activeAction = activeAction;
        for (int i = 0; i < initialCapacity; i++)
        {
            _pool.Enqueue(createFunc());
        }
    }

    /// <summary>
    /// Retrieves an object from the pool.
    /// </summary>
    /// <returns>The retrieved object.</returns>
    public T Get()
    {
        if (_pool.Count == 0)
            _pool.Enqueue(_createFunc());
        T item = _pool.Dequeue();
        _activeAction?.Invoke(item);
        return item;
    }

    /// <summary>
    /// Returns an object to the pool for reuse.
    /// </summary>
    /// <param name="item">The object to return to the pool.</param>
    public void Return(T item)
    {
        _deactiveAction?.Invoke(item);
        _pool.Enqueue(item);
    }
}

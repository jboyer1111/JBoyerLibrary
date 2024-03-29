﻿namespace JBoyer.Testing
{

    public class ReturnsQueue<T>
    {

        #region Private Variables

        private Queue<T> _queue;
        private object?[]? _args;
        private Func<T?> _constructorFunc;

        #endregion

        #region Constructor

        public ReturnsQueue(T defaultItem)
        {
            _queue = new Queue<T>();
            _constructorFunc = () => defaultItem;
        }

        public ReturnsQueue(params object[] args)
        {
            // Setup Private variables
            _queue = new Queue<T>();
            _args = args;

            try
            {
                var testObj = Activator.CreateInstance(typeof(T), args);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Invalid arguments passed in for type", e);
            }

            _constructorFunc = () => (T?)Activator.CreateInstance(typeof(T), _args);
        }

        #endregion

        #region Public Properties

        public void Enqueue(T item) => _queue.Enqueue(item);

        public void Enqueue(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Enqueue(item);
            }
        }

        /// <summary>
        ///  This gets the next item in the Queue. If the queue is empty than it creates a new object using the arguments passed at creation time
        /// </summary>
        /// <returns></returns>
        public T? GetNext()
        {
            if (_queue.Count < 1)
            {
                return _constructorFunc();
            }
            
            return _queue.Dequeue();
        }

        #endregion

    }

}

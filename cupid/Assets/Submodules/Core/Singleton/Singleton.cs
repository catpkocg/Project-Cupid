namespace Wayway.Engine.Singleton
{
    public class Singleton<T> where T : class, new()
    {
        private static T instance;

        private static object @lock = new ();

        public static T Instance
        {
            get
            {
                lock (@lock)
                {
                    return instance ??= new T();
                }
            }
        }

        protected Singleton() { }
    }
}

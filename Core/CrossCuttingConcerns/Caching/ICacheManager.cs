namespace Core.CrossCuttingConcerns.Caching
{
    //Şimdilik .Net kendi içerisinde var olan Microsoft'un kendi InMemory Caching sistemini kullanacağız, ancak ilerde Redis gibi ya da başka gelişmiş bir Caching mekanizmasına geçmek istersek Interface yazdığımız için sistemimize kolayca entegre edebileceğiz
    public interface ICacheManager
    {
        T Get<T>(string key);
        object Get(string key); //yukarıdakiden farkı, bir tip dönüşümü yani casting işlemi yapmamız gerekir
        void Add(string key, object value, int duration);
        bool CheckIfInCache(string key);
        void Remove(string key);
        void RemoveByPattern(string pattern);
    }
}
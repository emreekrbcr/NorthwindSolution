using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager:ICacheManager
    {
        //Önemli!!! Normalde bağımlılık zinciri WebAPI-->Business-->DataAccess şeklinde ilerliyor ancak Aspect'lerimiz bu zincirin içerisinde değil bambaşka bir yerde dolayısıyla Aspect için çözümleme yaparken kendi yazdığımız ServiceTool'u kullanmamız gerek. Bunu her proje için ortak çözümlemeleri yapacak CoreModule'a ekleyebiliriz
        
        //Aşağıda dikkat edilirse aynı metodları aynı isimlerle manyak gibi çağırıp çağırıp duruyor gibi duruyoruz. Direkt bunu nerede kullanacak oraya yazabilirdik. Ancak yarın öbür gün sistemi Microsoft'un Memory Cache'in başka bir yapıya çevirmek istersek bu metodlara bağımlı olduğumuzdan dolayı patlarız. Patlalamak için Interface'den inherit edilen bir class'ın içine koyup kendi sistemimize adapte ediyoruz. Buna da Adapter Pattern deniyor.  

        private readonly IMemoryCache _memoryCache;

        public MemoryCacheManager()
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));
        }

        public bool CheckIfInCache(string key)
        {
            return _memoryCache.TryGetValue(key, out _); //metod zorla out value ile değerini döndürmek istiyor ancak eyvallah kardeş sana zahmet olmasın bana var mı yok mu onu öğren yeter diyoruz. Bunun da syntax'ı _ işareti
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            //Microsoft bellekte birşeyi cache'lediğinde onu EntriesCollection isimli bir property'de tutuyor
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            //Definition'u MemoryCache olanları bul
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            //Cache'ları tek tek dolaş
            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }
            //Regex'e uyanları kontrol et ve al
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Singleline | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key)
                .ToList();
            //Onları bellekten uçur
            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }
        }
    }
}
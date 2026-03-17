using UnityEngine;

namespace Calculator.Data.Base
{
    public abstract class BaseRepository<T> : IRepository<T>
    {
        protected abstract string Key { get; }

        public T Load()
        {
            if (!PlayerPrefs.HasKey(Key))
            {
                return default;
            }

            var json = PlayerPrefs.GetString(Key);
            return JsonUtility.FromJson<T>(json) ?? default;
        }

        public void Save(T data)
        {
            var json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(Key, json);
            PlayerPrefs.Save();
        }
    }
}
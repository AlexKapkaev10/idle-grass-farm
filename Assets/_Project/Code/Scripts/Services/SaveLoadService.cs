using UnityEngine;

namespace Project.Services
{
    public interface ISaveLoadService
    {
        void SaveInt(int value, string key);
        void SaveFloat(float value, string key);
        int LoadInt(string key, int defaultValue = 0);
        float LoadFloat(string key, float defaultValue = 0);
    }
    
    public sealed class SaveLoadService : ISaveLoadService
    {
        public void SaveInt(int value, string key)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public void SaveFloat(float value, string key)
        {
            PlayerPrefs.SetFloat(key, value);
        }

        public int LoadInt(string key, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }

        public float LoadFloat(string key, float defaultValue = 0.0f)
        {
            return PlayerPrefs.GetFloat(key, defaultValue);
        }
    }
}
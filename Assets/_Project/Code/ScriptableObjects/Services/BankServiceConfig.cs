using Project.Game;
using UnityEngine;

namespace Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(BankServiceConfig), menuName = "Config/Service/Bank")]
    public class BankServiceConfig : ScriptableObject
    {
        private const string _saveFirstCurrencyKey = "FirstCurrencySaveKey";
        private const string _saveSecondCurrencyKey = "SecondCurrencySaveKey";
        
        public string GetCurrencySaveKeyByType(ResourceType resourceType)
        {
            return resourceType == ResourceType.First ? _saveFirstCurrencyKey : _saveSecondCurrencyKey;
        }
    }
}
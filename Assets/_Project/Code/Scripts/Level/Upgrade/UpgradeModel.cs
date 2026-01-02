using System;
using Project.ScriptableObjects;

namespace Project.Game
{
    public interface IUpgradeModel
    {
        void Initialize(AbilityType type);
        AbilityType GetType();
    }
    
    public class UpgradeModel : IUpgradeModel
    {
        private AbilityType _type;

        public void Initialize(AbilityType type)
        {
            _type = type;
        }

        AbilityType IUpgradeModel.GetType()
        {
            return _type;
        }
    }
}
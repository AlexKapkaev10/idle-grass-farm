namespace Project.Services
{
    public interface IToolService
    {
        int GetLevel();
        float GetMowRange();
        void Initialize(int level, float mowRange);
        void UpgradeLevel(float mowRange);
    }
    
    public class ToolService : IToolService
    {
        private int _level;
        private float _mowRange;

        void IToolService.Initialize(int level, float mowRange)
        {
            _level = level;
            _mowRange = mowRange;
        }

        public void UpgradeLevel(float mowRange)
        {
            _level++;
            _mowRange = mowRange;
        }

        public int GetLevel()
        {
            return _level;
        }

        public float GetMowRange()
        {
            return _mowRange;
        }
    }
}
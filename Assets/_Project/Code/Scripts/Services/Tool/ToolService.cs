namespace Project.Services
{
    public interface IToolService
    {
        int GetLevel();
        float GetMowRange();
        void UpgradeLevel(float mowRange);
    }
    
    public class ToolService : IToolService
    {
        private int _level;
        private float _mowRange;

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
namespace Project.Game
{
    public interface IAnimatorComponent
    {
        void SetBool(int id, bool value);
        void SetTrigger(int id);
        AnimationEventsReceiver EventsReceiver { get; }
    }
}
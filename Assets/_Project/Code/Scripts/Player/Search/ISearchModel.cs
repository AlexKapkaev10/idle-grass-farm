using System;

namespace Project.Game
{
    public interface ISearchModel : IDisposable
    {
        event Action<IInteractable, bool> Interacted;
        void Process();
    }
}
using System;
using Zenject;

namespace _Project.Code.Infrastructure
{
    public interface ILocalContextService
    {
        DiContainer Context { get; }
        void CleanUp();
        void WarmUp(Action<DiContainer> installation);
        void Inject(object obj);
    }
}
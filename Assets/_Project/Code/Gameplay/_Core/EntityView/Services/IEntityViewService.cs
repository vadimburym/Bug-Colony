using System.Collections.Generic;
using _Project.Code.Core.Keys;

namespace _Project.Code.Gameplay._Core
{
    public interface IEntityViewService
    {
        IReadOnlyCollection<EntityView> Views { get; }
        bool TryGetView(EntityGUID guid, out EntityView view);
        void Register(EntityView view, EntityGUID guid);
        void Unregister(EntityGUID guid);
    }
}
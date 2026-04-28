using System.Collections.Generic;
using _Project.Code.Core.Keys;

namespace _Project.Code.Gameplay._Core
{
    public sealed class EntityViewService : IEntityViewService
    {
        public IReadOnlyCollection<EntityView> Views => _views.Values;
        
        private readonly Dictionary<EntityGUID, EntityView> _views = new();

        public bool TryGetView(EntityGUID guid, out EntityView view) => _views.TryGetValue(guid, out view);
        
        public void Register(EntityView view, EntityGUID guid)
        {
            if (view == null || guid == EntityGUID.Null || _views.ContainsKey(guid))
                return;
            _views.Add(guid, view);
        }

        public void Unregister(EntityGUID guid)
        {
            _views.Remove(guid);
        }
    }
}
using System;
using _Project.Code.Core.Abstractions;
using _Project.Code.Gameplay.CoreFeatures;

namespace _Project.Code.Gameplay._Core
{
    public sealed class EntityViewPositionSyncSystem : ILateTick
    {
        private readonly IEntityViewService _entityViewService;
        private readonly ICirclePhysicsService _physicsService;
        
        public EntityViewPositionSyncSystem(
            IEntityViewService entityViewService,
            ICirclePhysicsService physicsService)
        {
            _entityViewService = entityViewService;
            _physicsService = physicsService;
        }

        public void LateTick()
        {
            foreach (var view in _entityViewService.Views)
            {
                if (!_physicsService.TryGetBody(view.GUID, out var body))
                    continue;
                
                view.UpdateWorldPosition(body.WorldPosition);
            }
        }
    }
}
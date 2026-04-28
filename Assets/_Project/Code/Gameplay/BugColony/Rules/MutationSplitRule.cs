using System;
using _Project.Code.Core.Abstractions;
using _Project.Code.Core.Events;
using _Project.Code.Core.Keys;
using _Project.Code.Infrastructure;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Project.Code.Gameplay.CoreFeatures.BugColony
{
    [Serializable]
    public sealed class MutationSplitRule : ISplitRule
    {
        [FoldoutGroup("Split Settings"), SerializeField] private int _consumptionToSplit;
        [FoldoutGroup("Split Settings"), SerializeField] private BugId _bugId;
        [FoldoutGroup("Split Settings"), SerializeField] private int _splitCount;

        [FoldoutGroup("Mutation Settings"), SerializeField] private BugId _mutation;
        [FoldoutGroup("Mutation Settings"), SerializeField, Range(0f, 1f)] private float _mutationChance;
        [FoldoutGroup("Mutation Settings"), SerializeField] private int _bugsCountCondition;
        
        [Inject] private readonly IBugFactory _bugFactory;
        [Inject] private readonly IDestructibleService _destructibleService;
        [Inject] private readonly IBugColonyService _bugColonyService;
        [Inject] private readonly IRandomService _randomService;
        [Inject] private readonly ICirclePhysicsService _physicsService;
        
        public void Execute(ConsumptionUpdateEvent @event)
        {
            if (@event.Consumption < _consumptionToSplit)
                return;

            var position = Vector2.zero;
            if (_physicsService.TryGetBody(@event.Entity, out var body))
                position = body.WorldPosition;
            
            if (_destructibleService.IsDestructible(@event.Entity))
                _destructibleService.Destroy(DestroyCause.Split, @event.Entity);
            
            var isMutation = (_bugColonyService.BugsCount > _bugsCountCondition) && (_randomService.Value01() <= _mutationChance);
            if (isMutation)
                _bugFactory.CreateAndRegister(_mutation, position);
            
            var count = isMutation ? _splitCount - 1 : _splitCount;
            for (int i = 0; i < count; i++)
                _bugFactory.CreateAndRegister(_bugId, position);
        }
    }
}
using System;
using UniRx;
using UnityEngine;

namespace _Project.Code.Gameplay.Statistics.UI
{
    public interface IDeathStatPresenter : IDisposable
    {
        Color IconColor { get; }
        IReadOnlyReactiveProperty<string> Deaths { get; }
    }
}
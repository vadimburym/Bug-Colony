using System;
using System.Collections.Generic;

namespace _Project.Code.Gameplay.Statistics.UI
{
    public interface IStatisticPresenter : IDisposable
    {
        IReadOnlyList<IDeathStatPresenter> Presenters { get; }
    }
}
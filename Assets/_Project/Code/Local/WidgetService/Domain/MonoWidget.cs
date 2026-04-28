using System;
using UnityEngine;

namespace _Project.Code.Local
{
    public abstract class MonoWidget<TPresenter> : MonoBehaviour
        where TPresenter : IDisposable
    {
        public abstract void Initialize(TPresenter presenter);
    }
}
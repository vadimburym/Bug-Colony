using System;
using _Project.Code.Core.Keys;

namespace _Project.Code.Local
{
    public interface IWidgetService
    {
        event Action<ScreenId> OnScreenChanged;
        bool IsMainScreen { get; }
        void ShowScreen(ScreenId name);
        void HideCurrentScreen();
    }
}
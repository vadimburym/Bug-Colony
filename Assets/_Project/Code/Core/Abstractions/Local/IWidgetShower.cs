using _Project.Code.Core.Keys;

namespace _Project.Code.Core.Abstractions
{
    public interface IWidgetShower
    {
        WidgetId ID { get; }
        void Show();
        void Hide();
    }
}
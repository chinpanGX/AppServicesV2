using UnityEngine.UIElements;

namespace UniVerse.Editor.CustomToolbar
{
    public abstract class CustomToolbarItem
    {
        public enum LayoutPositionType
        {
            Left,
            Right
        }

        protected const float BaseFontSize = 12.0f;

        public int Priority { get; }

        public LayoutPositionType Position { get; }

        protected CustomToolbarItem(int priority, LayoutPositionType position)
        {
            Priority = priority;
            Position = position;
        }

        public abstract void OnCreateElement(VisualElement visualElement);

        public virtual void OnFocusChanged(bool isFocused) { }
    }
}
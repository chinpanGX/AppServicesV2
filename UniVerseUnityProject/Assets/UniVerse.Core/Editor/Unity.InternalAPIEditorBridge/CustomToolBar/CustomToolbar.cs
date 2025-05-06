using System.Collections.Generic;
using UnityEngine.UIElements;

namespace UniVerse.Editor.CustomToolbar
{
    internal class CustomToolbar
    {
        private readonly List<CustomToolbarItem> leftItems;
        private readonly List<CustomToolbarItem> rightItems;

        public CustomToolbar(List<CustomToolbarItem> leftItems, List<CustomToolbarItem> rightItems)
        {
            this.leftItems = leftItems;
            this.rightItems = rightItems;
        }

        public void InitializeElement(VisualElement left, VisualElement right)
        {
            var leftPane = CreatePane();
            left.Add(leftPane);
            foreach (var item in leftItems)
            {
                item.OnCreateElement(leftPane);
            }

            var rightPane = CreatePane();
            right.Add(rightPane);
            foreach (var item in rightItems)
            {
                item.OnCreateElement(rightPane);
            }
        }

        public void OnFocusChanged(bool isFocused)
        {
            foreach (var item in leftItems)
            {
                item.OnFocusChanged(isFocused);
            }
            foreach (var item in rightItems)
            {
                item.OnFocusChanged(isFocused);
            }
        }

        private VisualElement CreatePane()
        {
            return new VisualElement() 
            { 
                style = 
                {
                    flexGrow = 1, alignItems = Align.Auto, 
                    flexDirection = FlexDirection.Row, justifyContent = Justify.FlexEnd
                }
            };
        }
    }
}
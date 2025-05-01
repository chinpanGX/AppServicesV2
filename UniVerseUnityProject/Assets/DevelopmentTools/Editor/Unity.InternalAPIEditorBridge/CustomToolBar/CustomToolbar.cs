using System.Collections.Generic;
using UnityEngine.UIElements;

namespace DevelopmentTools.CustomToolbar
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
            var leftElement = Create();
            left.Add(leftElement);
            foreach (var item in leftItems)
            {
                item.OnCreateElement(leftElement);
            }

            var rightElement = Create();
            right.Add(rightElement);
            foreach (var item in rightItems)
            {
                item.OnCreateElement(rightElement);
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

        private VisualElement Create()
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
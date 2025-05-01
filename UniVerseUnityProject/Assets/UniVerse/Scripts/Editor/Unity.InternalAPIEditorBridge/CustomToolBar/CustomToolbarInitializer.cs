using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UniVerse.Editor.CustomToolbar
{
    internal class CustomToolbarInitializer
    {
        [UnityEditor.InitializeOnLoadMethod]
        private static void OnInitializeCustomToolBar()
        {
            var toolbar = CreateCustomToolbar();
            var initializer = new CustomToolbarInitializer(toolbar);
            initializer.Initialize();
        }

        private static List<Type> DetectDerivedToolbarItems()
        {
            return TypeCache.GetTypesDerivedFrom<CustomToolbarItem>()
                .Where(type => !type.IsAbstract && type.IsClass)
                .ToList();
        }

        private static CustomToolbar CreateCustomToolbar()
        {
            var leftButtonList = new List<CustomToolbarItem>();
            var rightButtonList = new List<CustomToolbarItem>();
            var types = DetectDerivedToolbarItems();
            foreach (var type in types)
            {
                try
                {
                    if (Activator.CreateInstance(type) is CustomToolbarItem val)
                    {
                        switch (val.Position)
                        {
                            case CustomToolbarItem.LayoutPositionType.Left:
                                leftButtonList.Add(val);
                                break;
                            case CustomToolbarItem.LayoutPositionType.Right:
                                rightButtonList.Add(val);
                                break;
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
            leftButtonList.Sort((a, b) => a.Priority.CompareTo(b.Priority));
            rightButtonList.Sort((a, b) => a.Priority.CompareTo(b.Priority));
            return new CustomToolbar(leftButtonList, rightButtonList);
        }

        CustomToolbar customToolbar;

        public CustomToolbarInitializer(CustomToolbar customToolbar)
        {
            this.customToolbar = customToolbar;
        }

        public void Initialize()
        {
            EditorApplication.update += LazyInitialization;
            EditorApplication.focusChanged += customToolbar.OnFocusChanged;
        }

        private void LazyInitialization()
        {
            var toolbar = UnityEditor.Toolbar.get;
            if (toolbar.windowBackend?.visualTree is not VisualElement visualTree)
                return;
            if (visualTree.Q("ToolbarZoneLeftAlign") is not { } leftZone)
                return;
            if (visualTree.Q("ToolbarZoneRightAlign") is not { } rightZone)
                return;
            EditorApplication.update -= LazyInitialization;
            customToolbar.InitializeElement(leftZone, rightZone);
        }
    }
}
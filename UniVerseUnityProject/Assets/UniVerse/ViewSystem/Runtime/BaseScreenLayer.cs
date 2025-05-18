using System.Collections.Generic;
using UnityEngine;
using UniVerse.ViewSystem.Logger;
using UnVerse.Core;

namespace UnVerse.ViewSystem
{
    public abstract class BaseScreenLayer : MonoBehaviour
    {
        [SerializeField] private Camera screenCamera;
        private readonly List<IView> views = new(10);
        
        protected abstract int SortingOrder  { get; set; }

        public void Push(IView view)
        {
            if (view == null)
            {
                LoggerEx.LogWarning("Attempted to push a null view.");
                return;
            }

            if (!views.Contains(view))
            {
                views.Add(view);
            }
            else
            {
                LoggerEx.LogWarning("The view is already in the stack.");
                return;
            }

            ConfigureView(view);
        }

        public void Pop(IView view)
        {
            if (view == null)
            {
                LoggerEx.LogWarning("Attempted to pop a null view.");
                return;
            }

            if (views.Count == 0)
            {
                LoggerEx.LogWarning("No views to pop.");
                return;
            }

            if (views.Remove(view))
            {
                SortingOrder = Mathf.Max(--SortingOrder, 0);
            }
            else
            {
                LoggerEx.LogWarning("The view is not in the stack.");
            }
        }

        private void ConfigureView(IView view)
        {
            view.Canvas.worldCamera = screenCamera;
            view.Canvas.sortingOrder = ++SortingOrder;
            view.Canvas.transform.SetParent(transform, false);
        }
    }
}
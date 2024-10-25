using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UISystem.Core
{
    public class ViewManager : MonoBehaviour
    {
        public static ViewManager instance;

        public event UnityAction<View> ViewShow;
        public void OnViewShow(View view) => ViewShow?.Invoke(view);

        [SerializeField] private View startingView;
        [SerializeField] private View[] allViews;

        private readonly Stack<View> viewHistory = new();
        private View activeView;

        private void Awake() => instance = this;

        private void Start()
        {
            foreach (View view in allViews)
            {
                view.Initialize();
                view.Hide();
            }

            if (startingView == null)
                return;
            ShowView(startingView);
        }

        public void ShowView<T>(bool saveInHistory = true)
        {
            foreach (View view in allViews)
            {
                if (view is not T)
                    continue;
                if (activeView != null)
                {
                    if (saveInHistory)
                    {
                        instance.viewHistory.Push(activeView);
                    }
                    activeView.Hide();
                }

                view.Show();
                activeView = view;
            }
        }

        public void ShowView(View view, bool saveInHistory = true)
        {
            if (activeView != null)
            {
                if (saveInHistory)
                {
                    viewHistory.Push(instance.activeView);
                }

                activeView.Hide();
            }

            view.Show();
            activeView = view;
        }

        public void ShowLastView()
        {
            if (instance.viewHistory.Count <= 0)
                return;
            ShowView(instance.viewHistory.Pop());
        }
    }
}
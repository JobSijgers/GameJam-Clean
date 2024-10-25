using UnityEngine;

namespace UISystem.Core
{
    public abstract class View : MonoBehaviour
    {
        [SerializeField] private ViewComponent[] viewComponents;

        public virtual void Initialize()
        {
            if (viewComponents == null || viewComponents.Length == 0)
                return;

            foreach (ViewComponent viewComponent in viewComponents)
            {
                if (viewComponent == null)
                    continue;
                viewComponent.Initialize();
            }
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
            ShowViewComponents();
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
            HideViewComponents();
        }

        private void ShowViewComponents()
        {
            foreach (ViewComponent viewComponent in viewComponents)
            {
                if (viewComponent == null)
                    continue;
                viewComponent.Show();
            }
        }

        private void HideViewComponents()
        {
            foreach (ViewComponent viewComponent in viewComponents)
            {
                if (viewComponent == null)
                    continue;
                viewComponent.Hide();
            }
        }
    }
}
using UnityEngine;

namespace UISystem.Core
{
    public abstract class ViewComponent : MonoBehaviour
    {
        public virtual void Initialize() { }
        public abstract void Show();
        public abstract void Hide();
    }
}
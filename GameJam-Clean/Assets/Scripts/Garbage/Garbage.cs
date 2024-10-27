using UnityEngine;
using UnityEngine.Events;

namespace Garbage
{
    public class Garbage : MonoBehaviour
    {
        public UnityEvent _onPickup;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;
            _onPickup?.Invoke();
            Destroy(gameObject);
        }
    }
}
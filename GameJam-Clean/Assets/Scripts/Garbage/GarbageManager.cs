using UnityEngine;
using UnityEngine.Events;

namespace Garbage
{
    public class GarbageManager : MonoBehaviour
    {
        [SerializeField] private Garbage[] _allGarbage;
        [SerializeField] private UnityEvent<int> _onGarbageUpdated;
        [SerializeField] private UnityEvent _onAllGarbageCleaned;
        private int _garbageCount;

        private void Start()
        {
            _garbageCount = _allGarbage.Length;
            foreach (Garbage garbage in _allGarbage)
            {
                garbage._onPickup.AddListener(OnGarbagePickup);
            }
            
            _onGarbageUpdated?.Invoke(_garbageCount);
        }

        private void OnGarbagePickup()
        {
            _garbageCount--;
            _onGarbageUpdated?.Invoke(_garbageCount);
            if (_garbageCount == 0)
                _onAllGarbageCleaned?.Invoke();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class Robot : MonoBehaviour, IRobot
    {
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _rotationSpeed = 1f;
        
        private Coroutine _moveCoroutine;
    
        public void FollowPath(Queue<Vector3> path, UnityAction completeAction)
        {
            _moveCoroutine = StartCoroutine(FollowQueue(path, completeAction));
        }

        private IEnumerator FollowQueue(Queue<Vector3> path, UnityAction completeAction)
        {
            while (path.Count > 0)
            {
                Vector3 target = path.Dequeue();
                yield return RotateTowards(target);
                yield return MoveTowards(target);
            }
            completeAction?.Invoke();
            _moveCoroutine = null;
        }
    
        private IEnumerator RotateTowards(Vector3 target)
        {
            Vector3 direction = (target - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f) 
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
                yield return null;
            }
        }

        private IEnumerator MoveTowards(Vector3 target)
        {
            while (Vector3.Distance(transform.position, target) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
                yield return null;
            }
        }

        public bool IsMoving()
        {
            return _moveCoroutine != null;
        }
    }
}

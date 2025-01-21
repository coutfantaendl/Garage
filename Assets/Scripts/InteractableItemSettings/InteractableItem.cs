using UnityEngine;

namespace InteractableItemSettings
{
    public class InteractableItem : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        public void PickUp(Transform holdPoint)
        {
            transform.SetParent(holdPoint);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            _rigidbody.isKinematic = true;
        }
        
        public void Throw(Vector3 force)
        {
            transform.SetParent(null);
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(force, ForceMode.Impulse);
        }
    }
}
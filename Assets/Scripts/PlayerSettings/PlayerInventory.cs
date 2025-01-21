using UnityEngine;

namespace InteractableItemSettings
{
    public class PlayerInventory : MonoBehaviour
    {
        public InteractableItem HeldItem { get; set; }
        
        public void SetHeldItem(InteractableItem item)
        {
            HeldItem = item;
        }

        public void ClearHeldItem()
        {
            HeldItem = null;
        }
    }
}
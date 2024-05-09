using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeHub.OtherUtilities
{
    public class JumpButtonSimulator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IJumpSimulator
    {
        private bool _isPressed;

        public bool IsPressed => _isPressed;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _isPressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isPressed = false;
        }
    }
}
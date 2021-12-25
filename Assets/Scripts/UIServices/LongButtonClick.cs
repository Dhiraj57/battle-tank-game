using GlobalServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class LongButtonClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        EventService.Instance.InvokeOnFireButtonPressedEvent();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        EventService.Instance.InvokeOnFireButtonReleasedEvent();
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollRectEventHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public PageSwiper pageSwiper;

    public void OnDrag(PointerEventData eventData)
    {
        // forward the eventData from OnDrag to the page swiper
        pageSwiper.OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // forward the eventData from OnEndDrag to the page swiper
        pageSwiper.OnEndDrag(eventData);
    }
}

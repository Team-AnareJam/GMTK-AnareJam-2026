using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnHoverButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        SoundEffectsManager.Instance.PlaySound("Confirm");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundEffectsManager.Instance.PlaySound("Hover");
    }
}

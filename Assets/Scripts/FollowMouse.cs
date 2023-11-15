using UnityEngine;
using UnityEngine.UI;

public class FollowMouse : MonoBehaviour
{
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Convert the mouse position to canvas space
        Vector2 canvasPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponentInParent<Canvas>().transform as RectTransform, Input.mousePosition, GetComponentInParent<Canvas>().worldCamera, out canvasPosition);

        // Move the object to the mouse position
        rectTransform.anchoredPosition = canvasPosition;
    }
}

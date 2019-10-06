using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public Inventory inv;
    public BlockItem block;

    public void OnDrag(PointerEventData eventData)
    {
        if (inv.CheckIfAvailable(block))
        {
            transform.position = Input.mousePosition;
            gameObject.GetComponent<Image>().color += new Color(0, 0, 0, 1);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (inv.CheckIfAvailable(block))
        {
            inv.RemoveItem(block, 1);
            transform.localPosition = Vector3.zero;
            Color tmp = gameObject.GetComponent<Image>().color;
            tmp.a = 0f;
            gameObject.GetComponent<Image>().color = tmp;
            MouseHandler.PlaceBlock(block);
        }
    }
}

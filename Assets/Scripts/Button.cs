using UnityEngine.EventSystems;
using UnityEngine;

public class Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (gameObject.name)
        {
            case "MoveButton":
                player.IsMoving = true;
                break;
            case "EditButton":
                player.IsEditing = true;
                break;
            case "AttackButton":
                player.IsAttacking = true;
                break;
            case "SuckButton":
                player.IsSucking = true;
                break;
            default:
                break;
        }
      }

    public void OnPointerUp(PointerEventData eventData)
    {
        switch (gameObject.name)
        {
            case "MoveButton":
                player.IsMoving = false;
                break;
            case "EditButton":
                player.IsEditing = false;
                break;
            case "AttackButton":
                player.IsAttacking = false;
                break;
            case "SuckButton":
                player.IsSucking = false;
                break;
            default:
                break;
        }
    }
}

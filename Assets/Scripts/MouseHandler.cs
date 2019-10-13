using UnityEngine;

public class MouseHandler : MonoBehaviour
{
    public static void PlaceBlock(BlockItem block)
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray, Mathf.Infinity);
            foreach (var hit in hits)
            {
                Player player = FindObjectOfType<Player>();
                player.AttachBlock(block, hit);
            }
        }
    }
}

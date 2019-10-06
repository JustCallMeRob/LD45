using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public GameObject prefab;
    public BlockTypes type;
    [TextArea(15, 20)]
    public string description;
}

[CreateAssetMenu(fileName = "New Block", menuName = "Inventory System/Items/Default")]
public class BlockItem : Item
{
    public void Awake()
    { }
}

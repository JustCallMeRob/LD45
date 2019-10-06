using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class Inventory : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>();

    public void AddItem(BlockItem _block, int _amount)
    {
        bool hasItem = false;
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].block == _block)
            {
                Container[i].Add(_amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            Container.Add(new InventorySlot(_block, _amount));
        }
    }

    public void RemoveItem(BlockItem _block, int _amount)
    {
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].block == _block)
            {
                Container[i].Remove(_amount);
                break;
            }
        }
    }

    public bool CheckIfAvailable(BlockItem _block)
    {
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].block == _block && Container[i].amount > 0)
            {
                return true;
            }
        }
        return false;
    }
}

[System.Serializable]
public class InventorySlot
{
    public BlockItem block;
    public int amount;

    public InventorySlot(BlockItem _block, int _amount)
    {
        block = _block;
        amount = _amount;
    }

    public void Add(int value)
    {
        amount += value;
    }

    public void Remove(int value)
    {
        amount -= value;
    }
}

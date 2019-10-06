using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryScreen : MonoBehaviour
{
    public Inventory inventory;

    void Start()
    {
        CreateDisplay();
    }

    void Update()
    {
        UpdateDisplay();
    }

    public void CreateDisplay()
    {
        GameObject blackText = GameObject.FindGameObjectWithTag("BlackText");
        GameObject blueText = GameObject.FindGameObjectWithTag("BlueText");
        GameObject redText = GameObject.FindGameObjectWithTag("RedText");
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (inventory.Container[i].block.type == BlockTypes.Black)
            {
                blackText.GetComponent<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            }
            else if (inventory.Container[i].block.type == BlockTypes.Blue)
            {
                blueText.GetComponent<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            }
            else if (inventory.Container[i].block.type == BlockTypes.Red)
            {
                redText.GetComponent<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            }
        }
    }

    public void UpdateDisplay()
    {
        GameObject blackText = GameObject.FindGameObjectWithTag("BlackText");
        GameObject blueText = GameObject.FindGameObjectWithTag("BlueText");
        GameObject redText = GameObject.FindGameObjectWithTag("RedText");
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (inventory.Container[i].block.type == BlockTypes.Black)
            {
                blackText.GetComponent<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            }
            else if (inventory.Container[i].block.type == BlockTypes.Blue)
            {
                blueText.GetComponent<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            }
            else if (inventory.Container[i].block.type == BlockTypes.Red)
            {
                redText.GetComponent<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            }
        }
    }

}

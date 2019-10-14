using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputManager : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
    }

    public void OnPressMove()
    {
        player.IsMoving = true;
    }

    public void OnReleaseMove()
    {
        player.IsMoving = false;
    }

    public void OnPressAttack()
    {

    }

    public void OnReleaseAttack()
    {

    }

    public void OnPressEdit()
    {

    }

    public void OnReleaseEdit()
    {

    }
}

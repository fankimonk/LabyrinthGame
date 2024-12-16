using System;
using UnityEngine;
using UnityEngine.Events;


public class Player : Character
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) MoveUp();
        if (Input.GetKeyDown(KeyCode.S)) MoveDown();
        if (Input.GetKeyDown(KeyCode.D)) MoveRight();
        if (Input.GetKeyDown(KeyCode.A)) MoveLeft();
    }
}
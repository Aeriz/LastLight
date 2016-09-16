using UnityEngine;
using System.Collections;
using InControl;

public class MyCharacterActions : PlayerActionSet {

    public PlayerAction Left;
    public PlayerAction Right;
    public PlayerAction LockOn;
    public PlayerAction RotateMirror;
    public PlayerAction lightAttack;
    public PlayerAction heavyAttack;

    public MyCharacterActions()
    {
        LockOn = CreatePlayerAction("Lock-On");
        RotateMirror = CreatePlayerAction("Rotate Mirror");
        Left = CreatePlayerAction("Left");
        Right = CreatePlayerAction("Right"); 
        lightAttack = CreatePlayerAction("lightAttack");
        heavyAttack = CreatePlayerAction("heavyAttack");  
    }

}

﻿using UnityEngine;
using System.Collections;
using InControl;

public class MyCharacterActions : PlayerActionSet {

    public PlayerAction Left;
    public PlayerAction Right;
    public PlayerAction LockOn;
    public PlayerAction RotateMirror;
    public PlayerAction lightAttack;
    public PlayerAction heavyAttack;
    public PlayerAction block;
    public PlayerAction save;
    public PlayerAction load;
    public PlayerAction beamSpell;
    public PlayerAction AOESpell;

    public MyCharacterActions()
    {
        LockOn = CreatePlayerAction("Lock-On");
        RotateMirror = CreatePlayerAction("Rotate Mirror");
        Left = CreatePlayerAction("Left");
        Right = CreatePlayerAction("Right"); 
        lightAttack = CreatePlayerAction("lightAttack");
        heavyAttack = CreatePlayerAction("heavyAttack");
        block = CreatePlayerAction("block");
        save = CreatePlayerAction("save");
        load = CreatePlayerAction("load");
        beamSpell = CreatePlayerAction("beamSpell");
        AOESpell = CreatePlayerAction("AOESpell");
    }

}

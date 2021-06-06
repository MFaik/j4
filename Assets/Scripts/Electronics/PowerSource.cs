using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource : MonoBehaviour
{
    [HideInInspector]public bool powered = false;
    [SerializeField]PistonController[] piston;
    public void switchPower(bool update)
    {
        powered = update;
        foreach(PistonController pc in piston)
        {
            pc.refresh();
        }
    }
}

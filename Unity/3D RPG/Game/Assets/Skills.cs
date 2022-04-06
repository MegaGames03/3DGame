using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public int CombatLevel;
    public int ChoppingLevel;
    public int BarginLevel;

    public float CompbatEXP;
    public float ChoppingEXP;
    public float BarginEXP;

    public int StartingEXP;
    private float CompbatEXPNeed;
    private float ChoppingEXPNeed;
    private float BarginEXPNeed;

    public void Start()
    {
        CompbatEXPNeed = StartingEXP;
        ChoppingEXPNeed = StartingEXP;
        BarginEXPNeed = StartingEXP;
    }

    public void AddCombatEXP(float EXPAdded)
    {
        CompbatEXP += EXPAdded;
        if (CompbatEXP >= CompbatEXPNeed)
        {
            CompbatEXP -= CompbatEXPNeed;
            CompbatEXPNeed *= 2;
            CombatLevel += 1;
        }
    }

    public void AddChopingEXP(float EXPAdded)
    {
        ChoppingEXP += EXPAdded;
        if (ChoppingEXP >= ChoppingEXPNeed)
        {
            ChoppingEXP -= ChoppingEXPNeed;
            ChoppingEXPNeed *= 2;
            ChoppingLevel += 1;
        }
    }

    public void AddBarginEXP(float EXPAdded)
    {

        BarginEXP += EXPAdded;
        if (BarginEXP >= BarginEXPNeed)
        {
            BarginEXP -= BarginEXPNeed;
            BarginEXPNeed *= 2;
            BarginLevel += 1;
        }
    }
}

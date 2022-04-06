using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "NewEquipment")]
public class Equipment : ScriptableObject
{
    public EquipmentType equipmentType;
    public int Damage;
    public int HarvestPower;
    public int Health;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

public enum EquipmentType { Weapon, Head, Torso, Legs, Feet }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Counter", menuName = "Tools/Counter", order = 0)]
public class Inventory : PersistenceScriptableObject
{
    
    public int coins;
    public float maxMagic = 10;
    public float currentMagic;
    public float maxDash = 10;
    public float currentDash;

  
}
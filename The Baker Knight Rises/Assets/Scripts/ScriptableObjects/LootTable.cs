using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    public GameObject lootObject;
    public int lootChance;
}

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loots;
    
    public GameObject GetLoot()
    {
        int cumProb = 0;
        int curProb = Random.Range(0, 100);

        for(int i = 0; i < loots.Length; ++i)
        {
            cumProb += loots[i].lootChance;
            if (curProb <= cumProb)
            {
                return loots[i].lootObject;
            }
        }

        return null;
    }
}

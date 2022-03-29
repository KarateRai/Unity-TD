using System.Collections;
using UnityEngine;

[System.Serializable]
public class TurretBluePrint
{
    public GameObject prefab;
    //public Turret turret; Behövs?
    public int cost;
    public int upgrades;

    public GameObject[] upgradedPrefabs;
    public int upgradeCost;
    
    

    public string TurretInfo1(int version)
    {
        Turret tempT = upgradedPrefabs[version + 1].GetComponent<Turret>();
        string tempString;
        if (tempT.useLaser)
        {
            tempString = "<b>Damage: " + tempT.damageOverTime + "\nRange: " + tempT.range + "Slow: " + tempT.slowPercent + "%.</b>"; 
            
        }
        else
        {
            return "<b>Damage: " + tempT.startDamage + "\nRoF: " +
                tempT.fireRate + "</b>";
        }
        return tempString;

    }

    public string TurretInfo2(int version)
    {
        Turret tempT = upgradedPrefabs[version + 1].GetComponent<Turret>();
        string tempString;
        if (tempT.useLaser)
        {
            tempString = "<b>Damage multiplier:\n" + tempT.multiplier + "  / s.</b>";

        }
        else
        {
            return "<b>Range: " + tempT.range + "</b>";
        }
        return tempString;

    }

    public string GetUpgradeCostString()
    {
        return upgradeCost.ToString();
    }
    
    public int GetSellAmount(int version)
    {
        int sellAmount = cost;
        for (int i = 1; i <= version; i++)
        {
            sellAmount += upgradeCost * i;
        }
        return (sellAmount / 2);
    }

}

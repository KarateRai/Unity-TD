using UnityEngine.EventSystems;
using UnityEngine;

public class Node : MonoBehaviour
{
    BuildManager buildManager;
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset; 

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBluePrint turretBluePrint;
    [HideInInspector]
    private int version;
    [HideInInspector]
    public int upgradeCost;

    private Renderer rend;
    private Color startColor;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBluePrint bluePrint)
    {
        if (PlayerStats.Money < bluePrint.cost)
        {
            Debug.Log("Not Enough money to build that!");
            return;
        }

        PlayerStats.Money -= bluePrint.cost;

        GameObject turret = (GameObject)Instantiate(bluePrint.prefab, GetBuildPosition(), Quaternion.identity);
        this.turret = turret;

        turretBluePrint = bluePrint;
        upgradeCost = turretBluePrint.upgradeCost;
        version = 0;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);


        Debug.Log("Turret build");
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < GetUpgradeCost())
        {
            Debug.Log("Not Enough money to upgrade that!");
            return;
        }

        version++;
        upgradeCost = (turretBluePrint.upgradeCost * version);
        PlayerStats.Money -= (upgradeCost);
        

        //Get rid of old turet
        Destroy(this.turret);

        //Build new turret
        GameObject turret = (GameObject)Instantiate(turretBluePrint.upgradedPrefabs[version], GetBuildPosition(), Quaternion.identity);
        this.turret = turret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
        
        Debug.Log("Turret upgraded");
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBluePrint.GetSellAmount(version);

        //Spawn effect
        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 2f);

        Destroy(this.turret);
        turretBluePrint = null;
    }

    public string GetTurretInfo1()
    {
        if (FullyUpgraded())
        {
            return "";
        }
        else
        {
            return turretBluePrint.TurretInfo1(version);
        }
        
    }
    public string GetTurretInfo2()
    {
        if (FullyUpgraded())
        {
            return "";
        }
        else
        {
            return turretBluePrint.TurretInfo2(version);
        }

    }

    public int GetUpgradeCost()
    {
        return ((turretBluePrint.upgradeCost * (version + 1)));
    }

    public string GetCashBack()
    {
        return turretBluePrint.GetSellAmount(version).ToString();
    }

    public bool FullyUpgraded()
    {
        if (version == turretBluePrint.upgrades)
        {
            return true;
        }
        else
            return false;
    }

    private void OnMouseEnter() //Show turret on hover (in color)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

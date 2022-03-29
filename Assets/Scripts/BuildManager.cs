using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public GameObject buildEffect;
    public GameObject sellEffect;
    public Transform spawnPoint;
    public static BuildManager instance;


    private TurretBluePrint turretToBuild;
    private Node selectedNode;

    public TurretUI turretUI;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More then one BuildManager in scene!");
        }
        instance = this;
        
    }
    

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }
    
    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;
        turretToBuild = null;
        turretUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        turretUI.Hide();
    }

    public void SelectTurretToBuild(TurretBluePrint turretBluePrint)
    {
        turretToBuild = turretBluePrint;
        DeselectNode();
    }

    public void ReleaseBomb(GameObject bomb)
    {
        Instantiate(bomb, spawnPoint.position, spawnPoint.rotation);
        DeselectNode();
    }

    public TurretBluePrint GetTurretToBuild()
    {
        return turretToBuild;
    }

}

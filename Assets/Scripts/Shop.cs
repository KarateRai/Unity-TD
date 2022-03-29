using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public TurretBluePrint standardTurret;
    public Text standardTurretText;
    public TurretBluePrint rocketLauncher;
    public Text rocketTurretText;
    public TurretBluePrint lazerBeamer;
    public Text lazerTurretText;
    public Button bombButton;
    public GameObject bomb;
    public Text bombText;
    public int bombCost;
    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
        standardTurretText.text = standardTurret.cost.ToString();
        rocketTurretText.text = rocketLauncher.cost.ToString();
        lazerTurretText.text = lazerBeamer.cost.ToString();
        bombText.text = bombCost.ToString();
    }
    
    public void SelectGattlingTurret()
    {
        Debug.Log("GattlingGun lvl1 Selected");
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectRocketTurret()
    {
        Debug.Log("RocketLauncher lvl1 Selected");
        buildManager.SelectTurretToBuild(rocketLauncher);
    }

    public void SelectLazerBeamer()
    {
        Debug.Log("Lazer Selected");
        buildManager.SelectTurretToBuild(lazerBeamer);
    }

    public void SpawnBomb()
    {
        if (PlayerStats.Money < bombCost)
        {
            bombButton.interactable = false;
        }
        else
        {
            PlayerStats.Money -= bombCost;
            buildManager.ReleaseBomb(bomb);
        }
    }

}

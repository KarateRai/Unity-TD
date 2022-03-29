using UnityEngine;
using UnityEngine.UI;

public class TurretUI : MonoBehaviour
{   
    public GameObject ui;

    public Text upgradeCost;
    public Button upgradeButton;
    private Color standardColor;

    public Text additionalInfo_1;
    public Text additionalInfo_2;
    public Text upgradeVersion;

    public Text sellAmount;

    private Node target;
    private void Awake()
    {
        standardColor = upgradeButton.GetComponent<Image>().color;
    }

    public void SetTarget(Node target)
    {
        this.target = target;
       
        transform.position = target.GetBuildPosition();
        additionalInfo_1.text = target.GetTurretInfo1();
        additionalInfo_2.text = target.GetTurretInfo2();
        if (target.FullyUpgraded())
        {
            upgradeVersion.text = "FULLY UPGRADED!";
        }
        else
        {
            upgradeVersion.text = "UPGRADED VERSION:";
        }

        if (target.GetUpgradeCost() > PlayerStats.Money)
        {
            upgradeButton.GetComponent<Image>().color = Color.red;
            upgradeCost.text = "Not Enough Cash!";
            upgradeButton.interactable = false;
        }
        else
        {
            if (!target.FullyUpgraded())
            {
                upgradeButton.GetComponent<Image>().color = standardColor;
                upgradeCost.text = "$" + target.GetUpgradeCost();
                upgradeButton.interactable = true;
            }
            else
            {
                upgradeCost.text = "N/A";
                upgradeButton.interactable = false;
            }
        }

       

        sellAmount.text = "$" + target.GetCashBack();


        ui.SetActive(true);
    }
   
    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}

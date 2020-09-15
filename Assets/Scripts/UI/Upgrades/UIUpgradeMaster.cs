using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpgradeMaster : MonoBehaviour
{
    public static UIUpgradeMaster _instance;

    public UIUpgradeHandler upgradeHandler;
    public Transform upgradeHandlerPanel;

    private void Awake()
    {
        if (_instance)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    public static UIUpgradeMaster GetInstance() { return _instance; }

    public void OpenUpgrades()
    {
        foreach (Transform t in upgradeHandlerPanel)
            Destroy(t.gameObject);
        Instantiate(upgradeHandler, upgradeHandlerPanel).GetComponent<UIUpgradeHandler>().Load();
    }
}

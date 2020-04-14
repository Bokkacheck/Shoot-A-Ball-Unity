using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas shopCanvas;
    public Canvas startCanvas;
    public Canvas settingsCanvas;
    public GameObject pnlShopStart;
    public GameObject pnlCoinsGems;
    public GameObject pnlUpgrade;
    public GameObject pnlAbilities;
    public GameObject pnlStyle;

    public void GetStart()
    {
        CloseCanvasesStart();
        startCanvas.gameObject.SetActive(true);
    }
    public void GetShop()
    {
        CloseCanvasesStart();
        shopCanvas.gameObject.SetActive(true);
        GetShopStart();
    }
    public void GetSettings()
    {
        CloseCanvasesStart();
        settingsCanvas.gameObject.SetActive(true);
    }
    private void CloseCanvasesStart()
    {
        startCanvas.gameObject.SetActive(false);
        shopCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(false);
    }
    public void GetShopStart()
    {
        ClosePanelsShop();
        pnlShopStart.SetActive(true);
    }
    public void GetCoinsGems()
    {
        ClosePanelsShop();
        pnlCoinsGems.SetActive(true);
    }
    public void GetUpgrade()
    {
        ClosePanelsShop();
        pnlUpgrade.SetActive(true);
    }
    public void GetAbilities()
    {
        ClosePanelsShop();
        pnlAbilities.SetActive(true);
    }
    public void GetStyle()
    {
        ClosePanelsShop();
        pnlStyle.SetActive(true);
        GameObject.Find("Helmet").GetComponent<MeshRenderer>().material = ButtonHandler.selectedMaterial;
        GameObject.Find("BODY").GetComponent<MeshRenderer>().material = ButtonHandler.selectedMaterial;
    }
    private void ClosePanelsShop()
    {
        pnlShopStart.SetActive(false);
        pnlCoinsGems.SetActive(false);
        pnlUpgrade.SetActive(false);
        pnlStyle.SetActive(false);
        pnlAbilities.SetActive(false);
    }
}

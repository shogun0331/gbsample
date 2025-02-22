
using GB;
using TMPro;
using UnityEngine;

public class LobyScene : UIScreen
{
    public TextMeshProUGUI text;

    

    private void Awake()
    {
        Regist();
        RegistButton();
    }

    private void OnEnable()
    {
        Presenter.Bind("LobyScene",this);
    }

    private void OnDisable() 
    {
        Presenter.UnBind("LobyScene", this);


    }

    public void RegistButton()
    {
        foreach(var v in mButtons)
            v.Value.onClick.AddListener(() => { OnButtonClick(v.Key);});
        
    }

    public void OnButtonClick(string key)
    {
        key.GBLog("Button Click",Color.green);

        switch(key)
        {
            case "Battle":
            Presenter.Send("CLoby","GameStart");
            break;

            case "Settings":
            UIManager.ShowPopup(POPUP.LanguagePopup);
            break;

            case "EnergyADD":
            UserDataManager.I.AddEnegy(1);
            break;

            case "CoinAdd":
            UserDataManager.I.AddCoin(1);
            break;

            case "RubyAdd":
            UserDataManager.I.AddRuby(1);
            break;


            case "Shop":
            break;

            case "Inventory":
            break;

            case "Cards":
            break;

            case "Clan":
            break;

            case "Stage":
            break;

            
            case "Ranking":
            break;

            case "Rewards":
            break;

            case "Quests":
            break;

            case "Friends":
            break;

            case "InBox":
            break;

            case "Notice":
            break;

        

        }
    }
    public override void ViewQuick(string key, IOData data)
    {
         
    }

    public override void Refresh()
    {
            
    }



}
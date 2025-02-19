using UnityEngine;
using GB;


public class LanguagePopup : UIScreen
{
    private void Awake()
    {
        Regist();
        RegistButton();
    }

    private void OnEnable()
    {
        Presenter.Bind("LanguagePopup",this);
    }

    void Start()
    {
        mButtons[LocalizationManager.I.Language.ToString()].GetComponent<TapButton>().OnTap();
    }

    private void OnDisable() 
    {
        Presenter.UnBind("LanguagePopup", this);

    }

    public void RegistButton()
    {
        foreach(var v in mButtons)
            v.Value.onClick.AddListener(() => { OnButtonClick(v.Key);});
        
    }

    public void OnButtonClick(string key)
    {
        switch(key)
        {

            case "Close":
            Close();
            break;

            default:
            key.GBLog("Language",Color.cyan);
            LocalizationManager.I.SetSystemLanguage(key);
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
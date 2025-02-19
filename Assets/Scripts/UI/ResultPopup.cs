using GB;


public class ResultPopup : UIScreen
{

    

    private void Awake()
    {
        Regist();
        RegistButton();
    }

    private void OnEnable()
    {
        Presenter.Bind("ResultPopup",this);
    }

    private void OnDisable() 
    {
        Presenter.UnBind("ResultPopup", this);

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
            case "OK":
            Presenter.Send("CGame","GameEnd");
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
using GB;
using UnityEngine;

public class CLoby : View
{

    void OnEnable()
    {
        Presenter.Bind("CLoby",this);
    }

    void OnDisable()
    {
        Presenter.Bind("CLoby",this);
    }

    void GameStart()
    {
        UIManager.ChangeScene(DEF.SCENE_GAME);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        UIManager.ShowPopup(POPUP.LanguagePopup);

        if(Input.GetMouseButtonDown(1))
        {
            UIManager.ShowPopup(POPUP.ResultPopup);
        }
       
    }

    public override void ViewQuick(string key, IOData data)
    {

        switch(key)
        {
            case "GameStart":
            GameStart();
            break;
            

        }
    
    }
}

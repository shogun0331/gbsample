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
        
        float deltaTime = GBTime.GetDeltaTime("GAME");

        GBTime.Play("GAME");
        //[GAME]  Time.deltaTime
        deltaTime = GBTime.GetDeltaTime("GAME");


        GBTime.I.SetTimeScale("GAME",0.5f);
        //[GAME] Time.deltaTime * 0.5f
        deltaTime = GBTime.GetDeltaTime("GAME");



        GBTime.Stop("GAME");
        //[GAME] 0;
        deltaTime = GBTime.GetDeltaTime("GAME");

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

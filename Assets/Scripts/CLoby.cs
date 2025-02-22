using System.Linq;
using System.Collections.Generic;
using GB;
using NaughtyAttributes;
using Newtonsoft.Json;
using UnityEngine;
using System;

public class CLoby : View
{

    void OnEnable()
    {
        Presenter.Bind("CLoby", this);
        
    }

    void OnDisable()
    {
        Presenter.Bind("CLoby", this);
        
        
    }

    void GameStart()
    {
        UIManager.ChangeScene(DEF.SCENE_GAME);
    }

   
   
    

    public override void ViewQuick(string key, IOData data)
    {

        switch (key)
        {
            case "GameStart":
                GameStart();
                break;


        }

    }
}

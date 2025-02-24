using System.Collections;
using System.Collections.Generic;
using GB;
using UnityEngine;
using UnityEngine.UI;

public class BinderCurrency : MonoBehaviour
{
    [SerializeField] string _ODataKey;

    Text _text;

    void Awake()
    {
        _text = GetComponent<Text>();
    }


    void OnEnable()
    {
        if(ODataBaseManager.Contains(_ODataKey))
        {
            if(_text != null)
            _text.text = ODataBaseManager.Get<int>(_ODataKey).ToString("N0");

        }

        ODataBaseManager.Bind(this,_ODataKey,(data)=>
        {
            if(_text != null)
            _text.text = data.OConvert<int>().ToString("N0");
        });
    }



    void OnDisable()
    {
        ODataBaseManager.UnBind(this,_ODataKey);
    }

}

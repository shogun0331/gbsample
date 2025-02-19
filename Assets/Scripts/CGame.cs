using System.Collections;
using System.Collections.Generic;
using GB;
using UnityEngine;

public class CGame : View
{
    [SerializeField] Transform _particlePoint;
    public override void ViewQuick(string key, IOData data)
    {
        switch (key)
        {
            case "GameEnd":
                UIManager.ChangeScene(DEF.SCENE_LOBY);
                break;
        }

    }

    void OnEnable()
    {
        Presenter.Bind("CGame", this);
    }

    void OnDisable()
    {
        Presenter.UnBind("CGame", this);
    }

    List<GameObject> _bulletList = new List<GameObject>();
    List<GameObject> _removeList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GB.Timer.Create(5, () => { UIManager.ShowPopup(POPUP.ResultPopup); });
    }



    void Update()
    {
        if (Time.time > _time)
        {
            _time = Time.time + 0.05f;
            CreateParticle();
        }

        MoveParticle();

    }


    float _time;

    void CreateParticle()
    {
        var bullet = ObjectPooling.Create(RES_PREFAB.Fx_Rotate_Light01, 50);
        _bulletList.Add(bullet);
        bullet.transform.SetParent(_particlePoint);
        bullet.transform.localScale = new Vector3(50, 50, 50);
        bullet.transform.position = _particlePoint.position;

    }

    void MoveParticle()
    {

        for (int i = 0; i < _bulletList.Count; ++i)
        {
            _bulletList[i].transform.Translate(Vector2.right * Time.deltaTime * 1000);
            if (_bulletList[i].transform.position.x > Screen.width) _removeList.Add(_bulletList[i]);
        }

        for (int i = 0; i < _removeList.Count; ++i)
        {
            _bulletList.Remove(_removeList[i]);
            ObjectPooling.Return(_removeList[i]);
        }

        _removeList.Clear();

    }


}

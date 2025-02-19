
using System;
using Newtonsoft.Json;

[Serializable]
public class UserData 
{
    public int Enegy = 0;

    public int Coin = 0;

    public int Ruby = 0;


    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}
    
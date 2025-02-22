using UnityEngine;
using GB;
using System.IO;
using Newtonsoft.Json;
using UniRx;
using NaughtyAttributes;
namespace GB
{

    public class UserDataManager : AutoSingleton<UserDataManager>
    {
        private Subject<Unit> _callSaveSubject = new Subject<Unit>();

        private void Awake()
        {
            if (I != null && I != this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(this.gameObject);

            if(!UserDataManager.Load()) UserDataManager.Save();
            
            ODataBaseManager.Set(DEF.O_Ruby,UserDataManager.Current.Ruby);
            ODataBaseManager.Set(DEF.O_Enegy,UserDataManager.Current.Enegy);
            ODataBaseManager.Set(DEF.O_Coin,UserDataManager.Current.Coin);

            _callSaveSubject.Throttle(System.TimeSpan.FromSeconds(1))
            .Subscribe(_ =>Save())
            .AddTo(this);

            


            LocalizationManager.I.SetSystemLanguage(PlayerPrefs.GetString("Language", SystemLanguage.English.ToJson()));
            LocalizationManager.I.Load();
             

      
        }
        
        [SerializeField] UserData _current = new UserData();
        public static UserData Current { get { return I._current; } }

        public void AddCoin(int coin)
        {
            Current.Coin += coin;
            ODataBaseManager.Set(DEF.O_Coin,UserDataManager.Current.Coin);
            SaveEvent();
        }

        public void AddRuby(int ruby)
        {
            Current.Ruby += ruby;
            ODataBaseManager.Set(DEF.O_Ruby,UserDataManager.Current.Ruby);
            SaveEvent();
        }

        public void AddEnegy(int enegy)
        {
            Current.Enegy += enegy;
            ODataBaseManager.Set(DEF.O_Enegy,UserDataManager.Current.Enegy);
            SaveEvent();
        }

        void SaveEvent()
        {
            _callSaveSubject.OnNext(Unit.Default);
        }

        [Button]
        public static bool Load()
        {
            string path = Path.Combine(Application.persistentDataPath, "user_data.data");

            if (File.Exists(path))
            {
                string gz = File.ReadAllText(path); // 파일에서 읽기
                string json = Gzip.DeCompression(gz);
                I._current = JsonConvert.DeserializeObject<UserData>(json);
                json.GBLog("LOAD",Color.yellow);
                return true;
            }
            else { return false; }
        }


        public static bool Load(string json)
        {
            try
            {
                I._current = JsonConvert.DeserializeObject<UserData>(json);
                json.GBLog("LOAD",Color.yellow);
                return true;
            }
            catch { return false; }
        }

        [Button]
        public static void Save()
        {
            if (I._current == null) I._current = new UserData();

            string json = I._current.ToJson();
            string gz = Gzip.Compression(json);
            string path = Path.Combine(Application.persistentDataPath, "user_data.data");
            File.WriteAllText(path, gz); // 파일에 저장
            json.GBLog("SAVE",Color.yellow);
        }
    }
}
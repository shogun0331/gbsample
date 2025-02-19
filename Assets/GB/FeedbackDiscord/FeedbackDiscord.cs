using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.IO;
using Newtonsoft.Json;
namespace GB
{
    public class FeedbackDiscord : AutoSingleton<FeedbackDiscord>
    {
        private void Awake() 
        {
            if(I != null && I != this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
            
        }
       

        string _webhookURL; // 디스코드 웹훅 URL
        string _message; // 보낼 메시지
        string _filePath; // 스크린샷 저장 경로

        string _userInfo;

        public static void Send(string url,string userInfo,string message)
        {
            I._userInfo = userInfo;
            
            string msg = I.GetDeviceInfo();
            msg += @"
- Message
```

{0}

```
- ScreenShot
";

            msg = string.Format(msg,message);
            I._message = msg;
            I._webhookURL = url;
            
            I.StartCoroutine(I.UploadScreenshotAndSendMessage());
        }

        string GetDeviceInfo()
        {
            string graphicsDeviceName = SystemInfo.graphicsDeviceName;
            string graphicsDeviceVendor = SystemInfo.graphicsDeviceVendor;
            int graphicsMemorySize = SystemInfo.graphicsMemorySize;
            
            string deviceInfo = @"
            #  {0}

            Platform : {1}
            PackageName : {2}
            AppVersion : {3}
            UnityVersion: {4}

            - Device Info

            DeviceModel : {5}
            DeviceName  : {6}
            OS : {7}
            RAM : {8}
            GPU Name : {9}

            - UserInfo

            ```
            {10}
            ```
            ";
            deviceInfo = string.Format( deviceInfo,
            Application.productName,
            Application.platform.ToString(),
            Application.identifier,
            Application.version,
            Application.unityVersion,

            SystemInfo.deviceModel,
            SystemInfo.deviceName,
            SystemInfo.operatingSystem,
            SystemInfo.systemMemorySize,
            graphicsDeviceName + " (" + graphicsDeviceVendor + "), Memory: " + graphicsMemorySize + " MB",
            _userInfo);

            return deviceInfo;

        }


        IEnumerator UploadScreenshotAndSendMessage()
        {
            // 1. 스크린샷 캡쳐 및 저장
            yield return new WaitForEndOfFrame(); // 모든 렌더링이 완료될 때까지 대기
            Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();
            byte[] bytes = texture.EncodeToPNG();

            // 저장 경로 설정 (Application.persistentDataPath 사용 권장)
            _filePath = Application.persistentDataPath + "/screenshot.png";
            File.WriteAllBytes(_filePath, bytes);

            // 2. 디스코드 웹훅 메시지 생성 (JSON 형식)
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["content"] = _message;
            string json = JsonConvert.SerializeObject(data);
            Debug.Log("_message :\n" +json );
            
            // 3. 파일 업로드 및 메시지 전송 (Multipart FormData 사용)
            WWWForm form = new WWWForm();


            form.AddField("payload_json", json);
            form.AddBinaryData("file", bytes, "screenshot.png", "image/png");

            using (UnityWebRequest www = UnityWebRequest.Post(_webhookURL, form))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("Error sending message and screenshot: " + www.error);
                }
                else
                {
                    Debug.Log("Message and screenshot sent successfully!");
                }
            }
        }


        


    }

    

}
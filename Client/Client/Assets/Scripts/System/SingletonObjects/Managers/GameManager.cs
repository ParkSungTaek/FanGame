using Client;
using System.Collections.Generic;
using UnityEngine;


namespace Client
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        static GameManager _instance;
        static GameManager Instance { get { Init(); return _instance; } }
        GameManager() { }

        #endregion

        #region Managers

        InGameDataManager _inGameDataManager = new InGameDataManager();
        NetworkManager _networkManager = new NetworkManager();

        public static InGameDataManager InGameData { get { return Instance._inGameDataManager; } }
        public static NetworkManager Network { get { return Instance._networkManager; } }

        #endregion

        private void Start()
        {
            Init();
            TestManager.Instance.TestDebug();

        }

        /// <summary> instance 생성, 산하 매니저들 초기화 </summary>
        static void Init()
        {
            if (_instance == null)
            {
                GameObject gm = GameObject.Find("GameManager");
                if (gm == null)
                {
                    gm = new GameObject { name = "GameManager" };
                    gm.AddComponent<GameManager>();
                }
                _instance = gm.GetComponent<GameManager>();
                DontDestroyOnLoad(gm);


                _instance._inGameDataManager.Init();
                _instance._networkManager.Init();

                GoogleSheet googleSheet = _instance._networkManager.data;
                _instance.StartCoroutine(_instance._networkManager.GoogleSheetsDataParsing(googleSheet.associatedSheet, googleSheet.GetData, googleSheet.associatedDataWorksheet));
            }

        }
    }
}

namespace Client
{
    public class SceneManager
    {
        /// <summary> Enum으로 정의한 씬 전환 </summary>
        public static void LoadScene(SystemEnum.Scenes scene, bool cacheClear = false)
        {
            //ui popup 초기화
            UIManager.Instance.Clear();
            //게임 진행사항 초기화
            GameManager.InGameData.Clear();
            //ResourceManager 초기화
            if (cacheClear)
            {
                ObjectManager.Instance.Clear();
                AudioManager.Instance.Clear();
            }
            UnityEngine.SceneManagement.SceneManager.LoadScene((int)scene);
        }
    }
}
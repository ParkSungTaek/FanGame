using UnityEngine;


namespace Client
{
    /// <summary>
    /// Scene 초기화 class
    /// GameManager의 Init이 게임 시스템 초기화라면 
    /// {Scene이름}Scene 시리즈는 해당 씬의 초기화를 담당
    /// </summary>
    public class GameScene : MonoBehaviour
    {
        [SerializeField] AudioClip _BGM;
        private void Start()
        {
            UIManager.Instance.ShowSceneUI<UI_Drop>();
            AudioSystem.Instance.GetAudioPlayer(SystemEnum.Sounds.BGM).PlayAudio(_BGM);

        }
    }
}
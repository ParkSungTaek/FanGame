using UnityEngine;


namespace Client
{
    /// <summary>
    /// Scene �ʱ�ȭ class
    /// GameManager�� Init�� ���� �ý��� �ʱ�ȭ��� 
    /// {Scene�̸�}Scene �ø���� �ش� ���� �ʱ�ȭ�� ���
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
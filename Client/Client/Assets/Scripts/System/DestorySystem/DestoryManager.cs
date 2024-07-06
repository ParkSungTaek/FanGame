using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    public class DestoryManager : MonoBehaviour
    {

        static DestoryManager _Instance;

        public static DestoryManager Instance { get { Init(); return _Instance; } }

        private DestoryManager() { }

        List<Object> _NeedToDestory = new List<Object>();

        static void Init()
        {
            if (_Instance == null)
            {
                _Instance = FindAnyObjectByType<DestoryManager>();
                if (_Instance == null)
                {
                    GameObject gm = new GameObject { name = "DestoryManager" };
                    _Instance = gm.AddComponent<DestoryManager>();
                    DontDestroyOnLoad(gm);
                }
            }
        }

        public void SetDestroyAndExecuteNextFrame(Object destroyObj, int nextFrame = 1)
        {
            _NeedToDestory.Add(destroyObj);
            StartCoroutine(DestroyList(nextFrame));
        }

        IEnumerator DestroyList(int nextFrame)
        {
            for (int i = 0; i < nextFrame; i++)
            {
                yield return null;
            }
            if (_NeedToDestory != null)
            {
                foreach (var OBJ in _NeedToDestory)
                {
                    if (OBJ == null)
                    {
                        continue;
                    }

                    if (OBJ is Entity entity)
                    {
                        EntityManager.Instance.DestoryEntity(entity);
                    }

                    Destroy(OBJ);
                }
                _NeedToDestory.Clear();
            }
        }

    }
}
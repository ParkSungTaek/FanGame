using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Client
{
    public class UI_Drop : UI_Scene
    {
        Spawner spawner;

        enum Buttons
        {
            Drop,

        }
        public override void Init()
        {
            base.Init();
            Bind<Button>(typeof(Buttons));
            
            ButtonBind();


        }

        #region Buttons
        void ButtonBind()
        {
            BindEvent(GetButton((int)Buttons.Drop).gameObject, Btn_Drop);
        }

        void Btn_Drop(PointerEventData eventData)
        {
            if (spawner == null)
            {
                spawner = EntityManager.Instance.GetSpawner();
                if (spawner == null)
                {
                    Debug.LogError(" UI_Drop Can't Find spawner");
                }
            }
            spawner.DropSphere();

        }

        #endregion Buttons
    }
}
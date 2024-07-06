using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Client
{
    public abstract class UI_Popup : UI_Base
    {
        /// <summary> 정렬 설정 </summary>
        public override void Init()
        {
            base.Init();
            UIManager.Instance.SetCanvas(gameObject, true);
        }

        /// <summary> pop up 닫기 </summary>
        public virtual void ClosePopUpUI()
        {
            UIManager.Instance.ClosePopUpUI(this);
        }
        /// <summary> pop up 다시 열 때마다 실행</summary>
        public virtual void ReOpenPopUpUI() { }
    }
}
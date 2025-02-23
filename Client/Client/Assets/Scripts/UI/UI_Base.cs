using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Client;

namespace Client
{
    public abstract class UI_Base : MonoBehaviour
    {
        /// <summary>
        /// 관리할 산하 오브젝트들
        /// </summary>
        protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

        float setWidth = 3200; // 사용자 설정 너비
        float setHeight = 1440; // 사용자 설정 높이
        private CanvasScaler canvasScaler;
        /// <summary>
        /// UI 최초 초기화
        /// </summary>
        public virtual void Init()
        {
            canvasScaler = GetComponent<CanvasScaler>();
            SetResolution();
        }
        private void Awake()
        {
            Init();
        }
        /// <summary>
        /// 산하의 T type object들 _objects dictionary에 저장
        /// </summary>
        /// <typeparam name="T">해당 타입</typeparam>
        /// <param name="type">해당 타입 정보 가진 enum(각 UI에서 정의)</param>
        protected void Bind<T>(Type type) where T : UnityEngine.Object
        {
            string[] names = Enum.GetNames(type);
            UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
            _objects.Add(typeof(T), objects);

            for (int i = 0; i < names.Length; i++)
            {
                if (typeof(T) == typeof(GameObject))
                    objects[i] = Util.FindChild(gameObject, names[i], true);
                else
                    objects[i] = Util.FindChild<T>(gameObject, names[i], true);

                if (objects[i] == null)
                    Debug.LogError($"Failed to bind : {names[i]} on {gameObject.name}");
            }
        }

        /// <summary>
        /// bind된 object에서 원하는 object 얻기
        /// </summary>
        protected T Get<T>(int idx) where T : UnityEngine.Object
        {
            UnityEngine.Object[] objects = null;
            if (_objects.TryGetValue(typeof(T), out objects) == false)
                return null;

            return objects[idx] as T;
        }
        #region Get_Override
        protected GameObject GetGameObject(int idx) => Get<GameObject>(idx);
        protected TMP_Text GetText(int idx) => Get<TMP_Text>(idx);
        protected Image GetImage(int idx) => Get<Image>(idx);
        protected Button GetButton(int idx) => Get<Button>(idx);
        #endregion Get_Override

        /// <summary>
        /// 해당 game object에 이벤트 할당
        /// </summary>
        /// <param name="action">할당할 이벤트</param>
        /// <param name="type">이벤트 발생 조건</param>
        public static void BindEvent(GameObject go, Action<PointerEventData> action, SystemEnum.UIEvent type = SystemEnum.UIEvent.Click)
        {
            UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

            switch (type)
            {
                case SystemEnum.UIEvent.Click:
                    evt.OnClickHandler -= action;
                    evt.OnClickHandler += action;
                    break;
                case SystemEnum.UIEvent.Drag:
                    evt.OnDragHandler -= action;
                    evt.OnDragHandler += action;
                    break;
                case SystemEnum.UIEvent.DragEnd:
                    evt.OnDragEndHandler -= action;
                    evt.OnDragEndHandler += action;
                    break;
            }
        }

        /// <summary> 반복문으로 사용하기 위한 index 사용 event 할당 </summary>
        public static void BindEvent(GameObject go, Action<PointerEventData, object> action, object pivot, SystemEnum.UIEvent type = SystemEnum.UIEvent.Click)
        {
            UI_PivotEventHandler evt = Util.GetOrAddComponent<UI_PivotEventHandler>(go);
            evt.Pivot = pivot;

            switch (type)
            {
                case SystemEnum.UIEvent.Click:
                    evt.OnClickHandler -= action;
                    evt.OnClickHandler += action;
                    break;
            }
        }
        public void SetResolution()
        {

            float deviceWidth = Screen.width; // 기기 너비 저장
            float deviceHeight = Screen.height; // 기기 높이 저장


            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(setWidth, setHeight);
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;

            if (setWidth / setHeight < deviceWidth / deviceHeight)
            {
                canvasScaler.matchWidthOrHeight = 1f;
            }
            else
            {
                canvasScaler.matchWidthOrHeight = 0f;
            }
        }

    }
}
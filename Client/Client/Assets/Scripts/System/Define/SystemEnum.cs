using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Client
{
    public class SystemEnum
    {
        public enum SphereLevel
        {
            Sphere1,
            Sphere2,
            Sphere3,


            MAX
        }

        public enum Sounds
        {
            BGM,
            SFX,
            MaxCount
        }
        public enum BGM
        {

            MaxCount
        }
        public enum SFX
        {
            MaxCount
        }
        public enum State
        {
            MaxCount
        }

        public enum Tag
        {
            MaxCount
        }

        /// <summary>
        /// UI Event 종류 지정
        /// </summary>
        public enum UIEvent
        {
            Click,
            Drag,
            DragEnd,
        }

        public enum Scenes
        {
            Title,
            Loby,
            Game,
        }
    }
}
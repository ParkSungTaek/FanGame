using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Client
{
    public class DataManager : Singleton<DataManager>
    {
        /// <summary> 로드한 적 있는 object cache </summary>
        Dictionary<string, Object> _cache = new Dictionary<string, Object>();



    }
}
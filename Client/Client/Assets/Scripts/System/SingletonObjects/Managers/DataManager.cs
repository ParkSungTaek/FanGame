using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Client
{
    public class DataManager : Singleton<DataManager>
    {
        /// <summary> �ε��� �� �ִ� object cache </summary>
        Dictionary<string, Object> _cache = new Dictionary<string, Object>();



    }
}
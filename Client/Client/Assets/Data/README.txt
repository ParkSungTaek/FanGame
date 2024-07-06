
/// <summary> 로드한 적 있는 object cache </summary>
    Dictionary<string, Object> _cache = new Dictionary<string, Object>();

obj = Resources.Load<T>(path);
        _cache.Add(name, obj);



이 이후에  Resources.UnloadAsset 을 해줘야할까?
Resources.UnloadAsset 이걸 써야한다는데 뭘 어떻게 써야하는지 모르겠어
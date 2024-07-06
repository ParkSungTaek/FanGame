using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    public class EntityManager : Singleton<EntityManager>
    {
        private Dictionary<int, Entity> _EntityData = new Dictionary<int, Entity>();
        private int _NextID = 1;

        private Spawner spawner = null;

        EntityManager() { }


        public void SetEntity(Entity entity)
        {
            if (entity == null)
                return;

            _EntityData.Add(entity.ID, entity);
        }
        public int GetNextID()
        {
            return _NextID++;
        }

        public Entity GetEntity(Collider2D collision)
        {
            if (collision == null)
                return null;

            foreach (var entity in _EntityData)
            {
                Entity val = entity.Value;
                if (val == null) 
                {
                    continue;
                }

                if (val.GetCollision2D() == collision)
                {
                    return val;
                }
            }
            return null;
        }

        public Entity GetEntity(int id)
        {
            if (_EntityData.ContainsKey(id))
            {
                return _EntityData[id];
            }
            else
            {
                return null;
            }
        }

        public void DestoryEntity(int id)
        {
            if (_EntityData.ContainsKey(id))
            {
                Entity destroyOBJ = _EntityData[id];
                _EntityData.Remove(id);
                DestoryManager.Instance.SetDestroyAndExecuteNextFrame(destroyOBJ);
            }
        }

        public void DestoryEntity(Collider2D collision)
        {
            if (collision == null)
                return;

            DestoryEntity(GetEntity(collision));

        }
        public void DestoryEntity(Entity entity)
        {
            if (entity == null)
                return;

            DestoryEntity(entity.ID);
        }

        public Entity SpawnSphere(SphereLevel level, Vector3 position)
        {
            if (level == SphereLevel.MAX)
            {
                level = SphereLevel.MAX - 1;
            }
            Sphere sphere = ObjectManager.Instance.Instantiate<Sphere>($"Objects/{level}");
            if (sphere == null)
            {
                Debug.LogError($"EntityManager Can't Find Objects/{level}");
                return null;
            }

            int sphereId = sphere.ID;
            if (_EntityData.ContainsKey(sphereId))
            {
                Debug.LogError($"!!! {sphereId} IS ALREADY IN EntityMANAGER !!!");
            }
            EntityManager.Instance.SetEntity(sphere);
            sphere.transform.position = position;
            return sphere;
        }

        public void Clear()
        {
            _NextID = 1;
            foreach (var entity in _EntityData)
            {
                Resources.UnloadAsset(entity.Value);
            }
            _EntityData.Clear();
        }

        public Spawner GetSpawner()
        {
            if (spawner == null)
            {
                spawner = GameObject.FindAnyObjectByType<Spawner>();
                if (spawner == null)
                {
                    Debug.LogError("NO Spawner");
                }
            }
            return spawner;

        }

    }
}
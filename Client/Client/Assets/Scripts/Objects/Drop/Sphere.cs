using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    public class Sphere : Entity
    {
        [SerializeField]
        SphereLevel level;
        public SphereLevel Level => level;

        Rigidbody2D rigidbody2D = null;

        private void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == gameObject.layer)
            {
                
                Sphere sphere = EntityManager.Instance.GetEntity(collision.collider) as Sphere;
                if (sphere == null)
                {
                    Debug.Log("sphere == null");
                    return;
                }

                if (Level != sphere.Level)
                {
                    Debug.Log("Level != sphere.Level");
                    return;
                }

                Debug.Log($"ID : {ID} sphere.ID {sphere.ID}");
                SphereLevel NextLevel = (SphereLevel)(level + 1);
                if (ID < sphere.ID)
                {
                    
                    if (NextLevel != SphereLevel.MAX)
                    {
                        Vector3 position = (transform.position + collision.transform.position) / 2;
                        EntityManager.Instance.SpawnSphere(NextLevel, position);
                    }
                }

                if (NextLevel != SphereLevel.MAX)
                {
                    DestoryManager.Instance.SetDestroyAndExecuteNextFrame(gameObject);
                }
            }
        }

        public void SetGravity(bool gravity)
        {
            if (rigidbody2D == null)
            {
                rigidbody2D = GetComponent<Rigidbody2D>();
                if (rigidbody2D == null)
                {
                    return;
                }
            }
            if (gravity)
            {
                rigidbody2D.gravityScale = 1f;
            }
            else 
            {
                rigidbody2D.gravityScale = 0f;
            }

        }
        


    }
}
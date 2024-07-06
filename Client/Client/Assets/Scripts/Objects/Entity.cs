using Client;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Client
{
    public class Entity : MonoBehaviour
    {
        private int _ID;
        Collider2D collision = null;
        public int ID => _ID;

        public Entity()
        {
            _ID = EntityManager.Instance.GetNextID();
        }

        public Collider2D GetCollision2D()
        {
            if (collision == null)
            {
                collision = GetComponent<Collider2D>();
                if (collision == null)
                {
                    Debug.LogError($"Entity{_ID} NO Collision2D");
                    return null;
                }

            }
            return collision;
        }

    }

}
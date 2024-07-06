using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    public class Spawner : MonoBehaviour
    {
        enum point
        {
            _Point1,
            _Point2,

            MaxCount
        }

        [SerializeField]
        private Transform[] _Point = new Transform[(int)point.MaxCount];
        [SerializeField]
        private float _Speed;
        [SerializeField]
        private point To = point._Point1;

        private const float Dis = 0.3f;

        private Sphere sphere = null;
        private WaitForSeconds seconds = new WaitForSeconds(1.2f);

        // Start is called before the first frame update
        void Start()
        {
            SpawnerSphere();
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, _Point[(int)To].position, _Speed * Time.deltaTime);
            if (sphere != null)
            {
                sphere.transform.position = transform.position;
            }
            if (Distance() < Dis)
            {
                To++;
                if (To == point.MaxCount)
                {
                    To = (point)0;
                }
            }
        }

        private Sphere SpawnerSphere()
        {
            sphere = EntityManager.Instance.SpawnSphere(SphereLevel.Sphere1,transform.position) as Sphere;
            sphere.SetGravity(false);
            if (sphere == null)
            {
                return null;
            }

            return sphere;
        }

        private float Distance()
        {
            Vector3 distance = transform.position - _Point[(int)To].position;
            return distance.sqrMagnitude;
        }

        public bool DropSphere()
        {
            if (sphere == null)
                return false;

            sphere.SetGravity(true);
            sphere = null;
            StartCoroutine(SpawnSphere());

            return true;
        }

        IEnumerator SpawnSphere()
        {
            yield return seconds;

            sphere = SpawnerSphere();
        }
    }
}
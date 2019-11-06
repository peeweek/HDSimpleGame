using UnityEngine;

namespace ThirdPersonController
{
    public class PrefabSpawner : MonoBehaviour
    {
        public GameObject PrefabToSpawn;
        public Transform Origin;

        public void Spawn()
        {
            var go = Instantiate(PrefabToSpawn);
            go.transform.position = Origin.transform.position;
            go.transform.rotation = Origin.transform.rotation;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public class SimplePoolService : MonoBehaviour
    {
        public static SimplePoolService Instance { get; private set; }
        private readonly Dictionary<GameObject, Queue<GameObject>> _pools = new();

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this; DontDestroyOnLoad(gameObject);
        }

        public GameObject Get(GameObject prefab, Vector3 pos, Quaternion rot)
        {
            if (!_pools.TryGetValue(prefab, out var q)) { q = new Queue<GameObject>(); _pools[prefab] = q; }
            GameObject go = (q.Count > 0) ? q.Dequeue() : Instantiate(prefab);
            var t = go.transform; t.SetPositionAndRotation(pos, rot); go.SetActive(true);
            return go;
        }

        public void Recycle(GameObject prefab, GameObject instance)
        {
            if (!_pools.TryGetValue(prefab, out var q)) { q = new Queue<GameObject>(); _pools[prefab] = q; }
            instance.SetActive(false); q.Enqueue(instance);
        }
    }
}
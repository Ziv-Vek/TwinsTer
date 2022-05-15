using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Twinster.Scenes
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [Tooltip("This prefab will only be spawned once and persisted between scenes.")]
        [SerializeField] GameObject persistantObjectPrefab = null;

        static bool hasSpawned = false;

        private void Awake() {
            if (hasSpawned) return;

            SpawnPersistentObject();

            hasSpawned = true;
        }

        private void SpawnPersistentObject()
        {
            GameObject persistentObject = Instantiate(persistantObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}


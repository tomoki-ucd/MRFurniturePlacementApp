using Unity.VisualScripting;
using UnityEngine;

public class PrefabInstantiator: MonoBehaviour
{
    public GameObject prefabToInstantiate;
    public Canvas canvas;

    public void InstantiatePrefab()
    {
        if(prefabToInstantiate == null)
        {
            Debug.LogError($"[{this.name}] No prefab is set to instantiate");
            return;
        }
        if(canvas == null)
        {
            Debug.LogError($"[{this.name}] No canvas is set to instantiate");
            return;
        }

        Transform spawnPoint= canvas.transform;
//        spawnPoint.position.x += 2.0f;
        Vector3 spawnPosition = spawnPoint.position;
        spawnPosition.x += 2.0f;

        Vector3 position = spawnPosition != null ? spawnPosition: Vector3.zero;
//        Quaternion quaternion = spawnPoint != null ? spawnPoint.rotation : Quaternion.identity;
        Quaternion quaternion = Quaternion.identity;

        Instantiate(prefabToInstantiate, position, quaternion);
    }
}

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
        Vector3 spawnPosition = spawnPoint.position;
        spawnPosition.x += 2.0f;

        Vector3 position = spawnPosition != null ? spawnPosition: Vector3.zero;
//        Quaternion quaternion = spawnPoint != null ? spawnPoint.rotation : Quaternion.identity;
//        Quaternion quaternion = Quaternion.identity;  // This sets the rotation 0,0,0 against its parent object.

//        Instantiate(prefabToInstantiate, position, quaternion);
        GameObject spawnedInstance = Instantiate(prefabToInstantiate, position, prefabToInstantiate.transform.rotation);


        // Adjust the height of the spawned object by moving it downward by half of its own height.
        Collider objectCollider = spawnedInstance.GetComponent<Collider>(); // This will get ANY type of Collider including MeshCollider
        if(objectCollider != null)
        {
            float halfHeight = objectCollider.bounds.size.y / 2f;
            spawnedInstance.transform.position -= new Vector3(0, halfHeight, 0);
            Debug.Log($"[{this.name}] Spawned object {spawnedInstance} is lowered by {halfHeight}.");
        }
        else
        {
            Debug.LogError($"[{this.name}] Spawned object {spawnedInstance} has no Collider. Cannot determine the height to lower it.");
        }
    }
}

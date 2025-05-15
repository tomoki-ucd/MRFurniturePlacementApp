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

        Vector3 spawnPosition = canvas.transform.position;
        spawnPosition.x += 2.0f;

        GameObject spawnedInstance = Instantiate(prefabToInstantiate, spawnPosition, prefabToInstantiate.transform.rotation);

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

using UnityEngine;
using Meta.XR;
using Meta.XR.MRUtilityKit;
using Meta.XR.EnvironmentDepth;

/// <summary>
/// Provides the functionality to place objects using ray
/// </summary>
public class ObjectPlacementManager : MonoBehaviour
{
    private EnvironmentRaycastManager _raycastManager;  // This class uses Depth API to provide raycasting against the physical environment.
    [SerializeField] private Transform _rightControllerAnchor;
    [SerializeField] private GameObject _objectToSpawn;

    void Awake()
    {
        if(EnvironmentRaycastManager.IsSupported != true)
        {
            Debug.Log($"[{this.name}] EnvironmentRaycastManager is not supported.");
        }
        else
        {
            Debug.Log($"[{this.name}] EnvironmentRaycastManager is supported.");
            // This will also add the EnvironmentDepthManager instance if not present in the scene.
            // Disable EnvironemtDepthManager manually when disable EnvironemtRaycastManager.
            _raycastManager = gameObject.AddComponent<EnvironmentRaycastManager>();
        }
    }


    void Update()
    {
        if(OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            // Create a standard Unity ray
            var ray = new Ray(_rightControllerAnchor.position, _rightControllerAnchor.forward); // Ray(from, to)
            if(_raycastManager.Raycast(ray, out var hit))
            {
                // Spawn an object
//                var obj = _objectToSpawn;     // Option1: This does not work
//                var obj = Instantiate(_objectToSpawn, hit.point, Quaternion.LookRotation(hit.normal, Vector3.up));    // Option2: This works
                var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);   // Option3: This works
                obj.GetComponent<Renderer>().material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                obj.transform.SetPositionAndRotation(hit.point, Quaternion.LookRotation(hit.normal, Vector3.up));  // SetPositionAndRotation(position, rotation)
                                                                                                                    // LookRotation(forward, upward). forward is prioritized.

                obj.transform.localScale *= 0.1f;                                                                                                                    
                //If MRUK's world locking is not active, Add spatial anchor to the objs's position. 
                if(MRUK.Instance != null)
                {
                    if(MRUK.Instance.IsWorldLockActive != true)
                    {
                        Debug.Log($"[{this.name}] WorldLock is not set active.");
                        obj.AddComponent<OVRSpatialAnchor>();
                    }
                }
                else
                {
                    Debug.Log($"[{this.name}] No MRUK Instance.");
                }
            }
        }
    }
}

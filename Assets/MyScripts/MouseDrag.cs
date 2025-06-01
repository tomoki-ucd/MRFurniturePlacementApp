using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    private GameObject selectedObject;
    private bool isDragging;
    private float dragDistance = 5.0f;  // Need the distance to move the object as this script is not supposed to run in XR environment
    private Camera mainCamera;
    public float lerpSpeed = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Cast ray from mouse position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);        
        RaycastHit hit;

        if(Input.GetMouseButtonDown(0)) // GetMousebuttonDonw(0) is left mouse button
        {
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))   // Mathf.Infinity is the distance of the ray, meaning it will hit anything
            {
                selectedObject = hit.collider.gameObject;
                isDragging = true;
            }
        }

        // Stop draggin on mouse release
        if(Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            selectedObject = null;
        }

        // Update dragged object position
        if(isDragging && selectedObject != null)
        {
            // Project mouse position to a point in 3D space at dragDistance
            Vector3 targetPos = ray.origin + ray.direction * dragDistance;  // ray.direction is Vector3 having the length of 1.
            // Lerp is a function that interpolates between two values. The third parameter is the time it takes to interpolate. Multiply by 10f to make it faster.
            selectedObject.transform.position = Vector3.Lerp(selectedObject.transform.position, targetPos, Time.deltaTime * lerpSpeed);
        }

        // Visualize ray for debugging
        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.blue);
    }
}

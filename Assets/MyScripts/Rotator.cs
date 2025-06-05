using UnityEngine;

public class Rotator : MonoBehaviour
{
    float rotationSpeed = 180f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 右コントローラーのGrabボタンが押されているかどうか状態を取得
        // OVRInput is a global class
        // PrimaryHand is the left and SecondaryHand is the right.
        // HandTrigger is the trigger button to be pressed with the middle finger.
        // IndexTrigger is the trigger button to be pressed with the index finger.
        if(OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))  
        {
            Debug.Log($"{this.GetType().Name}: Right controller grab button is pressed.");
            // 右コントローラーのRaycastでGrabしているオブジェクトを取得
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit)) // Returns true if the ray hits something
            {
                {
                    Debug.Log($"{this.GetType().Name}: Hit object is {hit.collider.gameObject.name}.");
                    // TO DO: Modify so that it rotates when the trigger of the right controller is pressed.
                    if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
                    {
                        hit.collider.gameObject.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
                    }
                }
            }
            else
            {
                Debug.Log($"{this.GetType().Name}: Hit object is null.");
            }
        }
    }
}

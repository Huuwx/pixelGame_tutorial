using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector2 maxPostion;
    public Vector2 minPostion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(Vector3.Distance(target.position, transform.position) > float.Epsilon) 
        {
            Vector3 targetPostion = new Vector3(target.position.x, target.position.y, transform.position.z);
            targetPostion.x = Mathf.Clamp(targetPostion.x, minPostion.x, maxPostion.x);
            targetPostion.y = Mathf.Clamp(targetPostion.y ,minPostion.y, maxPostion.y);
            transform.position = Vector3.Lerp(transform.position, targetPostion, smoothing);
        }
    }
}

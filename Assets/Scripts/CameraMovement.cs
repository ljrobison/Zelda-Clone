using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPostion;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    void Update()
    {

    }

    //LateUpdate is called after all Update functions have been called.
    void LateUpdate()
    {
        if(transform.position != target.position)
        {
            Vector3 targetPostion = new Vector3(target.position.x, target.position.y, transform.position.z);
            targetPostion.x = Mathf.Clamp(targetPostion.x, minPostion.x, maxPosition.x);
            targetPostion.y = Mathf.Clamp(targetPostion.y, minPostion.y, maxPosition.y);
            transform.position = Vector3.Lerp(transform.position, targetPostion, smoothing);
        }
    }
}

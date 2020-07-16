using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMenuController : MonoBehaviour
{

    public GameObject roundabout;
    Vector3 vec3;
    Quaternion quaternion;

    void Start()
    {

		vec3 = new Vector3(0, 144, 0);
        quaternion = Quaternion.Euler(vec3);
        roundabout.transform.rotation = quaternion;


    }
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            vec3.y += Input.GetAxisRaw("Horizontal");
            quaternion = Quaternion.Euler(vec3);
            roundabout.transform.rotation = quaternion;
        }
        if (Input.touchCount > 0)
        {
            Vector2 touchPos = Input.GetTouch(0).position;
            if (Input.touchCount == 1)
            {
                Touch touchZero = Input.GetTouch(0);
                if (touchZero.phase == TouchPhase.Moved)
                {
                    Vector2 touchDelta = touchZero.deltaPosition;
					vec3.y -= touchDelta.x * 0.05f;
					quaternion = Quaternion.Euler(vec3);
            		roundabout.transform.rotation = quaternion;            
                }
            }
        }

    }
}

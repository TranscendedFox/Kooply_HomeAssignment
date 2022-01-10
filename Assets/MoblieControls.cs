using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoblieControls : MonoBehaviour
{
    private float rotationAngle;
    [SerializeField] private float addRotSpeed;
    [SerializeField] private float maxRotSpeed;
    private float minRotSpeed;

    [SerializeField] private float zoomSpeed;
    private float cameraFOV;
    // Start is called before the first frame update
    void Start()
    {
        minRotSpeed = maxRotSpeed * -1;
        cameraFOV = Camera.main.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {

        //Rotate charcter ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.position.y > 200 && touch.position.y < Screen.height - 200f)
            {
                Vector2 touchPrevPos = touch.position - touch.deltaPosition;

                if (touch.position.x > touchPrevPos.x)
                {
                    rotationAngle += addRotSpeed;
                }
                else if (touch.position.x < touchPrevPos.x)
                {
                    rotationAngle -= addRotSpeed;
                }
            }            
        }

        if (rotationAngle != 0)
        {
            transform.Rotate(Vector3.up, Mathf.Clamp(rotationAngle, minRotSpeed, maxRotSpeed));
            rotationAngle = Mathf.Lerp(rotationAngle, 0, 0.1f);
        }


        //Zoom charcter ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        if (Input.touchCount == 2)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            Vector2 firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            Vector2 secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            float distance = Vector2.Distance(firstTouch.position, secondTouch.position);
            float prevDistance = Vector2.Distance(firstTouchPrevPos, secondTouchPrevPos);

            if (distance != prevDistance)
            {
                //zoom in
                Camera.main.fieldOfView += (prevDistance - distance) * zoomSpeed;
                Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 40f, cameraFOV);
            }

            if (firstTouch.phase == TouchPhase.Ended || secondTouch.phase == TouchPhase.Ended)
            {
                Camera.main.fieldOfView = cameraFOV;
            }
        }
    }
}

using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 touchStart, newPos;
    private float zoomOutMin = 4;
    private float zoomOutMax = 30;


	private void Update () 
    {
        if(Input.GetMouseButtonDown(0)){
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        
        if (Input.touchCount > 0)
        {
            if(Input.touchCount == 2){
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                float difference = currentMagnitude - prevMagnitude;

                zoom(difference * 0.01f);
            }else if(Input.GetMouseButton(0) && Input.GetTouch(0).phase == TouchPhase.Moved){
                Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                newPos = Camera.main.transform.position + direction;
                Camera.main.transform.position = new Vector3(
                    ClampPos(Camera.main.orthographicSize, newPos.x, 'x'),
                    Camera.main.transform.position.y,
                    ClampPos(Camera.main.orthographicSize, newPos.z, 'z')
                );
            }
            zoom(Input.GetAxis("Mouse ScrollWheel"));
        }

	}


    private void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
        // Camera.main.transform.position = new Vector3(
        //         ClampPos(Camera.main.orthographicSize, Camera.main.transform.position.x, 'x'),
        //         Camera.main.transform.position.y,
        //         ClampPos(Camera.main.orthographicSize, Camera.main.transform.position.z, 'z')
        //     );
    }
    

    private float ClampPos(float orthoSize, float pos, char axis)
    {
        if (axis == 'x')
        {
            switch ((int)orthoSize)
            {
                case 4:
                    return Mathf.Clamp(pos, -67f, -5f);
                case 5:
                    return Mathf.Clamp(pos, -67f, -7f);
                case 6:
                    return Mathf.Clamp(pos, -67f, -9f);
                case 7:
                    return Mathf.Clamp(pos, -66f, -10f);
                case 8:
                    return Mathf.Clamp(pos, -65f, -11f);
                case 9:
                    return Mathf.Clamp(pos, -63f, -13f);
                case 10:
                    return Mathf.Clamp(pos, -61f, -15f);
                case 11:
                    return Mathf.Clamp(pos, -61f, -15f);
                case 12:
                    return Mathf.Clamp(pos, -61f, -15f);
                case 13:
                    return Mathf.Clamp(pos, -60f, -16f);
                case 14:
                    return Mathf.Clamp(pos, -58f, -18f);
                case 15:
                    return Mathf.Clamp(pos, -56f, -20f);
                default:
                    return pos;
            }
        }
        else
        {
            switch ((int)orthoSize)
            {
                case 4:
                    return Mathf.Clamp(pos, -62f, 19f);
                case 5:
                    return Mathf.Clamp(pos, -60f, 17f);
                case 6:
                    return Mathf.Clamp(pos, -58f, 15f);
                case 7:
                    return Mathf.Clamp(pos, -56f, 13f);
                case 8:
                    return Mathf.Clamp(pos, -55f, 12f);
                case 9:
                    return Mathf.Clamp(pos, -53f, 10f);
                case 10:
                    return Mathf.Clamp(pos, -51f, 8f);
                case 11:
                    return Mathf.Clamp(pos, -49f, 6f);
                case 12:
                    return Mathf.Clamp(pos, -48f, 5f);
                case 13:
                    return Mathf.Clamp(pos, -46f, 3f);
                case 14:
                    return Mathf.Clamp(pos, -44f, 2f);
                case 15:
                    return Mathf.Clamp(pos, -42f, 0f);
                default:
                    return pos;
            }
        }

    }
}

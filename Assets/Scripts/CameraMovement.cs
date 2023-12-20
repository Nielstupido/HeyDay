using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 touchStart, newPos;
    private float zoomOutMin = 10;
    private float zoomOutMax = 20;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (Input.touchCount == 1 && touch.phase == TouchPhase.Began)
            {
                touchStart = Camera.main.ScreenToWorldPoint(touch.position);
            }

            if (Input.touchCount == 1 && touch.phase == TouchPhase.Moved)
            {
                Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(touch.position);
                newPos = Camera.main.transform.position + direction;
                Camera.main.transform.position = new Vector3(
                    ClampPos(Camera.main.orthographicSize, newPos.x, 'x'),
                    Camera.main.transform.position.y,
                    ClampPos(Camera.main.orthographicSize, newPos.z, 'z')
                );
            }

            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                float difference = currentMagnitude - prevMagnitude;

                zoom(difference * 0.01f);
            }
        }

        // Zoom using the mouse scroll wheel
        zoom(Input.GetAxis("Mouse ScrollWheel"));
    }


    private void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }


    private float ClampPos(float orthoSize, float pos, char axis)
    {
        if (axis == 'x')
        {
            switch ((int)orthoSize)
            {
                case 10: case 11: case 12: case 13:
                    return Mathf.Clamp(pos, -90f, -4f);
                case 14: case 15: case 16:
                    return Mathf.Clamp(pos, -85f, -8f);
                case 17: case 18: case 19: case 20:
                    return Mathf.Clamp(pos, -80f, -10f);
                default:
                    return pos;
            }
        }
        else
        {
            switch ((int)orthoSize)
            {
                case 10: case 11: case 12: 
                    return Mathf.Clamp(pos, -58f, 25f);
                case 13: case 14: 
                    return Mathf.Clamp(pos, -55f, 22f);
                case 15: case 16: case 17: 
                    return Mathf.Clamp(pos, -50f, 17f);
                case 18: case 19: case 20:
                    return Mathf.Clamp(pos, -47f, 13f);
                default:
                    return pos;
            }
        }
    }
}

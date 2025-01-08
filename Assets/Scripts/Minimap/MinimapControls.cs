using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapControls : MonoBehaviour
{
    RectTransform rt;
    [SerializeField]
    Camera mmCam;
    [SerializeField]
    float scrollSpeed = 1, maxSize, minSize;

    Vector2 mousePos;
    Vector2 lastMousePos = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        mmCam.orthographic = true;
    }

    // Update is called once per frame
    void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, Input.mousePosition, null, out mousePos);
        
        Zoom();

        MoveAround();
    }
    bool IsMouseOver()
    {
        return mousePos.x < rt.sizeDelta.x / 2 
            && mousePos.x > -rt.sizeDelta.x / 2 
            && mousePos.y < rt.sizeDelta.y / 2 
            && mousePos.y > -rt.sizeDelta.y / 2;
    }
    void Zoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0 && IsMouseOver())
        {
            mmCam.orthographicSize -= scroll * scrollSpeed;

            if (mmCam.orthographicSize > maxSize)
                mmCam.orthographicSize = maxSize;

            if (mmCam.orthographicSize < minSize)
                mmCam.orthographicSize = minSize;
        }
    }
    void MoveAround()
    {
        if (Input.GetButton("Fire1") && IsMouseOver())
        {
            if (lastMousePos != Vector2.zero)
            {
                Vector2 v = (lastMousePos - mousePos) * 0.01f;
                mmCam.transform.position = new Vector3(mmCam.transform.position.x + v.x, mmCam.transform.position.y, mmCam.transform.position.z + v.y);
            }

            lastMousePos = mousePos;
        }else
        {
            lastMousePos = Vector2.zero;
        }
    }
}

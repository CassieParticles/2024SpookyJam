using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMovement : MonoBehaviour
{
    Camera cameraObj;

    LinkedList<GameObject> ObjectsIntersecting;

    void Start()
    {
        cameraObj = GameObject.Find("Main Camera").GetComponent<Camera>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3 cursorPos = cameraObj.ScreenToWorldPoint(mousePos);
        cursorPos.z = 0;
        transform.position = cursorPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collieded");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered");
    }
}

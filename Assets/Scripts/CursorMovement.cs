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
        ObjectsIntersecting = new LinkedList<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3 cursorPos = cameraObj.ScreenToWorldPoint(mousePos);
        cursorPos.z = 0;
        transform.position = cursorPos;

        //Bad practice, will udpate if we have time
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed");
        }
        if(Input.GetMouseButtonUp(0))
        {
            Debug.Log("Released");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MeteorPhysics meteorComp = collision.gameObject.GetComponent<MeteorPhysics>();
        if(meteorComp != null) 
        {
            //If game object has meteor component
            foreach(GameObject g in ObjectsIntersecting)
            {
                //Check if meteor comp is already in list (won't be but good to check)
                if(g.GetComponent<MeteorPhysics>()==meteorComp)
                {
                    Debug.Log("Something went wrong");
                    return; //If more needs to happen after this, change return with something else to prevent adding
                }
            }

            ObjectsIntersecting.AddLast(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Check if object is a meteor
        if(other.gameObject.GetComponent<MeteorPhysics>()==null) 
        {
            //If more is added, change this return for something to prevent it running the foreach
            return;
        }
        
        foreach (GameObject g in ObjectsIntersecting)
        {
            //Ensure item is in list (no reason it wouldn't be)
            if (g == other.gameObject)
            {
                ObjectsIntersecting.Remove(g);
                return;
            }
        }
    }
}

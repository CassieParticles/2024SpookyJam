using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeteorPhysics : MonoBehaviour
{

    private float angle;
    private bool isSelected;
    private GameObject cursor;
    private Rigidbody2D rb;
    private Rigidbody2D cursorRB;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected) {
            rb.velocity += new Vector2(cursorRB.position.x - rb.position.x, cursorRB.position.y - rb.position.y) / 2;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        //Explode
    }

    public void Select(GameObject selectingCursor) {
        isSelected = true;
        cursor = selectingCursor;
        cursorRB = cursor.GetComponent<Rigidbody2D>();
    }

    public void DeSelect() {
        isSelected = false;
    }

    public void setDespawnRange(float spawnRange) {
        //Here you go Cassie    
    }
}

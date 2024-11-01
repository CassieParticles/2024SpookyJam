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
    private float despawnRange = 15;
    [SerializeField][Range(0, 0.1f)] private float grabDrag = 0;
    [SerializeField] private float speedLimit = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected) {
            rb.velocity += -rb.velocity * grabDrag;
            rb.velocity += new Vector2(cursor.transform.position.x - rb.position.x, cursor.transform.position.y - rb.position.y) / 2;
            if (rb.velocity.magnitude > speedLimit) {
                rb.velocity *= speedLimit / rb.velocity.magnitude;
            }
        } else {
            if (rb.position.magnitude > despawnRange) {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        //Explode
    }

    public void Select(GameObject selectingCursor) {
        isSelected = true;
        cursor = selectingCursor;
    }

    public void DeSelect() {
        isSelected = false;
    }

    public void SetDespawnRange(float spawnRange) {
        //Here you go Cassie
        despawnRange = spawnRange + 1;
    }
}

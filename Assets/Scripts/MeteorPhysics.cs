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

    private float explosionTimer;
    private bool exploding = false;

    [SerializeField][Range(0, 0.1f)] private float grabDrag = 0;
    [SerializeField] private float speedLimit = 10;
    [SerializeField] private float accelerationDivider = 2; //5 for what we had before
    [SerializeField] private bool ExperimentalFling;

    //Audio related SerializeFields
    [SerializeField] private float flingSoundThreshold = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!exploding) {
            if (isSelected) {
                rb.velocity += -rb.velocity * grabDrag;
                if (!ExperimentalFling) {
                    rb.velocity += new Vector2(cursor.transform.position.x - rb.position.x, cursor.transform.position.y - rb.position.y) / accelerationDivider;
                } else {
                    rb.velocity += new Vector2(cursor.transform.position.x - rb.position.x, cursor.transform.position.y - rb.position.y) / rb.velocity.magnitude * accelerationDivider / 4;
                }
                if (rb.velocity.magnitude > speedLimit) {
                    rb.velocity *= speedLimit / rb.velocity.magnitude;
                }
            } else {
                if (rb.position.magnitude > despawnRange) {
                    Destroy(gameObject);
                }
            }
        } else {
            explosionTimer += Time.deltaTime;
            if (explosionTimer > 1) {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Explode();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name == "Explosion") {
            Explode();
        }
    }

    public void Explode() {
        exploding = true;
        rb.velocity = Vector2.zero;
        GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(true);
        DeSelect();

        //Plays the Meteor_Explode event
        AkSoundEngine.PostEvent("Meteor_Explode", this.gameObject);
    }

    public void Select(GameObject selectingCursor) {
        isSelected = true;
        cursor = selectingCursor;
    }

    public void DeSelect() {
        isSelected = false;
        if (rb.velocity.magnitude > flingSoundThreshold && !exploding) {
            
            //Plays the Meteor_Throw event
            AkSoundEngine.PostEvent("Meteor_Throw", this.gameObject);
        }
    }

    public void SetDespawnRange(float spawnRange) {
        despawnRange = spawnRange + 1;
    }

    public bool IsExploding() {
        return exploding;
    }
}

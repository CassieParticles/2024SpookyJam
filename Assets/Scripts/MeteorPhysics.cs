using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class MeteorPhysics : MonoBehaviour
{

    //private float angularMoveDir, newMoveDir;
    private bool isSelected;
    private GameObject cursor;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private float despawnRange = 15;

    private float explosionTimer;
    private bool exploding = false;

    [SerializeField][Range(0, 0.1f)] private float grabDrag = 0;
    [SerializeField] private float speedLimit = 10;
    [SerializeField] private float accelerationDivider = 2; //5 for what we had before

    //Audio related SerializeFields
    [SerializeField] private float flingSoundThreshold = 10;

    [SerializeField] private Sprite[] sprites;

    [SerializeField] private GameObject MiniExplosion;
    private GameObject ExplosionManager;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.GetChild(0).gameObject.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[(int) Mathf.Ceil(Random.value * sprites.Length) - 1];
        ExplosionManager = GameObject.Find("ExplosionManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (!exploding) {
            if (isSelected) {
                rb.velocity += -rb.velocity * grabDrag;
                rb.velocity += new Vector2(cursor.transform.position.x - rb.position.x, cursor.transform.position.y - rb.position.y) / rb.velocity.magnitude * accelerationDivider / 4;
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
        if (collision.gameObject.name == "Meteor(Clone)") {
            AdeleExplosion(collision);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name == "Explosion" || collision.name == "WinProtection") {
            Explode();
        }
    }

    public void Explode() {
        exploding = true;
        rb.velocity = Vector2.zero;
        GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(true);
        DeSelect();
        rb.freezeRotation = true;

        //Plays the Meteor_Explode event
        AkSoundEngine.PostEvent("Meteor_Explode", this.gameObject);
    }

    public void Select(GameObject selectingCursor) {
        isSelected = true;
        cursor = selectingCursor;
    }

    public void DeSelect() {
        isSelected = false;
        if (rb.velocity.magnitude > flingSoundThreshold) {
            
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

    public void AdeleExplosion(Collision2D collision) {
        Vector2 meteor1Dir = gameObject.transform.GetComponent<Rigidbody2D>().velocity;
        Vector2 meteor2Dir = collision.transform.GetComponent<Rigidbody2D>().velocity;

        Vector2 AvgPos = new Vector2(gameObject.transform.position.x + collision.transform.position.x / 2, gameObject.transform.position.y + collision.transform.position.y / 2);

        ExplosionManager.GetComponent<ExplosionManager>().AddExplosion(meteor1Dir, meteor2Dir, AvgPos);
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        rb.velocity = Vector2.zero;
        exploding = true;
    }
}

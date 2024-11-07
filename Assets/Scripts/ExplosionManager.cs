using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
//using static UnityEditor.Progress;

public class explosionInfo {
    int id;
    float detonation;
    float disappear;
    Vector2 position;
    GameObject gameObject;
    bool alive = true;

    public explosionInfo(int idd, float det, float dis, Vector2 pos, GameObject game) {
        id = idd;
        detonation = det;
        disappear = dis;
        position = pos;
        gameObject = game;

        gameObject.transform.position = position;
        gameObject.SetActive(false);
    }
    public int GetID() {
        return id;
    }
    public float GetDetonation() {
        return detonation;
    }
    public float GetDisappear() {
        return disappear;    
    }
    public Vector2 GetPosition() {
        return position;
    }
    public GameObject GetGameObject() {
        return gameObject;    
    }
    public void SetAlive(bool isAlive) {
        alive = isAlive;
    }
    public bool IsAlive() {
        return alive;
    }
}

public class ExplosionManager : MonoBehaviour
{
    [SerializeField][Range(0.2f, 5)] private float clusterDuration;
    [SerializeField][Range(0.1f, 5)] private float clusterLength;
    [SerializeField][Range(0.1f, 2)] private float clusterStray;
    [SerializeField][Range(0.1f, 3)] private float explosionDuration;
    [SerializeField][Range(0, 8)] private int explosionCount;
    [SerializeField][Range(0, 8)] private int splitExplosionCount;
    private List<explosionInfo> MiniExplosionList;
    [SerializeField] private GameObject MiniExplosion;
    float timer;

    Vector2 distVector, collisionDir, explosionPos, strayVector; //Temp variables for calculations

    // Start is called before the first frame update
    void Start()
    {
        MiniExplosionList = new List<explosionInfo>();
        //MiniExplosionList.Add(Instantiate(MiniExplosion, gameObject.transform));
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (MiniExplosionList.Count > 0 ) {
            foreach (var item in MiniExplosionList) {
                if (item.GetDetonation() < timer) {
                    if (item.GetGameObject().activeSelf && item.GetDisappear() < timer) {
                        Destroy(item.GetGameObject());
                        item.SetAlive(false);
                        continue;
                    }
                    item.GetGameObject().SetActive(true);
                }
            }
            for (int i = 0; i < MiniExplosionList.Count; i++) {
                if (!MiniExplosionList[i].IsAlive()) {
                    MiniExplosionList.RemoveAt(i);
                    i--;
                }
            }
        }
    }

    public void AddExplosion(Vector2 dir1, Vector2 dir2, Vector2 pos) {
        collisionDir = (dir1 + dir2) / 4;

        for (int i = 0; i < splitExplosionCount; i++) {
            distVector = dir1 * UnityEngine.Random.value / 4;
            explosionPos = new Vector2(distVector.x + pos.x, distVector.y + pos.y);

            strayVector = new Vector2(Mathf.Cos(distVector.x - collisionDir.x) - Mathf.Sin(distVector.y - collisionDir.y), Mathf.Sin(distVector.x - collisionDir.x) - Mathf.Cos(distVector.y - collisionDir.y));
            strayVector *= UnityEngine.Random.value - 0.5f;

            MiniExplosionList.Add(new explosionInfo(MiniExplosionList.Count - 1, timer + (distVector.magnitude * clusterDuration / 10), timer + (distVector.magnitude * clusterDuration / 10) + explosionDuration + 0.5f - distVector.magnitude / 80, explosionPos + strayVector, Instantiate(MiniExplosion)));
        }
        for (int i = 0; i < explosionCount; i++) {
            distVector = collisionDir * UnityEngine.Random.value;
            explosionPos = new Vector2(distVector.x + pos.x, distVector.y + pos.y);

            strayVector = new Vector2(Mathf.Cos(distVector.x - collisionDir.x) - Mathf.Sin(distVector.y - collisionDir.y), Mathf.Sin(distVector.x - collisionDir.x) - Mathf.Cos(distVector.y - collisionDir.y));
            strayVector *= (UnityEngine.Random.value - 0.5f) * clusterStray;

            MiniExplosionList.Add(new explosionInfo(MiniExplosionList.Count - 1, timer + (distVector.magnitude * clusterDuration / 10), timer + (distVector.magnitude * clusterDuration / 10) + explosionDuration + 0.5f - distVector.magnitude / 80, explosionPos + strayVector, Instantiate(MiniExplosion)));
        }

        
    }

    public void AddExplosionSingle(Vector2 dir, Vector2 pos) {
        for (int i = 0; i < splitExplosionCount; i++) {
            distVector = dir * UnityEngine.Random.value / 4;
            explosionPos = new Vector2(distVector.x + pos.x, distVector.y + pos.y);
            
            strayVector = new Vector2(Mathf.Cos(distVector.x - collisionDir.x) - Mathf.Sin(distVector.y - collisionDir.y), Mathf.Sin(distVector.x - collisionDir.x) - Mathf.Cos(distVector.y - collisionDir.y));
            strayVector *= UnityEngine.Random.value - 0.5f;

            MiniExplosionList.Add(new explosionInfo(MiniExplosionList.Count - 1, timer + (distVector.magnitude * clusterDuration / 10), timer + (distVector.magnitude * clusterDuration / 10) + explosionDuration + 0.5f - distVector.magnitude / 80, explosionPos + strayVector, Instantiate(MiniExplosion)));
        }
    }
}

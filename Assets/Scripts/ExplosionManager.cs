using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class explosionInfo {
    int id;
    float detonation;
    float disappear;
    Vector2 position;
    GameObject gameObject;

    public explosionInfo(int idd, float det, float dis, Vector2 pos, GameObject game) {
        id = idd;
        detonation = det;
        disappear = dis;
        position = pos;
        gameObject = game;
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
}

public class ExplosionManager : MonoBehaviour
{
    [SerializeField][Range(0.2f, 5)] private float clusterDuration;
    [SerializeField][Range(0.1f, 5)] private float clusterLength;
    [SerializeField][Range(0.1f, 2)] private float clusterStray;
    [SerializeField][Range(0.1f, 3)] private float explosionDuration;
    [SerializeField][Range(0, 8)] private int explosionCount;
    private List<explosionInfo> MiniExplosionList;
    [SerializeField] private GameObject MiniExplosion;
    float timer;

    Vector2 distVector, collisionDir, explosionPos; //Temp variables for calculations

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
                if (item.GetDetonation() > timer) {
                    if (item.GetGameObject().activeSelf && item.GetDisappear() > timer) {
                        Destroy(item.GetGameObject());
                        continue;
                    }
                    item.GetGameObject().SetActive(true);
                }
            }
            for (int i = 0; i < MiniExplosionList.Count; i++) {
                if (!MiniExplosionList[i].GetGameObject()) {
                    MiniExplosionList.RemoveAt(i);
                    i--;
                }
            }
        }
    }

    public void AddExplosion(Vector2 dir1, Vector2 dir2, Vector2 pos) {
        collisionDir = (dir1 + dir2) / 2;

        for (int i = 0; i < explosionCount; i++) {
            distVector = collisionDir * UnityEngine.Random.value;
            explosionPos = new Vector2(distVector.x + pos.x, distVector.y + pos.y);
            MiniExplosionList.Add(new explosionInfo(MiniExplosionList.Count - 1, timer + (distVector.magnitude * clusterDuration / 60), timer + (distVector.magnitude * clusterDuration / 60) + explosionDuration + 0.5f - distVector.magnitude / 80, explosionPos, Instantiate(MiniExplosion)));
            Debug.Log("Time:" + timer + " Det:" + (timer + (distVector.magnitude * clusterDuration / 60)) + " Fade: " + (timer + (distVector.magnitude * clusterDuration / 60) + explosionDuration + 0.5f - distVector.magnitude / 80));
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSizeControl : MonoBehaviour
{
    [SerializeField][Range(0.01f,10.0f)] private float cursorSize = 1;


    private void Update()
    {
        transform.localScale = new Vector2(cursorSize,cursorSize);
    }
}

/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections;

class AnimateSpriteSheet : MonoBehaviour
{
    public int Columns = 5;
    public int Rows = 5;
    public float FramesPerSecond = 10f;
    public bool RunOnce = true;
 
    public float RunTimeInSeconds
    {
        get
        {
            return ( (1f / FramesPerSecond) * (Columns * Rows) );
        }
    }
 
    private Material materialCopy = null;
 
    void Start()
    {
        // Copy its material to itself in order to create an instance not connected to any other
        materialCopy = new Material(GetComponent<Renderer>().sharedMaterial);
        GetComponent<Renderer>().sharedMaterial = materialCopy;
 
        Vector2 size = new Vector2(1f / Columns, 1f / Rows);
        GetComponent<Renderer>().sharedMaterial.SetTextureScale("_MainTex", size);
    }
 
    void OnEnable()
    {
        StartCoroutine(UpdateTiling());
    }
 
    private IEnumerator UpdateTiling()
    {
        float x = 0f;
        float y = 0f;
        Vector2 offset = Vector2.zero;
 
        while (true)
        {
            for (int i = Rows-1; i >= 0; i--) // y
            {
                y = (float) i / Rows;
 
                for (int j = 0; j <= Columns-1; j++) // x
                {
                    x = (float) j / Columns;
 
                    offset.Set(x, y);
 
                    GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
                    yield return new WaitForSeconds(1f / FramesPerSecond);
                }
            }
 
            if (RunOnce)
            {
                yield break;
            }
        }
    }
}

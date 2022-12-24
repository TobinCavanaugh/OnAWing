using UnityEngine;

[ExecuteAlways]
public class CloudUVScroller : MonoBehaviour
{
    public Material cloudMat;

    public float addAmount = .001f;
    public float lerpSpeed = .01f;
    private float curAmount = 0;
    private void Update()
    {
        if (cloudMat is null)
        {
            return;
        }
        curAmount += addAmount;
        cloudMat.mainTextureOffset = new(curAmount, 0);
    }
}

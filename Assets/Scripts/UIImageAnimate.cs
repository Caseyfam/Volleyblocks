using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIImageAnimate : MonoBehaviour {

    public Sprite[] sprites;
    public float waitTime;
    public bool random = true;
    Coroutine localCor;

    private int spriteIndex = 0;

    UnityEngine.UI.Image image;

    private void Awake()
    {
        image = GetComponent<UnityEngine.UI.Image>();
    }

    private void OnEnable()
    {
        localCor = StartCoroutine(WaitToAnimate(waitTime));
    }

    IEnumerator WaitToAnimate(float time)
    {
        image.sprite = sprites[spriteIndex];
        yield return new WaitForSeconds(time);
        if (random)
        {
            int newSpriteIndex = Random.Range(0, sprites.Length);
            if (newSpriteIndex == spriteIndex)
            {
                spriteIndex = Random.Range(0, sprites.Length);
            }
            else
            {
                spriteIndex = newSpriteIndex;
            }
        }
        else
        {
            if (spriteIndex >= sprites.Length - 1)
            {
                spriteIndex = 0;
            }
            else
            {
                spriteIndex++;
            }
        }
   
        localCor = StartCoroutine(WaitToAnimate(waitTime));
    }
}

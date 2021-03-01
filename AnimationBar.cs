using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBar : MonoBehaviour
{
    public GameObject food;
    public AnimationClip timeLifeBigPoint;
    bool animationPlay = false;
    void Start()
    {
        
    }

    void Update()
    {
        animationPlay = food.GetComponent<SpawnPoint>().startAnimation;
        if (animationPlay == true)
        {
            AnimationBigPointBar();
        }
    }

    private void AnimationBigPointBar()
    {
        animationPlay = false;
        gameObject.GetComponent<Animation>().Play(timeLifeBigPoint.name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBigPoint : MonoBehaviour
{
    public AnimationClip animatiomBigPoint;
    public AnimationClip animatiomSimplePoint;
    bool animationPlay = false;

    void Update()
    {
        animationPlay = gameObject.GetComponent<SpawnPoint>().startAnimationBigPoint;
        if (animationPlay == true)
        {
            AnimationPoint(animatiomBigPoint.name);
        }
        else 
        {
            AnimationPoint(animatiomSimplePoint.name);
        }

    }

    private void AnimationPoint(string animationName)
    {
        animationPlay = false;
        gameObject.GetComponent<Animation>().Play(animationName);
    }
}

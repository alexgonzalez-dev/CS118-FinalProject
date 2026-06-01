using UnityEngine;

public class AnimationButton : MonoBehaviour
{
    public Animator animator;

    public void PlayAnimation()
    {
        animator.SetTrigger("PlayAnimation");
    }
}
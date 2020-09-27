using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileFirer), typeof(Animator))]
public class WizardFireballAnimation : MonoBehaviour
{
    private static readonly string ANIM_TRIGGER_START_CASTING = "StartCasting";
    private static readonly string ANIM_TRIGGER_FIRE = "Fire";

    private ProjectileFirer firer;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        firer = GetComponent<ProjectileFirer>();
        firer.Fired += OnFireballLaunched;
        animator.SetTrigger(ANIM_TRIGGER_START_CASTING);
    }

    private void OnFireballLaunched()
    {
        animator.SetTrigger(ANIM_TRIGGER_FIRE);

        StartCoroutine(DeferStartCasting());
    }

    private IEnumerator DeferStartCasting()
    {
        yield return new WaitForSeconds(0.1f);

        animator.SetTrigger(ANIM_TRIGGER_START_CASTING);
    }
}

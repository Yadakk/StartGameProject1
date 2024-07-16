using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrandmaDataHolder : MonoBehaviour
{
    public GrandmaData GrandmaData;

    private Animator _animator;
    public Animator Animator
    {
        get
        {
            if (_animator == null) _animator = GetComponent<Animator>();
            return _animator;
        }
        set => _animator = value;
    }

    private void Start()
    {
        SetAnimator();
    }

    public void SetAnimator()
    {
        Animator.runtimeAnimatorController = GrandmaData.AnimatorController;
    }
}

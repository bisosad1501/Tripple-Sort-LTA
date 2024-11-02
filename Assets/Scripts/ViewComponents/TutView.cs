using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutView : MonoBehaviour
{
    public int Phase;
    public bool Done;
    void Start()
    {
        Phase = 0;
        Done = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        Phase1();
    }
    public void Phase1(Action act = null)
    {
        Phase = 1;
        transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.6f).SetEase(Ease.InCubic).SetLoops(-1, LoopType.Yoyo);
    }
    public void Phase2()
    {
        DOTween.Kill(transform);
        transform.localScale = Vector3.one;
        Phase = 2;
        transform.DOLocalMove(new Vector3(-200f, -40f, 0), 3f).SetEase(Ease.InOutQuart).SetLoops(-1, LoopType.Restart);
    }
}

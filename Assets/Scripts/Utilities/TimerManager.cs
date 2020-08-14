using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Timers;
using UnityEngine;



public class TimerManager : MonoBehaviour
{
    private static List<Timer> timers = new List<Timer>();
    private static List<Action> conditionCheckers = new List<Action>();


    private class AnimItem
    {
        public Animator animator;
        public Action<Animator> method;
        public AnimItem(Animator animator, Action<Animator> method)
        {
            this.animator = animator;
            this.method = method;
        }
    }
    private static List<AnimItem> animItems;
    
    public static void TurnOnTimer(Timer timerToTurnOn)
    {
        timers.Add(timerToTurnOn);
    }

    public static void TurnOffTimer(Timer timerToTurnOff)
    {
        timers.Remove(timerToTurnOff);
    }

    public static void TurnOnCondition(Action conditionChecker)
    {
        conditionCheckers.Add(conditionChecker);
    }

    public static void TurnOffCondition(Action conditionChecker)
    {
        conditionCheckers.Remove(conditionChecker);
    }



    public static void DoWhenAnimationOver (Animator animator, Action<Animator> methodToDo)
    {
        animItems.Add(new AnimItem(animator, methodToDo));
    }

    public void Update()
    {
        for(int i = 0; i < timers.Count; i++)
        {
            timers[i].UpdateClock();
        }
        for (int i = 0; i < animItems.Count; i++)
        {
            if(animItems[i].animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= .99f)
            {
                animItems[i].method(animItems[i].animator);
                animItems.RemoveAt(i);
                i--;
            }
        }
    }

}

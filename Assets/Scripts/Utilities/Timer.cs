using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    //public float PercentComplete { get { return _timer / Duration; } }
    public float Duration { get; private set; }

    private bool resetTimerOnComplete;
    private float endTime;

    private Action onCompleteCallback;
    private Action onUpdateCallback;

    public void DoActionAfterTime(float time, Action onCompleteCallback, Action onUpdateCallback = null, bool resetTimerOnComplete = false)
    {
        Duration = time;
        endTime = Time.time + Duration;
        this.resetTimerOnComplete = resetTimerOnComplete;
        this.onCompleteCallback = onCompleteCallback;
        this.onUpdateCallback = onUpdateCallback;
        TimerManager.TurnOnTimer(this);
    }


    public void ResetTimer()
    {
        endTime = Time.time + Duration;
    }

    public void UpdateClock()
    {
        if (Time.time < endTime)
        {
            if (onUpdateCallback != null)
            {
                onUpdateCallback();
            }
        }
        else
        {
            if (onCompleteCallback != null)
            {
                onCompleteCallback();
            }

            if(resetTimerOnComplete)
            {
                ResetTimer();
            }
            else
            {
                TimerManager.TurnOffTimer(this);
            }
        }
    }
}

//public enum OperatorCondition
//{
//    equal = 0,
//    greater = 1,
//    less = 2,
//    notEqual = 3,
//}
//public class ConditionChecker
//{
//    //public float PercentComplete { get { return _timer / Duration; } }
//    public float Duration { get; private set; }

//    private bool resetTimerOnComplete;
//    private float _timer;
//    private float endTime;



//    private Action onCompleteCallback;
//    private Action onUpdateCallback;


//    private delegate bool ConditionCheck<Tcond, Tval>();
//    ConditionCheck<bool,bool> conditionCheck;

//    private delegate void Test();
//    Test test;

//    bool boolCondition;
//    bool boolValue;

//    float floatCondition;
//    ref float floatValue;

//    public void DoActionOnCondition(bool condition, OperatorCondition operatorCondition, ref bool value, Action onCompleteCallback, Action onUpdateCallback = null)
//    {
//        boolValue = value;
//        boolCondition = condition;
//        this.onCompleteCallback = onCompleteCallback;
//        this.onUpdateCallback = onUpdateCallback;
//        //TimerManager.TurnOnCondition(conditionChecker);
//        if (operatorCondition == OperatorCondition.equal)
//        {
//            conditionCheck = CheckBoolEquals;
//        }
//        else if(operatorCondition == OperatorCondition.notEqual)
//        {
//            conditionCheck = CheckBoolNotEqual;
//        }
//    }

//    public void DoActionOnCondition(float condition, OperatorCondition operatorCondition, ref float value, Action onCompleteCallback, Action onUpdateCallback = null)
//    {
//        ref floatValue = ref value;
//        floatCondition = condition;
//        this.onCompleteCallback = onCompleteCallback;
//        this.onUpdateCallback = onUpdateCallback;
//        //TimerManager.TurnOnCondition(conditionChecker);
//        if (operatorCondition == OperatorCondition.equal)
//        {
//            conditionCheck = CheckBoolEquals;
//        }
//        else if (operatorCondition == OperatorCondition.notEqual)
//        {
//            conditionCheck = CheckBoolNotEqual;
//        }
//    }



//    private bool CheckBoolEquals()
//    {
//        if(boolCondition == boolValue)
//        {
//            return true;
//        }
//        else
//        {
//            return false;
//        }
//    }

//    private bool CheckBoolNotEqual()
//    {
//        if (boolCondition != boolValue)
//        {
//            return true;
//        }
//        else
//        {
//            return false;
//        }
//    }

//    private bool CheckFloatEqual()
//    {
//        if (floatCondition == floatValue)
//        {
//            return true;
//        }
//        else
//        {
//            return false;
//        }
//    }


//    private bool CheckFloatNotEqual()
//    {
//        if(floatCondition != floatValue)
//        {
//            return true;
//        }
//        else
//        {
//            return false;
//        }
//    }

//    private bool CheckFloatGreater()
//    {
//        if (floatCondition > floatValue)
//        {
//            return true;
//        }
//        else
//        {
//            return false;
//        }
//    }

//    private bool CheckFloatLess()
//    {
//        if (floatCondition < floatValue)
//        {
//            return true;
//        }
//        else
//        {
//            return false;
//        }
//    }

//    private bool CheckFloatCondition()
//    {
//        if(floatCondition == floatValue)
//        {
//            return true;
//        }
//        else
//        {
//            return false;
//        }
//    }

//    public void ResetTimer()
//    {
//        endTime = Time.time + Duration;
//    }

//    public void UpdateConditionChecker()
//    {
//        if(conditionCheck())
//        {
//            OnConditionMet();
//        }
//        else
//        {
//            OnConditionNotMet();
//        }
        
//    }


//    public void OnConditionMet()
//    {
//            if (onCompleteCallback != null)
//            {
//                onCompleteCallback();
//            }
//       // TimerManager.TurnOffCondition(conditionChecker);
//    }

//    public void OnConditionNotMet()
//    {
//        if (onUpdateCallback != null)
//        {
//            onUpdateCallback();
//        }
//    }
//}

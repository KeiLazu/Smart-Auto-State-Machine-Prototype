/// <summary>
/// keiStateMachine V 1.0
/// Kei Lazu
/// 
/// Desc:
/// State Machine Controller
/// Input: keiSmartAutoController
/// Output: Controlling State based on keiSmartAutoController
/// 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace keiStateControlStuff
{
    public class keiStateMachine<T>
    {
        public keiState<T> keiCurrentState { get; private set; }
        public T keiOwner;

        public keiStateMachine(T kei_Owner)
        {
            keiOwner = kei_Owner;
            keiCurrentState = null;

        }

        public void keiChangeState(keiState<T> kei_NewState)
        {
            if (keiCurrentState != null)
            {
                keiCurrentState.keiExitState(keiOwner);

            }

            keiCurrentState = kei_NewState;
            keiCurrentState.keiEnterState(keiOwner);

        }

        public void keiUpdate()
        {
            if (keiCurrentState != null)
            {
                keiCurrentState.keiUpdateState(keiOwner);

            }

        }

    }

    public abstract class keiState<T>
    {
        public abstract void keiEnterState(T kei_Owner);
        public abstract void keiExitState(T kei_Owner);
        public abstract void keiUpdateState(T kei_Owner);

    }


}


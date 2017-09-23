/// <summary>
/// keiSmartAutoController V 1.1
/// Kei Lazu
/// 
/// Desc:
/// Controlling State here, or should i say, A.I. Controller
/// 
/// Changelog:
/// 1.1 : Change the way State Machine was called
/// 1.1 : Delay and get called by each state if the work for that state is finished (or you can say Behavior Tree State Machine)
/// 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using keiStateControlStuff;

public class keiSmartAutoController : MonoBehaviour {

    public int[] keiEnemyElemIntel = new int[9];
    public int[] keiEnemyCountDownIntel = new int[9];

    public int[] keiPlayerTypeResource = new int[5];
    public int[] keiPlayerElemResource = new int[5];

    public int keiDecisionAttack, keiDecisionAttackRow, keiDecisionAttackCol, keiDecisionAttackSlot;

    public bool keiIsFinished;

    public keiStateMachine<keiSmartAutoController> keiStateMachine { get; set; }

    private void Start()
    {
        keiStateMachine = new keiStateMachine<keiSmartAutoController>(this);
        keiStateMachine.keiChangeState(keiScanState.kei_getsetInstance);
        Invoke("keiChangingState()", 1f);

    }

    public void keiChangingState()
    {
        if (keiIsFinished)
        {
            StartCoroutine(keiPleaseWaitASec());

        }

        CancelInvoke("keiChangingState()");

    }

    //public void keiIntelChecker()
    //{
    //    for (int i = 0; i < keiPlayerTypeResource.Length; i++)
    //    {
    //        Debug.Log("Pos: " + i + " || Type: " + keiPlayerTypeResource[i] + " || Element: " + keiPlayerElemResource[i]);

    //    }

    //}

    IEnumerator keiPleaseWaitASec()
    {
        yield return new WaitForSeconds(1f);
        keiStateMachine.keiUpdate();
        StopCoroutine(keiPleaseWaitASec());

    }

}

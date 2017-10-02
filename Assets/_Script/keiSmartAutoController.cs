/// <summary>
/// keiSmartAutoController V 1.2
/// Kei Lazu
/// 
/// Desc:
/// Controlling State here, or should i say, A.I. Controller
/// 
/// Changelog:
/// 1.2 : Change Enemy Intel into Jagged Array
/// 1.1 : Change the way State Machine was called
/// 1.1 : Delay and get called by each state if the work for that state is finished (or you can say Behavior Tree State Machine)
/// 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using keiStateControlStuff;

public class keiSmartAutoController : MonoBehaviour {

    public int[][][] keiEnemyIntel = new int[3][][];
    public int[][] keiPlayerResource = new int[5][];

    public int[] keiEnemyElemIntel = new int[9]; // change scan and attack state
    public int[] keiEnemyCountDownIntel = new int[9]; // change scan and attack state

    //public int[] keiPlayerTypeResource = new int[5]; // change resource state
    //public int[] keiPlayerElemResource = new int[5]; // change resource state

    public int keiDecisionAttackRow, keiDecisionAttackCol, keiDecisionAttackSlot;
    public int keiDecisionAttack = 9;

    public bool keiIsFinished;

    public keiStateMachine<keiSmartAutoController> keiStateMachine { get; set; }

    private void Awake()
    {
        // Region: Init Enemy
        keiEnemyIntel[0] = new int[3][];
        keiEnemyIntel[1] = new int[3][];
        keiEnemyIntel[2] = new int[3][];

        keiEnemyIntel[0][0] = new int[2];
        keiEnemyIntel[0][1] = new int[2];
        keiEnemyIntel[0][2] = new int[2];

        keiEnemyIntel[1][0] = new int[2];
        keiEnemyIntel[1][1] = new int[2];
        keiEnemyIntel[1][2] = new int[2];

        keiEnemyIntel[2][0] = new int[2];
        keiEnemyIntel[2][1] = new int[2];
        keiEnemyIntel[2][2] = new int[2];

        // Region: Init Resources
        keiPlayerResource[0] = new int[2];
        keiPlayerResource[1] = new int[2];
        keiPlayerResource[2] = new int[2];
        keiPlayerResource[3] = new int[2];
        keiPlayerResource[4] = new int[2];

    }

    private void Start()
    {
        keiControlStateMachine();

    }

    public void keiControlStateMachine()
    {
        keiStateMachine = new keiStateMachine<keiSmartAutoController>(this);
        keiStateMachine.keiChangeState(keiScanState.kei_getsetInstance);
        Invoke("keiChangingState()", 2f);

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

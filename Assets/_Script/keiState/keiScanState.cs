/// <summary>
/// keiScanState V 1.2
/// Kei Lazu
/// 
/// Desc:
/// Scan State, pretty straightforward, isn't it?
/// Input: Attack has finished or Trigger from outside state
/// Output: Enemies Element and Types
/// 
/// - First State
/// 
/// Changelog:
/// 1.2 : Change the way AI scan using jagged array
/// 1.1 : added keiIsFinished as trigger to the next state
/// 1.1 : now can Scan enemy entirely with their countdown to attack also their element
/// 
/// </summary>

using keiStateControlStuff;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class keiScanState : keiState<keiSmartAutoController>
{
    private static keiScanState keiInstance;

    private keiScanState()
    {
        if (keiInstance != null)
        {
            return;
        }

        keiInstance = this;
    }

    public static keiScanState kei_getsetInstance
    {
        get
        {
            if (keiInstance == null)
            {
                new keiScanState();
            }

            return keiInstance;
        }
    }

    // Start Region: Init ---------------------------------------------

    // Region: Judgement
    private bool keiIsEnemyStillAlive = false;

    // Region: Pathfinder
    private keiInitializatorGameObject keiInitScript;
    private GameObject keiEnemyPos;

    // Region: Intel
    private Image[][] keiPositionIntel = new Image[3][];

    // Region: Logging
    private int keiEnemyCounted;

    // End Region: Init -----------------------------------------------

    public void keiInitArray()
    {
        keiPositionIntel[0] = new Image[3];
        keiPositionIntel[1] = new Image[3];
        keiPositionIntel[2] = new Image[3];

    }

    public override void keiEnterState(keiSmartAutoController kei_Owner)
    {
        kei_Owner.keiDecisionAttack = 9;
        kei_Owner.keiDecisionAttackSlot = 0;
        kei_Owner.keiDecisionAttackCol = 0;
        kei_Owner.keiDecisionAttackRow = 0;

        keiEnemyCounted = 0;

        keiIsEnemyStillAlive = false;

        keiInitScript = GameObject.Find("keiInitScript").GetComponent<keiInitializatorGameObject>();
        keiEnemyPos = GameObject.FindGameObjectWithTag("keiEnemyPos");

        keiInitScript.keiLblLogState.GetComponent<Text>().text = "State: Scanning";

        keiInitArray();

        keiPopulatingPosition(keiEnemyPos, kei_Owner);

    }

    public override void keiExitState(keiSmartAutoController kei_Owner)
    {
        //Debug.Log("Is Enemy Still Alive? " + keiIsEnemyStillAlive);
        kei_Owner.keiIsFinished = false;

    }

    public override void keiUpdateState(keiSmartAutoController kei_Owner)
    {
        if (keiIsEnemyStillAlive)
        {
            kei_Owner.keiStateMachine.keiChangeState(keiCheckResourceState.kei_getsetInstance);

        }
        else
        {
            Debug.Log("Finish");
            keiInitScript.keiLblLogState.GetComponent<Text>().text = "State: Finish";
            GameObject.Find("keiWaveControlScript").GetComponent<keiWaveController>().keiNewWave();

            return;

        }
    }

    public void keiPopulatingPosition(GameObject kei_EnemyPos, keiSmartAutoController kei_SmartAuto)
    {
        keiPositionIntel[0][0] = kei_EnemyPos.transform.Find("keiEnemyA1").GetComponent<Image>();
        keiPositionIntel[0][1] = kei_EnemyPos.transform.Find("keiEnemyB1").GetComponent<Image>();
        keiPositionIntel[0][2] = kei_EnemyPos.transform.Find("keiEnemyC1").GetComponent<Image>();

        keiPositionIntel[1][0] = kei_EnemyPos.transform.Find("keiEnemyA2").GetComponent<Image>();
        keiPositionIntel[1][1] = kei_EnemyPos.transform.Find("keiEnemyB2").GetComponent<Image>();
        keiPositionIntel[1][2] = kei_EnemyPos.transform.Find("keiEnemyC2").GetComponent<Image>();

        keiPositionIntel[2][0] = kei_EnemyPos.transform.Find("keiEnemyA3").GetComponent<Image>();
        keiPositionIntel[2][1] = kei_EnemyPos.transform.Find("keiEnemyB3").GetComponent<Image>();
        keiPositionIntel[2][2] = kei_EnemyPos.transform.Find("keiEnemyC3").GetComponent<Image>();

        keiCheckElemEnemy(keiPositionIntel, kei_SmartAuto);

    }

    public void keiCheckElemEnemy(Image[][] kei_PositionIntel, keiSmartAutoController kei_SmartAuto)
    {
        keiEnemyCounted = 0;

        for (int keiCol = 0; keiCol < 3; keiCol++)
        {
            for (int keiRow = 0; keiRow < 3; keiRow++)
            {
                if (kei_PositionIntel[keiCol][keiRow].color != Color.white)
                {
                    // Check and input intel based on enemy color and turns
                    if (kei_PositionIntel[keiCol][keiRow].color == Color.red)
                    {
                        kei_SmartAuto.keiEnemyIntel[keiCol][keiRow][0] = 1;
                        kei_SmartAuto.keiEnemyIntel[keiCol][keiRow][1] = int.Parse(kei_PositionIntel[keiCol][keiRow].gameObject.transform.GetChild(0).GetComponent<Text>().text);
                        keiIsEnemyStillAlive = true;
                        keiEnemyCounted++;

                    }
                    else if (kei_PositionIntel[keiCol][keiRow].color == Color.cyan)
                    {
                        kei_SmartAuto.keiEnemyIntel[keiCol][keiRow][0] = 2;
                        kei_SmartAuto.keiEnemyIntel[keiCol][keiRow][1] = int.Parse(kei_PositionIntel[keiCol][keiRow].gameObject.transform.GetChild(0).GetComponent<Text>().text);
                        keiIsEnemyStillAlive = true;
                        keiEnemyCounted++;

                    }
                    else if (kei_PositionIntel[keiCol][keiRow].color == Color.green)
                    {
                        kei_SmartAuto.keiEnemyIntel[keiCol][keiRow][0] = 3;
                        kei_SmartAuto.keiEnemyIntel[keiCol][keiRow][1] = int.Parse(kei_PositionIntel[keiCol][keiRow].gameObject.transform.GetChild(0).GetComponent<Text>().text);
                        keiIsEnemyStillAlive = true;
                        keiEnemyCounted++;

                    }
                    else if (kei_PositionIntel[keiCol][keiRow].color == Color.yellow)
                    {
                        kei_SmartAuto.keiEnemyIntel[keiCol][keiRow][0] = 4;
                        kei_SmartAuto.keiEnemyIntel[keiCol][keiRow][1] = int.Parse(kei_PositionIntel[keiCol][keiRow].gameObject.transform.GetChild(0).GetComponent<Text>().text);
                        keiIsEnemyStillAlive = true;
                        keiEnemyCounted++;

                    }
                    else if (kei_PositionIntel[keiCol][keiRow].color == Color.magenta)
                    {
                        kei_SmartAuto.keiEnemyIntel[keiCol][keiRow][0] = 5;
                        kei_SmartAuto.keiEnemyIntel[keiCol][keiRow][1] = int.Parse(kei_PositionIntel[keiCol][keiRow].gameObject.transform.GetChild(0).GetComponent<Text>().text);
                        keiIsEnemyStillAlive = true;
                        keiEnemyCounted++;

                    }

                }
                else
                {
                    kei_SmartAuto.keiEnemyIntel[keiCol][keiRow][0] = 0;
                    kei_SmartAuto.keiEnemyIntel[keiCol][keiRow][1] = int.Parse(kei_PositionIntel[keiCol][keiRow].gameObject.transform.GetChild(0).GetComponent<Text>().text);

                }

                //Debug.Log("Col: " + keiCol +
                //    " | Row: " + keiRow +
                //    " | EnemyElem: " + kei_SmartAuto.keiEnemyIntel[keiCol][keiRow][0] +
                //    " | EnemyTurn: " + kei_SmartAuto.keiEnemyIntel[keiCol][keiRow][1]);

            }

        }

        /// <Deprecated>
        /// for (int i = 0; i < kei_PositionIntel.Length; i++)
        ///{
        ///    if (kei_PositionIntel[i].color != Color.white)
        ///    {
        ///        // Check and input intel based on color
        ///       if (kei_PositionIntel[i].color == Color.red)
        ///        {
        ///            kei_SmartAuto.keiEnemyElemIntel[i] = 1;
        ///            kei_SmartAuto.keiEnemyCountDownIntel[i] = keiCheckTypeEnemy(kei_PositionIntel[i], kei_SmartAuto);
        ///            keiIsEnemyStillAlive = true;
        ///            keiEnemyCounted++;
        ///            continue;
        ///            
        ///        }
        ///        else if (kei_PositionIntel[i].color == Color.cyan)
        ///        {
        ///            kei_SmartAuto.keiEnemyElemIntel[i] = 2;
        ///            kei_SmartAuto.keiEnemyCountDownIntel[i] = keiCheckTypeEnemy(kei_PositionIntel[i], kei_SmartAuto);
        ///            keiIsEnemyStillAlive = true;
        ///            keiEnemyCounted++;
        ///            continue;
        ///
        ///        }
        ///        else if (kei_PositionIntel[i].color == Color.green)
        ///        {
        ///            kei_SmartAuto.keiEnemyElemIntel[i] = 3;
        ///            kei_SmartAuto.keiEnemyCountDownIntel[i] = keiCheckTypeEnemy(kei_PositionIntel[i], kei_SmartAuto);
        ///            keiIsEnemyStillAlive = true;
        ///            keiEnemyCounted++;
        ///            continue;
        ///
        ///        }
        ///        else if (kei_PositionIntel[i].color == Color.yellow)
        ///        {
        ///            kei_SmartAuto.keiEnemyElemIntel[i] = 4;
        ///            kei_SmartAuto.keiEnemyCountDownIntel[i] = keiCheckTypeEnemy(kei_PositionIntel[i], kei_SmartAuto);
        ///            keiIsEnemyStillAlive = true;
        ///            keiEnemyCounted++;
        ///            continue;
        ///
        ///        }
        ///        else if (kei_PositionIntel[i].color == Color.magenta)
        ///        {
        ///            kei_SmartAuto.keiEnemyElemIntel[i] = 5;
        ///            kei_SmartAuto.keiEnemyCountDownIntel[i] = keiCheckTypeEnemy(kei_PositionIntel[i], kei_SmartAuto);
        ///            keiIsEnemyStillAlive = true;
        ///            keiEnemyCounted++;
        ///            continue;
        ///
        ///        }
        ///        else
        ///        {
        ///            kei_SmartAuto.keiEnemyElemIntel[i] = 6;
        ///            kei_SmartAuto.keiEnemyCountDownIntel[i] = keiCheckTypeEnemy(kei_PositionIntel[i], kei_SmartAuto);
        ///            keiIsEnemyStillAlive = true;
        ///            keiEnemyCounted++;
        ///            continue;
        ///
        ///        }
        ///
        ///    }
        ///    else
        ///    {
        ///        kei_SmartAuto.keiEnemyElemIntel[i] = 0;
        ///        kei_SmartAuto.keiEnemyCountDownIntel[i] = 0;
        ///        continue;
        ///
        ///    }
        ///
        ///}
        /// </Deprecated>

        //kei_SmartAuto.keiIntelChecker();
        kei_SmartAuto.keiIsFinished = true;
        keiScanLogging(keiInitScript, kei_SmartAuto, keiEnemyCounted);
        kei_SmartAuto.keiChangingState();

    }

    public int keiCheckTypeEnemy (Image kei_PositionIntelIndv, keiSmartAutoController kei_SmartAuto)
    {
        return int.Parse(kei_PositionIntelIndv.gameObject.transform.GetChild(0).GetComponent<Text>().text);

    }

    public void keiScanLogging(keiInitializatorGameObject kei_Init, keiSmartAutoController kei_SmartAuto, int kei_EnemyCounted)
    {
        kei_Init.keiLblLogEnemyCount.text = "Enemy Counted: " + kei_EnemyCounted.ToString();

    }

}
/// <summary>
/// keiScanState V 1.1
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
    private Image[] keiPositionIntel = new Image[9];

    // Region: Logging
    private int keiEnemyCounted;

    // End Region: Init -----------------------------------------------

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
        keiPositionIntel[0] = kei_EnemyPos.transform.Find("keiEnemyA1").GetComponent<Image>();
        keiPositionIntel[1] = kei_EnemyPos.transform.Find("keiEnemyA2").GetComponent<Image>();
        keiPositionIntel[2] = kei_EnemyPos.transform.Find("keiEnemyA3").GetComponent<Image>();

        keiPositionIntel[3] = kei_EnemyPos.transform.Find("keiEnemyB1").GetComponent<Image>();
        keiPositionIntel[4] = kei_EnemyPos.transform.Find("keiEnemyB2").GetComponent<Image>();
        keiPositionIntel[5] = kei_EnemyPos.transform.Find("keiEnemyB3").GetComponent<Image>();

        keiPositionIntel[6] = kei_EnemyPos.transform.Find("keiEnemyC1").GetComponent<Image>();
        keiPositionIntel[7] = kei_EnemyPos.transform.Find("keiEnemyC2").GetComponent<Image>();
        keiPositionIntel[8] = kei_EnemyPos.transform.Find("keiEnemyC3").GetComponent<Image>();

        keiCheckElemEnemy(keiPositionIntel, kei_SmartAuto);

    }

    public void keiCheckElemEnemy(Image[] kei_PositionIntel, keiSmartAutoController kei_SmartAuto)
    {
        keiEnemyCounted = 0;

        for (int i = 0; i < kei_PositionIntel.Length; i++)
        {
            if (kei_PositionIntel[i].color != Color.white)
            {
                // Check and input intel based on color
                if (kei_PositionIntel[i].color == Color.red)
                {
                    kei_SmartAuto.keiEnemyElemIntel[i] = 1;
                    kei_SmartAuto.keiEnemyCountDownIntel[i] = keiCheckTypeEnemy(kei_PositionIntel[i], kei_SmartAuto);
                    keiIsEnemyStillAlive = true;
                    keiEnemyCounted++;
                    continue;

                }
                else if (kei_PositionIntel[i].color == Color.cyan)
                {
                    kei_SmartAuto.keiEnemyElemIntel[i] = 2;
                    kei_SmartAuto.keiEnemyCountDownIntel[i] = keiCheckTypeEnemy(kei_PositionIntel[i], kei_SmartAuto);
                    keiIsEnemyStillAlive = true;
                    keiEnemyCounted++;
                    continue;

                }
                else if (kei_PositionIntel[i].color == Color.green)
                {
                    kei_SmartAuto.keiEnemyElemIntel[i] = 3;
                    kei_SmartAuto.keiEnemyCountDownIntel[i] = keiCheckTypeEnemy(kei_PositionIntel[i], kei_SmartAuto);
                    keiIsEnemyStillAlive = true;
                    keiEnemyCounted++;
                    continue;

                }
                else if (kei_PositionIntel[i].color == Color.yellow)
                {
                    kei_SmartAuto.keiEnemyElemIntel[i] = 4;
                    kei_SmartAuto.keiEnemyCountDownIntel[i] = keiCheckTypeEnemy(kei_PositionIntel[i], kei_SmartAuto);
                    keiIsEnemyStillAlive = true;
                    keiEnemyCounted++;
                    continue;

                }
                else if (kei_PositionIntel[i].color == Color.magenta)
                {
                    kei_SmartAuto.keiEnemyElemIntel[i] = 5;
                    kei_SmartAuto.keiEnemyCountDownIntel[i] = keiCheckTypeEnemy(kei_PositionIntel[i], kei_SmartAuto);
                    keiIsEnemyStillAlive = true;
                    keiEnemyCounted++;
                    continue;

                }
                else
                {
                    kei_SmartAuto.keiEnemyElemIntel[i] = 6;
                    kei_SmartAuto.keiEnemyCountDownIntel[i] = keiCheckTypeEnemy(kei_PositionIntel[i], kei_SmartAuto);
                    keiIsEnemyStillAlive = true;
                    keiEnemyCounted++;
                    continue;

                }

            }
            else
            {
                kei_SmartAuto.keiEnemyElemIntel[i] = 0;
                kei_SmartAuto.keiEnemyCountDownIntel[i] = 0;
                continue;

            }

        }

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
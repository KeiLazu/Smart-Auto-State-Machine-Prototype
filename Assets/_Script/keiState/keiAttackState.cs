/// <summary>
/// keiAttackState V1.0
/// Kei Lazu
/// 
/// Desc:
/// Attacking the enemy based from Prioritize state
/// no output will be given in this state
/// 
/// - Fourth State
/// 
/// </summary>


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using keiStateControlStuff;
using UnityEngine.UI;

public class keiAttackState : keiState<keiSmartAutoController> {

    public static keiAttackState keiInstance;

    public keiAttackState()
    {
        if (keiInstance != null)
        {
            return;

        }

        keiInstance = this;

    }

    public static keiAttackState kei_getsetInstance
    {
        get
        {
            if (keiInstance == null)
            {
                new keiAttackState();

            }

            return keiInstance;

        }
        
    }

    // Start Region: Init ---------------------------------------------

    // Region: Pathfinder
    keiInitializatorGameObject keiInitScript;

    // Region: Enemy Position
    Image[] keiEnemyPosition = new Image[9];
    Text[] keiEnemyCooldown = new Text[9];

    // Region: Translator
    int keiTranslatePos;

    // End Region: Init -----------------------------------------------

    public void keiInitGameObject()
    {
        keiEnemyPosition[0] = keiInitScript.keiEnemyA1;
        keiEnemyPosition[1] = keiInitScript.keiEnemyA2;
        keiEnemyPosition[2] = keiInitScript.keiEnemyA3;

        keiEnemyPosition[3] = keiInitScript.keiEnemyB1;
        keiEnemyPosition[4] = keiInitScript.keiEnemyB2;
        keiEnemyPosition[5] = keiInitScript.keiEnemyB3;

        keiEnemyPosition[6] = keiInitScript.keiEnemyC1;
        keiEnemyPosition[7] = keiInitScript.keiEnemyC2;
        keiEnemyPosition[8] = keiInitScript.keiEnemyC3;

        for (int i = 0; i < keiEnemyPosition.Length; i++)
        {
            keiEnemyCooldown[i] = keiEnemyPosition[i].transform.GetChild(0).GetComponent<Text>();

        }

    }

    public override void keiEnterState(keiSmartAutoController kei_Owner)
    {
        kei_Owner.keiIsFinished = false;

        keiInitScript = GameObject.Find("keiInitScript").GetComponent<keiInitializatorGameObject>();

        keiInitScript.keiLblLogState.GetComponent<Text>().text = "State: Attacking";

        Debug.Log("Decision Attack: " + kei_Owner.keiDecisionAttack);

        keiTranslateAttack(kei_Owner.keiDecisionAttack, kei_Owner.keiDecisionAttackRow, kei_Owner.keiDecisionAttackCol, kei_Owner);

    }

    public override void keiExitState(keiSmartAutoController kei_Owner)
    {
        kei_Owner.keiIsFinished = false;

        kei_Owner.keiDecisionAttack = 0;

    }

    public override void keiUpdateState(keiSmartAutoController kei_Owner)
    {
        if (kei_Owner.keiIsFinished)
        {
            kei_Owner.keiStateMachine.keiChangeState(keiScanState.kei_getsetInstance);

        }
        
    }

    public void keiTranslateAttack(int kei_DecisionAttack, int kei_DecisionRow, int kei_DecisionCol, keiSmartAutoController kei_SmartAuto)
    {
        keiInitGameObject();

        keiTranslatePos = 0;

        //Debug.Log("Row: " + kei_DecisionRow + " || Col: " + kei_DecisionCol);

        switch (kei_DecisionRow)
        {
            case 0:
                keiTranslatePos += 0;
                break;

            case 1:
                keiTranslatePos += 3;
                break;

            case 2:
                keiTranslatePos += 6;
                break;

            default:
                goto case 0;
        }

        keiTranslatePos += kei_DecisionCol;

        switch (kei_DecisionAttack)
        {
            case 0: // Single
                keiEnemyPosition[keiTranslatePos].color = Color.white;
                keiEnemyCooldown[keiTranslatePos].text = "0";
                kei_SmartAuto.keiEnemyElemIntel[keiTranslatePos] = 0;
                break;

            case 1: // Row
                switch (kei_DecisionRow)
                {
                    case 0:
                        keiEnemyPosition[0].color = Color.white;
                        keiEnemyPosition[1].color = Color.white;
                        keiEnemyPosition[2].color = Color.white;

                        keiEnemyCooldown[0].text = "0";
                        keiEnemyCooldown[1].text = "0";
                        keiEnemyCooldown[2].text = "0";

                        kei_SmartAuto.keiEnemyElemIntel[0] = 0;
                        kei_SmartAuto.keiEnemyElemIntel[1] = 0;
                        kei_SmartAuto.keiEnemyElemIntel[2] = 0;

                        break;

                    case 1:
                        keiEnemyPosition[3].color = Color.white;
                        keiEnemyPosition[4].color = Color.white;
                        keiEnemyPosition[5].color = Color.white;

                        keiEnemyCooldown[3].text = "0";
                        keiEnemyCooldown[4].text = "0";
                        keiEnemyCooldown[5].text = "0";

                        kei_SmartAuto.keiEnemyElemIntel[3] = 0;
                        kei_SmartAuto.keiEnemyElemIntel[4] = 0;
                        kei_SmartAuto.keiEnemyElemIntel[5] = 0;
                        break;

                    case 2:
                        keiEnemyPosition[6].color = Color.white;
                        keiEnemyPosition[7].color = Color.white;
                        keiEnemyPosition[8].color = Color.white;

                        keiEnemyCooldown[6].text = "0";
                        keiEnemyCooldown[7].text = "0";
                        keiEnemyCooldown[8].text = "0";

                        kei_SmartAuto.keiEnemyElemIntel[6] = 0;
                        kei_SmartAuto.keiEnemyElemIntel[7] = 0;
                        kei_SmartAuto.keiEnemyElemIntel[8] = 0;
                        break;
                }

                break;

            case 2: // Col
                switch (kei_DecisionCol)
                {
                    case 0:
                        keiEnemyPosition[0].color = Color.white;
                        keiEnemyPosition[3].color = Color.white;
                        keiEnemyPosition[6].color = Color.white;

                        keiEnemyCooldown[0].text = "0";
                        keiEnemyCooldown[3].text = "0";
                        keiEnemyCooldown[6].text = "0";

                        kei_SmartAuto.keiEnemyElemIntel[0] = 0;
                        kei_SmartAuto.keiEnemyElemIntel[3] = 0;
                        kei_SmartAuto.keiEnemyElemIntel[6] = 0;
                        break;

                    case 1:
                        keiEnemyPosition[1].color = Color.white;
                        keiEnemyPosition[4].color = Color.white;
                        keiEnemyPosition[7].color = Color.white;

                        keiEnemyCooldown[1].text = "0";
                        keiEnemyCooldown[4].text = "0";
                        keiEnemyCooldown[7].text = "0";

                        kei_SmartAuto.keiEnemyElemIntel[1] = 0;
                        kei_SmartAuto.keiEnemyElemIntel[4] = 0;
                        kei_SmartAuto.keiEnemyElemIntel[7] = 0;
                        break;

                    case 2:
                        keiEnemyPosition[2].color = Color.white;
                        keiEnemyPosition[5].color = Color.white;
                        keiEnemyPosition[8].color = Color.white;

                        keiEnemyCooldown[2].text = "0";
                        keiEnemyCooldown[5].text = "0";
                        keiEnemyCooldown[8].text = "0";

                        kei_SmartAuto.keiEnemyElemIntel[2] = 0;
                        kei_SmartAuto.keiEnemyElemIntel[5] = 0;
                        kei_SmartAuto.keiEnemyElemIntel[8] = 0;
                        break;
                }

                break;

            case 3: // X
                for (int i = 0; i < 9; i+=2)
                {
                    keiEnemyPosition[i].color = Color.white;
                    keiEnemyCooldown[i].text = "0";
                    kei_SmartAuto.keiEnemyElemIntel[i] = 0;
                    Debug.Log(i);

                }
                break;

            case 4: // AoE
                for (int i = 0; i < 9; i++)
                {
                    keiEnemyPosition[i].color = Color.white;
                    keiEnemyCooldown[i].text = "0";
                    kei_SmartAuto.keiEnemyElemIntel[i] = 0;

                }
                break;

            default: // not attacking
                break;
        }

        keiFinishAttacking(kei_SmartAuto);

    }

    public void keiFinishAttacking(keiSmartAutoController kei_SmartAuto)
    {
        keiEnemyTurn(kei_SmartAuto);

    }

    public void keiEnemyTurn(keiSmartAutoController kei_SmartAuto)
    {
        for (int i = 0; i < kei_SmartAuto.keiEnemyElemIntel.Length; i++)
        {
            if (kei_SmartAuto.keiEnemyElemIntel[i] > 0)
            {
                int keiReadCountdown = int.Parse (keiEnemyPosition[i].transform.GetChild(0).GetComponent<Text>().text);
                keiReadCountdown--;
                keiEnemyPosition[i].transform.GetChild(0).GetComponent<Text>().text = keiReadCountdown.ToString();

            }

        }

        kei_SmartAuto.keiIsFinished = true;
        kei_SmartAuto.keiChangingState();

    }

}

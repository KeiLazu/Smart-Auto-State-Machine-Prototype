/// <summary>
/// keiWaveController V 1.0
/// Kei Lazu
/// 
/// Desc:
/// Controlling Wave for enemy after the enemy wiped out
/// 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class keiWaveController : MonoBehaviour {

    int keiCurrentWave;

    keiInitializatorGameObject keiInit;
    keiSmartAutoController keiSmartAuto;

    Random keiRandomizer = new Random();

    Text keiWaveDisplay;

    Image[] keiEnemyPosition = new Image[9];

    private void Awake()
    {
        keiCurrentWave = 1;

        keiInit = GameObject.Find("keiInitScript").GetComponent<keiInitializatorGameObject>();
        keiSmartAuto = GameObject.FindGameObjectWithTag("Player").GetComponent<keiSmartAutoController>();

        //keiWaveDisplay.text = "Wave: " + keiCurrentWave;

    }

    public void keiPopulatingPosition(GameObject kei_EnemyPos, keiSmartAutoController kei_SmartAuto)
    {
        keiEnemyPosition[0] = kei_EnemyPos.transform.Find("keiEnemyA1").GetComponent<Image>();
        keiEnemyPosition[1] = kei_EnemyPos.transform.Find("keiEnemyA2").GetComponent<Image>();
        keiEnemyPosition[2] = kei_EnemyPos.transform.Find("keiEnemyA3").GetComponent<Image>();

        keiEnemyPosition[3] = kei_EnemyPos.transform.Find("keiEnemyB1").GetComponent<Image>();
        keiEnemyPosition[4] = kei_EnemyPos.transform.Find("keiEnemyB2").GetComponent<Image>();
        keiEnemyPosition[5] = kei_EnemyPos.transform.Find("keiEnemyB3").GetComponent<Image>();

        keiEnemyPosition[6] = kei_EnemyPos.transform.Find("keiEnemyC1").GetComponent<Image>();
        keiEnemyPosition[7] = kei_EnemyPos.transform.Find("keiEnemyC2").GetComponent<Image>();
        keiEnemyPosition[8] = kei_EnemyPos.transform.Find("keiEnemyC3").GetComponent<Image>();

    }

    public void keiNewWave()
    {
        keiPopulatingPosition(keiInit.keiEnemyPos, keiSmartAuto);

        keiCurrentWave++;
        keiInit.keiLblLogWave.text = "Wave: " + keiCurrentWave.ToString();

        keiTrulyRandomEnemyGenerator();

        keiSmartAuto.keiControlStateMachine();

    }

    public void keiTrulyRandomEnemyGenerator()
    {
        int keiRandomHolder;

        for (int i = 0; i < keiEnemyPosition.Length; i++)
        {
            keiRandomHolder = keiRandomizer.Next(0, 6);

            keiEnemyGenerator(i, keiRandomHolder);

        }

    }

    public void keiEnemyGenerator(int kei_I, int kei_RandomHolder)
    {
        switch (kei_RandomHolder)
        {
            case 1:
                keiEnemyPosition[kei_I].color = Color.red;
                keiEnemyTypeGenerator(kei_I, keiRandomizer.Next(3, 6));
                break;

            case 2:
                keiEnemyPosition[kei_I].color = Color.cyan;
                keiEnemyTypeGenerator(kei_I, keiRandomizer.Next(3, 6));
                break;

            case 3:
                keiEnemyPosition[kei_I].color = Color.green;
                keiEnemyTypeGenerator(kei_I, keiRandomizer.Next(3, 6));
                break;

            case 4:
                keiEnemyPosition[kei_I].color = Color.yellow;
                keiEnemyTypeGenerator(kei_I, keiRandomizer.Next(3, 6));
                break;

            case 5:
                keiEnemyPosition[kei_I].color = Color.magenta;
                keiEnemyTypeGenerator(kei_I, keiRandomizer.Next(3, 6));
                break;

            case 6:
                keiEnemyPosition[kei_I].color = Color.grey;
                keiEnemyTypeGenerator(kei_I, keiRandomizer.Next(3, 6));
                break;

            default:
                keiEnemyPosition[kei_I].color = Color.white;
                break;
        }

    }

    public void keiEnemyTypeGenerator(int kei_I, int kei_RandomType)
    {
        switch (kei_RandomType)
        {
            case 3:
                keiEnemyPosition[kei_I].gameObject.transform.GetChild(0).GetComponent<Text>().text = "3";
                break;

            case 4:
                keiEnemyPosition[kei_I].gameObject.transform.GetChild(0).GetComponent<Text>().text = "4";
                break;

            case 5:
                keiEnemyPosition[kei_I].gameObject.transform.GetChild(0).GetComponent<Text>().text = "5";
                break;

            default:
                keiEnemyPosition[kei_I].gameObject.transform.GetChild(0).GetComponent<Text>().text = "6";
                break;
        }

    }

}

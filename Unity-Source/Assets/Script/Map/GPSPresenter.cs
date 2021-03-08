﻿using UnityEngine;
using System.Collections;

public class GPSPresenter : MonoBehaviour
{

    void Start()
    {
        // 非同期処理
        StartCoroutine(GPSTest());
        Debug.Log("Start()は実行済");     
    }

    IEnumerator GPSTest()
    {
        Debug.Log("GPSTest()は実行済");     
        // 最初に、ユーザーがロケーションサービスを有効にしているかを確認する。無効の場合は終了する
        if (!Input.location.isEnabledByUser)
            print("ロケーションサービスが無効");            
            yield break;

        // 位置を取得する前にロケーションサービスを開始する
        Input.location.Start();
        Debug.Log("ロケーションサービス開始");

        // 初期化が終了するまで待つ
        int maxWait = 20; // タイムアウトは20秒
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1); // 1秒待つ
            maxWait--;
        }

        // サービスの開始がタイムアウトしたら（20秒以内に起動しなかったら）、終了
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // サービスの開始に失敗したら終了
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // アクセスの許可と位置情報の取得に成功
            print("Location: " + Input.location.lastData.latitude + " "
                               + Input.location.lastData.longitude + " "
                               + Input.location.lastData.altitude + " "
                               + Input.location.lastData.horizontalAccuracy + " "
                               + Input.location.lastData.timestamp);
        }

        // 位置の更新を継続的に取得する必要がない場合はサービスを停止する
        Input.location.Stop();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class LocationPresenter : MonoBehaviour
{
    // GameObjectをメソッド外で定義することで、InspectorにてObjectを紐づけできる。
    private const string STATIC_MAP_URL = "https://maps.googleapis.com/maps/api/staticmap?key=AIzaSyCN773NQ0NQsV4OjQjSd-TuFv-5Z9IEbWU&zoom=15&size=640x640&scale=2&maptype=terrain&style=element:labels|visibility:off";

    private int frame = 0;

    void Start()
    {
        // 非同期処理
        StartCoroutine(getStaticMap());
    }

    // unity は毎秒30~60フレームで画面を構成しており、update()内に書かれた処理は1フレーム毎に実行される
    // 毎秒60回実行されるとカウントする場合、if (frame >= 300){}は、5秒に一度、実行される計算
    void Update()
    {
        // 5秒に一度の実行
        if (frame >= 300)
        {
            StartCoroutine(getStaticMap());
            // getStaticMap()実行後にframeを初期化
            frame = 0;
        }
        frame++;
    }

    IEnumerator getStaticMap()
    {
        var query = "";

        // centerで取得するミニマップの中央座標を設定
            // UnityWebRequest.UnEscapeURL = 特殊文字を人間が扱う文字列に変換するメソッド
            // string.Format("{}" hogehoge )...{}番目の値を"{}"の形式で表示する ※{},引数は複数指定可
            // 例:string.Format("{0}です" hogehoge, hogeeeeee ) = hogehogeです
            query += "&center" + UnityWebRequest.UnEscapeURL(string.Format("{0},{1}", Input.location.lastData.latitude, Input.location.lastData.longitude));
        // markersで渡した座標(=現在位置)にマーカーを立てる
            query += "&markers=" + UnityWebRequest.UnEscapeURL(string.Format("{0},{1}", Input.location.lastData.latitude, Input.location.lastData.longitude));

        //リクエストの定義
        // UnityWebRequestTexture.GetTexture(hogehoge)...HTTP GET経由でイメージをダウンロードするときに
        // hogehogeを基にTextureを作成するメソッド
            var req = UnityWebRequestTexture.GetTexture(STATIC_MAP_URL + query);
        // リクエスト実行
        // yield return XX; ... XXの間、処理を停止
            yield return req.SendWebRequest();

            if (req.error == null)
            {
                // 既に表示しているマップを更新
                // レンダラー...オブジェクトを画面に描画するためのもの
                // マテリアルのテクスチャ...オブジェクトに質感や見た目を変えるためのもの
                // material.mainTexture... LocationPresenter.cs がアタッチされているマテリアルそのものを指す、と予想
                    Destroy(GetComponent<Renderer>().material.mainTexture);
                // DownloadHandlerTexture()...画像データのダウンロードに関するサブクラス
                    GetComponent<Renderer>().material.mainTexture = ((DownloadHandlerTexture)req.downloadHandler).texture;
            }
    }
}

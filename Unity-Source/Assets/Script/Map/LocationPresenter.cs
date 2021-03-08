using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LocationPresenter : MonoBehaviour
{
        // imageオブジェクトの取得
        GameObject MapImage;
        // imageコンポーネントの取得
        Image MapImage_Component;
    
    // GameObjectをメソッド外で定義することで、InspectorにてObjectを紐づけできる。
    private const string STATIC_MAP_URL = "https://maps.googleapis.com/maps/api/staticmap?key=AIzaSyCN773NQ0NQsV4OjQjSd-TuFv-5Z9IEbWU&zoom=10&size=240x240&scale=2&maptype=terrain&style=element:labels|visibility:off";

    private int frame = 0;

    void Start()
    {
        // imageオブジェクトの取得
            MapImage = GameObject.Find("MapImage");
        // imageコンポーネントの取得
            MapImage_Component = MapImage.GetComponent<Image>();
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
        // Start service before querying location
        var query = "";

        // centerで取得するミニマップの中央座標を設定
            // UnityWebRequest.UnEscapeURL = 特殊文字を人間が扱う文字列に変換するメソッド
            // string.Format("{}" hogehoge )...{}番目の値を"{}"の形式で表示する ※{},引数は複数指定可
            // 例:string.Format("{0}です" hogehoge, hogeeeeee ) = hogehogeです
            query += "&center=" + UnityWebRequest.UnEscapeURL(string.Format("{0},{1}", Input.location.lastData.latitude, Input.location.lastData.longitude));
        // markersで渡した座標(=現在位置)にマーカーを立てる
            query += "&markers=" + UnityWebRequest.UnEscapeURL(string.Format("{0},{1}", Input.location.lastData.latitude, Input.location.lastData.longitude));
            Debug.Log(query);
        
        //リクエストの定義
        // UnityWebRequestTexture.GetTexture(hogehoge)...HTTP GET経由でイメージをダウンロードするときに
        // hogehogeを基にTextureを作成するメソッド
            var req = UnityWebRequestTexture.GetTexture(STATIC_MAP_URL + query);
            Debug.Log(STATIC_MAP_URL + query);

        // リクエスト実行
        // yield return XX; ... XXの間、処理を停止
            yield return req.SendWebRequest();

            if (req.error == null)
            {
                Debug.Log("マップをspriteに書き出します");              
                // 既に表示しているマップを更新
                // レンダラー...オブジェクトを画面に描画するためのもの
                // マテリアルのテクスチャ...オブジェクトに質感や見た目を変えるためのもの
                // material.mainTexture... LocationPresenter.cs がアタッチされているマテリアルそのものを指す、と予想
                    // Destroy(GetComponent<Renderer>().material.mainTexture);
                // DownloadHandlerTexture()...画像データのダウンロードに関するサブクラス
                    // GetComponent<Renderer>().material.mainTexture = ((DownloadHandlerTexture)req.downloadHandler).texture;

                // スプライト（インスタンス）を明示的に開放
                    if (MapImage_Component.sprite != null)
                    {
                        Destroy(MapImage_Component.sprite);
                        yield return null;
                        MapImage_Component.sprite = null;
                        yield return null;
                        Debug.Log("初期化完了");                        
                    }
                
                // downloadHandlerをDownloadHandlerTextureにキャストしてテクスチャを取得
                    var texture = (req.downloadHandler as DownloadHandlerTexture).texture;
                    Debug.Log("テクスチャを取得: "  + texture);              
                // down
                    MapImage_Component.sprite = Sprite.Create(
                        texture: texture,
                        rect : new Rect(0, 0, 480, 480),
                        pivot : new Vector2(0.5f, 0.5f)
                    );

            }
    }
}

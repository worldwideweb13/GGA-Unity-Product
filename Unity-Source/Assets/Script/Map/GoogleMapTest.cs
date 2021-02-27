using System;
using UnityEngine;
using UnityEngine.UI;
 
public class GoogleMapTest : MonoBehaviour
{
    // GameObjectをメソッド外で定義することで、InspectorにてObjectを紐づけできる。
    public GameObject LatitudeText = null;
    public GameObject LongitudeText = null;
 
    // Start is called before the first frame update
    void Start()
    {
        // GPS機能の利用開始
        Input.location.Start();
    }
 
    // Update is called once per frame
    void Update()
    {
        // 紐づけたTextのオブジェクトを変数に格納
        Text latitude_component = LatitudeText.GetComponent<Text>();
        Text longitude_component = LongitudeText.GetComponent<Text>();
 
        // Textオブジェクトのtext部分を取得したGPS情報の緯度・経度で上書き
        latitude_component.text = Convert.ToString(Input.location.lastData.latitude);
        longitude_component.text = Convert.ToString(Input.location.lastData.longitude);
    }
}
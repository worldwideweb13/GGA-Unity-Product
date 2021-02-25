using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GoogleMapDrawer : MonoBehaviour
{ 
    public float lat = 35.6259034f;
    public float lng = 139.7268499f;
    public string key = null;
    public int zoom = 15;

    private string GoogleApiKey = "AIzaSyCN773NQ0NQsV4OjQjSd-TuFv-5Z9IEbWU";

    // Google Maps Embed API
    private string BaseUrl      = @"https://maps.googleapis.com/maps/api/staticmap?";

    // Start is called before the first frame update
    // void Start()
    // {
    //     Build();
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    // public void Build() {
    //     // 中心座標
    //     Url += "center=" + lat "," + lng;

    //     // ズームレベル
    //     Url += "&zoom" + zoom;

    //     // 地図画像のサイズ
    //     Url += "&size="
    // }

}

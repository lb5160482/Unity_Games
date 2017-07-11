using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;



public class AndroidPluginScript : MonoBehaviour {

    public GUIText scoreText;

    private AndroidJavaClass plugin;
    private int testNum = 0;
    private Thread initializeThread = null;

    // Use this for initialization
    void Start () {
        try
        {
            plugin = new AndroidJavaClass("com.dreamworldvision.unityplugin.PluginClass");
            plugin.CallStatic("startCountThread");
            //initializeThread = new Thread(() =>
            //{
            //    try
            //    {
            //        plugin.CallStatic("startCountThread");
            //    }
            //    catch (Exception e)
            //    {
            //        scoreText.text = e.ToString();
            //    }
            //}
            //);
            //initializeThread.Start();
        }
        catch (Exception e)
        {
            scoreText.text = e.ToString();
        }
        
    }

    void Initialize()
    {
        try
        {
            plugin.CallStatic("startCountThread");
        }
        catch (Exception e)
        {
            scoreText.text = e.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            scoreText.text = plugin.CallStatic<string>("getPluginText", testNum);
            testNum++;
        }
        catch (Exception e)
        {
            scoreText.text = e.ToString();
        }
    }
}

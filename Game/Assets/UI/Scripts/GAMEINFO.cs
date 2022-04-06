using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GAMEINFO : MonoBehaviour
{
    public Text FrameTime_TXT;

    // Update is called once per frame
    void Update()
    {
        FrameTime_TXT.text = "Time From Last Frame: " + Time.deltaTime.ToString();
    }
}

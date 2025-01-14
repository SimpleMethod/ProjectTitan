﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Text))]
[RequireComponent(typeof(AudioClip))]

public class Menu : MonoBehaviour
{
    private bool RunStatus = false;
    static public bool ReadyToGetID;
    public AudioClip audioClip;
    public Text textgameobject, signgameobject;
    public Transform OnlineButton;
    ReadJsonFileHelper ReadFileJ = new ReadJsonFileHelper();

    void Update()
    {
        Text text = textgameobject.GetComponent<Text>();
        Text sign = signgameobject.GetComponent<Text>();

        if (ReadyToGetID)
        {

#if UNITY_EDITOR
            SendData._uniqueid = "1";
#endif

            if (SendData._uniqueid != null)
            {
                sign.color = new Color32(30, 255, 0, 255);
                text.text = String.Format(ReadFileJ.ReadJsonFile(SendData._File, SendData._language, "Status"), ReadFileJ.ReadJsonFile(SendData._File, SendData._language, "StatusArray", 1, "text"));
                SendData._OnlineAccess = true;
            }
            else
            {
                UnityEngine.Debug.LogError("Błąd otwarcia pliku MENU");
                sign.color = new Color32(255, 242, 0, 255);
                text.text = String.Format(ReadFileJ.ReadJsonFile(SendData._File, SendData._language, "Status"), ReadFileJ.ReadJsonFile(SendData._File, SendData._language, "StatusArray", 2, "text"));
                SendData._OnlineAccess = false;
                OnlineButton.GetComponent<Button>().interactable = false;
            }
        }

        if (CrossPlatformInputManager.GetButtonDown(KeyMap._Submit) && RunStatus != true)
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            audio.volume = SendData._master_Volume;
            RunStatus = true;
        }
    }
}
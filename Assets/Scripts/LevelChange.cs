﻿using Doozy.Engine.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public UIView transitionView;
    public UIView label;
    public GameObject player;
    public SaveState save;
    public AudioManager AM;
    public string sceneToLoad;
    public float activationDistance;
    public string key;

    [Range(0f, 1f)] public float useVolume;
    public AudioClip useSound;

    void Start()
    {
        AM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        transitionView = GameObject.Find("View - Transition").GetComponent<UIView>();
        player = GameObject.FindGameObjectWithTag("Player");
        save = GameObject.FindGameObjectWithTag("SaveState").GetComponent<SaveState>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < activationDistance && player.GetComponent<Player>().isInputAvailable)
        {
            if (!key.Equals(""))
            {
                if (!save.flags[key])
                {
                    this.enabled = false;
                }
            }
            label.Show();
            if (Input.GetKeyDown(KeyCode.F))
            {
                label.Hide();
                StartCoroutine(LevelTransition());
            }
        }
        else
        {
            label.Hide();
        }
    }

    IEnumerator LevelTransition()
    {
        player.GetComponent<Player>().isInputAvailable = false;
        AM.PlaySound(useSound, useVolume);
        transitionView.Show();
        yield return new WaitForSeconds(useSound.length - 1f);
        SceneManager.LoadScene(sceneToLoad);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{

    public GameObject camera;
    public GameObject enemyManager;

    private AudioSource audio1;
    private AudioSource audio2;
    private Slider volumeSlider;
    private float baseaudio1;
    private float baseaudio2;
    // Start is called before the first frame update
    void Start()
    {
        audio1 = camera.GetComponent<AudioSource>();
        audio2 = enemyManager.GetComponent<AudioSource>();
        baseaudio1 = audio1.volume;
        baseaudio2 = audio2.volume;
        volumeSlider = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        audio1.volume = baseaudio1 * volumeSlider.value;
        audio2.volume= baseaudio2 * volumeSlider.value;
    }
}

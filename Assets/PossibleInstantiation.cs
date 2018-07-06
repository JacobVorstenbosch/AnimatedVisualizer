using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//If I can get this to work, I'll add it back again.
//Until then, it stays here.


public class PossibleInstantiation : MonoBehaviour
{
    public GameObject cubePrefab;
    GameObject[] bandedCubes = new GameObject[8];
    int bandSamples = 8;
    public float scale = 100.0f;

    public float scaleMultiplier;
    public float startScale;
    public float previousFrequencyValue = 0.0f;
    public float previousLocation = 0.0f;

    public bool useBuffer;

    public List<GameObject> visualizerCubes = new List<GameObject>();

    // Use this for initialization

    void Start()
    {

        for (int i = 0; i < bandSamples; i++)
        {
            GameObject instanceCubes = (GameObject)Instantiate(cubePrefab);
            instanceCubes.name = "Sample" + i;
            bandedCubes[i] = instanceCubes;
            visualizerCubes.Add(bandedCubes[i]);
            bandedCubes[i].transform.parent = transform;
            bandedCubes[i].transform.position = new Vector3((i * 20) - 70, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

        //if (useBuffer)
        //{
        //    transform.localScale = new Vector3(transform.localScale.x, (AudioVisualizer.bandBuffer[i] * scaleMultiplier) + startScale, transform.localScale.z);
        //    transform.localPosition = new Vector3(transform.localPosition.x, ((AudioVisualizer.bandBuffer[i] * scaleMultiplier) / 2), transform.localPosition.z);
        //}

        //if (!useBuffer)
        //{
        //    transform.localScale = new Vector3(transform.localScale.x, (AudioVisualizer.frequencyBands[i] * scaleMultiplier) + startScale, transform.localScale.z);
        //    transform.localPosition = new Vector3(transform.localPosition.x, ((AudioVisualizer.frequencyBands[i] * scaleMultiplier) / 2), transform.localPosition.z);

        //    transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(previousFrequencyValue, (AudioVisualizer.frequencyBands[i] * scaleMultiplier) + startScale, Time.deltaTime), transform.localScale.z);
        //    previousFrequencyValue = AudioVisualizer.frequencyBands[i] * scaleMultiplier + startScale;
        //    transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(previousLocation, ((AudioVisualizer.frequencyBands[i] * scaleMultiplier) / 2), Time.deltaTime), transform.localPosition.z);
        //    previousLocation = AudioVisualizer.frequencyBands[i] * scaleMultiplier / 2;
        //}
        for (int i = 0; i < bandSamples; i++)
        {
            if (useBuffer)
            {
                bandedCubes[i].transform.localScale = new Vector3(10, (AudioVisualizer.bandBuffer[i] * scaleMultiplier) + startScale, 10);
                bandedCubes[i].transform.localPosition = new Vector3(bandedCubes[i].transform.localPosition.x, ((AudioVisualizer.bandBuffer[i] * scaleMultiplier) / 2), bandedCubes[i].transform.localPosition.z);
            }

            if (!useBuffer)
            {
                //transform.localScale = new Vector3(transform.localScale.x, (AudioVisualizer.frequencyBands[i] * scaleMultiplier) + startScale, transform.localScale.z);
                //transform.localPosition = new Vector3(transform.localPosition.x, ((AudioVisualizer.frequencyBands[i] * scaleMultiplier) / 2), transform.localPosition.z);
                bandedCubes[i].transform.localScale = new Vector3(bandedCubes[i].transform.localScale.x, Mathf.Lerp(previousFrequencyValue, (AudioVisualizer.frequencyBands[i] * scaleMultiplier) + startScale, Time.deltaTime), bandedCubes[i].transform.localScale.z);
                previousFrequencyValue = AudioVisualizer.frequencyBands[i] * scaleMultiplier + startScale;
                bandedCubes[i].transform.localPosition = new Vector3(bandedCubes[i].transform.localPosition.x, Mathf.Lerp(previousLocation, ((AudioVisualizer.frequencyBands[i] * scaleMultiplier) / 2), Time.deltaTime), bandedCubes[i].transform.localPosition.z);
                previousLocation = AudioVisualizer.frequencyBands[i] * scaleMultiplier / 2;
            }
        }

    }
}

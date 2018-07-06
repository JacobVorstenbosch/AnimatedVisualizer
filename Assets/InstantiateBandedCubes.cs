using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBandedCubes : MonoBehaviour
{
    public GameObject cubePrefab;
    GameObject[] bandedCubes = new GameObject[16];

    public static int bandSamples = 16;

    public float scaleMultiplier;
    public float previousFrequencyValue = 0.0f;
    public float previousLocation = 0.0f;
    public bool useBuffer;

    float startScale = 1.0f;
    
    public List<GameObject> visualizerCubes = new List<GameObject>();

    public Material audioColours;
    Material[] materialStorage = new Material[bandSamples];
    //Material mat;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < bandSamples; i++)
        {
            GameObject instanceCubes = (GameObject)Instantiate(cubePrefab);
            instanceCubes.name = "Band" + i;
            bandedCubes[i] = instanceCubes;
            visualizerCubes.Add(bandedCubes[i]);
            bandedCubes[i].transform.parent = transform;
            bandedCubes[i].transform.position = new Vector3((i * 12) - 90, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < bandSamples; i++)
        {
            if (useBuffer)
            {
                bandedCubes[i].transform.localScale = new Vector3(10, (AudioVisualizer.bandBuffer[i] * scaleMultiplier) + startScale, 10);
                bandedCubes[i].transform.localPosition = new Vector3(bandedCubes[i].transform.localPosition.x, ((AudioVisualizer.bandBuffer[i] * scaleMultiplier) / 2), bandedCubes[i].transform.localPosition.z);

                bandedCubes[i].GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.HSVToRGB(1.0f - AudioVisualizer.audioBandBuffer[i], 1.0f, 1.0f), bandedCubes[i].GetComponent<MeshRenderer>().material.color, Time.deltaTime/2); 

                //bandedCubes[i].GetComponent<MeshRenderer>().material.SetColor("Albedo", color)

                //Color colour = Color.HSVToRGB(AudioVisualizer.audioBandBuffer[i], 1.0f, 1.0f);
                //materialStorage[i].SetColor("Albedo", colour);
                //bandedCubes[i].GetComponent<MeshRenderer>().material = materialStorage[i];
                //mat.SetColor("Albedo", colour);
                //bandedCubes[i].GetComponent<MeshRenderer>().material = mat;
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

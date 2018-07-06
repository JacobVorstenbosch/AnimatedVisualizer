using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tutorial Given Code
public class InstatiateCubes : MonoBehaviour
{
    static int spectrumSamples = 64;

    public GameObject cubePrefab;
    GameObject[] sampleCubes = new GameObject[spectrumSamples];
    public float scale = 100.0f;
    float previousFreq, previousLocation;

    public float radius = 5.0f;
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < spectrumSamples; i++)
        {

            float angle = i * Mathf.PI * 2 / spectrumSamples;
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            Instantiate(cubePrefab, pos, Quaternion.identity);
            //GameObject instanceSampleCubes = Instantiate(cubePrefab);
            //instanceSampleCubes.transform.position = transform.position;
            //instanceSampleCubes.transform.parent = transform;
            //instanceSampleCubes.name = "Sample" + i;
            //transform.eulerAngles = new Vector3(0, )
        }
        sampleCubes = GameObject.FindGameObjectsWithTag("Cube");
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < spectrumSamples; i++)
        {
            if (sampleCubes != null)
            {
                //sampleCubes[i].transform.localScale = new Vector3(1, (AudioVisualizer.samples[i] * scale) , 1);
                //sampleCubes[i].transform.localPosition = new Vector3(sampleCubes[i].transform.localPosition.x, ((AudioVisualizer.samples[i] * scale) / 2), sampleCubes[i].transform.localPosition.z);
                sampleCubes[i].transform.localScale = new Vector3(5, (Mathf.Lerp(previousFreq, AudioVisualizer.samples[i] * scale, Time.deltaTime)), 5);
                previousFreq = AudioVisualizer.samples[i] * scale;
                sampleCubes[i].transform.localPosition = new Vector3(sampleCubes[i].transform.localPosition.x, Mathf.Lerp(previousLocation, ((AudioVisualizer.samples[i] * scale) / 2), Time.deltaTime), sampleCubes[i].transform.localPosition.z);
                previousLocation = (AudioVisualizer.samples[i] * scale) / 2;

            }
        }
	}
}

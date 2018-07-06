using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioVisualizer : MonoBehaviour {

    AudioSource source;

    static public int bands = 16;

    static public float[] samples = new float[512];
    static public float[] frequencyBands = new float[bands];
    static public float[] bandBuffer = new float[bands];
    static float[] bufferDecrease = new float[bands];
    
    //creating a number between 0 and 1 for useable colour changes :D
    static public float[] audioBand = new float[bands];
    static public float[] audioBandBuffer = new float[bands];
    static float[] highestFreqBand = new float[bands];

    public float confirm;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        getSpectrumAudio();
        createFrequencyBands();
        createBandBuffer();
        createAudioBands();
	}

    void getSpectrumAudio()
    {
        source.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    void createAudioBands()
    {
        for (int i = 0; i < bands; i++)
        {
            if (frequencyBands[i] > highestFreqBand[i])
            {
                highestFreqBand[i] = frequencyBands[i];
            }
            audioBand[i] = frequencyBands[i] / highestFreqBand[i];
            audioBandBuffer[i] = bandBuffer[i] / highestFreqBand[i];
            confirm = audioBand[i];
        }
    }

    void createFrequencyBands()
    {
        // Bands of frequencies - putting groups of similar frequencies together to create a more even visualizer
        //0 = 2 bands of the array in samples (40hz per band)
        //1 = 4
        //2 = 8
        //3 = 16
        //4 = 32
        //5 = 64
        //6 = 128
        //7 = 256
        // total of all bands is 510 - error of 2 samples. make sure to do somethign about that

        //0 - 15 is the new bands.
        /* 0 - 1
         * 1 - 2
         * 2 - 4 
         * 3 - 8 
         * 4 - 16 
         * 5 - 32 
         * 6 - 64
         * 7 - 128
         * 8 - 256
         * 9 - 512
         * 10.. um.. yeah that aint't gunna work
         * 
         */
        // MATH TIME
        // 2 ^0 = 1
        // 2 ^1 2 / 2
        //2^2
        // if i % 2 =/= good (odd numbers)
        // divide by 2
        int count = 0;
        for (int i = 0; i < bands; i++)
        {
            int bandCount = (int)Mathf.Pow(2, i/2); // need to be double what would normally be returned as we start at 2, not 1
            float average = 0.0f; //need to average out the frequencies in each band

            if (i == 15)
                bandCount += 2;

            for (int j = 0; j < bandCount; j++)
            {
                average += samples[count] * (count + 1); // average is equal to the number in the array multiplied by the count
                count++;
            }

            average /= count;

            frequencyBands[i] = average * 10; // multiplied by 10 cuz numbers are tiny
        }
    }

    void createBandBuffer()
    {
        for (int i = 0; i < bands; i++)
        {
            if (frequencyBands[i] > bandBuffer[i])
            {
                bandBuffer[i] = frequencyBands[i];
                bufferDecrease[i] = 0.1f;
            }

            if (frequencyBands[i] < bandBuffer[i])
            {
                bandBuffer[i] -= bufferDecrease[i];
                bufferDecrease[i] *= 1.2f;

                if (bandBuffer[i] < 0)
                    bandBuffer[i] = 0;
            }
        }

    }
}

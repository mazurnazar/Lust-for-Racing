using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;
using UnityEngine.SceneManagement;
public class Manager : MonoBehaviour
{
    public static Manager Instance;
    private const int topNumbers = 5;
    public bool isPlaying = true;

    public float[] bestDistances ;
    public float[] BestTimes ;
    public int obstacleNumber;
    public float totalDistance;
    public delegate void StopStart();
    public  event StopStart stopStart;
    public bool Sound = true;
    public bool Vibration = true;
    // Start is called before the first frame update

    public void Awake()
    {
        bestDistances = new float[topNumbers] { 0, 0, 0, 0, 0 };
        obstacleNumber = 0;
        totalDistance = 0;
        if(Instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        //SaveInfo();
        LoadInfo();
    }
    public void SaveInfo()
    {
        SaveData data = new SaveData();
        for (int i = 0; i < topNumbers; i++)
        {
            data.bestDistances[i] = bestDistances[i];
        }
        data.totalDistance = totalDistance;
        data.obstacleNumber = obstacleNumber;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadInfo()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        Debug.Log(Application.persistentDataPath);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            for (int i = 0; i < topNumbers; i++)
            {
                bestDistances[i] = data.bestDistances[i];
            }
            totalDistance = data.totalDistance;
            obstacleNumber = data.obstacleNumber;
        }
    }
    public void CheckToSave(float value)
    {
        if (value > bestDistances.Min())
        {
            float minValue = bestDistances.Min();
            int minIndex = Array.IndexOf(bestDistances, minValue);
            bestDistances[minIndex] = value;
            Array.Sort(bestDistances);
        }
    }
    [System.Serializable]
    class SaveData
    {
        public float[] bestDistances = new float[topNumbers];
        public int obstacleNumber;
        public float totalDistance;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) SceneManager.LoadScene(0);
    }
    public void StopGame()
    {
        stopStart.Invoke();
    }
    public void GameOver()
    {
        isPlaying = false;
        CheckToSave(transform.position.z);
        obstacleNumber++;
        totalDistance += transform.position.z;
        SaveInfo();
        SceneManager.LoadScene(0);
        isPlaying = true;
    }
        
}

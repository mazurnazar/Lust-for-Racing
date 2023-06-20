using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Text top;
    [SerializeField] Text distance;
    [SerializeField] Text obstacles;
    [SerializeField] Scrollbar sound, vibration;
    private void Start()
    {
        sound.value = Manager.Instance.Sound ? 0 : 1;
        sound.GetComponent<Image>().color = Manager.Instance.Sound ? Color.green : Color.red;
        vibration.value = Manager.Instance.Vibration ? 0 : 1;
        vibration.GetComponent<Image>().color = Manager.Instance.Vibration ? Color.green : Color.red;
    }
    public void ChangeSound()
    {
        Manager.Instance.Sound = sound.value == 0 ? true : false;
        sound.GetComponent<Image>().color = sound.value == 0 ? Color.green : Color.red;
    }
    public void ChangeVibration()
    {
        Manager.Instance.Vibration = vibration.value == 0 ? true : false;
        vibration.GetComponent<Image>().color = vibration.value == 0 ? Color.green : Color.red;
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Garage()
    {
        SceneManager.LoadScene(2);
    }
    public void SetActive(GameObject window)
    {
        window.SetActive(true);
        if (window.name == "Statistics") ShowStatistics();
    }
    public void CloseWindow(GameObject window)
    {
        window.SetActive(false);
    }
    public void ShowStatistics()
    {
        int count = 1;
        top.text = "";
        for (int i = Manager.Instance.bestDistances.Length-1; i >= 0; i--)
        {
            top.text += count + ". " + Manager.Instance.bestDistances[i] + "\n";
            count++;
        }
        distance.text += Manager.Instance.totalDistance.ToString();
        obstacles.text += Manager.Instance.obstacleNumber.ToString();
    }
}

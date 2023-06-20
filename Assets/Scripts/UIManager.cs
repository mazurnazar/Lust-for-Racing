using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField] Image fuel;
    [SerializeField] PlayerMovement player;
    [SerializeField] Text speed;
    [SerializeField] Text distance;
    [SerializeField] GameObject pauseMenu;

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

    // Update is called once per frame
    void Update()
    {
        fuel.fillAmount = player.currentFuel/100f;
        speed.text = ((int)(player.currentSpeed*3.6)).ToString();
        distance.text = (player.transform.position.z / 100).ToString("0.00") + " km";
    }
    public void Pause()
    {
        
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Manager.Instance.StopGame();
    }
    public void Continue()
    {
        pauseMenu.SetActive(false);
        Manager.Instance.StopGame();
    }
    public void Restart()
    {
        SceneManager.LoadScene(3);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}

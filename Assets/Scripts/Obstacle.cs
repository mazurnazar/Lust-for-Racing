using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Obstacles
{
    Cone,
    Oil,
    Fence,
    Car,
    Fuel
}
public class Obstacle : MonoBehaviour
{
    public Obstacles obstacleType;
    public ObjectPool ObjectPool;
    public void CheckObstacle(PlayerMovement player)
    {
        switch(obstacleType)
        {
            case Obstacles.Cone:
                ConeAction(player);
                break;
            case Obstacles.Oil:
                OilAction(player);
                break;
            case Obstacles.Fence:
                GameOver();
                break;
            case Obstacles.Car:
                GameOver();
                break;
            case Obstacles.Fuel:
                FuelAction(player);
                break;

        }
    }    
    void ConeAction(PlayerMovement player)
    {
        player.Speed /= 2;
        Manager.Instance.obstacleNumber++;
    }

    void OilAction(PlayerMovement player)
    {
        if (!player.canSlide) return;
        player.canSlide = false;
        GetComponent<SphereCollider>().isTrigger = false;
        GetComponent<SphereCollider>().enabled = false;
        int r = Random.Range(0, 2);
        if (r == 0)
        {
            if (!player.CheckForBorders(Vector3.right))
            {
                player.Move("MoveOilRight");
            }
            else if (!player.CheckForBorders(-Vector3.right))
                player.Move("MoveOilLeft");
           // MoveLeft(player);
        }
        else
        {
            if (!player.CheckForBorders(-Vector3.right))
                player.Move("MoveOilLeft");
               // MoveLeft(player);
            else if (!player.CheckForBorders(Vector3.right))
                player.Move("MoveOilRight");
            //MoveRight(player);
        }
        Manager.Instance.obstacleNumber++;
    }
    /*
    public void MoveLeft(PlayerMovement player)
    {
       
        player.animator.SetBool("MoveOilLeft", true);
        player.animator.SetFloat("speed", player.currentSpeed / 20f + 0.5f);


    }
    public void MoveRight(PlayerMovement player)
    {
        player.animator.SetBool("MoveOilRight", true);
        player.animator.SetFloat("speed", player.currentSpeed / 20f + 0.5f);
    }*/
    void GameOver()
    {
        Manager.Instance.isPlaying = false;
        Manager.Instance.CheckToSave(transform.position.z);
        Manager.Instance.obstacleNumber++;
        Manager.Instance.totalDistance += transform.position.z;
        Manager.Instance.SaveInfo();
        SceneManager.LoadScene(0);
        Manager.Instance.isPlaying = true;

    }
    void FuelAction(PlayerMovement player)
    {
        player.Refuel();
        Destroy(gameObject);
    }

}

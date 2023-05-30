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
        if (!player.canMove) return;
        GetComponent<SphereCollider>().isTrigger = false;
        GetComponent<SphereCollider>().enabled = false;
        int r = Random.Range(0, 2);
        if (r == 0)
        {
            if (!player.CheckForBorders(Vector3.right))
            {
                MoveRight(player);
            }
            else if (!player.CheckForBorders(-Vector3.right))
                MoveLeft(player);
        }
        else
        {
            if (!player.CheckForBorders(-Vector3.right))
                MoveLeft(player);
            else if (!player.CheckForBorders(Vector3.right))
                MoveRight(player);
        }
        Manager.Instance.obstacleNumber++;
    }
    public void MoveLeft(PlayerMovement player)
    {
        player.animator.SetBool("MoveOilLeft", true);
    }
    public void MoveRight(PlayerMovement player)
    {
        player.animator.SetBool("MoveOilRight", true);
    }
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
    }

}

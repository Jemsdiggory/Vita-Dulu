using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonManager : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Home Page");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BukaResto()
    {
        SceneManager.LoadSceneAsync("Scene Pilih Mode");
    }

    public void BukaKebun()
    {
        SceneManager.LoadSceneAsync("Scene Kebun");
    }

    public void BukaStore()
    {
        SceneManager.LoadSceneAsync("Scene Store");
    }

    public void BukaResep()
    {
        SceneManager.LoadSceneAsync("Scene Resep");
    }

    public void KembaliKeHomePage()
    {
        SceneManager.LoadSceneAsync("Home Page");
    }

    public void ModeKasual()
    {
        SceneManager.LoadSceneAsync("Scene Casual");
    }

    public void ModeHard()
    {
        SceneManager.LoadSceneAsync("Scene Hard");
    }

}

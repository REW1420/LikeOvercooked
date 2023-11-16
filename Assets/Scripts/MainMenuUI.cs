using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button quitButton;
    [SerializeField] private Button playLVL1;
    [SerializeField] private Button playLVL2;
    [SerializeField] private Button playLVL3;

    private void Awake()
    {
        playLVL1.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.Level1_GameScene);
        });
        playLVL2.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.Level2_GameScene);
        });
        playLVL3.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.Level3_GameScene);
        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}

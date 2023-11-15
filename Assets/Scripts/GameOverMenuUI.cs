using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenuUI : MonoBehaviour
{
    [SerializeField] private Button restartGame;
    [SerializeField] private Button mainMenu;

    private void Awake()
    {
        mainMenu.onClick.AddListener(() =>
        {
            if (!GameManager.Instance.IsGamePlaying())
            {
                GameManager.Instance.SetWaitingToStartState();
                DeliveryManager.Instance.ClearRecipeSOList();
            }
        });
        restartGame.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MenuScene);
        });
    }
}

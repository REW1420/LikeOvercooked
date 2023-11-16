using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level3_GameOverMenu : MonoBehaviour
{
    [SerializeField] private Button test;

    private void Awake()
    {
        test.onClick.AddListener(() =>
        {
            Debug.Log("click");
        });
    }
}

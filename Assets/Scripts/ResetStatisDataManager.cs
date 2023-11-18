using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStatisDataManager : MonoBehaviour
{
    private void Awake()
    {
        CuttingCounter.ResetStatisData();
        BaseCounter.ResetStatisData();
        TrashCounter.ResetStatisData();

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpgradeSystem.Instance.HideUpgradeMenu();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            UpgradeSystem.Instance.ShowUpgradeMenu();
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // Restart Button Settings
   public void ReStart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour
{
    public void Scene(int num)
    {
        SceneManager.LoadScene(num);
    }
}

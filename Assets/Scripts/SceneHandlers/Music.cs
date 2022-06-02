using System.Collections;
using System.Collections.Generic;
using SceneHandlers;
using UnityEngine;

public class Music : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        StaticContainer.Music = transform.gameObject;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScore : MonoBehaviour
{
    private static TestScore instance;
    public static TestScore Instance { get => instance; set => instance = value; }

    public int score;

    private void Awake()
    {
        instance = this;
    }
}

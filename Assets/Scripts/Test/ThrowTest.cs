using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowTest : MonoBehaviour
{
    public Test test;
    public float speed;
    private void Awake()
    {
        test = GameObject.FindAnyObjectByType<Test>().GetComponent<Test>();
    }
    private void Update()
    {
        StartCoroutine("Fly");
    }

    IEnumerator Fly()
    {
        for (int i = 0; i < test.lastListOfPoint.Count; i++)
        {
            transform.position = test.lastListOfPoint[i];
            yield return null;
        }

        // gameObject.SetActive(false);
    }
}

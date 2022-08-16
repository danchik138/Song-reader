using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PairScript : MonoBehaviour
{
    [SerializeField] float startScale = 1.2f;
    [SerializeField] float scaleDelta = -0.01f;
    void Start()
    {
        transform.localScale = new Vector3(startScale, startScale, startScale);
    }

    void Update()
    {
        transform.localScale += new Vector3(1, 1, 1) * scaleDelta * Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleScript : MonoBehaviour
{
    public AudioSource audio;

    public GameObject pair;
    void Start()
    {
        audio = GameObject.Find("SpawnManager").GetComponent<AudioSource>();
        StartCoroutine(DestroyOnTime());
        pair = Instantiate(pair, transform.position, transform.rotation);
    }

    private void OnMouseDown()
    {
        audio.Play();
        Destroy(pair);
        Destroy(gameObject);
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
        {
            audio.Play();
            Destroy(pair);
            Destroy(gameObject);
        }
    }
    IEnumerator DestroyOnTime()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(pair);
        Destroy(gameObject);
    }
}

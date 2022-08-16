using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject circle;

    private float startTime;
    

    [SerializeField] private float scale = 0.3f;
    [SerializeField] private float xOffset = -256;
    [SerializeField] private float yOffset = -192;
    [SerializeField] private float circleLifeTime = 0.4f;

    private float TimeSinceSongStart
    {
        get { return Time.realtimeSinceStartup - startTime; }
    }
    public void StartSong(Song song)
    {
        audioSource.clip = song.audio;
        audioSource.Play();
        startTime = Time.realtimeSinceStartup;
        StartCoroutine(SpawnCircles(song));
    }

    IEnumerator SpawnCircles(Song song)
    {
        while (song.IfContainsHitObjects)
        {
            HitObject hitObject = song.GetHitObjectFromQueue();
            //print(hitObject);
            float nextTime = hitObject.ShowTime;
            float delta = nextTime - TimeSinceSongStart - circleLifeTime;
            print($"new circle in {delta} seconds");
            yield return new WaitForSecondsRealtime(delta);
            float x = (hitObject.XPosition + xOffset) * scale;
            float y = (hitObject.YPosition + yOffset) * scale;
            Instantiate(circle, new Vector3(x, y, 0), new Quaternion());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Song
{
    public string Name { get; private set; }

    private Queue<HitObject> hits;

    public AudioClip audio { get; private set; }

    public Song(AudioClip audio, Queue<HitObject> queue, string name = "unknown")
    {
        this.audio = audio;
        hits = queue;
        Name = name;
    }
 
    public HitObject GetHitObjectFromQueue()
    {
        return hits.Dequeue();
    }
    public HitObject PeekHitObjectFromQueue()
    {
        return hits.Peek();
    }

    public bool IfContainsHitObjects
    {
        get { return hits.Count > 0; }
    }

    public override string ToString()
    {
        return string.Format($"{Name}, length = {hits.Count}");
    }
}

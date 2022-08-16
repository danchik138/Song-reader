using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty
{
    #region Info
    public string name { get; set; } = "Normal";
    public float HPDrainRate { get; set; } = -1;
    public float CircleSize { get; set; } = -1;
    public float OverallDifficulty { get; set; } = -1;
    public float ApproachRate { get; set; } = -1;
    public float SliderMultiplier { get; set; } = -1;
    public float SliderTickRate { get; set; } = -1;
    public float starsCount { get; set; } = -1;
    #endregion

    private Queue<HitObject> hits;

    public Difficulty(Queue<HitObject> hits, string name, float[] info)
    {
        this.hits = hits;
        this.name = name;

        HPDrainRate = info[0];
        CircleSize = info[1];
        OverallDifficulty = info[2];
        ApproachRate = info[3];
        SliderMultiplier = info[4];
        SliderTickRate = info[5];
        starsCount = 6;
    }

    public Queue<HitObject> GetHitsQueue()
    {
        return new Queue<HitObject>(hits);
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
}

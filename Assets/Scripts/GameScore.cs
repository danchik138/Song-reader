using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameScore : MonoBehaviour
{
    public const int MAXHP = 100;
    public static int score { get; private set; }

    public static float accuracy { get; private set; }

    public static int hits { get; private set; }

    public static float hp { get; private set; }

    public static void ResetScore()
    {
        score = 0;
        accuracy = 0;
        hits = 0;
        hp = 0;
    }

    public static void AddScore(int hitScore)
    {
        if (hitScore > 0)
        {
            score += hitScore;
        }
        else
        {
            throw new Exception("Score delta is below zero");
        }
    }

    public static void AddAccuracy(float hitAccuracy)
    {
        if (hitAccuracy >= 0 && hitAccuracy <= 100)
        {
            accuracy = (accuracy * hits + hitAccuracy) / ++hits;
        }
        else
        {
            throw new Exception("Invalid hit accuracy");
        }
    }

    public static void AddHit(int hitScore, float hitAccuracy)
    {
        AddScore(hitScore);
        AddAccuracy(hitAccuracy);
    }

    public static void ChangeHP(float hpDelta)
    {
        hp = Math.Clamp(hp + hpDelta, 0, MAXHP);
    }
}

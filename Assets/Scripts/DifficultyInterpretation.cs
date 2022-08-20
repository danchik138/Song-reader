using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DifficultyInterpretation
{
    const int MaxCircleSizeDelta = 10;

    const int MinCircleSize = 7;

    /// <summary>
    /// HP drain rate in hp per second
    /// </summary>
    /// <param name="hpDrainRate"></param>
    /// <returns></returns>
    public static float HPDrainRateToNumber(float hpDrainRate)
    {
        return hpDrainRate * 0.1f;
    }

    /// <summary>
    /// Size of the circle as a fraction of the maximum screen height
    /// </summary>
    /// <param name="circleSize"></param>
    /// <returns></returns>
    public static float CircleSizeToNumber(float circleSize)
    {

        return MinCircleSize + MaxCircleSizeDelta * circleSize / 10.0f;
    }

    public static float ApproachRateToNumber(float approachRate)
    {
        if (approachRate < 5 && approachRate >=0)
        {
            return (1200 + 600 * (5 - approachRate) / 5.0f) / 1000.0f;
        }
        else if (approachRate == 5)
        {
            return 1.2f;
        }
        else if (approachRate > 5 && approachRate <= 10)
        {
            return (1200 - 750 * (approachRate - 5) / 5.0f) / 1000.0f;
        }
        else
        {
            throw new System.Exception("Incorrect ApproachRate value");
        }
    }
    public static float SliderMultiplierToNumber(float sliderMultiplier)
    {
        return 0;
    }
    public static float SliderTickRateToNumber(float sliderTickRate)
    {
        return 0;
    }
}

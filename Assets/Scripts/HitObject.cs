using System;
public class HitObject
{
    public HitType hitType;
    //public HitType hitType { get; private set; }
    
    //public SliderType sliderType { get; private set; }
    //public int sliderRepeatCount { get; private set; }

    //private float[,] Hits;
    //public float XPosition
    //{
    //    get
    //    {
    //        return Hits[0,0];
    //    }
    //}
    //public float YPosition
    //{
    //    get
    //    {
    //        return Hits[0, 1];
    //    }
    //}
    //public float ShowTime { get; private set; }
    //public float EndTime { get; private set; }



    //public override string ToString()
    //{
    //    return string.Format($"x position: {XPosition}, y position: {YPosition}, time : {ShowTime}");
    //}
    ///// <summary>
    ///// Creates simple hit
    ///// </summary>
    ///// <param name="xPosition"></param>
    ///// <param name="yPosition"></param>
    ///// <param name="showTime"></param>
    //public HitObject(float xPosition, float yPosition, float showTime)
    //{
    //    Hits = new float[1, 2];
    //    Hits[0, 0] = xPosition;
    //    Hits[0, 1] = yPosition;
    //    this.ShowTime = showTime;
    //    hitType = HitType.Hit;
    //}
    ///// <summary>
    ///// Creates spinner
    ///// </summary>
    ///// <param name="xPosition"></param>
    ///// <param name="yPosition"></param>
    ///// <param name="showTime"></param>
    ///// <param name="endTime"></param>
    //public HitObject(float xPosition, float yPosition, float showTime, float endTime)
    //{
    //    Hits = new float[1, 2];
    //    Hits[0, 0] = xPosition;
    //    Hits[0, 1] = yPosition;
    //    this.ShowTime = showTime;
    //    this.EndTime = endTime;
    //    hitType = HitType.Spinner;
    //}
    ///// <summary>
    ///// Creates slider
    ///// </summary>
    ///// <param name="hits"></param>
    //public HitObject(float showTime, int sliderRepeatCount, float[,] hits)
    //{
    //    this.sliderRepeatCount = sliderRepeatCount;
    //    this.ShowTime = showTime;
    //    this.Hits = hits;
    //    hitType = HitType.Slider;
    //    sliderType = SliderType.B;
    //}

    //public float[,] GetSliderArray()
    //{
    //    if (hitType == HitType.Slider)
    //    {
    //        return (float[,]) Hits.Clone();
    //    }
    //    else
    //    {
    //        throw new Exception("This HitObject isn't a slider");
    //    }
    //}

    public enum HitType
    {
        HitCircle,
        Slider,
        Spinner
    }

}

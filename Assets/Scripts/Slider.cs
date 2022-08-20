

public class Slider : HitObject
{
    public float showTime
    {
        get
        {
            return startTime - lifeTime;
        }
    }
    public float lifeTime { get; private set; }
    public float startTime { get; private set; }
    public float repeatCount { get; private set; }
    public SliderType type { get; private set; }

    private float[,] hits;

    public Slider(float lifeTime, float startTime, float repeatCount, SliderType type, float[,] hits)
    {
        this.lifeTime = lifeTime;
        this.startTime = startTime;
        this.repeatCount = repeatCount;
        this.type = type;
        this.hits = hits;
        this.hitType = HitType.Slider;
    }

    public enum SliderType
    {
        P,
        B,
        L
    }

    public override string ToString()
    {
        return $"Slider: x = {hits[0,0]}, y = {hits[0,1]}, startTime = {startTime}, number of hits = {hits.GetLength(0)}";
    }
}

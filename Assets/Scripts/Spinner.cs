public class Spinner : HitObject
{
    public float xPosition { get; private set; }
    public float yPosition { get; private set; }

    public float showTime
    {
        get
        {
            return startTime - lifeTime;
        }
    }
    public float lifeTime { get; private set; }
    public float startTime { get; private set; }
    public float endTime { get; private set; }

    public Spinner(float xPosition, float yPosition, float lifeTime, float startTime, float endTime)
    {
        this.xPosition = xPosition;
        this.yPosition = yPosition;
        this.lifeTime = lifeTime;
        this.startTime = startTime;
        this.endTime = endTime;
        this.hitType = HitType.Spinner;
    }

    public override string ToString()
    {
        return $"Spinner: x = {xPosition}, y = {yPosition}, start time = {startTime}, end time = {endTime}";
    }
}

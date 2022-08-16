public class HitObject
{
    public float XPosition { get; private set; }
    public float YPosition { get; private set; }
    public float ShowTime { get; private set; }

    public override string ToString()
    {
        return string.Format($"x position: {XPosition}, y position: {YPosition}, time : {ShowTime}");
    }

    public HitObject(float xPosition, float yPosition, float showTime)
    {
        XPosition = xPosition;
        YPosition = yPosition;
        ShowTime = showTime;
    }

    public HitObject()
    {
        XPosition = 0;
        YPosition = 0;
        ShowTime = 0;
    }
}

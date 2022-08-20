
public class HitCircle : HitObject
{
    public float xPosition { get; private set; }
    public float yPosition { get; private set; }

    public float showTime 
    { 
        get 
        {
            return hitTime - lifeTime;
        }
    }
    public float lifeTime { get; private set; }
    public float hitTime { get; private set; }

    public HitCircle(float xPosition, float yPosition, float lifeTime, float hitTime)
    {
        this.xPosition = xPosition;
        this.yPosition = yPosition;
        this.lifeTime = lifeTime;
        this.hitTime = hitTime;
        this.hitType = HitType.HitCircle;
    }

    public override string ToString()
    {
        return $"Hit circle: x = {xPosition}, y = {yPosition}, hit time = {hitTime}";
    }
}

namespace Obacher.RandomOrgSharp
{
    public interface IAdvisoryDelayManager
    {
        void Delay();
        void SetAdvisoryDelay(int advisoryDelay);
    }
}
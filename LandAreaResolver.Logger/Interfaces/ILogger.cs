namespace LandAreaResolver.Logger.Interfaces
{
    public interface ILogger
    {
        void Info(string messgae);
        void Warn(string messgae);
        void Error(string messgae);
        void Debug(string messgae);
        void Fatal(string messgae);
    }
}

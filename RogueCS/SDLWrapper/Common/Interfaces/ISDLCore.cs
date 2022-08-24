using RogueCS.SDLWrapper.Graphics.Interfaces;
using static SDL2.SDL;

namespace RogueCS.SDLWrapper.Common.Interfaces {
    public interface ISDLCore : IDisposable {
        // ***************************************************************
        /* Logging                                                      */
        void LogInfo   (string message);
        void LogDebug  (string message);

        string GetSDLError();

        // ***************************************************************
        /* Utility                                                      */
        ITimer CreateAveragingMsTimer(int averageOverX);

        UInt64 GetPerformanceFrequency();
        UInt64 GetPerformanceCounter();
    }
}

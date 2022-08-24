using RogueCS.SDLWrapper.Common.Interfaces;
using RogueCS.SDLWrapper.Graphics.Interfaces;

namespace RogueCS.SDLWrapper.Common.Implementations {

    public class AveragingMsTimer : ITimer {

      #region fields

        UInt64  _lastCount;
		UInt64  _frequency;
		int    _averageOverX;
		int    _currentTick;
		double _currentMsAverage;

        ISDLCore   _sdl;

      #endregion fields

      #region ctor

        public AveragingMsTimer(ISDLCore sdl, int averageOverX) {
            _sdl          = sdl;
            _averageOverX = averageOverX;
            _frequency    = _sdl.GetPerformanceFrequency();
            _lastCount    = _sdl.GetPerformanceCounter();
        }

      #endregion ctor

      #region ITimer

        public double Tick() {
	        if (_currentTick++ == _averageOverX) {
		        var currentCount = _sdl.GetPerformanceCounter();

		        _currentMsAverage = 
			    ((double)(currentCount - _lastCount) / (double)_frequency)*1000/_averageOverX;

		        _lastCount   = currentCount;
		        _currentTick = 0;
	        }
	        return _currentMsAverage;
        }

      #endregion ITimer

    }
}

using SDL2;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

SDL.SDL_SetHint(SDL.SDL_HINT_WINDOWS_DISABLE_THREAD_NAMING, "1");

SDL.SDL_Init(SDL.SDL_INIT_VIDEO);

var window = IntPtr.Zero;


window = SDL.SDL_CreateWindow("Test", SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED, 800, 800, SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE);

SDL.SDL_Delay(5000);
SDL.SDL_DestroyWindow(window);

SDL.SDL_Quit();


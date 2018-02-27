using System;

namespace MyLogAn2
{
    public interface IView
    {
        event Action Loaded;
        event Action<string> ErrorOccurred;
        void Render(string text);
    }
}

using System;

namespace MyLogAn2
{
    public interface IView
    {
        event Action Loaded;
        void Render(string text);
    }
}

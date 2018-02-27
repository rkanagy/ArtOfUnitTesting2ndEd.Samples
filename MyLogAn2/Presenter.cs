namespace MyLogAn2
{
    public class Presenter
    {
        private readonly IView _view;
        private readonly ILogger _logger;

        public Presenter(IView view, ILogger logger)
        {
            _view = view;
            _logger = logger;
            _view.Loaded += OnLoaded;
            _view.ErrorOccurred += OnError;
        }

        private void OnLoaded()
        {
            _view.Render("Hello World");
        }

        private void OnError(string error)
        {
            _logger.LogError(error);
        }
    }
}

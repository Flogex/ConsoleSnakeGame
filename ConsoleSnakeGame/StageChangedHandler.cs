using ConsoleSnakeGame.Gameplay;
using ConsoleSnakeGame.Rendering;

namespace ConsoleSnakeGame
{
    public static class StageChangedHandler
    {
        public static void Initialize(Stage stage, StageRenderer renderer)
        {
            stage.StageChangedEvent += (sender, _) => HandleStageChanged(sender, renderer);
        }

        private static void HandleStageChanged(object? sender, StageRenderer renderer)
        {
            if (sender is IStage nextStage)
                renderer.RenderNext(nextStage);
        }
    }
}

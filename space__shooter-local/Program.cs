using space__shooter;

var game = new Game();
var renderer = new Renderer();
var lastTime = DateTime.Now;

// Hlavní herní smyčka
while (true)
{
    // Ovládání hráče
    if (Console.KeyAvailable)
    {
        var key = Console.ReadKey(true).Key;
        switch (key)
        {
            case ConsoleKey.LeftArrow:
                game.MovePlayer(-1, 0);
                break;
            case ConsoleKey.RightArrow:
                game.MovePlayer(1, 0);
                break;
            case ConsoleKey.UpArrow:
                game.MovePlayer(0, -1);
                break;
            case ConsoleKey.DownArrow:
                game.MovePlayer(0, 1);
                break;
            case ConsoleKey.Spacebar:
                game.PlayerShoot();
                break;
        }
    }

    // Kontrola smrti hráče
    if (game.IsPlayerDead())
    {
        game.SaveHighScore();
        game.LoadHighScore();
        renderer.RenderGameOver(game);
        var key = Console.ReadKey(true).Key;
        switch (key)
        {
            case ConsoleKey.R:
                game = new Game();
                break;
            case ConsoleKey.Q:
                return;
        }
    }

    // Aktualizace hry
    var now = DateTime.Now;
    if ((now - lastTime).TotalMilliseconds >= game.GameSpeed)
    {
        game.Update();
        game.SpawnEntities();
        renderer.Render(game);
        lastTime = now;
    }

    Task.Delay(1).Wait();
}
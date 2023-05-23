using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using space_shooter;

class Program
{
    static async Task Main()
    {
        Console.CursorVisible = false;
        CancellationTokenSource cts = new CancellationTokenSource();
        ConcurrentQueue<ConsoleKey> movementKeys = new ConcurrentQueue<ConsoleKey>();
        ConcurrentQueue<ConsoleKey> shootingKeys = new ConcurrentQueue<ConsoleKey>();

        Game game = new Game();
        Renderer renderer = new Renderer();

        Task gameLoop = GameLoop(game, renderer, movementKeys, shootingKeys, cts.Token);
        Task inputLoop = InputLoop(movementKeys, shootingKeys, cts.Token);

        await Task.WhenAny(gameLoop, inputLoop);

        cts.Cancel();
        await Task.WhenAll(gameLoop, inputLoop);
    }

    static async Task GameLoop(Game game, Renderer renderer, ConcurrentQueue<ConsoleKey> movementKeys, ConcurrentQueue<ConsoleKey> shootingKeys, CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            while (movementKeys.TryDequeue(out var key))
            {
                if (key == ConsoleKey.W || key == ConsoleKey.UpArrow)
                    game.Player.MoveUp();

                if (key == ConsoleKey.A || key == ConsoleKey.LeftArrow)
                    game.Player.MoveLeft();

                if (key == ConsoleKey.S || key == ConsoleKey.DownArrow)
                    game.Player.MoveDown();

                if (key == ConsoleKey.D || key == ConsoleKey.RightArrow)
                    game.Player.MoveRight();
            }

            while (shootingKeys.TryDequeue(out var key))
            {
                if (key == ConsoleKey.Spacebar)
                    game.Player.Shoot(game);
            }

            game.Update();
            renderer.Render(game);

            await Task.Delay(10);
        }
    }

    static Task InputLoop(ConcurrentQueue<ConsoleKey> movementKeys, ConcurrentQueue<ConsoleKey> shootingKeys, CancellationToken token)
    {
        return Task.Run(() =>
        {
            while (!token.IsCancellationRequested)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.W || key == ConsoleKey.A || key == ConsoleKey.S || key == ConsoleKey.D || key == ConsoleKey.UpArrow || key == ConsoleKey.LeftArrow || key == ConsoleKey.DownArrow || key == ConsoleKey.RightArrow)
                        movementKeys.Enqueue(key);

                    if (key == ConsoleKey.Spacebar)
                        shootingKeys.Enqueue(key);
                }
            }
        });
    }
}

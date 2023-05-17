using System;
using System.Threading;
using System.Threading.Tasks;
using space_shooter;

class Program
{
    static async Task Main()
    {
        Game game = new Game();
        Renderer renderer = new Renderer();

        while (true)
        {
            var keyState = Console.ReadKey(true).Key;

            if (keyState == ConsoleKey.W || keyState == ConsoleKey.UpArrow)
                game.Player.MoveUp();

            if (keyState == ConsoleKey.A || keyState == ConsoleKey.LeftArrow)
                game.Player.MoveLeft();

            if (keyState == ConsoleKey.S || keyState == ConsoleKey.DownArrow)
                game.Player.MoveDown();

            if (keyState == ConsoleKey.D || keyState == ConsoleKey.RightArrow)
                game.Player.MoveRight();

            if (keyState == ConsoleKey.Spacebar)
                game.Player.Shoot(game);

            game.Update();
            await Task.Delay(100);
            renderer.Render(game);
        }
    }
}


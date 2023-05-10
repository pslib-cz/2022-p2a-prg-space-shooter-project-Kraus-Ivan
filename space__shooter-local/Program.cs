using space_shooter;

Game game = new Game();
Renderer renderer = new Renderer();

while (true) // vždycky jsem chtěl tento cyklus použít :)
{
    game.Update();
    renderer.Render(game);

    System.Threading.Thread.Sleep(100);
}
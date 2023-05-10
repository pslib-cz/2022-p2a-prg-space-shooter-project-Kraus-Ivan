using space_shooter;

Game game = new Game();
Renderer renderer = new Renderer();

while (true) // vždycky jsem chtěl tento cyklus použít :)
{
    game.Update();

    if (game.Input.IsFirePressed())
        game.Player.Shoot(game);
    
    if (game.Input.IsMoveLeftPressed())
        game.Player.MoveLeft();
    
    if (game.Input.IsMoveRightPressed())
        game.Player.MoveRight();
    
    if (game.Input.IsMoveUpPressed())
        game.Player.MoveUp();

    if (game.Input.IsMoveDownPressed())
        game.Player.MoveDown();
    
    System.Threading.Thread.Sleep(100);

    renderer.Render(game);
}
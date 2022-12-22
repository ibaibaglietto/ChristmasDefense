using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputComponent : InputComponent
{
    //An array to save the commands
    private Command[] commands = new Command[10];
    //An int to know what is the command that we are executing
    private int commandNumb = 0;

    //We check the inputs every frame
    public override void Update(Skeleton gameObject)
    {
        gameObject.inputHandler.HandleInput(); 
        commands = gameObject.inputHandler.ReturnInput();
        while (commandNumb < 10 && commands[commandNumb] != null)
        {
            commands[commandNumb].Execute(gameObject);
            commandNumb++;
        }
        commandNumb = 0;
    }

    //We don't need to use create with the player, because we don't need any delay on their movement
    public override void Create(int numb, Skeleton gameObject)
    {
        
    }
}

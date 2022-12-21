using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputHandler : MonoBehaviour
{
    //We create a dictionary to bind commands to keys
    Dictionary<string, Command> keys = new Dictionary<string, Command>();
    //Two variables to save the commands that will be saved in the previous commands and sent to the skeletons
    private Command[] commands = new Command[10];
    private Command[] sendCommands = new Command[10];
    //The number of commands saved in this command array, can be a maximum of 10
    private int totalCommands = 0;
    //The strings where we will save the name of the command that will be generated. For example, if a is the button connected to the moveLeft command, AButtonPress and AButtonRelease will be linked to the MoveLeftCommand and StopMoveLeftCommand.
    private string moveLeftPress;
    private string moveLeftRelease;
    private string moveRightPress;
    private string moveRightRelease;
    private string jumpPress;
    private string jumpRelease;
    private string dashPress;
    private string dashRelease;
    private string attackPress;
    private string attackRelease;
    private string crouchPress;
    private string crouchRelease;
    //The keycodes of the 4 movement commands
    private KeyCode moveLeftKey;
    private KeyCode moveRightKey;
    private KeyCode jumpKey;
    private KeyCode dashKey;
    private KeyCode attackKey;
    private KeyCode crouchKey;

    private void Start()
    {
        //We save the strings that will be connected to the commands
        moveLeftPress = PlayerPrefs.GetString("moveLeft") + "ButtonPress";
        moveLeftRelease = PlayerPrefs.GetString("moveLeft") + "ButtonRelease";
        moveRightPress = PlayerPrefs.GetString("moveRight") + "ButtonPress";
        moveRightRelease = PlayerPrefs.GetString("moveRight") + "ButtonRelease";
        jumpPress = PlayerPrefs.GetString("jump") + "ButtonPress";
        jumpRelease = PlayerPrefs.GetString("jump") + "ButtonRelease";
        dashPress = PlayerPrefs.GetString("dash") + "ButtonPress";
        dashRelease = PlayerPrefs.GetString("dash") + "ButtonRelease";
        attackPress = PlayerPrefs.GetString("attack") + "ButtonPress";
        attackRelease = PlayerPrefs.GetString("attack") + "ButtonRelease";
        crouchPress = PlayerPrefs.GetString("crouch") + "ButtonPress";
        crouchRelease = PlayerPrefs.GetString("crouch") + "ButtonRelease";
        //We add to the dictionary the strings with their connected command.
        keys.Add(moveLeftPress, new MoveLeftCommand());
        keys.Add(moveLeftRelease, new StopMoveLeftCommand());
        keys.Add(moveRightPress, new MoveRightCommand());
        keys.Add(moveRightRelease, new StopMoveRightCommand());
        keys.Add(jumpPress, new JumpCommand());
        keys.Add(jumpRelease, new StopJumpCommand());
        keys.Add(dashPress, new DashCommand());
        keys.Add(dashRelease, new StopDashCommand());
        keys.Add(attackPress, new AttackCommand());
        keys.Add(attackRelease, new StopAttackCommand());
        keys.Add(crouchPress, new CrouchCommand());
        keys.Add(crouchRelease, new StopCrouchCommand());
        //Using refraction, we save the keycodes that will activate the commands
        moveLeftKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("moveLeft"));
        moveRightKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("moveRight"));
        jumpKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jump"));
        dashKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("dash"));
        attackKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("attack"));
        crouchKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("crouch"));
    }
    //We save a maximum of 10 commands that will be sent every 1/50 seconds
    public void HandleInput()
    {
        if (totalCommands < 10)
        {
            if (Input.GetKeyDown(jumpKey))
            {
                commands[totalCommands] = keys[jumpPress];
                totalCommands++;
            }
            if (Input.GetKeyUp(jumpKey))
            {
                commands[totalCommands] = keys[jumpRelease];
                totalCommands++;
            }
            if (Input.GetKeyDown(moveLeftKey))
            {
                commands[totalCommands] = keys[moveLeftPress];
                totalCommands++;
            }
            if (Input.GetKeyUp(moveLeftKey))
            {
                commands[totalCommands] = keys[moveLeftRelease];
                totalCommands++;
            }
            if (Input.GetKeyDown(moveRightKey))
            {
                commands[totalCommands] = keys[moveRightPress];
                totalCommands++;
            }
            if (Input.GetKeyUp(moveRightKey))
            {
                commands[totalCommands] = keys[moveRightRelease];
                totalCommands++;
            }
            if (Input.GetKeyDown(dashKey))
            {
                commands[totalCommands] = keys[dashPress];
                totalCommands++;
            }
            if (Input.GetKeyUp(dashKey))
            {
                commands[totalCommands] = keys[dashRelease];
                totalCommands++;
            }
            if (Input.GetKeyDown(attackKey))
            {
                commands[totalCommands] = keys[attackPress];
                totalCommands++;
            }
            if (Input.GetKeyUp(attackKey))
            {
                commands[totalCommands] = keys[attackRelease];
                totalCommands++;
            }
            if (Input.GetKeyDown(crouchKey))
            {
                commands[totalCommands] = keys[crouchPress];
                totalCommands++;
            }
            if (Input.GetKeyUp(crouchKey))
            {
                commands[totalCommands] = keys[crouchRelease];
                totalCommands++;
            }
        }
    }
    //We return the saved commands 
    public Command[] ReturnInput()
    {
        sendCommands = commands;
        commands = new Command[10];
        totalCommands = 0;
        return sendCommands;
    }
}

######################################################################################################################
"""
Description  : Lab02 - Snake
Author       : Sharry Singh
Class        : CNT.A01
Submission Code : 1231_2850_L02
Date created : 15 -Nov-2023
Checkoff :
"""
######################################################################################################################


# all imports
import random, sys, time, threading, clr
import tkinter as tk

clr.AddReference('GDIDrawer')
from GDIDrawer import CDrawer
from GDIDrawer import RandColor
from Classes import Snake, Segment, HelloWorldApp

######################################################################################################################


# Global variables and locks
myLock = threading.Lock()
keyCode = 0
isRunning = True
isPause = False
currentLength = 0
bonusScore = 0


######################################################################################################################


def _drawer_ButtonPress(bIsDown, keycode, dr):
    """
    Drawer button press event
    :param bIsDown: bool if button is down or not
    :param keycode: current keycode
    :param dr: Drawer window
    :return: nothing
    """
    global keyCode, myLock, isPause, isRunning  # all globals

    """
    Below down I am making sure that the snake cannot go in the opposite direction
    """
    oppositeKeyCode = 0
    opposite_direction = tuple(map(lambda x: -x, Segment.KeysMapDict[keyCode]))
    for key, value in Segment.KeysMapDict.items():
        if value == opposite_direction:
            oppositeKeyCode = key

    with myLock:
        if bIsDown:
            if int(keycode) in Segment.KeysMapDict:
                if int(keycode) != oppositeKeyCode:
                    keyCode = int(keycode)

            # enhancement feature where user can pause the game or exist the gamne
            # if enter or pause button is pressed
            if int(keycode) == 13 or int(keycode) == 80:
                if isPause:
                    isPause = False
                else:
                    isPause = True

            # if escape button is pressed, then ending the game
            if int(keycode) == 27:
                isRunning = False


######################################################################################################################

def GameThread(**kwargs):
    """
    This function is the main thread that will run while the game is going on
    :param kwargs: array of arguments
    :return: none
    """

    global keyCode, myLock, isRunning, currentLength, bonusScore, isPause

    # the Scale for the CDrawer used for the game will be supplied, if no key exists, use the value 20.
    drawerScale = 20
    if 'Scale' in kwargs:
        drawerScale = kwargs['Scale']

    # Initialize the current direction keyCode global to a random direction.
    keyCode = random.randint(37, 40)

    # Create and initialize the CDrawer, set the scale, continuous update and KeyboardCallback event to your handler
    _drawer = CDrawer()
    _drawer.ContinuousUpdate = False
    _drawer.Scale = drawerScale

    _drawer.KeyboardEvent += _drawer_ButtonPress

    mySnake = Snake(int(_drawer.ScaledWidth / 2), int(_drawer.ScaledHeight / 2))

    ticks = 0
    isEated = True

    while isRunning:
        if not isPause:
            # sleep for 100ms
            time.sleep(0.100)

            # retrieve the current keyCode into a local
            currentKeyCode = keyCode

            # Was it escape ? clear the running flag and break out of the loop
            if not isRunning:
                break

            # Move the snake
            mySnake.Move(currentKeyCode)

            currentLength = mySnake.Head()[1]

            # checking if the game is over
            if mySnake.GameOver(_drawer):
                isRunning = False

            # Clear/Show/Render your snake
            _drawer.Clear()
            mySnake.Show(_drawer)

            # checking if the food is eated or not, otherwise creating new food
            if isEated:
                randX = random.randint(0, _drawer.ScaledWidth - 1)
                randY = random.randint(0, _drawer.ScaledHeight - 1)
                food = Segment(randX, randY, RandColor.GetColor())
                isEated = False

            food.Show(_drawer)
            _drawer.Render()

            if mySnake.Head()[0] == food:
                isEated = True
                bonusScore += 1
            ticks += 1

            if ticks == 10:
                ticks = 0
                mySnake._grow = True
        else:
            _drawer.AddText(f'Game Paused', 20, RandColor.GetColor())

    _drawer.Clear()
    print(f'Game Over\r\n Total Score = {str(currentLength + 5 * bonusScore)}')
    _drawer.AddText(f'Game Over\r\n Total Score = {str(currentLength + 5 * bonusScore)}', 20, RandColor.GetColor())
    time.sleep(5)
    _drawer.Close()


######################################################################################################################

def updateScore():
    global isRunning, currentLength, bonusScore

    # Create the main Tkinter window
    root = tk.Tk()

    # Create an instance of the HelloWorldApp class
    app = HelloWorldApp(root, initial_text=str(currentLength + 5 * bonusScore))

    while isRunning:
        # Your main application logic to determine the new string
        new_string_from_main = str(currentLength + 5 * bonusScore)

        # Update the string in the Tkinter app
        app.update_string(new_string_from_main)

        # Run the Tkinter main loop
        root.update_idletasks()
        root.update()
    time.sleep(5)
    # Destroy the Tkinter window when the loop is done
    root.destroy()


######################################################################################################################

if __name__ == "__main__":
    """
    main function , I will be using two threads one for snake game 
    and other for the window form
    """
    _threadList = []
    gameThread = threading.Thread(target=GameThread, kwargs={'Scale': 20}, daemon=True)
    _threadList.append(gameThread)
    gameThread.start()

    appThread = threading.Thread(target=updateScore, daemon=True)
    _threadList.append(appThread)
    appThread.start()

    for item in _threadList:
        item.join()

    while isRunning:
        time.sleep(1.0)

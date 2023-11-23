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
import clr

clr.AddReference('GDIDrawer')
from GDIDrawer import RandColor
from GDIDrawer import CDrawer
import tkinter as tk


######################################################################################################################
class Segment:
    """
    This is the base class that will contain the definition of each segment
    """
    # static dictionary mapping key input values to their respective direction
    KeysMapDict = {
        37: (-1, 0),  # left
        38: (0, -1),  # up
        39: (1, 0),  # right
        40: (0, 1)  # down
    }

    def __init__(self, x, y, color, parent=None):
        """
        CTOR for class
        :param x: current x position
        :param y: current xposition
        :param color:
        :param parent:
        """
        self._x = x
        self._y = y
        self._color = color
        self._parent = parent

    def __eq__(self, other):
        """
        Equals to method that will be true if x and y cordinates of this and other obj is same
        :param other: Segment class elem
        :return: bool
        """
        if isinstance(other, Segment):
            return self._x == other._x and self._y == other._y

    def Show(self, drawer):
        """
        This function will show the segment on the drawer
        :param drawer: CDrawer
        :return: nothing
        """
        drawer.AddCenteredEllipse(self._x, self._y, 1, 1, self._color)
        if self._parent:
            self._parent.Show(drawer)

    def Move(self, direction):
        """
        This function will move the segment in the given direction
        :param direction: current direction
        :return:
        """
        if self._parent:
            beforeX = self._parent._x
            beforeY = self._parent._y
            self._parent.Move(direction)
            self._x = beforeX
            self._y = beforeY
        else:
            dx, dy = Segment.KeysMapDict[direction]
            self._x += dx
            self._y += dy


######################################################################################################################

class Snake:
    """
    Snake class that  will form the entire snake
    """

    def __init__(self, x, y):
        """
        CTOR for Snake class
        :param x: current x location
        :param y: current y location
        """
        self._tailSeg = Segment(x, y, RandColor.GetColor())
        self._grow = False

    def Show(self, drawer):
        """
        This function will draw the snake on the drawer
        :param drawer: CDrawer
        :return: nothing
        """
        self._tailSeg.Show(drawer)

    def Move(self, direction):
        """
        This function will move the snake
        :param direction: direction in which snake should be move
        :return: nothing
        """
        if self._grow:
            _currentTailX = self._tailSeg._x
            _currentTailY = self._tailSeg._y
            self._tailSeg = Segment(_currentTailX, _currentTailY, RandColor.GetColor(), self._tailSeg)

        self._tailSeg.Move(direction)

        # if self._grow:
        #     self._tailSeg = Segment(_currentTailX, _currentTailY, RandColor.GetColor(), self._tailSeg)

        self._grow = False

    def Head(self):
        """
        This function will return the length of the snake and snake itself
        :return: tuple of snake and length
        """
        length = 1
        snake = self._tailSeg
        while snake._parent:
            length += 1
            snake = snake._parent
        return snake, length

    def GameOver(self, drawer):
        """
        This function will check if the game is over or not
        :param drawer:
        :return:
        """
        tailSeg, length = self.Head()

        return (
                tailSeg._x > drawer.ScaledWidth or tailSeg._x < 0 or
                tailSeg._y > drawer.ScaledHeight or tailSeg._y < 0 or
                (length > 2 and tailSeg == self._tailSeg)
        )


######################################################################################################################

class HelloWorldApp:
    """
    Enhancement feature
    This is the modal that will show the score on the screen
    """

    def __init__(self, root, initial_text="Hello, World!"):
        self.root = root
        self.root.title("Score Update App")

        # Set the minimum dimensions of the window
        self.root.minsize(300, 200)

        # Get the screen width and height
        screen_width = self.root.winfo_screenwidth()
        screen_height = self.root.winfo_screenheight()

        # Calculate the x and y coordinates to center the window
        x = (screen_width - 500) // 2
        y = (screen_height - 200) // 2

        # Set the geometry of the window to center it
        self.root.geometry(f"200x200+{x}+{y}")

        # Create a label for the current score
        self.score_label = tk.Label(root, text="Current Score", font=("Helvetica", 14))
        self.score_label.pack(pady=10)

        # Create a label with the initial text
        self.label = tk.Label(root, text=initial_text, font=("Helvetica", 16))
        self.label.pack(padx=20, pady=20)

    def update_string(self, new_text):
        # Update the label text
        self.label.config(text=new_text)

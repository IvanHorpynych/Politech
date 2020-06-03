from PIL import Image, ImageDraw
from random import *
from collections import *

WHITE = 255, 255, 255
BLACK = 0, 0, 0
RED = 255, 0, 0

def main(width, height):
    flood = Image.new('RGB', (width, height), WHITE)
    # Create borders
    for x in range(width):
        for y in range(height):
            flood.putpixel((x, 15), BLACK)
            flood.putpixel((x, height-15), BLACK)
            flood.putpixel((15, y), BLACK)
            flood.putpixel((width-15, y), BLACK)
    flood.save('flood.png')            
    floodFill(150, 108, RED, flood)
    # Save image
    #flood.save('flood.png')


def floodFill(x,y, color, image):
    a=0
    b=0
    c=0
    stack=[(x,y)]
    while (stack):
        (x,y) = stack.pop()
        (a,b,c) == image.getpixel((x,y))
        #if (a,b,c) == (255, 255, 255):
        image.putpixel((x,y), color)
        if x>10: stack.append((x-1,y))
        if x<290: stack.append((x+1,y))
        if y>10: stack.append((x,y-1))
        if y<290: stack.append((x,y+1))
    image.save("flood1.png")

if __name__ == '__main__':
    main(300, 300)
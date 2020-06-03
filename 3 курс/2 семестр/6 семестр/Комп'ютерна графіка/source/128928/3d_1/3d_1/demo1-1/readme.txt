OpenGL project 1.1:  Creation and renderization of simple objects

Objective: show a spinning cube in the center of the scene, iluminated by one light.

this project shows how to:
-Create and destroy the rendering context
-Create a simple object by specifying its polygons
-use the units uRickGL and u3Dpolys

The OpenGL code in this project has been "hidden" a little behind a small collection of pascal Objects, if you want to know exactly each step needed in order to create manually an OpenGL scene, you can trace the execution of the program, it will go in a very straight way, letting you get to the details with ease.

The main steps needed to create this small animation are:

1. Create the rendering context      			(initRC)
2. redefine the visible volume and the viewport		(UpdateArea)
Now the animation begins:
	3. clear the rendering area			(GlClear)
	4. draw each object				(Tentity.redraw)
		4.1. draw each face 			   (Tface.redraw)
	5. swap the rendering buffers so there is a
	   flickerless animation			(swapbuffers)
	6. rotate objects and go to 3. again.		(Tentity.rotate)
7. finally, release the rendering context		(ReleaseRC)

About OpenGL:
-when using Borland programming tools, you must mask the interruptions, see TsceneGL.redraw to understand this.  (in unit UrickGL)
-OpenGL is completely procedural, the classes I created aren´t necesary to deal with OpenGL
-If you want to see the true naked procedural code, take a look to the OpenGL.zip samples,  there you can learn more about OpenGL true basics.


To be enhanced in this project:
1. light handling:  it´s very poor, I think that a complete new section should be included in the tutorial about lighting.

2. Palette handling: this one is easy, so maybe I will do it myself

4. Note that using the method SetColor, you can specify the color to be used for the object before it is actually created, but once you have included faces, the code won´t change to color of the complete object if you call Tentity.SetColor again.  This could be fixed easily.

Tutorial OpenGL 1.2:  Load and animate a simple mesh, a spinning dolphin

Objective: to show a spinning mesh loaded from a DXF file

This project shows how to:
-use the "LoadDXF" function to read a DXF file and later show it on screen

Problems:
-if the file is not availiable, the program can fail in unpredictable ways.
-no checking is performed on the DXF data, if it is corrupted, the program will behave unexpectedly.
-at least once it popped the message:  access violation at 0xbff349d9: etc, etc. inside the delphi environment, some people comment something about failures with OpenGL inside the Delphi environment, but I haven´t tested the programs to check if this does not happen outside of delphi.


information:
-a DXF file is an ascii file format which can describe 2D images or (as is the case here) 3D images.
 this format is very easy to read, and most 3D design software can generate files with this format.
-The dolphin looks a little (okey, totally) flat, this is not supposed to be a tridimensional image ?.  This happens because the normals to each face hasn´t been specified, we will see how to do this in the next project.
-A good way to create a DXF file is to use Caligari Truespace 3D (version 2 and 3, or maybe even version 1, I am not sure), load or create any object and then save it as a DXF file.  The object will lose all color and textures on it, but this project won´t crash if the object has textures or color previously assigned.  You can use other 3D design software to create the DXF file, this is a very popular 3D file format.


Suggested enhancements:
- What about other formats ?, like 3D studio´s 3DS, you can take a look at the source code of the LoadDXF procedure, and if you know the internal structure of the file to be read you can create your own loading procedure.

- I don´t know how to do it, but you can get the color data from the DXF file and use it to set the color for each individual face of an entity, this way the color information wouldn´t  be lost.
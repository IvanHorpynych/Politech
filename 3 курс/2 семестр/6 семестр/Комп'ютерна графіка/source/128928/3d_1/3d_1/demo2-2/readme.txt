OpenGL tutorial 2.2: saving and loading entities.

Objective: DXF is good for exporting and importing 3D objects with ease, but it is not compact, and is complicated to store extra attributes of objects.  the objective is to create a new 3D binary file format, adapted to my particular needs.  that is to be able to store polygon info, vertex color, texture coordinates (in the future), etc.

In this project I will:
-Create two new methods in Tentity:  Load and Save.

-Show the internal structure of the new file format.

-Include a new file named dolphin.3DO.   it does not contain a dolphin but two joined spheres.  Don´t press the Button "Save Dolphin" before pressing Load Dolphin.
I didn´t include the original DXF file (but you can have it by just Us$ 49.95 plus taxes),
but it was about 170.000 bytes long, and as you can check, the 3DO file is about 45 k and includes info about normal vertices and vertex color, I haven´t done speed testings, but I think that a binary format should be faster to read than the DXF.

-Discover that the LoadDXF algorithm had a little error: it could not read polygons with 4 vertices, I fixed that problem, but another one surfaced: the vertex-coloring algorithm won´t work correctly with objects that have 4 sided polygons.  There are at least three possible solutions:
	1. modify loadDXF so that 4-sided polygons be converted to two triangles, it´s easy
	2. Investigate why is failing the coloring algorithm, I think that the error is
	   in how BUFFER is being read.
	3. Buy a copy of Caligari Truespace (or your favorite 3D objects editor) and 
	   triangulate the objects.  By now this is the solution I will be using.  Anyway
	   my dolphin is already triangulated.

INTERNAL STRUCTURE:
Header
  4 bytes wich always have the letters:   "3DPF"
  1 byte for version big number:	  1
  1 byte for version small number:   .0
  3 bytes for RGB color info on object
  4 bytes for Number of faces
  4 bytes for Number of Points
  4 bytes for Number of textures
  20 bytes reserved for future purposes
Body (Points) Repeated for each point in object
  3 single for x,y,z position
  3 bytes for r,g,b color
Body (Faces) Repeated for each face in object
  3 bytes for r,g,b color
  1 byte for number of vertices for the face
  SubBody (vertices) Repeated for each vertex in face
    3 single for normal x,y,z vector
    4 bytes for Point number in points


Suggestions for Enhacements:
-Since the format of the file can be altered in the future, it would be advisable to continue with Version 1.x until it were not possible to read a newer file with the algorithm version 1.0.  I mean; if it is possible to read a new file which has texturing and new features not supported in version 1.0, but the old algorithm can read the mesh data, then use 1.x as the version number.  If the new format causes an error or incorrect polygon geometry, then the version number should be 2.0, 2.9, 3.1 or something else.

-There is not a save/load method for lights, and what about scenes ?, it would be cool if someone implemented a method wich would reuse the code from Load and Save, (may be using a new boolean parameter named InsideFile, or something like that).  If the Scene save method is created, it should include position, rotation and Id for each object.


516 points
800 faces
 segundo vertice usa point 1


Problems:

Load and Save methods have not any checking code, so if the loaded file is invalid, the program will surely crash.
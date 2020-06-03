OpenGL tutorial 2.3: Using texture mapping with 24 bit BMP files

Objective: Load a Windows Bitmap File (BMP) and use it as a texture.  Use automatic generation of texture coordinates (I don´t like it, but it is very easy to use), and manual texture coordinate specification.

BUGS: automatic texture coordinates generation still does not work

In this project I will:
-put a 256*256 (2^8*2^8) pixels with 24 bit pixel depht BMP file on a triangle as a texture.

-put a BMP file on a rectangle as a texture

-Modify the unit U3dPolys so that a new class is created:  Ttexture.  this class will handle all the work related with Texture mapping.
	-Automatic versus manual texturing
	-texture parameters: mode (Decal, modulate or other), texture filtering (nearest, 
	 linear, other)
	-Texture image data storage
	-in automatic texturing: texture generation parameters

-Modify Tvertex so that it will include info on texture coordinates (Tx,Ty:word).  using word fields will limit textures to 64000 pixels by side.  I think that´s enough.

-Modify TsceneGL to include a new boolean field named "Textures", if it is true then the scene will enable use of 2D textures, if it is false then the scene will not enable texture mapping.  By default is true.

-add a new field "texture" to Tentity, if Texture is nil then the object has no texture, if Texture is not nil then the object has a TTexture object assigned.

-Add a new boolean field "ApplyTexture" to Tface, so that an object can have textured and non-textured faces.

-Learn that there is an option in Project/options/compiler that will enable or disable record alignment.  Record Alignment is okey if you want to optimize your code

-Tell you that if you don´t like to use my objects, just cut and paste the code for ttexture.LoadTexture and ttexture.redraw, it is where almost all of the work is done.

OpenGL Information:
-In order to place a texture on an object or on a part of it, we must tell OpenGL how to wrap the flat (2D) texture over a tridimensional surface.  There are two ways to do this:
	-Automatic generation of texture coordinates:  OpenGL will take the texture and
	 wrap it around the object calculating automatically where each pixel of the texture
	 (texel) will be placed on each polygon's pixels of the object.   Some times OpenGL
	 will have to put a lot of texels on a single pixel, and some other times OpenGL
	 will use a single texel to texture a group of pixels.  That is that in some places
	 the texture will lose some details, and in other places the texture will look
	 stretched.  That is specially true with complex objects with a lot of corners.

	-Manual specification of texture coordinates:  if we could use virtual scissors, and
	 cut pieces on the texture that were similar in shape and size to each of the poly-
	 gons that conform the object and paste those pieces on each polygon, we wouldn´t 
	 have the stretching problems that were present when using automatic generation.
	 Unfortunately, it is a big job to calculate the coordinates for each polygon, and
	 most of the time the texture at the edges of each polygon wouldn´t be the same as
	 their neighbors.

	-The width and height of a texture image must be 2^n (2 raised to the power of n,
	 where n can be any positive integer number), valid values include 2, 4, 8, 16, 32,
	 64, 128, 256, etc.	 

-If a polygon has a texture on it, it will cover the complete polygon, the coordinates for the texture are used by openGL to know which part of the texture will be used to cover the surface of the polygon, but (at least as I see it) there is no way to tell OpenGL to cover a polygon only partially.  Of course you could use a transparent texture with an extra alpha channel.

-In unit1 there are two lines surrounded by {}, they are used to apply Magfilter and Minfilter GL_linear to the textures, by using this filters instead of the GL_nearest default, the drawing will go slower, but que graphics quality will be better, that´s specially true when the object is place very near to the observer.  Using GL_nearest the image gets distorted (I think it is a bug in the OpenGL windows code, what do you think ?).

-Magnification Filter (magFilter) is applied when the texture is smaller than the polygon
(a texel is used to texture a lot of pixels)

-Minification filter (MinFilter) is applied when the polygon is small and the texture is big, so that a group of texels must be used to texture a single pixel.

-I think (that is my opinion) that openGL just hates non triangular polygons: these are my proofs:
	1. Texture with GL_nearest and look very near at the triangle: it does not
	   get deformed.  look at the rectangle: it´s texture is deformed.
	2. turn around the object and look at the colored polygons.  The triangle
	   can have all its corners outside the viewing area but its colors won´t
	   change, now slowly zoom-in the rectangle and you will see a suden change
	   in the color gradient when a corner gets out of sight.

Suggestions for Enhacements:
-the Load and Save methods for entity doesn´t store information about texture coordinates, texture parameters or anything else.  Maybe I will myself modify those methods to include that kind of information.

-Since Tentity has one pointer to a TTexture object, then it is not possible for an entity to have more than one texture, if a programmer would like to create a television set, it could be easier if the screen were a texture (glass or tv show) and the TV case were another texture (wood).  Can you imagine a better architecture to use with textures ?.

-Write some code that enables the use of BMP files of any width and height.

-Although I won´t ever worry about this one, may be you would like to add the hability to read 256 color BMP images as textures.

UNRESOLVED QUESTIONS:
-I am using display lists so that I don´t have to store the image in the TTexture object, but what happens if the method LoadTexture is called a lot of times ?, is there a memory leak ?, or the memory for the last texture is automatically disposed ?


Polygon info:
Face:1  outer rectangle
  vertex:0  x,y,z=2,0,2  tx,ty=1,0
  vertex:1  x,y,z=0,0,2  tx,ty=0,0
  vertex:2  x,y,z=0,0,0  tx,ty=0,1
  vertex:3  x,y,z=2,0,0  tx,ty=1,1
Face:1  outer triangle
  vertex:0  x,y,z=0,0,2  tx,ty=1,0
  vertex:1  x,y,z=0,2,0  tx,ty=0,1
  vertex:2  x,y,z=0,0,0  tx,ty=1,1  
Face:3  inner Triangle
  vertex:0  x,y,z=0,0,0  tx,ty=1,0
  vertex:1  x,y,z=0,2,0  tx,ty=0,0
  vertex:2  x,y,z=0,0,2  tx,ty=1,1

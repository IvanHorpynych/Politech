OpenGL tutorial 2.1: on how to set the color of each individual vertex of a mesh

Objective: using the mouse, click any vertex of a 3D object and make it´s color change.

In this project I will:
-enable clicking on the rendering window and obtain the nearest vertex under the mouse

-Use the FeedBack "rendering" mode, to obtain the transformed vertex coordinates

-Introduce a new field in Class Tentity, this id number enables us to give a "name" to 
 a certain entity (or a group of them) so that when using the FeedBack mode, we can detect
 where the information about a particular object starts, and ignore info about other  
 entities.

-Insert code for event: Form.resize.  There you can see what I did to respond to this event.

-add a new field Owner to Tface so that it can know which entity is its owner

-add a new field Owner to Tvertex so that it can know which face is its owner

-add a new field to TsceneGL which will allow to choose between orthogonal and perspective projection.  By default it will be perspective projection.

-add a new function SetPerspective to handle values for Field-of-view angle, distance to near clipping plane and distance to far clipping plane.  See picture bounding.bmp

-If you want to change the color for a group of vertices sharing a Tpoint you can call:
	Vertex.point.setColor(red,green,blue)
If you want to change the color for an individual vertex, regardless of the other vertices sharing the point, call:
	vertex.setColor(red,green,blue)
what the vertex will do is create a new Tpoint so that the vertex can have its own color and position.  (this also is done for SetPosition).
 This way, if you want to have an object with continuous color gradations, you can have it, but if you want to color each face by separate, you can do it too.
 This architecture also enables the creation of explosions, by moving the vertices away from the center.  (don´t move the Tpoints away from the center, that would enlarge the object, not blow it away).

-assign a different color to each individual vertex of an object.

-discover that the use of:
     glMaterialfv(GL_FRONT_and_back, GL_AMBIENT_AND_DIFFUSE, @Color);
 is not the best way to set the color properties of an object, so I use now:
     glenable(gl_color_material);
 which when called, tells OpenGL to begin paying attention to GlColor commands.

-Remember that the vertical axis in OpenGL windows grows upwards, not downwards, as occurs
in normal windows graphics.  so position 640,480 is not down to the right, but up to the
right.  Forgetting this had me debugging for more than three continuous hours.

-the use of the FeedBack rendering mode returns information only about visible polygons, so if a polygon is facing away from the viewer, it will not matter if any of its vertices is 
near to the mouse, it won´t be checked, that is why so important to have well oriented the
normals of an object.

-Discover that the polygon winding on my objects was reversed, so modifying a little my load DXF routine did the trick, now there is no need of using double the number of polygons to correctly render my dear dolphin.  that is if you have Caligari truespace 2 or 3, it will convert and correctly wind all the polygons of your objects.

How to enhance this project:
-To be able to draw a rectangle and select all vertices inside it, so that the color modification affects a group of vertices and not only one.

-it would be great if someone can find out why without the Try..except block a floating point error occurs in method T3Dmouse.GetVertices.

-The radio constant used in Traton3D.ObtenerVertice could be variable so that you could choose the level of precision.

-the coloring of the vertices is not completey correct in three different ways:
	2. if the object is not completely visible in the viewing area, the coloring
	   algorithm will fail most of the time.  I don´t know why this happens.
	3. Sometimes, specially with complex objects, an user can click on a vertex and some
	   barely visible vertex below also gets clicked, and two distant parts of the
	   object are painted.  may be using the 3D mode of FeedBack rendering would allow
	   a good programmer to make the program more precise.

- The coloring algorithm could be enhanced so that there were also not only color replacing
  but tint, darkening, etc, etc.


Comments:
1. a pair formed by a vertex and a Point is used by Tlight so that it  can (in the future) have orientation and position.

2. the mouse 3D modes all work now.

3. enhace the project so that there is a way to drag the dolphin no matter the distance.  As it is now the dolphin slides under the mouse pointer in an annoying way.

4. the minimalDistance constant can be a way of minimizing detail, try loading dolphin with minimaldistance at 0.1.   But the dolphin can lose big detail with 0.5.
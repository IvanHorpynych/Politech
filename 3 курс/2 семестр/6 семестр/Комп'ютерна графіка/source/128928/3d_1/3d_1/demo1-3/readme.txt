OpenGL tutorial 1.3: Normals generation for a mesh

Objective: obtain normal vectors for each of the polygons that compose a tridimensional object.

This project shows how to:
-find the normal unitary vector for any flat polygon.

Problems:
-if the file is not availiable, the program can fail in unpredictable ways.
-no checking is performed on the DXF data, if it is corrupted, the program will behave unexpectedly.
-the orientation of the normal vectors found is always perpendicular to the polygon's surface, but depending on the polygon's winding (counterclockwise versus clockwise), the normal vector will point outwards or inwards the object's center.


Suggestions for enhancements:
-Develop some algorithm that would find the correct direction of the normal vector for each polygon of an object, even if it is not correctly winded.  The algorithm would have to find out what is outside the entity and wha is inside, and based on that information, wind the polygons correctly.
-Include a new algorithm which would reorient the normal vectors so that the curved areas seemed softer.  (see picture soft.bmp) in this folder.

Information:
-Polygon winding (is this the correct wording ?) refers to the way the polygon was created, if the vertices of the polygon were specified on a counterclockwise direction then the polygon is said to be counterclockwise winded.  In school I learned about the "right hand rule", wich can tell you were the normal should be pointing according to the direction of the winding:  turn your right hand around your thumb so that your other fingers follow the direction of creation of the polygon, then the thumb will be pointing in the direction of the normal vector, so if the thumb is pointing upwards it will be a counterclockwise winding, and if the thumb points downwards it will be a clockwise winding.
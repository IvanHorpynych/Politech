OpenGL Tutorial 1.6: Miscelaneous things done with OpenGL
	a. Printing an OpenGL image
	b. Colored background
	c. Calculating the frames per second

Objectives: a. To send to the printer an OpenGL image
	    b. to specify the color of the background
	    c. to obtain the speed at which the computer
	       redraws the OpenGL scene.

---------------
A. Print OpenGL images.

This project was inspired in a question made by Al Broulliete.

What we do in this example is to use the form´s canvas instead of the
panel´s canvas and then do a copyRect to the printer´s canvas.

---------------
B. Background´s color.

This one is very easy.  A new OpenGL instruction "GlClearColor" is
used before clearing the OpenGL display area.  (see TsceneGLredraw
for the source)
---------------
C. Frames per second.

this is the commented source code:

In the panel1.mouseMove event we add this instruction to count each
time a new frame is redrawn:

  frames:=frames+1;  {a new frame has been drawn}


And two new event handlers were created:

One for when the user starts moving the dolphin:

	procedure TForm1.Panel1MouseDown
	begin
	  time1:=time;  {get initial time}
	  frames:=0;    {and reset the frame counter}
	end;

And other for when the user stops:

	procedure TForm1.Panel1MouseUp;
	var
	  difference:Tdatetime;
	  hour,minute,second,mseconds:word;
	  miliseconds:longint;
	  Frames_Per_Second,floFrames,FloMSec:double;
	begin
	  time2:=time;    {get the final time}
	  {frames per second are calculated here}
	  difference:=time2-time1;   {obtain the difference between times}
	  decodetime(difference,hour,minute,second,mseconds);
	  miliseconds:=mseconds+longint(second)*1000+longint(minute)*60000;
	  {I ignored hour and minute, I hope you don´t mind}
	  floFrames:=frames;   {convert integers to floats}
	  FloMSec:=miliseconds;  {elapsed miliseconds}
	  Frames_Per_Second:=FloFrames/(FloMSec/1000);
	  label4.caption:=FloatToStrF(Frames_Per_Second, ffGeneral, 4,2);
	end;

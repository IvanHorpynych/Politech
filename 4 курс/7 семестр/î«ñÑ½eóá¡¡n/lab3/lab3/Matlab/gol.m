function gol(imageStr,delay)
%GOL   Cedric Zoppolo's version 2.5.1 of the Game of life - Juego de la vida
%________________________________________________________________________________
%________________________Game of life - Juego de la vida_________________________
%________________________________________________________________________________
%
%   Title : gol
% Version : 2.5
%Platform : from Matlab 5.3.1 (R11.1) to Matlab 7 (R14)
%    Date : 19 february 2004
%Location : Montevideo, Uruguay, South America
%  Author : Cedric Zoppolo
% Contact : cedriczg@gmail.com
%Web site : http://cedriczg.50webs.com
%________________________________________________________________________________
%__Posible ways to execute the game of life :
%
%  gol
%  gol(imageStr)
%  gol(imageStr,delay)
%
%  imageStr : Starting image. 
%    - It must be a string with the name of the image followed by its extension.
%    - The image can be in black and white, in gray tones or in colors.
%    - If no root is specified the image must be in a directory of the setpath.
%  delay : Time in seconds between each image of the animation. 
%    _ It is 0 by default.
%    _ If "delay" is inf (infinite) there is a pause between each image
%      and you continue the animation pressing any key or clicking the mouse.
%________________________________________________________________________________
%__Toolbar :
%
%  View : 
%    Axes : Allows to show or hide the axes.
%  Delay : 
%    Increase(*): Increases the dalay in "variation delay" seconds.
%    Decrease(*) : Decreases the dalay in "variation delay" seconds.
%    Variation delay : Allows to change the value of "Variation deelay".
%    Delay(*) : Allows to change the value of the delay between each image.
%  Zoom : 
%    Enable Zoom : Enables or disables the zoom. With the left click it
%      increases the zoom and it decreases it with the rigth click. Dragging the
%      mouse it does a zoom over the desired area.
%      When the zoom is enable the image is erased in each iteration so that
%      when the zoom is decresed the image stays well.
%    Initial zoom : It goes to the initial zoom of the animation.
%  Animation :
%    New(*) : Allows to create a new animation from zero.
%    Initial image(*) : Loads a file as initial image of the animation.
%    Rewind(*) : rewinds the animation to the starting point.
%    Play(*) : plays the animation.
%    Pause(*) : Pauses or unpauses the animation. 
%    Stop(*) : Stps the animation.
%    Record(*) : Allows to star recording an animation.
%    Edit(*) : Allows to edit the animation adding (left click) and/or 
%      deleting cells (rigth click)
%    Parameters : Allows to change the parameters for the animation, changing
%      this way the rules for the life of the cells. 
%	Help : 
%    Display help on the "Command Window" : Displays the help of "gol" on the 
%      "Command Window".
%	  About gol : Message about the Game of life.
%  Exit : Exits the program.
%
%  (*) These functions are also available in the buttons at the bottom of the
%  window. 
%________________________________________________________________________________
%__Notes :	
%
%  - To increse or deecrease the delay it is useful to keep pressed the quick way
%    Ctrl+k (to increase or Ctrl+j (to decrease).
%  - Instead of using the zoom it can be useful to change the size of the window. 
%  - The program can be accelerated by reducing the size ot the window (it is
%    only necessary to reducee the vertical size).
%  - The program is faster with black and white images or with gray scaled images
%    than with colored images.
%  - The animation stops automatically when there are no more changes in the 
%    cells.
%
%________________________________________________________________________________
%__Rules for the life of the cells :
%
%  Using the negative of the original image and suposing that a cell (or pixel)
%  totally alive is a 255 (black in the original picture) and a dead cell is a 0
%  (white in the original picture) :
%  - A cell borns if it has in its surroundings more than 2 cells totally
%    alive and less than or equal to 3 cells totally alive.
%  - A cell survives if it has in its surroundings more than 1 cell totally alive 
%    and less than or equal to 2 cells totally alive.
%
%  These rules are applied directly to an image tha is not colored.
%  In the case that the image is in colors it is the same as not in colors but 
%  with the difference that there are "3 worlds" (the magenta, the cyan and the 
%  yewllow) that live separately.

warning off
pause on

global ejesHndl colorFondo...
   imgPathStr imgStr  imgHndl imgBtn gris rgb bits...
	Cels CelsIni animNueva CelsAnt imgCels golAxes iters itersTxt...
   demora  demoraBtn variacionDemora...
 	inicio reinicio pausa ejec grabar salir estadoTxt pausaTxt...
	nomAnimHndl numIniAnimHndl extAnimHndl...
   nomAnimStr extAnimStr numAnimStr numAnim exts...
   reiniciarHndl reiniciarBtn ejecutarHndl ejecutarBtn pausaHndl pausaBtn detenerHndl...
   detenerBtn grabarHndl grabarBtn editarHndl editarBtn zoomActivoHndl zoomInicialHndl...
   parNacen1 parNacen2 parViven1 parViven2
   
parNacen1 = 2;
parNacen2 = 3;
parViven1 = 1;
parViven2 = 2;


if nargin < 2, demora = 0;
else, demora = delay;end

colorFondo = [245,242,240]./255;
golFig = figure('color',colorFondo,'NumberTitle','off','MenuBar','figure',...
   'name','Game of life - Juego de la vida','CloseRequestFcn','presiona(''salir'')');

verHndl = uimenu(golFig,'Label','&View','Separator','on');
   ejesHndl = uimenu(verHndl,'Label','&Axes','Callback','presiona(''ejes'')');


demoraHndl = uimenu(golFig,'Label','&Delay');
   uimenu(demoraHndl,'Label','&Decrease','Callback','presiona(''demora-'')','Accelerator','j');
   uimenu(demoraHndl,'Label','&Increase','Callback','presiona(''demora+'')','Accelerator','k');
   uimenu(demoraHndl,'Label','&Variation delay','Callback','presiona(''variacionDemora'')','Separator','on');
   uimenu(demoraHndl,'Label','D&elay','Callback','presiona(''demora'')');
    
zoomHndl = uimenu(golFig,'Label','&Zoom');
	zoomActivoHndl = uimenu(zoomHndl,'Label','&Enable zoom',...
      'Callback','presiona(''zoomActivo'')','Accelerator','l');
	zoomInicialHndl = uimenu(zoomHndl,'Label','&Initial Zoom','Callback','presiona(''zoomInicial'')','Separator','on');
   
animHndl = uimenu(golFig,'Label','&Animation');
	uimenu(animHndl,'Label','&New','Callback','presiona(''nueva'')');  
	uimenu(animHndl,'Label','&Initial image','Callback','presiona(''imagen'')');  
   reiniciarHndl = uimenu(animHndl,'Label','&Rewind','Callback','presiona(''reiniciar'')','Accelerator','r','Separator','on');
   ejecutarHndl = uimenu(animHndl,'Label','&Play','Callback','presiona(''ejecutar'')','Accelerator','t');
	pausaHndl = uimenu(animHndl,'Label','P&ause','Callback','presiona(''pausa'')','Accelerator','y');
   detenerHndl = uimenu(animHndl,'Label','&Stop','Callback','presiona(''detener'')','Accelerator','u');
  	grabarHndl = uimenu(animHndl,'Label','Re&cord','Callback','presiona(''grabar'')','Separator','on');     
  	editarHndl = uimenu(animHndl,'Label','&Edit','Callback','presiona(''editar'')');     
   parametrosHndl = uimenu(animHndl,'Label','Para&meters','Callback','presiona(''parametros'')');  
     
ayuda = uimenu(golFig,'Label','&Help');   
	uimenu(ayuda,'Label','&Display help on the "Command Window""','Callback','help gol');
	uimenu(ayuda,'Label','&About gol','Callback','acerca','Separator','on');
   
uimenu(golFig,'Label','E&xit','Callback','presiona(''salir'')');

inicio = 1;
reinicio = 0;
animNueva = 0;
grabar = 0;  
exts = {'*.bmp','*.jpg','*.jpeg','*.tif','*.tiff','*.png','*.hdf','*.pcx','*.xwd'};
variacionDemora = 0.01;
iters = 0;
pausa = 0;
ejec = 0;
salir = 0;


reiniciarBtn = uicontrol('style','pushbutton','string','<<',...
   'FontWeight','bold','position',[3 19 17 14],'ForegroundColor',[0 0 120/255],...
   'TooltipString','Rewind','Callback','presiona(''reiniciar'')');
ejecutarBtn = uicontrol('style','pushbutton','string','>',...
   'FontWeight','bold','position',[22 19 17 14],'ForegroundColor',[0 100/255 0],...
   'TooltipString','Play','Callback','presiona(''ejecutar'')');
pausaBtn = uicontrol('style','pushbutton','string','||',...
   'FontWeight','bold','position',[41 19 17 14],'ForegroundColor',[0 100/255 0],...
   'TooltipString','Pause','Callback','presiona(''pausa'')');
detenerBtn = uicontrol('style','pushbutton  ','string','X',...
   'FontWeight','bold','position',[60 19 17 14],'ForegroundColor',[150/255 110/255 0],...
   'TooltipString','Stop','Callback','presiona(''detener'')');
grabarBtn = uicontrol('style','pushbutton','string','O',...
   'FontWeight','bold','position',[79 19 17 14],'ForegroundColor',[180/255 0 0],...
   'TooltipString','Record','Callback','presiona(''grabar'')');
editarBtn = uicontrol('style','pushbutton','string','Edit',...
   'position',[99 19 51 14],...
   'TooltipString','Edit the animation adding (left click) and/or deleting cells (rigth click)','Callback','presiona(''editar'')');
nuevaBtn = uicontrol('style','pushbutton','string','New',...
   'position',[152 19 51 14],...
   'TooltipString','New animation','Callback','presiona(''nueva'')');
pausaTxt=uicontrol('style','text',...
    'HorizontalAlignment','left','position',[347 19 207 14],'Visible','off');

imgBtn = uicontrol('style','pushbutton','HorizontalAlignment','left',...
   'position',[3 2 200 15],'string','Initial image : ',...
   'TooltipString','Load a file to use as initial image of the animation',...
   'Callback','presiona(''imagen'')');
demoraBtn = uicontrol('style','pushbutton','HorizontalAlignment','left',...
   'position',[205 2 100 15],'TooltipString','Delay in seconds between each image shown',...
   'Callback','presiona(''demora'')');
demoramenosBtn = uicontrol('style','pushbutton','string','-','HorizontalAlignment','center',...
   'position',[307 2 17 15],'TooltipString','Decrease delay',...
   'Callback','presiona(''demora-'')');
demoramenosBtn = uicontrol('style','pushbutton','string','+','HorizontalAlignment','center',...
   'position',[326 2 17 15],'TooltipString','Increase delay',...
   'Callback','presiona(''demora+'')');
itersTxt = uicontrol('style','text','HorizontalAlignment','left',...
   'position',[347 2 100 15],...
  	'TooltipString','Number of the iteration in the animation');
estadoTxt = uicontrol('style','text','HorizontalAlignment','left',...
   'position',[449 2 105 15],...
   'TooltipString','Status of the animation');   

set(reiniciarHndl,'enable','off');
set(reiniciarBtn,'enable','off');
set(ejecutarHndl,'enable','off');
set(ejecutarBtn,'enable','off');
set(pausaHndl,'enable','off');
set(pausaBtn,'enable','off');
set(detenerHndl,'enable','off');
set(detenerBtn,'enable','off');
set(grabarHndl,'enable','off');
set(grabarBtn,'enable','off');
set(grabarHndl,'enable','off');
set(grabarBtn,'enable','off');
set(editarHndl,'enable','off');
set(editarBtn,'enable','off');
set(zoomActivoHndl,'enable','off');
set(zoomInicialHndl,'enable','off');

    
actualizar('demora',demora);
actualizar('iters',iters);

golAxes = axes('Visible','off');

if nargin>0
   imgPathStr='';
   imgStr = imageStr;   
   cargarImg(imgPathStr,imgStr);
end

CelsAnt=[];

while ~salir 
   while ejec
      if demora ~= inf;pause(demora);end
      while pausa;pause(0.1);end         
      if ejec
      if grabar      	      
         if numAnim<10
            numAnimStr = strcat('000',numAnimStr);
         else 
            if numAnim<100
               numAnimStr = strcat('00',numAnimStr);
            else 
               if numAnim<1000
                  numAnimStr = strcat('0',numAnimStr);
               end      
            end
         end
         grabarStr = strcat(imgPathStr,nomAnimStr,numAnimStr,extAnimStr);    
         if rgb == 1
            imwrite(imgCels,gris,grabarStr);            
         else   
            imwrite(imgCels,grabarStr);
         end
         numAnim = numAnim+1;
      	numAnimStr = num2str(numAnim);      	   
      end
      
      CelsAnt = Cels;
      if rgb==3
         Cels(:,:,1) = vida(CelsAnt(:,:,1));
         Cels(:,:,2) = vida(CelsAnt(:,:,2));
         Cels(:,:,3) = vida(CelsAnt(:,:,3));
      else
         Cels = vida(CelsAnt);
      end
      imgCels = uint8(255-Cels);
      set(imgHndl,'CData',imgCels)
      if rgb ==1        
      	colormap(gris)
      end
      iters = iters + 1;
      actualizar('iters',iters); 
      
      if demora == inf
			set(pausaTxt,'Visible','on','string',' Press any key to continue')          
         pause
			set(pausaTxt,'Visible','off')
      end
      
      % La animación se detiene si las "células" no cambian   
      if isequal(Cels,CelsAnt)
         ejec = 0;
         grabar = 0;
         actualizar('estado',[pausa,ejec,grabar]);
      end
      end
   end
   pause(0.1)
end
closereq

%_______________________________________________________________________________________

function acerca
helpdlg({'__________________________________________________',...
			'_____________Game of life - Juego de la vida_____________',...
			'__________________________________________________',...
         ' ',...
         '      Title : gol',...
         'Version : 2.5',...
         'Platform : Matlab 5.3.1 (R11.1)',...
			'      Date : 19 february 2004',...
			'Location : Montevideo, Uruguay, South America',...
			'   Author : Cedric Zoppolo',...
			'  Contact : cedriczg@adinet.com.uy',...
			'Web site : http://usuarios.lycos.es/cedriczg'})

%_______________________________________________________________________________________

function cargarImg(imgPath,imgStr)
global ejesHndl colorFondo...
   imgPathStr imgHndl imgBtn gris rgb bits ...
	Cels animNueva CelsIni filas cols imgCels golAxes iters itersTxt ...
 	inicio reinicio pausa ejec grabar estadoTxt...
   reiniciarHndl reiniciarBtn ejecutarHndl ejecutarBtn pausaHndl pausaBtn detenerHndl...
   detenerBtn grabarHndl grabarBtn editarHndl editarBtn zoomActivoHndl zoomInicialHndl


try
   if reinicio
      Cels = CelsIni;
   else  
      if animNueva
         bits = 1;
         imgPathStr = '';
         Cels = CelsIni;
      else
         info = imfinfo(strcat(imgPath,imgStr));	
         % Si no encuentra la imagen lo que sigue en try no se ejecuta
         bits = getfield(info,'BitDepth');
         imgCels = imread(imgStr);
         Cels = double(imgCels);
         reinicio==1;
         if bits == 1 , Cels = 255.*Cels; end
      end
   end
   
   int = (0:255)';
   gris = [int,int,int]./255; 
   
   iters = 0;
   ejec = 1;
   actualizar('iters',iters);
   actualizar('imgStr',imgStr);
   actualizar('estado',[pausa,ejec,grabar]);
   
   [filas cols rgb] = size(Cels);
   
   imgCels = uint8(Cels);
   CelsIni = Cels;
   Cels = 255 - Cels;		% utilizo el negativo de la imagen para los cálculos
   
   delete imgHndl
   set(golAxes,'NextPlot','replace')
   
   imgHndl = image(imgCels);
   if strcmp(get(zoomActivoHndl,'checked'),'off')
      set(imgHndl,'EraseMode','none');
   else
      set(zoomActivoHndl,'checked','on')
   end
   
   axis equal
   set(golAxes,'DrawMode','fast','NextPlot','add','color',colorFondo)
   
   if strcmp(get(ejesHndl,'checked'),'on')
      set(golAxes,'Visible','on')
   else
      set(golAxes,'Visible','off')
   end
   if rgb ==1
      colormap(gris)
   end
   
   ejec = 0;
   actualizar('estado',[pausa,ejec,grabar]);
   set(reiniciarHndl,'enable','on');
   set(reiniciarBtn,'enable','on');
   set(ejecutarHndl,'enable','on');
   set(ejecutarBtn,'enable','on');
   set(pausaHndl,'enable','on');
   set(pausaBtn,'enable','on');
   set(detenerHndl,'enable','on');
   set(detenerBtn,'enable','on');
   set(grabarHndl,'enable','on');
   set(grabarBtn,'enable','on');
   set(editarHndl,'enable','on');
   set(editarBtn,'enable','on');
   set(zoomActivoHndl,'enable','on');
   set(zoomInicialHndl,'enable','on');
   
   
catch
   errordlg({strcat(imgStr,' not found'),' ',...
         'Verify that the name has a valid extension'},...
      'Error - Image not found')
end

%_______________________________________________________________________________________

function presiona(funcion)
global imgPathStr imgStr imgHndl rgb ...
	Cels animNueva CelsIni filas cols golAxes iters ...
	ejesHndl zoomActivoHndl colorFondo...
   demora  demoraBtn variacionDemora...
 	reinicio inicio pausa ejec grabar salir estadoTxt pausaTxt...
   grabarBtn grabarHndl nomAnimHndl numIniAnimHndl extAnimHndl...
   parNacen1Hndl parNacen2Hndl parViven1Hndl parViven2Hndl... 
	parNacen1 parNacen2 parViven1 parViven2...
   nomAnimStr extAnimStr numAnimStr exts...

AddOpts.Resize='off';
AddOpts.WindowStyle='modal';
AddOpts.Interpreter='none';

switch funcion
case 'ejes',
   if strcmp(get(ejesHndl,'checked'),'on')
      set(ejesHndl,'checked','off')
      set(golAxes,'Visible','off')
   else
      set(ejesHndl,'checked','on')
      set(golAxes,'Visible','on')
   end
   
case 'demora-',
   if demora>variacionDemora, demora = demora-variacionDemora; 
   else demora = 0 ; end
   actualizar('demora',demora)
case 'demora+',
   demora = demora+variacionDemora;
   actualizar('demora',demora)
case 'variacionDemora',
   resp = inputdlg('Variation of the delay between each image (in seconds) : ',...
      'Variation delay',1,{num2str(variacionDemora)},AddOpts);
   if ~isempty(resp)
      if ~isempty(str2num(resp{1})), variacionDemora=str2num(resp{1}); end
   end   
case 'demora',
   resp = inputdlg('Delay between each image (in seconds) belonging to the range [0,inf] : ',...
      'Delay',1,{num2str(demora)},AddOpts);
   if ~isempty(resp)
      if ~isempty(str2num(resp{1}))
         demora=str2num(resp{1});
         actualizar('demora',demora)   
      end
   end
case 'zoomActivo',
   zoom
   if strcmp(get(zoomActivoHndl,'checked'),'on')
      set(zoomActivoHndl,'checked','off')
      set(imgHndl,'EraseMode','none');
   else
      set(zoomActivoHndl,'checked','on')
      set(imgHndl,'EraseMode','normal');
   end
case 'zoomInicial',
   set(imgHndl,'EraseMode','normal');
   zoom out
   if strcmp(get(zoomActivoHndl,'checked'),'off')
      set(imgHndl,'EraseMode','none');
   end
case 'nueva',
   resp = inputdlg({'With (in pixels) :','Height (in pixels) :'},...
      'New animation',1,{'50','50'},AddOpts);
   if ~isempty(resp)
      if ~isempty(str2num(resp{1})) & ~isempty(str2num(resp{2}))
         cols=str2num(resp{1}); 
         filas=str2num(resp{2}); 
         imgStr = 'new.bmp';
         imgPathStr = '';
         animNueva = 1;
         inicio = 1;
         reinicio = 0;
         CelsIni = 255+zeros(filas,cols);
         cargarImg(imgPathStr,imgStr);
      end
   end
case 'imagen',
   v=sscanf(version,'%f');v=v(1);
   %if v<6.5
      [imgSt,imgPathSt] = uigetfile('*.bmp; *.jpg; *.jpeg; *.tif; *.tiff; *.png; *.hdf; *.pcx; *.xwd',...
         'Initial image');
   %else   
   %   [imgSt,imgPathSt] = uigetfile(exts,'Initial image');
   %end     
   if imgSt
      animNueva = 0;
      reinicio = 0;
      inicio = 1;
      imgStr = imgSt;
      imgPathStr = imgPathSt;
      cargarImg(imgPathStr,imgStr);       
   end         
case 'reiniciar'
   ejec = 0;
   reinicio = 1;
   cargarImg(imgPathStr,imgStr);
   actualizar('estado',[pausa,ejec,grabar]);
case 'ejecutar',
   reinicio = 0;
   inicio = 0;
   ejec = 1;
   pausa = 0;
   actualizar('estado',[pausa,ejec,grabar]);
case 'pausa',
   pausa = not(pausa);
   actualizar('estado',[pausa,ejec,grabar]);
case 'detener',
   pausa = 0;
   ejec = 0;
   if grabar         
      grabar = 0;
      set(grabarHndl,'enable','on');
      set(grabarBtn,'enable','on');
   end 
   actualizar('estado',[pausa,ejec,grabar]);
case 'grabar',
   pausa=1;
   grabarFig = set(figure,'windowstyle','modal','NumberTitle','off',...
      'name','Record animation','color',colorFondo,'Resize','off',...
      'position',[240 240 422 88],'CloseRequestFcn','aceptarGrabar(0);')
   uicontrol('style','text','position',[10 60 80 26],'BackgroundColor',colorFondo,...
      'string','Name of the animation : ','HorizontalAlignment','left')
   uicontrol('style','text','position',[10 33 80 26],'BackgroundColor',colorFondo,...
      'string','Number of the first image : ','HorizontalAlignment','left')
   uicontrol('style','text','position',[10 7 80 26],'BackgroundColor',colorFondo,...
      'string','                       Save as : ','HorizontalAlignment','left')
   [nom,ext]=strtok(imgStr,'.');
   nomAnimHndl = uicontrol('style','edit','position',[105 57 225 20],...
      'string',nom,'BackgroundColor',[1 1 1],...
      'HorizontalAlignment','left');
   numIniAnimHndl = uicontrol('style','edit','position',[105 32 225 20],...
      'string',num2str(iters),'BackgroundColor',[1 1 1],...
      'HorizontalAlignment','left');
   extAnimHndl = uicontrol('style','popupmenu','position',[105 7 225 20],...
      'string',exts,...
      'BackgroundColor',[1 1 1]);
   % elije la misma extensión que la imagen inicial 
   set(extAnimHndl,'value',strmatch(strcat('*',ext),exts)) 
   aceptarGrabarHndl = uicontrol('style','pushbutton','position',[340 32 70 20],...
      'string','Record','Callback','aceptarGrabar(1);');
   cancelarGrabarHndl = uicontrol('style','pushbutton','position',[340 7 70 20],...
      'string','Cancel','Callback','aceptarGrabar(0);');
case 'editar',
   btn = 1;
   i = 1;
   set(pausaTxt,'Visible','on','string',' Press any key to finish editing')
   while btn==1 | btn==3
      [xEdit,yEdit,btn] = ginput(1);
      xEdit = round(xEdit);
      yEdit = round(yEdit);
      if xEdit>0.5 & yEdit>0.5 & xEdit<cols+0.5 & yEdit<filas+0.5 & ~isempty(btn)
         switch btn
         case 1,
            if rgb==3
               Cels(yEdit,xEdit,:) = 255;
            else                  
               Cels(yEdit,xEdit) = 255;
            end
         case 3,
            if rgb==3
               Cels(yEdit,xEdit,:) = 0;
            else                  
               Cels(yEdit,xEdit) = 0;
            end
         end
      end
      imgCels = uint8(255-Cels);
      set(imgHndl,'CData',imgCels)
   end
   if demora==inf & ejec
      set(pausaTxt,'string',' Press any key to continue')
   else   
      set(pausaTxt,'Visible','off')
   end
   if reinicio | inicio;
      CelsIni = 255-Cels;
   end 
case 'parametros'
   pausa=1;
   grabarFig = set(figure,'windowstyle','modal','NumberTitle','off',...
      'name','Change parameters of the animation','color',colorFondo,'Resize','off',...
      'position',[240 240 432 140],'CloseRequestFcn','aceptarParametros(0);')
   uicontrol('style','text','position',[10 110 422 20],'BackgroundColor',colorFondo,...
      'string','A cell borns if it has in its surroundings more than             cells totally alive','HorizontalAlignment','left')
   uicontrol('style','text','position',[10 84 422 20],'BackgroundColor',colorFondo,...
      'string','and less than or equal to             cells totally alive.',...
      'HorizontalAlignment','left')
   uicontrol('style','text','position',[10 58 422 20],'BackgroundColor',colorFondo,...
      'string','A cell survives if it has in its surroundings more than             cells totally alive','HorizontalAlignment','left')
   uicontrol('style','text','position',[10 32 422 20],'BackgroundColor',colorFondo,...
      'string','and less than or equal to             cells totally alive.',...
      'HorizontalAlignment','left')

   parNacen1Hndl = uicontrol('style','edit','position',[245 114 30 20],...
      'string',num2Str(parNacen1),'BackgroundColor',[1 1 1],...
      'HorizontalAlignment','left');
   parNacen2Hndl = uicontrol('style','edit','position',[130 88 30 20],...
      'string',num2Str(parNacen2),'BackgroundColor',[1 1 1],...
      'HorizontalAlignment','left');
   parViven1Hndl = uicontrol('style','edit','position',[257 62 30 20],...
      'string',num2Str(parViven1),'BackgroundColor',[1 1 1],...
      'HorizontalAlignment','left');
   parViven2Hndl = uicontrol('style','edit','position',[130 36 30 20],...
      'string',num2Str(parViven2),'BackgroundColor',[1 1 1],...
      'HorizontalAlignment','left');
   defectoParametrosHndl = uicontrol('style','pushbutton','position',[350 32 70 20],...
      'string','Default','Callback','aceptarParametros(2);');
   aceptarParametrosHndl = uicontrol('style','pushbutton','position',[275 7 70 20],...
      'string','OK','Callback','aceptarParametros(1);');
   cancelarParametrosHndl = uicontrol('style','pushbutton','position',[350 7 70 20],...
      'string','Cancel','Callback','aceptarParametros(0);');
case 'salir',
   salir = 1;ejec = 0;pausa = 0;
   
end

%_______________________________________________________________________________________
function aceptarGrabar(aceptar)
global  estadoTxt imgPathStr imgStr imgHndl reinicio grabar pausa ejec...
	grabarBtn grabarHndl nomAnimHndl numIniAnimHndl extAnimHndl...
   	 nomAnimStr extAnimStr numAnimStr numAnim exts...


if aceptar
   inicio = 0;
   reinicio = 0;
   nomAnimStr = get(nomAnimHndl,'string');
   numAnimStr = get(numIniAnimHndl,'string');
   numAnim = str2num(numAnimStr);
   extAnimStr = exts{get(extAnimHndl,'value')};
   extAnimStr = extAnimStr(2:length(extAnimStr));
   closereq
   set(grabarHndl,'enable','off');
   set(grabarBtn,'enable','off');
   ejec=1;
   pausa = 0;
   grabar = 1;         
   actualizar('estado',[pausa,ejec,grabar]);
else
   closereq
   if ~strcmp(get(estadoTxt,'string'),' Animation in pause')
      pausa=0;
   end
end

%_______________________________________________________________________________________
function aceptarParametros(aceptar)
global  pausa estadoTxt	parNacen1 parNacen2 parViven1 parViven2...
   parNacen1Hndl parNacen2Hndl parViven1Hndl parViven2Hndl...
	parNacen1 parNacen2 parViven1 parViven2

switch aceptar
case 0,   
   closereq
	if ~strcmp(get(estadoTxt,'string'),' Animation in pause')
   pausa=0;
	end   
case 1,
   if ~isempty(str2num(get(parNacen1Hndl,'string')))
      parNacen1 = str2num(get(parNacen1Hndl,'string'));
   end
   if ~isempty(str2num(get(parNacen2Hndl,'string')))
      parNacen2 = str2num(get(parNacen2Hndl,'string'));
   end
   if ~isempty(str2num(get(parNacen1Hndl,'string')))
      parViven1 = str2num(get(parViven1Hndl,'string'));   
   end
   if ~isempty(str2num(get(parViven1Hndl,'string')))
      parViven2 = str2num(get(parViven2Hndl,'string'));   
   end
   closereq
   if ~strcmp(get(estadoTxt,'string'),' Animation in pause')
   pausa=0;
end

case 2,
   set(parNacen1Hndl,'string','2');
   set(parNacen2Hndl,'string','3');
   set(parViven1Hndl,'string','1');
   set(parViven2Hndl,'string','2');
end


%_______________________________________________________________________________________

function actualizar(funcion,valor)
global imgBtn itersTxt demoraBtn estadoTxt grabarHndl grabarBtn

switch funcion
   case 'imgStr',
      set(imgBtn,'string',strcat(' Initial image : ',valor));
	case 'iters',
      set(itersTxt,'string',strcat(' Iteration nº ',num2str(valor)));
   case 'demora'
      set(demoraBtn,'string',strcat(' Delay : ',num2str(valor),'s'))
   case 'estado'
      pausa = valor(1);
      ejec = valor(2);
      grabar = valor(3);
      if grabar
      	set(grabarHndl,'enable','off');
         set(grabarBtn,'enable','off');
      else
         set(grabarHndl,'enable','on');
			set(grabarBtn,'enable','on');
      end
      
      if pausa          
         set(estadoTxt,'string',' Animation in pause')
      else
         if ejec
            set(estadoTxt,'string',' Playing animation')
            if grabar
               set(estadoTxt,'string',' Recording animation');
            end
         else
            set(estadoTxt,'string',' Animation stopped');
         end
      end      
end

%_______________________________________________________________________________________

function CelsVivas = vida(Cels)
global parNacen1 parNacen2 parViven1 parViven2 filas cols

n = [filas 1:filas-1];
s = [2:filas 1];
e = [2:cols 1];
o = [cols 1:cols-1];

CelsAlr = Cels(n,:) + Cels(s,:) + Cels(:,e) + Cels(:,o)+...
   Cels(n,e) + Cels(s,e) + Cels(s,o) + Cels(n,o);

% Suponiendo que una célula totalmente viva es un 255 y una muerta un 0
% Y cuando se pasa a la imagen es a la inversa
  
% Una célula nace si tiene alrededor un poco más de 2 células totalmente vivas y
% menos o igual de 3 células totalmente vivas
CelsNacen = ((CelsAlr>255*parNacen1 & CelsAlr<=255*parNacen2).*CelsAlr./3);
  
% Una célula sobrevive si tiene alrededor un poco más de 1 células totalmente vivas y
% menos o igual de 2 células totalmente vivas
CelsViven = (CelsAlr>255*parViven1 & CelsAlr<=255*parViven2).*Cels;
   
CelsVivas = CelsNacen + CelsViven;

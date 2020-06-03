$(document).ready(function(){
				   
$("div[id^='parent']").addClass("parent-element");
$("div[id^='leaf']").addClass("leaf-element");


CollapseAll();

	 $('div .ui-textblock').hover(
	 function(){ $(this).addClass('ui-state-highlight');},
	 function(){ $(this).removeClass('ui-state-highlight');}
	 );
	 
	 $('#parentRoot').css('margin-left','0');
	 
});

function SetElementState(currentElement)
{
	if(currentElement.hasClass('ui-plus'))
	{
		//currentElement.parent().children('div').next().show("fade", {}, 500);
		currentElement.parent().children('div').next().show();
		currentElement.removeClass('ui-plus');
		currentElement.addClass('ui-minus');
	}
	else
	{
		//currentElement.parent().children('div').next().hide("fade", {}, 400);
		currentElement.parent().children('div').next().hide();
		currentElement.removeClass('ui-minus');
		currentElement.addClass('ui-plus');
	}
}

function CollapseAll()
{
	$("span[class*='ui-minus']").each(function()
											   {
												   SetElementState($(this))
											});
										
}

function ExpandAll()
{
	$("span[class*='ui-plus']").each(function()
											   {
												   SetElementState($(this))
											});
										
}

function LoadPageContent(address)
{
	parent.parent.contentFrame.location.href =address;
}

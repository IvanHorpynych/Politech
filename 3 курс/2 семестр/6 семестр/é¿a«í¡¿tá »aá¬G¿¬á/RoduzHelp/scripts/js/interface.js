		(function(jq) {
    jq.autoScroll = function(ops) {
        ops = ops || {};
        ops.styleClass = ops.styleClass || "scroll-to-top-button";
        var t = jq( "<div class="+ops.styleClass+"></div>"),  d = jq(ops.target || document);   jq(ops.container || "body" ).append(t);

        t.css({
                 opacity: 0,
                 position: "absolute",
                 top: 0,
			     left: ($(window).width()-120)
         }).click(function() {
                                   jq("html,body").animate({
                                                                       scrollTop: 0
                                                                        }, ops.scrollDuration || 1000);
                                    });
        $(window).resize(function() {
                                                    t.css({
                                                     left: ($(window).width()-120)
                                                   })
       });

        $(window).scroll(function() { 
            var sv = d.scrollTop();
		
            if (sv < 10) {  t.clearQueue().fadeOut(ops.hideDuration || 200);      return;    }
            t.css("display", "").clearQueue().animate({
                top: sv,
                opacity: 0.8
            }, ops.showDuration || 200);
        });
    };
})(jQuery);
 
$(function() {
	$(document).ready(function() {
		dp.SyntaxHighlighter.ClipboardSwf = '/syntax/scripts/clipboard.swf';
		dp.SyntaxHighlighter.HighlightAll('code',true,false,false,1,false);
		$( "#accordion" ).accordion({
		autoHeight: false,
		collapsible: true,
		navigation: true,
		});
			$('.ui-widget-header').each(function(index)
			{
				$(this).click(function(){
											   SetHeaderState($(this).children('span'));
											   });
				
			});
						
	$.autoScroll({
			scrollDuration: 1000, 
			showDuration: 300, 
			hideDuration: 300
		});
		});
	
		function runEffect() {
		
		var seed=Math.floor(Math.random()*11);
		var selectedEffect ="clip";
		if(seed<3)
			selectedEffect ="clip";
			else if(seed>3 && seed<6)
			selectedEffect ="drop";
			else
			selectedEffect="puff";
			var options = {};
			$('body').show( selectedEffect, options, 500, callback );
		};
		function callback() {
			setTimeout(function() {
				$( "#effect" ).removeAttr( "style" ).hide().fadeIn();
			}, 1000 );
		};
		
		
	});

function SetHeaderState(currentElement)
{
	if(currentElement.hasClass('ui-icon-collapsed'))
	{
		
		currentElement.parent().parent().children('span').show("slide", {}, 500);
		currentElement.removeClass('ui-icon-collapsed');
		currentElement.addClass('ui-icon-expanded');
	}
	else
	{
	    currentElement.parent().parent().children('span').hide("slide", {}, 400);
		currentElement.removeClass('ui-icon-expanded');
		currentElement.addClass('ui-icon-collapsed');
	}
}
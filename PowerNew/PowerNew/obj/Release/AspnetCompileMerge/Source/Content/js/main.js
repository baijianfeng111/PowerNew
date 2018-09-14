

function subDetailTop(obj, plate, aClass) {
    var obj = '.' + obj;
    var plate = '.' + plate;
    var oFset = $(obj).offset();
    var oTop = oFset.top;
    var topArr;
    //var diff = [0, 0, 0];

    $(window).on('scroll', function () {
        var sTop = $(window).scrollTop();
        var plates = $(plate);
        var size = plates.length;
        if (topArr == null) {
            topArr = new Array();
            for (var i = 0; i < size; i++) {
                //var value = plates.eq(i).offset().top + diff[i];
                var ele = plates.eq(i);
                topArr.push(ele);
				
            }

        }
		
        var topIndex = 0;
        for (var i = 1; i < size; i++) {
            var tmp = topArr[i].offset().top-200;
            if (sTop < tmp) {
                topIndex = i - 1;
                break;
            }
            if (i == size - 1) {
                topIndex = i;
            }
        }
		if(!fmenu){
        	$(obj).find('li').eq(topIndex).addClass(aClass).siblings().removeClass(aClass);
		}

        if (oTop < sTop) {
            //$(obj).css({'position':'fixed'});
            //$(obj).parent().css({height:$(obj).innerHeight()});
        }
        else {
            //$(obj).css({'position':'relative'});
            //$(obj).parent().css({height:0});
        }
    })

    $(obj).find('li').on('click', function () {
        var sTop = $(window).scrollTop();
        var index = $(this).index();
        var pTop 
        if(index==0){
        	pTop=$(plate).eq(index).offset().top-100
        }
        else if(index==1){
        	pTop=$(plate).eq(index).offset().top+400
        }
        else{
        	pTop=$(plate).eq(index).offset().top;
        }
        $(this).addClass(aClass).siblings().removeClass(aClass);
		fmenu = true;
		
        $('body,html').stop(true,false).animate({ 'scrollTop': pTop }, 300, function () { $(window).on('scroll'); fmenu = false;});
    })


    //返回头部
    //var aH = $(window).height()
    //$('.mian').css({ 'min-height': aH })
    /*$(window).on('scroll',function(){
	
		var sTop=$(window).scrollTop()
		if(sTop>aH){
			$('.rt_top').show()
		}
		else{
			$('.rt_top').hide()
		}
	})	*/
	
    //$('.rt_top').on('click', function () {
//        $('body,html').animate({ scrollTop: 0 }, 500, function () { $(window).on('scroll'); });
//        return false;
//    })



}


             
var fmenu = false;	
$(window).scroll(function(){
        	sidshow()
        	sidshow2()
        });
		sidshow();
        
        /***点击回到顶部****/
        $(".return_top").on('click', function () {
			fmenu = true;
            $('body,html').animate({ scrollTop: 0 }, 300,function(){
				fmenu = false;
				
			});
			
        });
        
		function sidshow(){
			var top = $(window).scrollTop();
		    if (top >400) {
		          $(".cl-menu").fadeIn(500);
		    }
		    else {
		    
		       $(".cl-menu").fadeOut(100);
		       
		    }
		}
		
		function sidshow2(){
			var wid = $(window).width();
			var fix_wd=$('.cl-menu').outerWidth();
		    if (wid <(1200+fix_wd+40)) {
		          $(".cl-menu").addClass('f_menu2');
		    }
		    else {
		    
		       $(".cl-menu").addClass('f_menu');
		       
		    }
		}

$('.return_pro img').on('click',function(){
	fmenu = true;
            $('body,html').animate({ scrollTop: 0 }, 300,function(){
				fmenu = false;
				
			});
})
 

function tabs(btn,obj,cla){
	$(btn).on('click',function(){
		$(this).addClass(cla).siblings().removeClass(cla);
		$(obj).eq($(this).index()).show().siblings(obj).hide();
	})
}

//返回上一页不刷新
function rtpage(){
    window.history.back(-1);
}

function addHeader(){		
	$('.main').before('<div class="hd-header"></div>');
    $(".hd-header").load("~/Views/Shared/Header");//头部
}

function addFooter(){		
	$('.main').append('<div class="hd-bottom"></div>');
	$(".hd-bottom").load("../Shared/Footer");//底部
}


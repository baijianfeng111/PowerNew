$(function () {
    if ($('#slides').children().length == 0) {
        $('#slides').hide();
    }
    if ($('#slides').children().length > 1) {
        $('#slides').slidesjs({
            width: 1920,
            height: 696,
            navigation: false,
            play: {
                active: false,
                auto: true,
                interval: 4000,
                swap: true
            }
        });
    } else {
        $('#slides').css({ 'height': '768px' });
    }
});
var topArr = null;
$(document).on('scroll',
    function () {
        var sTop = $(window).scrollTop();
        var plates = $('.main .ct');
        var size = plates.length;
        if (topArr == null) {
            topArr = new Array();
            for (var i = 0; i < size; i++) {
                var ele = plates.eq(i);
                topArr.push(ele);
            }
        }
        var wHeight = $(window).height()
        var topIndex = 0;
        for (var i = 1; i < size; i++) {
            var tmp = topArr[i].offset().top - wHeight / 2;
            if (sTop < tmp) {
                topIndex = i - 1;
                break;
            }
            if (i == size - 1) {
                topIndex = i;
            }
        }
        //var dateClass=plates.eq(topIndex).find('.animated').attr('date-class');
        plates.eq(topIndex).find('.animated').each(function (i) {
            var dateClass = $(this).attr('date-class');
            $(this).addClass(dateClass);
            if (i == 0) {
                $('.int-pic p').eq(0).addClass('ratoe');
            }
        });
    });
$('.pic img,.news-li img,.news-bigpic img,.t-left p,.city-left .join-btn').hover(function () {
        var daClass = $(this).attr('date-class');
        $(this).addClass(daClass);
    },
    function () {
        var daClass = $(this).attr('date-class');
        $(this).removeClass(daClass);
    });


$('#city').slidesjs({
    width: 530,
    height: 200,
    navigation: false,
    play: {
        active: false,
        auto: true,
        interval: 4000,
        swap: true
    }
});


//center-detail
$('#news-bigpic').slidesjs({
    width: 598,
    height: 464,
    pagination: false
});

$('.c-d-news-tab ul li').on('click',
    function() {
        $(this).addClass('active').siblings().removeClass('active');
        $('.c-d-newli').eq($(this).index()).show().siblings('.c-d-newli').hide();
    });

$('.teacher-mn ul li').hover(function() {
        $(this).addClass('active').siblings().removeClass('active');
    },
    function() {
        $('.teacher-mn ul li').removeClass('active');
    });
$('#album-mn').slidesjs({
    width: 1200,
    height: 500,
    navigation: false,
    play: {
        active: false,
        auto: true,
        interval: 4000,
        swap: true
    }
});

$('#teacher-mn').slidesjs({
    width: 1200,
    height: 620,
    navigation: false,
    play: {
        active: false,
        auto: true,
        interval: 4000,
        swap: true
    }
});
//news-Index
$('.c-d-news-tab ul li').on('click',
    function() {
        $(this).addClass('active').siblings().removeClass('active');
        $('.c-d-newli').eq($(this).index()).show().siblings('.c-d-newli').hide();
    });

$('.teacher-mn ul li').hover(function() {
        $(this).addClass('active').siblings().removeClass('active');
    },
    function() {
        $('.teacher-mn ul li').removeClass('active');
    });

//teacher-Index
$(function() {
    if ($('#slides').children().length == 0) {
        $('#slides').hide();
    }
    if ($('#slides').children().length > 1) {
        $('#slides').slidesjs({
            width: 1920,
            height: 696,
            navigation: false,
            play: {
                active: false,
                auto: true,
                interval: 4000,
                swap: true
            }
        });
    } else {
        $('#slides').css({ 'height': '768px' });
    }
});
var topArr = null;
$(document).on('scroll',
    function() {
        var sTop = $(window).scrollTop();
        var plates = $('.main .ct');
        var size = plates.length;
        if (topArr == null) {
            topArr = new Array();
            for (var i = 0; i < size; i++) {
                var ele = plates.eq(i);
                topArr.push(ele);
            }
        }
        var wHeight = $(window).height();
        var topIndex = 0;
        for (var i = 1; i < size; i++) {
            var tmp = topArr[i].offset().top - wHeight / 2;
            if (sTop < tmp) {
                topIndex = i - 1;
                break;
            }
            if (i == size - 1) {
                topIndex = i;
            }
        }
        //var dateClass=plates.eq(topIndex).find('.animated').attr('date-class');
        plates.eq(topIndex).find('.animated').each(function(i) {
            var dateClass = $(this).attr('date-class');
            $(this).addClass(dateClass);
            if (i == 0) {
                $('.int-pic p').eq(0).addClass('ratoe');
            }
        });
    });
$('.pic img,.news-li img,.news-bigpic img,.t-left p,.city-left .join-btn').hover(function() {
        var daClass = $(this).attr('date-class');
        $(this).addClass(daClass);
    },
    function() {
        var daClass = $(this).attr('date-class');
        $(this).removeClass(daClass);
    });

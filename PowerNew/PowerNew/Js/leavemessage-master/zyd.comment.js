
$(function () {
    //submit();
});

//回复框
function showreply(pid, c) {
    console.log(pid);
    $('#comment-pid').val(pid);
    $('#cancel-reply').show();
    $('.comment-reply').show();
    $(c).hide();
    $(c).parents('.comment-body').append($('#comment-post'));
};

//关闭回复框
function cancelreply(c) {
    $('#comment-pid').val("");
    $('#cancel-reply').hide();
    $(c).parents(".comment-body").find('.comment-reply').show();
    $("#comment-place").append($('#comment-post'));
}

//提交
function submit() {
    //$('[data-toggle="tooltip"]').tooltip();
    $("#comment-form-btn").click(function () {
        var $this = $("#comment-form-btn");
        $("#comment-form-btn").button('loading');

        $('#detail-modal').modal('show');
        $(".close").click(function () {
            setTimeout(function () {
                $this.html("<i class='fa fa-close'></i>取消操作...");
                setTimeout(function () {
                    $this.button('reset');
                }, 1000);
            }, 500);
        });
        // 模态框抖动
        //		$('#detail-modal .modal-content').addClass("shake");
        $("#detail-form-btn").click(function () {
            $.ajax({
                type: "get",
                url: "./server/comment.json",
                async: true,
                success: function (json) {
                    if (json.statusCode == 200) {
                        console.log(json.message);
                    } else {
                        console.error(json.message);
                    }
                    $('#detail-modal').modal('hide');

                    setTimeout(function () {
                        $this.html("<i class='fa fa-check'></i>" + json.message);
                        setTimeout(function () {
                            $this.button('reset');
                            window.location.reload();
                        }, 1000);
                    }, 1000);
                },
                error: function (data) {
                    console.error(data);
                }
            });
        });
    });
}


//页面加载时候
function loadshow() {
    var E = window.wangEditor;
    var editor = new E('#editor');
    // 自定义菜单配置
    editor.customConfig.menus = [
        'code', // 插入代码
        //			'head', // 标题
        'bold', // 粗体
        'italic', // 斜体
        'underline', // 下划线
        //			'strikeThrough', // 删除线
        //			'foreColor', // 文字颜色
        //			'backColor', // 背景颜色
        'image', // 插入图片
        'link', // 插入链接
        'list', // 列表
        //			'justify', // 对齐方式
        'quote', // 引用
        'emoticon' // 表情
        //			'table', // 表格
        //			'video', // 插入视频
        //			'undo', // 撤销
        //			'redo' // 重复
    ];
    // debug模式下，有 JS 错误会以throw Error方式提示出来
    editor.customConfig.debug = true;

    // 关闭粘贴样式的过滤
    editor.customConfig.pasteFilterStyle = false;
    // 自定义处理粘贴的文本内容
    editor.customConfig.pasteTextHandle = function (content) {
        // content 即粘贴过来的内容（html 或 纯文本），可进行自定义处理然后返回
        return content + '<p>在粘贴内容后面追加一行</p>';
    };
    // 插入网络图片的回调
    editor.customConfig.linkImgCallback = function (url) {
        console.log(url); // url 即插入图片的地址
    };
    editor.customConfig.zIndex = 100;
    editor.create();
    E.fullscreen.init('#editor');
    //		editor.txt.clear(); //清空编辑器内容
    //		editor.txt.html('<p>用 JS 设置的内容</p><strong>hello</strong><script>alert(/xss/);<\/script>');
    //		editor.txt.append('<p>追加的内容</p>');
    // 读取 html
    console.log(editor.txt.html());
    // 读取 进行 xss 攻击过滤后的html
    console.log(filterXSS(editor.txt.html()));
    // 读取 text
    console.log(editor.txt.text());
    // 获取 JSON 格式的内容
    console.log(editor.txt.getJSON());
}


//获取地理位置  暂有问题
var map;
var gpsPoint;
var baiduPoint;
var gpsAddress;
var baiduAddress;
var x;
var y;
function getLocation() {
    //根据IP获取城市 
    var myCity = new BMap.LocalCity();
    myCity.get(getCityByIP);

    //获取GPS坐标 
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showMap, handleError, { enableHighAccuracy: true, maximumAge: 1000 });
    } else {
        alert("您的浏览器不支持使用HTML 5来获取地理位置服务");
    }
}

function showMap(value) {
    var longitude = value.coords.longitude;
    var latitude = value.coords.latitude;
    map = new BMap.Map("map");
    x = latitude;
    y = longitude;
    //alert("坐标经度为：" + latitude + "， 纬度为：" + longitude ); 
    gpsPoint = new BMap.Point(longitude, latitude); // 创建点坐标 


    //根据坐标逆解析地址 
    var geoc = new BMap.Geocoder();
    geoc.getLocation(gpsPoint, getCityByCoordinate);

    BMap.Convertor.translate(gpsPoint, 0, translateCallback);
    map.enableScrollWheelZoom(true);
}

translateCallback = function (point) {
    baiduPoint = point;
    map.centerAndZoom(baiduPoint, 18);
    var geoc = new BMap.Geocoder();
    geoc.getLocation(baiduPoint, getCityByBaiduCoordinate);
}

function getCityByCoordinate(rs) {
    gpsAddress = rs.addressComponents;
    var address = "GPS标注：" + gpsAddress.province + "," + gpsAddress.city + "," + gpsAddress.district + "," + gpsAddress.street + "," + gpsAddress.streetNumber;
    var marker = new BMap.Marker(gpsPoint); // 创建标注 
    map.addOverlay(marker); // 将标注添加到地图中 
    var labelgps = new BMap.Label(address, { offset: new BMap.Size(20, -10) });
    marker.setLabel(labelgps); //添加GPS标注 
}

function getCityByBaiduCoordinate(rs) {
    baiduAddress = rs.addressComponents;
    var address = "百度标注：" + baiduAddress.province + "," + baiduAddress.city + "," + baiduAddress.district + "," + baiduAddress.street + "," + baiduAddress.streetNumber;
    var marker = new BMap.Marker(baiduPoint); // 创建标注 
    map.addOverlay(marker); // 将标注添加到地图中 
    var labelbaidu = new BMap.Label(address, { offset: new BMap.Size(20, -10) });
    marker.setLabel(labelbaidu); //添加百度标注 
}

//根据IP获取城市 
function getCityByIP(rs) {
    var cityName = rs.name;
    alert("根据IP定位您所在的城市为:" + cityName);
}

function handleError(value) {
    switch (value.code) {
        case 1:
            alert("位置服务被拒绝");
            break;
        case 2:
            alert("暂时获取不到位置信息");
            break;
        case 3:
            alert("获取信息超时");
            break;
        case 4:
            alert("未知错误");
            break;
    }
}

function init() {
    getLocation();
}

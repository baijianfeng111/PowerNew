CheckEmail = function (email) {
    if (email.length <= 0) {
        return false;
    }
    var reg = /[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?/;
    return reg.test(email);
};
CheckUName = function (uname) {
    if (uname.length <= 0) {
        return false;
    }
    var reg = /^[a-zA-Z][a-zA-Z0-9]{3,11}$/;
    return reg.test(uname);
}
CheckPassword = function (pwd) {
    if (pwd.length <= 0) {
        return false;
    }
    var reg = /^[a-zA-Z]\w{5,15}$/;
    return reg.test(pwd);
}
CheckUNickName = function (unickname) {
    if (unickname.length <= 0) {
        return false;
    }
    var reg = /^[a-zA-Z\u4e00-\u9fa5][a-zA-Z0-9_\u4E00-\u9FA5]{1,11}$/;
    return reg.test(unickname);
}
//将序列化成json格式后日期(毫秒数)转成日期格式
ChangeDateFormat = function (cellval) {
    var date = new Date(parseInt(cellval.replace("/Date(", "").replace(")/", ""), 10));
    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
    var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
    return date.getFullYear() + "-" + month + "-" + currentDate;
}
//获取文章数据(前台页面使用)
function GetArticleInfo(pageIndex) {
    $.post('/Blog/GetArticleList', { PageIndex: pageIndex }, function (result) {
        //清空内容
        $("#articleInfoDiv").empty();
        $("#pageBarUl").empty();

        //开始遍历数据
        $.each(result.data, function (index, item) {
            $("#articleInfoDiv").append(
                '<article class="blog-main" style="max-height:305px;">' +
                //文章标题
                '<h3 class="am-article-title" style="color:#3cb371"><a href="/Blog/Detail?id=' + item.id + '">' + item.title + '</a></h3>' +
                //,发布日期,文章分类,作者,阅读次数
                '<h4 class="am-article-meta blog-meta">' +
                '<span class="am-icon-clock-o"> 发表于</span> ' + ChangeDateFormat(item.createtime) +
                //' • under <a href="javascript:void(0);">' + item.typeid + '</a>' +
                ' &nbsp;&nbsp;&nbsp;&nbsp;<span class="am-icon-user">作者</span>&nbsp;<a href="javascript:void(0);">' + item.UserName + '</a>' +
                ' &nbsp;&nbsp;&nbsp;&nbsp;<span class="am-icon-eye"> ' + item.readcount + '次围观</span>' +
                '</h4>' +
                '<div class="am-g blog-content">' +
                //文章内容
                '<div class="am-u-lg-7">' + item.content.substring(0,200) + '</div>' +
                //文章封面
                '<div class="am-u-lg-5">' + '<p><img class="am-img-responsive" style="width: 241px;height:151px;" src="' + item.photourl + '"></p></div>' +
                '</div>' +
                '</article>' +
                '<hr class="am-article-divider blog-hr" style="color:red">');
        });
        //填充pagebar
        $("#pageBarUl").append(data.pagehtml);
        //为pageBarUl下的a标签注册点击事件
        $("#pageBarUl a").click(function () {
            //当前a标签 存在pageIndex属性的话
            if (typeof ($(this).attr('PageIndex')) != "undefined") {
                GetArticleInfo($(this).attr('PageIndex'));
            }
        });
    });
};



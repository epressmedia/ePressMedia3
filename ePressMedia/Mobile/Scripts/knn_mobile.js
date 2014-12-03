$(document).ready(function () {

    $("[tag='biz']").click(
       function () {
           window.location = "biz.aspx";
       });


    $("[tag='classified']").click(
       function () {
           window.location = "list.aspx";
       });

    $("[tag='news']").click(
       function () {
           window.location = "news.aspx";
       });

    $("img#logo").click(
       function () {
           window.location = "default.aspx";
       });

    $("img#futureit").click(
       function () {
           window.open("http://www.future-it.ca");
       });

    $("img#bottom_ad").click(
       function () {
           window.open("http://www.future-it.ca");
       });



    $("a#btn_prev").ready(
    function () {
        var pageNumber
        try {
            pageNumber = document.getElementById("ctl00_PageNumber").value;

            if (pageNumber < 2) {
                $("div.#div_prev.ui-block-b").remove();
                $("div.ui-block-b").css("width", "100% !important");
            }
        }
        catch (ex) {
            pageNumber = -1
        }

    });


    $("a#btn_next").ready(
    function () {
        var pageNumber
        try {
            pageNumber = document.getElementById("ctl00_PageNumber").value;
            MaxPageNumber = document.getElementById("ctl00_MaxPageNumber").value;
           
            if (MaxPageNumber == "0") {
                $("#navi_field").remove();
                //alert("plan A");
            }

            if (parseInt(pageNumber) >= parseInt(MaxPageNumber)) {
                //alert("plan B");
                $("div.#div_next.ui-block-b").remove();
                $("div.ui-block-b").css("width", "100% !important");
            }
        }
        catch (ex) {
  
            pageNumber = -1
        }

    });

    $("select#NewsCatSelector").live("change",
    function () {
        window.location = "news.aspx?CatId=" + $(this).val();
    });

    $("select#AdCatSelector").live("change",
    function () {
        window.location = "list.aspx?CatId=" + $(this).val();
    });



    $("#search").keydown(function () {
        //        if (event.keyCode == 13) {
        //            alert("abc");
        //            return false;
        //        }
    });


});

var _gaq = _gaq || [];
_gaq.push(['_setAccount', 'UA-18071543-9']);
_gaq.push(['_setDomainName', '.seattlekcr.com']);
_gaq.push(['_trackPageview']);

(function () {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
})();







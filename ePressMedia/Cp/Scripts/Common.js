$(document).ready(function () {

    var header_name = $("#content-table-inner h1").text();
});

$(document).ready(function () {

    $(".HasFullControl").click(function () {

        if ($(this).children("[type=checkbox]").is(':checked')) {
            $(this).parent().parent().children("td.data").children("span").children("input").attr("checked", true);
        }
        else {
            $(this).parent().parent().children("td.data").children("span").children("input").attr("checked", false);
        }
    });

    $(".CanRead").click(function () {
        if ($(this).children("[type=checkbox]").is(':checked')) {
            $(this).parent().parent().children("td.data").children("span.CanList").children("input").attr("checked", true);
        }

    });

    $(".CanList").click(function () {
        if ($(this).children("[type=checkbox]").is(':checked')) {
            $(this).parent().parent().children("td.data").children("span.CanRead").children("input").attr("checked", true);
        }
        else {
            $(this).parent().parent().children("td.data").children("span.CanRead").children("input").attr("checked", false);
        }
    });


    $(".PageBuilder_ShowXML").click(function () {
    //$(".PageBuilder_ShowXML").live('click',function(){
        if ($(this).text() == "Show XML") {
            $(this).parent().parent().children(".PageBuilder_XML").slideDown("slow");
            $(this).text("Hide XML")
        }
        else {
            $(this).parent().parent().children(".PageBuilder_XML").slideUp("slow");
            $(this).text("Show XML")
        }

    });




});

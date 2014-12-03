$(document).ready(function () {
    $('#subMenu span.sm').hide();
    $('#mainMenu a').hover(function () {
        $('#subMenu span.sm').hide();
        $('#subMenu span.sm:eq(' + $(this).index() + ')').show();
    });

    //ViewResAd.aspx Width Adjustment
    var class_lbl = $('.thrdVw .inf .lbl2').outerWidth();
    $('.thrdVw .inf .data').width(($('.thrdVw .inf').width() - (class_lbl * 2) - 32) / 2);

    var class_box = $('.thrdVw .exinf > div').width();
    $('.thrdVw .exinf .slbl').width(class_box * (11/100));
    $('.thrdVw .exinf .sdata ').width(class_box * (13 / 100));



    //  Control Container Indicator for Edit Mode(Admin)
    $(".FrontEditButton").mouseover(function () {
        $(this).closest(".Admin_Op").fadeTo("fast", 0.33);
    });
    $(".FrontEditButton").mouseout(function () {
        $(this).closest(".Admin_Op").fadeTo("fast", 1);
    });

});





function AdjustBannerImage(bannerID) {

    var cont_width = $("#rightPnl").width();

    var width = $("[bannerID=" + bannerID + "]").attr("width");
    var height = $("[bannerID=" + bannerID + "]").attr("height");

    if (cont_width < width) {
        $("[bannerID=" + bannerID + "]").attr('width', cont_width);
        $("[bannerID=" + bannerID + "]").attr('height', ((cont_width * height) / width));
    }
}




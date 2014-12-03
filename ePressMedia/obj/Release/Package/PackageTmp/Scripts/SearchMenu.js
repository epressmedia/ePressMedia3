$(document).ready(function () {
    $('.srchMenu li').hover(function () {
        if ($(this).hasClass('sel') == false)
            $(this).addClass('tsel');
    }, function () {
        $(this).removeClass('tsel');
    });
});
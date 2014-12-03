
var caroTimer;

$(document).ready(function () {
    //move the last list item before the first item.
    $('#carousel_ul li:first').before($('#carousel_ul li:last'));

    $('#navNext img').click(function () {
        clearTimeout(caroTimer);
        rotateNext();
    });

    $('#navPrev img').click(function () {
        clearTimeout(caroTimer);
        rotatePrev();
    });
});


$(document).ready(function () {
    $('#descPnl > div').hide();
    var div = $('#descPnl > div').eq(0);
    div.show();
    $('#vidPnl').html(getPlayHtml(div.attr('id')));

    //caroTimer = setTimeout(rotateNext, 2500);

    $('#carousel_ul a').click(function (e) {
        $('#descPnl > div').hide();

        var div = $('#descPnl ' + $(this).attr('href'));
        div.show();
        $('#vidPnl').html(getPlayHtml(div.attr('id')));

        e.preventDefault();
    });
});

function rotateNext() {
    var item_width = $('#carousel_ul li').outerWidth() + 20;
    var left_indent = parseInt($('#carousel_ul').css('left')) - item_width;

    $('#carousel_ul:not(:animated)').animate({ 'left': left_indent }, 500, function () {
        $('#carousel_ul li:last').after($('#carousel_ul li:first'));
        $('#carousel_ul').css({ 'left': '-120px' });
    });

    caroTimer = setTimeout(rotateNext, 2500);
}

function rotatePrev() {
    var item_width = $('#carousel_ul li').outerWidth() + 20;
    var left_indent = parseInt($('#carousel_ul').css('left')) + item_width;

    $('#carousel_ul:not(:animated)').animate({ 'left': left_indent }, 500, function () {
        $('#carousel_ul li:first').before($('#carousel_ul li:last'));
        $('#carousel_ul').css({ 'left': '-120px' });
    });

    caroTimer = setTimeout(rotateNext, 2500);
}

function getPlayHtml(id) {
    var html = '';

    html += '<object height="200" width="280">';
    html += '<param name="movie" value="http://www.youtube.com/v/' + id + '&autoplay=0"></param>';
    html += '<param name="wmode" value="transparent"></param>';
    html += '<embed src="http://www.youtube.com/v/' + id + '&autoplay=0"';
    html += 'type="application/x-shockwave-flash" wmode="transparent"  height="200" width="280"></embed>';
    html += '</object>';

    return html;
}


var VideoAdBoxSmall_caroTimer;
var spacePerItem;

$(document).ready(function () {
    //move the last list item before the first item.
    $('#VideoAdBoxSmall_carousel_ul li:first').before($('#VideoAdBoxSmall_carousel_ul li:last'));

    $('#VideoAdBoxSmall_navNext img').click(function () {
        clearTimeout(VideoAdBoxSmall_caroTimer);
        VideoAdBoxSmall_rotateNext();
    });

    $('#VideoAdBoxSmall_navPrev img').click(function () {
        clearTimeout(VideoAdBoxSmall_caroTimer);
        VideoAdBoxSmall_rotatePrev();
    });

    var availSpace = $('.VideoAdBoxSmall_Header').width() - $('#VideoAdBoxSmall_navPrev').css('width').replace("px", "") - $('#VideoAdBoxSmall_navNext').css('width').replace("px", "");
    $('#VideoAdBoxSmall_carousel_inner').css('width', availSpace);

    spacePerItem = availSpace / $('[id$="_VideoAdBoxSmall_itemPerPage"]').attr("Value");
    var v_image_size = spacePerItem * 0.8;
    $('#VideoAdBoxSmall_carousel_ul li').width(v_image_size);
    $('#VideoAdBoxSmall_carousel_ul li').height(v_image_size * (3 / 4));
    $('#VideoAdBoxSmall_carousel_ul li').css('margin', (spacePerItem - v_image_size) / 2);
    $('#VideoAdBoxSmall_carousel_ul li img').width(v_image_size);
    $('#VideoAdBoxSmall_carousel_ul li img').height(v_image_size * (3 / 4));
    $('#VideoAdBoxSmall_carousel_ul').css({ 'left': (spacePerItem * -1) + 'px' });
    $('.VideoAdBoxSmall_List').css('height', v_image_size * (3 / 4) + (spacePerItem - v_image_size) + 10);
    $('#VideoAdBoxSmall_carousel_inner').css('height', '73px');
    //$('#VideoAdBoxSmall_carousel_inner').css('height', (v_image_size * (3 / 4)) + (spacePerItem - v_image_size));
//    $('.VideoAdBoxSmall_bizLbl').css('margin-top', (spacePerItem - v_image_size) / 4);
//    $('.VideoAdBoxSmall_bizLbl').css('line-height', (spacePerItem - v_image_size) / 2 + 'px');
    $('.VideoAdBoxSmall_bizLbl').css('font-size', '10px');

});


$(document).ready(function () {
//    $('#VideoAdBoxSmall_descPnl > div').hide();
    var div = $('#VideoAdBoxSmall_descPnl > div').eq(0);
    div.show();
    $('#VideoAdBoxSmall_vidPnl').html(VideoAdBoxSmall_getPlayHtml(div.attr('id')));

    //VideoAdBoxSmall_caroTimer = setTimeout(VideoAdBoxSmall_rotateNext, 5000);

    $('#VideoAdBoxSmall_carousel_ul a').click(function (e) {
        //$('#VideoAdBoxSmall_descPnl > div').hide();

        var div = $('#VideoAdBoxSmall_descPnl ' + $(this).attr('href'));
        div.show();
        $('#VideoAdBoxSmall_vidPnl').html(VideoAdBoxSmall_getPlayHtml(div.attr('id')));

        e.preventDefault();
    });
});

function VideoAdBoxSmall_rotateNext() {
    var item_width = parseInt($('#VideoAdBoxSmall_carousel_ul li').width()) + parseInt($('#VideoAdBoxSmall_navPrev').css('width').replace("px", "")) + parseInt($('#VideoAdBoxSmall_navNext').css('width').replace("px", ""));
    //alert(item_width);
    var left_indent = parseInt($('#VideoAdBoxSmall_carousel_ul').css('left'));
    //alert($('#VideoAdBoxSmall_carousel_ul').css('left'));
    //alert(left_indent);

    $('#VideoAdBoxSmall_carousel_ul:not(:animated)').animate({ 'left': '+=' + left_indent + 'px' }, 500, function () {
        $('#VideoAdBoxSmall_carousel_ul li:last').after($('#VideoAdBoxSmall_carousel_ul li:first'));
        $('#VideoAdBoxSmall_carousel_ul').css({ 'left': (spacePerItem * -1)+'px' });
    });

    VideoAdBoxSmall_caroTimer = setTimeout(VideoAdBoxSmall_rotateNext, 5000);
}

function VideoAdBoxSmall_rotatePrev() {
    var item_width = parseInt($('#VideoAdBoxSmall_carousel_ul li').outerWidth()) + parseInt($('#VideoAdBoxSmall_navPrev').css('width').replace("px", "")) + parseInt($('#VideoAdBoxSmall_navNext').css('width').replace("px", ""));
    var left_indent = parseInt($('#VideoAdBoxSmall_carousel_ul').css('left')) ;

    $('#VideoAdBoxSmall_carousel_ul:not(:animated)').animate({ 'left': '-=' + left_indent + 'px' }, 500, function () {
        $('#VideoAdBoxSmall_carousel_ul li:first').before($('#VideoAdBoxSmall_carousel_ul li:last'));
        $('#VideoAdBoxSmall_carousel_ul').css({ 'left': (spacePerItem * -1) + 'px' });
    });

    VideoAdBoxSmall_caroTimer = setTimeout(VideoAdBoxSmall_rotateNext, 5000);
}

function VideoAdBoxSmall_getPlayHtml(id) {

    var width = $(".VideoAdBoxSmall_Header").width();
    
    var html = '';

    html += '<object height=' + width * (3 / 4) + ' width=' + width + '>';
    html += '<param name="movie" value="http://www.youtube.com/v/' + id + '&autoplay=0"></param>';
    html += '<param name="wmode" value="transparent"></param>';
    html += '<embed src="http://www.youtube.com/v/' + id + '&autoplay=0"';
    html += 'type="application/x-shockwave-flash" wmode="transparent"  height=' + width * (3 / 4) + ' width=' + width + '></embed>';
    html += '</object>';

    $('.VideoAdBoxSmall_Contents').height(width * (3 / 4));

    return html;
}

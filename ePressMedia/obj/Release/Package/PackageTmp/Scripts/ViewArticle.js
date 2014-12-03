
$(document).ready(function () {
    var fontSize = $.cookie('fontSize');
    var lineHei = $.cookie('lineHei');
    if (fontSize != null) {
        $('#mainArtBody').css('font-size', fontSize + 'px');
        $('#mainArtBody').css('line-height', lineHei + 'px');
    }

    setFontSizeHandler();
});

function setFontSizeHandler() {
    $('#incFont').click(function () {
        var curSizeStr = $('#mainArtBody').css('font-size');
        var curSize = parseFloat(curSizeStr, 10);
        var newSize = curSize + 2;
        var curHeiStr = $('#mainArtBody').css('line-height');
        var curHei = parseFloat(curHeiStr, 10);
        var newHei = curHei + 3;

        setFontLineSize(newSize, newHei);
    });

    $('#decFont').click(function () {
        var curSizeStr = $('#mainArtBody').css('font-size');
        var curSize = parseFloat(curSizeStr, 10);
        var newSize = curSize - 2;
        var curHeiStr = $('#mainArtBody').css('line-height');
        var curHei = parseFloat(curHeiStr, 10);
        var newHei = curHei - 3;

        setFontLineSize(newSize, newHei);
    });
}

function setFontLineSize(newSize, newHei) {
    $('#mainArtBody').css('font-size', newSize + 'px !important');
    $('#mainArtBody').css('line-height', newHei + 'px');

    $.cookie('fontSize', newSize, { expires: 30 });
    $.cookie('lineHei', newHei, { expires: 30 });
}

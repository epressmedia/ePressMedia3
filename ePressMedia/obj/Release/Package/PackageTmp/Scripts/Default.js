



var colTimer;
var adTimer;
var newsTimer;
var adListIds = new Array('#hmstyList', '#resRealList', '#bizRealList', '#sellList', '#carList', '#shopList', '#tutorList');

$(document).ready(function () {
    $('#tabPnl ul').hide();
    $('#tabPnl ul:first-child').show();
    $('#newsTabs a:first-child').attr("class", "sel");
    var idx = Math.floor(Math.random() * 15);
    $('#colPnl > ul > li').eq(idx).show();
    $('#colPnl > ul > li').eq(idx).addClass('sel');

    colTimer = setTimeout(nextColumns, 5000);

    initClsAds();

    newsTimer = setTimeout(nextNews, 5000);

    $('#newsTabs a').click(function (e) {
        //clearTimeout(newsTimer);
        // activate new tab
        if ($(this).attr("class") != "empty_news") {
            $(this).parent().children('.sel').removeClass('sel');
            //$('#newsTabs a.sel').removeClass('sel');
            $(this).addClass('sel');

            // make the corresponding content visible
            $(this).parent().siblings('#tabPnl').children().hide();
            //$('#tabPnl ul').hide();
            $('#tabPnl ' + $(this).attr('href')).show();

            //newsTimer = setTimeout(nextNews, 5000);

            e.preventDefault();
        }
    });

    $('.colNav a').click(function (e) {
        clearTimeout(colTimer);

        if ($(this).index() == 1)
            nextColumns();
        else
            prevColumns();

        e.preventDefault();
    });

    $('.clsList li').hover(function () {
        $(this).parent().children('.emp').removeClass('emp');
        $(this).addClass('emp');

        var prvw = $(this).parent().parent().children('.clsPrvw');
        prvw.children('img').attr('src', $(this).children('img.thumb').attr('src'));
        //prvw.children('p').text($(this).children('.desc').text());

        clearTimeout(adTimer);
    }, function () {
        adTimer = setTimeout(nextAds, 3000);
    });




});

function nextNews() {
    var cur = $('#newsTabs a.sel');
    var next = cur.next().length ? cur.next() : cur.parent().children(':first');

//    cur.removeClass('sel');
//    next.addClass('sel');

//    $('#tabPnl ul').hide();
//    $('#tabPnl ' + next.attr('href')).show();

//    newsTimer = setTimeout(nextNews, 5000);
}

function initClsAds() {
    for (i = 0; i < adListIds.length; i++) {
        $(adListIds[i] + ' li:first').addClass('emp');
        $(adListIds[i] + ' .clsPrvw img').attr('src', $(adListIds[i] + ' li:first').children('img.thumb').attr('src'));
        //$(adListIds[i] + ' .clsPrvw p').text($(adListIds[i] + ' li:first').children('.desc').text());
    }

    adTimer = setTimeout(nextAds, 3000);

}

function nextAds() {
    for (i = 0; i < adListIds.length; i++)
        nextAdItem(adListIds[i]);

    adTimer = setTimeout(nextAds, 3000);
}

function nextAdItem(listId) {
    var cur = $(listId + ' li.emp');
    var next = cur.next().length ? cur.next() : cur.parent().children(':first');

    cur.removeClass('emp');
    next.addClass('emp');

    $(listId + ' .clsPrvw img').attr('src', next.children('img.thumb').attr('src'));
    //$(listId + ' .clsPrvw p').text(next.children('.desc').text());
}

function prevColumns() {
    var curItem = $('#colPnl ul li.sel');
    var prevItem = curItem.prev().length ? curItem.prev() : curItem.parent().children(':last');

    curItem.removeClass('sel');
    curItem.hide();
    prevItem.addClass('sel');
    prevItem.show();

    colTimer = setTimeout(nextColumns, 5000);
}

function nextColumns() {
    var curItem = $('#colPnl ul li.sel');
    var nextItem = curItem.next().length ? curItem.next() : curItem.parent().children(':first');

    curItem.removeClass('sel');
    curItem.hide();
    nextItem.addClass('sel');
    nextItem.show();

    colTimer = setTimeout(nextColumns, 5000);
}

function hidePopup1(boxName) {
    if ($('[id$="PopCheck1"]').attr('checked')) {
        $.cookie('pop' + $('[id$="PopCheck1"]').attr('value'), '1', { expires: 1 });
    }
    $('[id$="Pop1"]').hide();
}

function hidePopup2(boxName) {
    if ($('[id$="PopCheck2"]').attr('checked')) {
        $.cookie('pop' + $('[id$="PopCheck2"]').attr('value'), '1', { expires: 1 });
    }
    $('[id$="Pop2"]').hide();
}



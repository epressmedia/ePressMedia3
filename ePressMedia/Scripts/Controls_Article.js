$(document).ready(function () {
    $('.MultiImageArticleSummary_repNext img').click(function () {


        var $repCaroUl = $(this).parent().parent().children('.MultiImageArticleSummary_repCaro').children('.MultiImageArticleSummary_repCaroUl');
        var $repSec = $repCaroUl.children('.MultiImageArticleSummary_repSec');
        var itemWid = $repSec.outerWidth() + 5;
        var indent = parseInt($repCaroUl.css('left')) - itemWid;

        $repCaroUl.not(':animated').animate({ 'left': indent }, 500, function () {
            $repCaroUl.children('.MultiImageArticleSummary_repSec:last').after($repCaroUl.children('.MultiImageArticleSummary_repSec:first'));
            $repCaroUl.css({ 'left': '-95px' });
        });
    });

    $('.MultiImageArticleSummary_repPrev img').click(function () {
        var $repCaroUl = $(this).parent().parent().children('.MultiImageArticleSummary_repCaro').children('.MultiImageArticleSummary_repCaroUl');
        var $repSec = $repCaroUl.children('.MultiImageArticleSummary_repSec');
        var itemWid = $repSec.outerWidth() + 5;
        var indent = parseInt($repCaroUl.css('left')) + itemWid;

        $repCaroUl.not(':animated').animate({ 'left': indent }, 500, function () {
            $repCaroUl.children('.MultiImageArticleSummary_repSec:first').before($repCaroUl.children('.MultiImageArticleSummary_repSec:last'));
            $repCaroUl.css({ 'left': '-95px' });
        });
    });


});




/************* ArticleImageSlider - Start *****************/



/************* ArticleImageSlider - End *****************/




/************* PageableArticleListView - Start *****************/
$(document).ready(function () {
    $(".PageableArticleListView_Contents .CatCotainer:not(:first-child)").hide();
    $(".PageableArticleListView_Contents .CatCotainer:first-child").addClass('CatCotainer_Sel');
    $('.PageableArticleListView_Header a').click(function (e) {

        var current = $(this);
        
        if (current.index() == 1)
            nextColumns(current);
        else
            prevColumns(current);

        e.preventDefault();
    });

    function prevColumns(init_control) {
        var curItem = init_control.parent(".PageableArticleListView_Header").parent(".PageableArticleListView_Container").children(".PageableArticleListView_Contents").children(".CatCotainer_Sel");
//        var curItem = $('.PageableArticleListView_Contents .CatCotainer_Sel');
        var prevItem = curItem.prev().length ? curItem.prev() : curItem.parent().children(':last-child');

        curItem.removeClass('CatCotainer_Sel');
        curItem.hide();
        prevItem.addClass('CatCotainer_Sel');
        prevItem.show();
    }

    function nextColumns(init_control) {
        var curItem = init_control.parent(".PageableArticleListView_Header").parent(".PageableArticleListView_Container").children(".PageableArticleListView_Contents").children(".CatCotainer_Sel"); // $(".PageableArticleListView_Contents .CatCotainer_Sel");
        var nextItem = curItem.next().length ? curItem.next() : curItem.parent().children(':first-child');

        curItem.removeClass('CatCotainer_Sel');
        curItem.hide();
        nextItem.addClass('CatCotainer_Sel');
        nextItem.show();


    }

});

/************* PageableArticleListView - End *****************/
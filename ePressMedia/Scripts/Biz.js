$(document).ready(function () {


    //$('.bizInfo').width($('.biz').width() - $('.bizImg').outerWidth()-8);


    $('.tab').hover(function () {
        $(this).addClass('mapLinkHoverIn');
    }, function () {
        $(this).addClass('mapLinkHoverOut');
    });


    $('.tabs span:eq(0)').css('backgroundImage', 'url(../img/seltabbg.png)');
    $('.tabs span:eq(0)').css('color', '#ffffff');
    $('.multiPnl').css('display', 'none');
    $('.multiPnl:eq(0)').css('display', 'block');

    $('.tabs span').click(function () {
        $('.tabs span').css('backgroundImage', 'url(../img/tabbg.png)');
        $('.tabs span').css('color', '#333333');
        $(this).css('backgroundImage', 'url(../img/seltabbg.png)');
        $(this).css('color', '#ffffff');
        var idx = $(this).index();

        $('.multiPnl').css('display', 'none');
        $('.multiPnl:eq(' + idx + ')').css('display', 'block');
    });
});

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
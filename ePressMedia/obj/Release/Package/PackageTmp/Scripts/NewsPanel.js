
$(document).ready(function () {

    $('#NewsPanel_newsTabs a').mouseover(function (e) {
        //clearTimeout(newsTimer);

        // activate new tab
        if ($(this).attr("class") != "NewsPanel_empty_news") {
            $('#NewsPanel_newsTabs a.sel').removeClass('sel');
            $(this).addClass('sel');

            // make the corresponding content visible
            $('#NewsPanel_tabPnl > div').hide();
            $('#NewsPanel_tabPnl ' + $(this).attr('href')).show();

            //newsTimer = setTimeout(nextNews, 5000);

            e.preventDefault();
        }
    });

    $('.NewsPanel_Main').ready(function () {
    var control_width = $('.NewsPanel_Main').width();

    var number_a = $('#NewsPanel_newsTabs a').size() - 1;
    var tab_size = $('#NewsPanel_newsTabs .sel').width() + 1;
    var padding_val = 4;
    var empty_tab_size = control_width - (number_a * tab_size) - ((number_a + 1) * 2 * padding_val) - number_a;


    $('#NewsPanel_newsTabs .NewsPanel_empty_news').width(empty_tab_size);


});

});
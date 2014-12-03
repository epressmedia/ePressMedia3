
$(document).ready(function () {

    $('#PhotoNewsPanel_newsTabs a').mouseover(function (e) {
        //clearTimeout(newsTimer);

        // activate new tab
        if ($(this).attr("class") != "PhotoNewsPanel_empty_news") {
            $('#PhotoNewsPanel_newsTabs a.sel').removeClass('sel');
            $(this).addClass('sel');

            // make the corresponding content visible
            $('#PhotoNewsPanel_tabPnl > div').hide();
            $('#PhotoNewsPanel_tabPnl ' + $(this).attr('href')).show();

            //newsTimer = setTimeout(nextNews, 5000);

            e.preventDefault();
        }
    });

    $('.PhotoNewsPanel_Main').ready(function () {
        var control_width = $('.PhotoNewsPanel_Main').width();

        var number_a = $('#PhotoNewsPanel_newsTabs a').size() - 1;
        var tab_size = $('#PhotoNewsPanel_newsTabs .sel').width() + 1;
        var padding_val = 4;
        var empty_tab_size = control_width - (number_a * tab_size) - ((number_a + 1) * 2 * padding_val) - number_a;


        $('#PhotoNewsPanel_newsTabs .PhotoNewsPanel_empty_news').width(empty_tab_size);


    });

    

});
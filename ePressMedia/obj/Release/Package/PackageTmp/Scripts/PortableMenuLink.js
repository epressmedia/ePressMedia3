
var PortableMenuLink_spacePerItem 
$(document).ready(function () {


    $('.PortableMenuLink_Center ul li:first').before($('.PortableMenuLink_Center ul li:last'));

    $('.PortableMenuLink_Left img').click(function () {
        PortableMenuLink_rotatePrev();
    });

    $('.PortableMenuLink_Right img').click(function () {
        
        PortableMenuLink_rotateNext();
    });

    var total_cont_size = $(".PortableMenuLink_Container").width();

    // 16px comes from the image right left margin
    var total_avail_size = total_cont_size - $(".PortableMenuLink_Left").width() - $(".PortableMenuLink_Right").width() - 16;

    PortableMenuLink_spacePerItem = total_avail_size / $('[id$="_PortableMenuLink_itemPerPage"]').attr("Value");

    $(".PortableMenuLink_Center").width(total_avail_size);

    $('.PortableMenuLink_Container li').width(PortableMenuLink_spacePerItem);

    $('.PortableMenuLink_Center ul').css({ 'left': (PortableMenuLink_spacePerItem * -1) + 'px' });
});


function PortableMenuLink_rotateNext() {
    var item_width = parseInt($('.PortableMenuLink_Center ul li').width()) + parseInt($('.PortableMenuLink_Left').css('width').replace("px", "")) + parseInt($('.PortableMenuLink_Right').css('width').replace("px", ""));
    var left_indent = parseInt($('.PortableMenuLink_Center ul').css('left'));

    $('.PortableMenuLink_Center ul:not(:animated)').animate({ 'left': '+=' + left_indent + 'px' }, 500, function () {
        $('.PortableMenuLink_Center ul li:last').after($('.PortableMenuLink_Center ul li:first'));
        $('.PortableMenuLink_Center ul').css({ 'left': (PortableMenuLink_spacePerItem * -1) + 'px' });
    });


}

function PortableMenuLink_rotatePrev() {
    var item_width = parseInt($('.PortableMenuLink_Center ul li').width()) + parseInt($('.PortableMenuLink_Left').css('width').replace("px", "")) + parseInt($('.PortableMenuLink_Right').css('width').replace("px", ""));
    var left_indent = parseInt($('.PortableMenuLink_Center ul').css('left'));

    $('.PortableMenuLink_Center ul:not(:animated)').animate({ 'left': '-=' + left_indent + 'px' }, 500, function () {
        $('.PortableMenuLink_Center ul li:first').before($('.PortableMenuLink_Center ul li:last'));
        $('.PortableMenuLink_Center ul').css({ 'left': (PortableMenuLink_spacePerItem * -1) + 'px' });
        
    });


}
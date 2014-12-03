<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_Article_ArticleImageSlider" Codebehind="ArticleImageSlider.ascx.cs" %>
<script>
    $(document).ready(function () {

        $('.ArticleImageSilder_Container').load("onload", function () {



            //Execute the slideShow
            var control = $(this).children('.ArticleImageSilder_gallary');
            slideShow(control);
            sizeAdjustment(control);
        });

        function slideShow(control) {

            //Set the opacity of all images to 0
            control.children('a').css({ opacity: 0.0 });

            //Get the first image and display it (set it to full opacity)
            control.children('a:first').css({ opacity: 1.0 });

            //Set the caption background to semi-transparent
            control.children('.caption').css({ opacity: 0.7 });

            //Resize the width of the caption according to the image width
            //$('#gallery .caption').css({ width: $('#gallery a').find('img').css('width') });

            //Get the caption of the first image from REL attribute and display it

            control.children('.caption').children('.content').html(control.children(' a:first').find('img').attr('rel')).animate({ opacity: 0.7 }, 400);
            //$('#gallery .content').html($('#gallery a:first').find('img').attr('rel')).animate({ opacity: 0.7 }, 400);

            //Call the gallery function to run the slideshow, 6000 = change to next image after 6 seconds
            //setInterval('gallery('+control+')', 6000);

            setInterval(function () {

                //if no IMGs have the show class, grab the first image
                var current = (control.children('a.show') ? control.children('a.show') : control.children('a:first'));

                //Get next image, if it reached the end of the slideshow, rotate it back to the first image
                var next = ((current.next().length) ? ((current.next().hasClass('caption')) ? control.children('a:first') : current.next()) : control.children('a:first'));

                //Get next image caption
                var caption = next.find('img').attr('rel');

                //Set the fade in effect for the next image, show class has higher z-index
                next.css({ opacity: 0.0 })
	    .addClass('show')
	    .animate({ opacity: 1.0 }, 1000);

                //Hide the current image
                current.animate({ opacity: 0.0 }, 1000)
	.removeClass('show');

                //Set the opacity to 0 and height to 1px
                control.children('.caption').animate({ opacity: 0.0 }, { queue: false, duration: 0 }).animate({ height: '1px' }, { queue: true, duration: 300 });

                //Animate the caption, opacity to 0.7 and heigth to 100px, a slide up effect
                control.children('.caption').animate({ opacity: 0.7 }, 50).animate({ height: '50px' }, 500);

                //Display the content
                control.children('.caption').html(caption);


            }, $("#<%= hf_interval.ClientID %>").val());

        }

        function gallery(control) {

            //if no IMGs have the show class, grab the first image
            var current = ($('#gallery a.show') ? $('#gallery a.show') : $('#gallery a:first'));

            //Get next image, if it reached the end of the slideshow, rotate it back to the first image
            var next = ((current.next().length) ? ((current.next().hasClass('caption')) ? $('#gallery a:first') : current.next()) : $('#gallery a:first'));

            //Get next image caption
            var caption = next.find('img').attr('rel');

            //Set the fade in effect for the next image, show class has higher z-index
            next.css({ opacity: 0.0 })
	    .addClass('show')
	    .animate({ opacity: 1.0 }, 1000);

            //Hide the current image
            current.animate({ opacity: 0.0 }, 1000)
	.removeClass('show');

            //Set the opacity to 0 and height to 1px
            $('#gallery .caption').animate({ opacity: 0.0 }, { queue: false, duration: 0 }).animate({ height: '1px' }, { queue: true, duration: 300 });

            //Animate the caption, opacity to 0.7 and heigth to 100px, a slide up effect
            $('#gallery .caption').animate({ opacity: 0.7 }, 50).animate({ height: '50px' }, 500);

            //Display the content
            $('div.caption').html(caption);


        }

        function sizeAdjustment(control) {

            var control_width = control.width();
            control.children().children('img').width(control_width);
            control.children('.content').css("width", control_width);
            control.children('a:first').attr("class", "ArticleImageSlideImageContainer_Item show");

        }
    });
</script>

<div id="ArticleImageSilder_Container" runat="server" class="ArticleImageSilder_Container">
<div id="gallery" class="ArticleImageSilder_gallary" runat="server">
<asp:Repeater ID="image_repeater" runat="server">
<ItemTemplate>
	<a href='<%# Eval("ArticleURL") %>' class="ArticleImageSlideImage_Item">
		<img id = "ArticleImageSlideImage" src="<%# Eval("ImageURL") %>" title="" alt="<%# Eval("Title")%>"  rel='<%# Eval("Title")%>' />
	</a>
    </ItemTemplate>
</asp:Repeater>



	<div class="caption">
    <div class="content">
    </div>
    </div>
</div>
<asp:HiddenField ID="hf_interval" runat="server"/>
<div class="clear"></div>
</div>

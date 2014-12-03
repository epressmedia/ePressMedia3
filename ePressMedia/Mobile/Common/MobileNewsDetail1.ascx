<%@ Control Language="C#" AutoEventWireup="true" Inherits="Mobile_Common_MobileNewsDetail1" Codebehind="MobileNewsDetail1.ascx.cs" %>


<script type="text/javascript">
    $(document).ready(function () {




        var width = $(".msg img").attr("width");
        var height = $(".msg img").attr("height");

        $(".msg img").load(function () {


            if (width == undefined) {

                var t = new Image();


                var newImg = new Image();
                newImg.src = $(".msg img").attr("src");
                height = newImg.height;
                width = newImg.width;
                imageresize();
            }
        });


        function imageresize() {

            var contentwidth = $(".msg").width();


            if (contentwidth < width) {
                $(".msg img").attr('width', contentwidth);
                $(".msg img").attr('height', ((contentwidth * height) / width));
            }

        }

        imageresize(); //Triggers when document first loads      


        $(window).bind("resize", function () {//Adjusts image when browser resized  
            imageresize();

        });


    });
</script>

 <ul data-role="listview" data-theme="d" data-inset="true">
<li>
<div id="threadView" class="thrdVw" >
    <div class="ttl">
      <asp:Literal ID="MsgTitle" runat="server" />
    </div>
    <div class="inf" style="position:relative">
      <div class="poster"><asp:Literal ID="PostBy" runat="server" /></div>
      <span class="lbl">등록일: </span><asp:Literal ID="IssueDate" runat="server" /><span class="colSep"></span>
      <span class="lbl">조회수: </span><asp:Literal ID="ViewCount" runat="server" /><span class="colSep"></span>
    </div>
    <div class="msg">
      <div>
        <asp:Label ID="SubTitle" runat="server" CssClass="subTtl" />
      </div>
       
      <asp:Literal ID="Message" runat="server" />
      
     </div>
</div>
</li>
</ul>
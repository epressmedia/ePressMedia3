<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="ArticleYouTubeView.ascx.cs" Inherits="Controls_Article_ArticleYouTubeView" %>
<script src=<%= this.ResolveClientUrl("~/Scripts/jquery.youtubeplaylist.js") %> type="text/javascript"></script>
<script type="text/ecmascript">
    $(function () {

        $("ul.demo2").ytplaylist({

            playerHeight: <%=ControlHeight%>,
            playerWidth: <%=ControlWidth%>,
            addThumbs: true,
            autoPlay: false,
            onChange: function () {
                console.log('changed');
            },
            holderId: 'ytvideo2'
        });
    });
</script>
<div id="ArticleYouTubeView_Container" runat="server" class="ArticleYouTubeView_Container">
<div id="ArticleYouTubeView_Contents" class="ArticleYouTubeView_Contents" runat="server">
    <div class="yt_holder">
    <div id="ytvideo2"></div>
    <ul class="demo2">
    <asp:Repeater ID="image_repeater" runat="server">
<ItemTemplate>
<li><a href='<%# Eval("YouTubeLink") %>' ><asp:Label ID="aa" runat="server" Text='<%# Eval("Subject") %>'></asp:Label></a></li>
</ItemTemplate>
</asp:Repeater>

    </ul>
    </div>
    </div>
    <div style="clear:both"></div>
    </div>

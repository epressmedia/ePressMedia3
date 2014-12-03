<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArticleImageSliderNivo.ascx.cs" Inherits="ePressMedia.Controls.Article.ArticleImageSliderNivo" %>
<link rel="stylesheet" href="../styles/nivo-slider.css" type="text/css" media="screen" />
    <script type="text/javascript" src="../scripts/jquery.nivo.slider.js"></script>
    <script type="text/javascript">
        $(window).load(function () {
            $('#slider').nivoSlider({
                pauseTime: $("#<%= hf_interval.ClientID %>").val()
            });
        });
    </script>
        <div id="wrapper" runat="server">
          <div class="slider-wrapper theme-default">
            <div id="slider" class="nivoSlider" style=<%= ControlHeightPx %>>
            <asp:Repeater ID="ArticleImageSliderNivo_Repeater" runat="server" 
                    onitemdatabound="ArticleImageSliderNivo_Repeater_ItemDataBound">
            <ItemTemplate>
            <a href='<%# Eval("ArticleURL") %>' class="ArticleImageSlideImage_Item" id="Article_Navigator" runat="server">
                <img src='<%# Eval("ImageURL") %>' data-thumb='<%# Eval("ImageURL") %>' alt="" data-transition='<%= TransitionType %>' />
                </a>
                </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <asp:HiddenField ID="hf_interval" runat="server"/>

    </div>
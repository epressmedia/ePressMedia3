<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_Article_RelatedArticleList"
    CodeBehind="RelatedArticleList.ascx.cs" %>
<div class="RelatedArticleList_Container">
    <div>
        <asp:Label ID="lbl_header" CssClass="lbl_header" runat="server" Visible="false"></asp:Label>
    </div>
    <style>
        .RelatedArticleList_Container .lbl_header
        {
            font-family: sans-serif;
            font-size: 20px;
        }
        
        
        .RelatedArticleList_GalleryView_Container
        {
            clear: both;
            width: 100%;
            padding: 4px 0;
        }

        .gw_panel
        {
            width: 128px;
            float: left;
            background: none;
            font-size: 11px;
            border: 1px solid transparent !important;
            display: block;
            text-decoration: none;
            cursor: pointer;
            margin: 1px;
            padding: 1px 1px 0;
            height: 180px;
            overflow: hidden;
        }
        .gw_img_div
        {
            display: block;
            line-height: 0;
            position: relative;
            background: none repeat scroll 0 0 #000;
            background-color: transparent;
            border: 1px solid #ccc;
            overflow: hidden;
            height: 120px;
        }
        
        .gw_img
        {
            border: none;
            display: block;
            margin-left: auto !important;
            margin-right: auto !important;
            background: none repeat scroll 0 0 #F0F0F0;
            border-top: 1px solid #FFF;
            outline: 1px solid #DDD; /*width: auto !important;*/
            padding: 2px !important;
        }
        
        .gw_img_div img
        {
            border: 1px solid #FFF !important;
            -moz-box-shadow: none !important;
            -webkit-box-shadow: none !important;
            -khtml-box-shadow: none !important;
            box-shadow: none !important;
            margin: 0 !important;
        }
        
        .gw_panel:hover
        {
            background: #F2F2F5;
            border: 1px solid #CCC !important;
            color: #000;
            text-decoration: none;
            margin: 1px;
            padding: 1px 1px 0;
        }
        
        .gw_panel:hover .gw_img
        {
            background: none repeat scroll 0 0 #333;
            border: 1px solid #555 !important;
            outline: 1px solid #111;
        }
        
        .gw_text
        {
            font-family: sans-serif;
            font-weight: 700;
            line-height: 140%;
            text-align: left;
        }
    </style>
    <div class="RelatedArticleList_GalleryView_Container">
        <asp:Repeater ID="ra_gw_repeater" runat="server" OnItemDataBound="ra_gw_repeater_ItemDataBound">
            <ItemTemplate>
                <a id="gw_panel" runat="server" class="gw_panel gw_rc_link gw_link gw_internal" href="">
                    <span class="gw_img_div">
                        <img id="gw_img" runat="server" class="gw_img" src="" style="height: 120px; width: 120px;"
                            alt="" />
                    </span><span class="gw_text"><span class="gw_post_title">
                        <asp:Literal ID="ltr_title" runat="server"></asp:Literal>
                    </span></span></a>
            </ItemTemplate>
        </asp:Repeater>
        <div style="clear: both;">
        </div>
    </div>

    <style>
        .RelatedArticleList_TextView_Container
        {
            clear: both;
width: 100%;
padding: 4px 0;
        }
         .RelatedArticleList_TextView_Container ul {
margin-bottom: 0!important;
margin: 0 0 15px 0;
padding: 0 0 0 30px;
list-style: none;
}
 .RelatedArticleList_TextView_Container  ul li {
list-style-type: circle;
}
.rl_post_title {
font-weight: 700;
font-size: 14px;
color:
}
.rl_post_title:hover
{
    color: rgb(111, 111, 111);
    text-decoration:underline;
}
    </style>
    <div class="RelatedArticleList_TextView_Container">
      
            <ul>
                    <asp:Repeater ID="ra_tw_repeater" runat="server" OnItemDataBound="ra_tw_repeater_ItemDataBound">
            <ItemTemplate>
                <li><a id = "tw_panel" runat="server" class="tw_panel" href="" style="height: 17px;">
                    <span class="rl_post_title">
                    <asp:Literal ID="ltr_title" runat="server"></asp:Literal>
                    </span>
                    <div class="rl_clear" style="height: 1px;">
                    </div>
                </a></li>
                </ItemTemplate>
                </asp:Repeater>
            </ul>
            <div style="clear: both;">
            </div>
    </div>
    <style>
    
    .ra_lw_panel {
padding: 6px;
border-bottom: 1px dotted  #DBDBDB;
overflow: hidden;
height: 1%;
}

.ra_lw_panel:hover
{
    background-color: #f7f7f7
}

.ra_lw_thumb
{
    float: left;
margin-right: 10px;
padding:2px;
border: 1px solid #C4C4C4;
}

.ra_lw_thumb img {
width: 80px;
height: 80px;
padding: 0;
border: none !important;


}
    .ra_lw_content
    {
        overflow: hidden;
    }
    .ra_lw_desc
    {
position: relative;
    }
    .ra_lw_detail
    {
    line-height: 16pt;
    color: #666;
    }
    .ra_lw_title {
font-weight: 700;
}
    
    </style>
    <div class="RelatedArticleList_ListView_Container">
        <asp:Repeater ID="ra_lw_repeater" runat="server" OnItemDataBound="ra_lw_repeater_ItemDataBound">
            <ItemTemplate>
                <div class="ra_lw_panel">
                    <div class="ra_lw_thumb" id="ra_lw_thumb" runat="server">
                        <asp:HyperLink ID="ViewImageLink" runat="server">
                            <asp:Image ID="Thumb" runat="server" />
                        </asp:HyperLink>
                    </div>
                    <div class="ra_lw_content">
                        <asp:HyperLink ID="ViewLink" runat="server" Text=''>
            <div class="ra_lw_desc">
            <div class="ra_lw_title"><%# Eval("Title") %></div>
                <div class="ra_lw_subtitle"><%# Eval("SubTitle") %></div>
                </div>
                <div class="ra_lw_detail">
                    <asp:Literal ID="ltr_detail" runat="server"></asp:Literal>
                </div>
                        </asp:HyperLink>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>

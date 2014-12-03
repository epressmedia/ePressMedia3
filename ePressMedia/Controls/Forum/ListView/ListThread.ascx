<%@ Control Language="C#" AutoEventWireup="true" Inherits="Forum_ListThread" CodeBehind="ListThread.ascx.cs" %>
<%@ Register Src="~/Controls/Pager/FitPager.ascx" TagName="FitPager" TagPrefix="uc1" %>
<script type="text/javascript" src="../scripts/jquery.lightbox-0.5.js"></script>
<link rel="stylesheet" type="text/css" href="../styles/jquery.lightbox-0.5.css" media="screen" />
<asp:HiddenField ID="CurThread" runat="server" Value="0" />
<asp:HiddenField ID="Params" runat="server" />
<asp:HiddenField ID="CatId" runat="server" />
<div>
    <asp:Button ID="btn_post2" runat="server" CssClass="boxLnk toRite" Text="<%$ Resources: Resources, Forum.lbl_NewThread%>" />
    
</div>
  <div class="secClr"></div>
<asp:Repeater ID="DataRepeater" runat="server" OnItemDataBound="DataRepeater_ItemDataBound"
    Visible="false">
    <HeaderTemplate>
        <table width="100%">
            <tr>
                <th class="label num">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources: Resources, Forum.lbl_Number%>"></asp:Literal>
                </th>
                <th class="label">
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources: Resources, Forum.lbl_Subject%>"></asp:Literal>
                </th>
                <th class="label name">
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources: Resources,Forum.lbl_Name %>"></asp:Literal>
                </th>
                <th class="label name">
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources: Resources, Forum.lbl_Date %>"></asp:Literal>
                </th>
                <th class="label num">
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources: Resources, Forum.lbl_Views %>"></asp:Literal>
                </th>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td class="data cntr">
                <asp:Literal runat="server" Text='<%# Eval("ThreadNum") %>' Visible='<%# !((bool)Eval("Announce")) %>' />
                <asp:Label runat="server" CssClass="notice" Text="Notice" Visible='<%# Eval("Announce") %>' />
            </td>
            <td class="data">
                <asp:HyperLink ID="ViewLink" runat="server" Text='<%# Eval("Subject") %>' />
                <img runat="server" src="~/Img/lock.png" visible='<%# Eval("Private") %>' alt="" />
                <img id="img_attachment" runat="server" src="~/Img/attachment.png" visible="false" alt="Attachment" />

                <asp:Label ID="CommCount" CssClass="comCnt" runat="server" />
                <img id="img_new" runat="server" src="~/Img/inew.gif" alt="" visible="false" />
            </td>
            <td class="data cntr">
                <%# Eval("PostBy") %>
            </td>
            <td class="data cntr">
                <asp:Literal ID="lbl_PostDate" runat="server"></asp:Literal>
            </td>
            <td class="data cntr">
                <%# Eval("ViewCount") %>
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </table></FooterTemplate>
</asp:Repeater>
<asp:Repeater ID="DataImageRepeater" runat="server" OnItemDataBound="DataImageRepeater_ItemDataBound">
    <HeaderTemplate>
        <div class="DataImage_Container">
    </HeaderTemplate>
    <ItemTemplate>
        <div class="picBox">
            <div class="picNew">
                <asp:Image ID="ImgNew" runat="server" CssClass="mdl" ImageUrl="~/img/inew.gif" AlternateText="New"
                    Visible="false" />
            </div>
            <div class="picFrm">
                <asp:HyperLink ID="ViewLink1" runat="server">
                    <img id="Img1" runat="server" alt="" width="112" height="80" /></asp:HyperLink>
            </div>
            <div class="picTtl">
                <asp:HyperLink ID="ViewLink2" runat="server"><%# Eval("Subject") %></asp:HyperLink>
            </div>
            <div class="picDesc">
                <%# Eval("PostDate", "{0:MM/dd}") %>
                |
                <asp:Literal ID="Literal1" runat="server" Text=" <%$ Resources: Resources, Forum.lbl_Views %>"></asp:Literal>
                <%# Eval("ViewCount") %>
                <asp:Literal ID="CommCount" runat="server" />
            </div>
        </div>
    </ItemTemplate>
    <FooterTemplate>
        </div></FooterTemplate>
</asp:Repeater>
<div class="secClr">
</div>
<div class="cntrPnl">
    <asp:Button ID="btn_post" runat="server" CssClass="boxLnk toRite" Text="<%$ Resources: Resources, Forum.lbl_NewThread%>" />
    &nbsp;
    <uc1:FitPager ID="FitPager1" runat="server" OnPageNumberChanged="PageNumber_Changed"
        Visible="true" />
    &nbsp;
</div>
<div class="cntrPnl" id="forum_search_panel" runat="server">
    <asp:Panel ID="SearchPanel" runat="server" DefaultButton="SearchButton">
        <asp:DropDownList ID="SearchList" runat="server">
            <asp:ListItem Value="PostedBy" Text="<%$ Resources: Resources, Forum.lbl_Name %>" />
            <asp:ListItem Value="Subject" Text="<%$ Resources: Resources, Forum.lbl_Subject %>" />
            <asp:ListItem Value="Content" Text="<%$ Resources: Resources, Forum.lbl_Content %>" />
        </asp:DropDownList>
        <asp:TextBox ID="SearchValue" runat="server" Width="240px" />
        <asp:Button ID="SearchButton" runat="server" Text=" <%$ Resources: Resources, Gen.lbl_Search %> "
            OnClick="SearchButton_Click" CausesValidation="false" />
    </asp:Panel>
</div>
<div>
<epm:EntryPopup ID="EntryPopup" runat="server" />
</div>

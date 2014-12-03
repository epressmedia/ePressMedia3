<%@ Control Language="C#" AutoEventWireup="true"    Inherits="Mobile_Common_MobileBiz1" Codebehind="MobileBiz1.ascx.cs" %>
<div data-role="collapsible" data-collapsed="false">
    <h3>
        업소명 검색</h3>
    <form action="" method="get" id="biz_search_form">
    <div data-role="fieldcontain">
        <label for="search">
            업소 검색:</label>
        <input type="search" name="q" id="search" />
    </div>
    </form>
</div>
    <asp:Repeater runat="server" ID="FavBizRepeater">
    <HeaderTemplate>
    <ul id="biz_icon" class="biz_icon">
    </HeaderTemplate>
    <ItemTemplate>
        <li class="biz_icon_item">
        <a href="<%# Eval("URL") %>" class="biz_icon_link" rel="external">
    <span class="biz_icon_img" style="background-position: 0px 0px; background-image:url(<%# Eval("Icon") %>);"></span>
    <span class="biz_icon_name"><%# Eval("CatName") %></span>
    </a>
    </li>
    </ItemTemplate>
    <FooterTemplate>
    </ul>
    </FooterTemplate>
    </asp:Repeater>
     
     

<%--<div data-role="collapsible" data-collapsed="true">
    <h3>
        카타고리 검색</h3>
    <asp:Repeater runat="server" ID="BizRepeater">
        <HeaderTemplate>
            <ul data-role="listview" data-theme="g" data-inset="true">
                <li data-role="list-divider">카타고리 검색</li>
        </HeaderTemplate>
        <ItemTemplate>
            <li><a href="<%# string.Concat("Biz.aspx?char=",Container.DataItem)%>" rel="external">
                <%# Container.DataItem %></a> </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
</div>--%>

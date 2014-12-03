<%@ Control Language="C#" AutoEventWireup="true" Inherits="Mobile_Common_MobileBizSub1" Codebehind="MobileBizSub1.ascx.cs" %>
    <asp:Repeater runat="server" ID="BizSubRepeater" 
    onitemdatabound="BizSubRepeater_ItemDataBound">
    <HeaderTemplate>
    <ul data-role="listview" data-theme="g" data-inset="true">
    <li data-role="list-divider">카타고리 검색</li>
    </HeaderTemplate>
    <ItemTemplate>
        <li>
                         <a href='<%# string.Concat("Biz.aspx?catId=", Eval("CategoryId")) %>' rel="external">
                    <%# Eval("CategoryName") %><span class="ui-li-count"><asp:Literal ID="Count" runat="server"></asp:Literal></span></a>
                  

        </li>
     </ItemTemplate>
     <FooterTemplate>
    </ul>
    </FooterTemplate>
    </asp:Repeater>



<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MultiImageArticleSummary.ascx.cs"
    Inherits="ePressMedia.Controls.Article.MultiImageArticleSummary" %>

<div id="MultiImageArticleSummary_Container" runat="server" class="MultiImageArticleSummary_Container">
        <div class="MultiImageArticleSummary_Header">
        <a href='<%= MoreLink %>' class="MultiImageArticleSummary_btn_more_link">
            <asp:Label ID="lbl_header" runat="server"></asp:Label>
            </a>
                
        </div>
        <div style="clear: both">
        </div>
    <div class="MultiImageArticleSummary_TabContents" >

        <div  class="MultiImageArticleSummary_tabPnl">
            <div  class="MultiImageArticleSummary_repPrev">
             
                <img src='/img/caroprev.png' /></div>
            <div class="MultiImageArticleSummary_repCaro">
                <div  class="MultiImageArticleSummary_repCaroUl">
                    <asp:Repeater ID="ReportSetRep" runat="server" OnItemDataBound="ReportSetRep_ItemDataBound">
                        <ItemTemplate>
                            <div class="MultiImageArticleSummary_repSec">
                                <div>
                                   <asp:HyperLink ID="ImageLink" runat="server">
                                    <asp:Image ID="Thumb" runat="server" Width="85px" Height="60px" /></asp:HyperLink>
                                </div>
                                <div class="MultiImageArticleSummary_repTtl">
                                    <asp:HyperLink ID="ViewLink" runat="server" /></div>
                                <p>
                                    <asp:Literal ID="Preview" runat="server" />
                                </p>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="MultiImageArticleSummary_repNext">
                <img src='/img/caronext.png' /></div>
        </div>
    </div>
    <div style="clear: both">
    </div>
</div>

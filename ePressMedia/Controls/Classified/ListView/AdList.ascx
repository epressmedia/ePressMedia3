<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdList.ascx.cs" Inherits="ePressMedia.Classified.Controls.AdList" %>
<%@ Register src="~/Controls/Pager/FitPager.ascx" tagname="FitPager" tagprefix="uc1" %>
<style>

    
</style>
<asp:HiddenField ID="CatId" runat="server"  />
  <asp:HiddenField ID="CurAdId" runat="server" Value="0" />
  <asp:HiddenField ID="Params" runat="server" />  
  <div class="AdList_Container">
  <div id="classified_search_panel" runat="server" class="subPnl" style="line-height:24pt; text-align:center;">
  <asp:Panel ID="SearchPanel" runat="server" DefaultButton = "SearchButton">
    <span class="colLbl"><asp:Literal ID="lbl_Seach" runat="server" Text="<%$ Resources: Resources, Classified.lbl_Search%>"></asp:Literal></span>
    <asp:TextBox ID="SearchValue" runat="server" Width="180px" />
    <asp:Button ID="SearchButton" runat="server" Text=" <%$ Resources: Resources, Classified.lbl_Search%> " onclick="SearchButton_Click" CausesValidation="false" />
    </asp:Panel>    
  </div>
  
    <div class="" >
 

      <asp:Button ID="btn_post2" runat="server" CssClass="boxLnk toRite" Text="<%$ Resources: Resources, Gen.lbl_Add%>" />
      </div>
      <div class="secClr"></div>
  
    <asp:Repeater ID="AdRepeater_Simple" runat="server"  onitemdatabound="AdRepeater_Simple_ItemDataBound">

    <ItemTemplate>
                    <div class="AdRepeaterSimple_list">

            <div class="AdRepeaterSimple_list_thumb" id="AdRepeaterSimple_list_thumb" runat="server">
                <asp:HyperLink ID="ViewImageLink" runat="server">
                    <asp:Image ID="Thumb" runat="server" />
                </asp:HyperLink>
            </div>
            <div class="AdRepeaterSimple_list_artInf">
            <div class="AdRepeaterSimple_list_title">
                        <asp:HyperLink ID="ViewLink" runat="server" Text=''>
            <div class="ArticleSummaryList_title"><%# Eval("Subject") %></div>
            </asp:HyperLink>
            </div>
            <div>
            <asp:HyperLink ID="ViewLink2" runat="server" Text='' CssClass="AdList_Subtitle">
                </asp:HyperLink></div>
            </div>
        </div>
    </ItemTemplate>
      </asp:Repeater>
    <asp:Repeater ID="AdRepeater" runat="server" 
      onitemdatabound="AdRepeater_ItemDataBound">
          <HeaderTemplate>
    <table width="100%">
        <tr>
    <th class="label" >
        <asp:Literal ID="lbl_AdNumber" runat="server" Text="<%$ Resources: Resources, Classified.lbl_AdNumber%>"></asp:Literal></th>
      <th class="label" ><asp:Literal ID="lbl_Title" runat="server" Text="<%$ Resources: Resources, Classified.lbl_Title%>"></asp:Literal></th>
      <th class="label name"><asp:Literal ID="lbl_Name" runat="server" Text="<%$ Resources: Resources, Classified.lbl_Name%>"></asp:Literal></th>
      <th class="label date"><asp:Literal ID="lbl_Date" runat="server" Text="<%$ Resources: Resources, Classified.lbl_Date %>"></asp:Literal></th>
      <th class="label num"><asp:Literal ID="lbl_View" runat="server" Text="<%$ Resources: Resources, Classified.lbl_View %>"></asp:Literal></th>
    </tr>
    </HeaderTemplate>
      <ItemTemplate>
        <tr class="AdList_Divider">

        <td class="data" align="center">

        <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("AdId")%>' Visible='<%# !((bool)Eval("Announce")) %>' />
            <asp:Label ID="Label1" runat="server" CssClass="notice" Text="<%$ Resources: Resources, Classified.lbl_Notice%>" Visible='<%# Eval("Announce") %>' />
         
        </td>
          
          <td class="data">
            <asp:Image ID="Thumb" runat="server"  style="margin-right:5px;float:left" />
            <asp:HyperLink ID="ViewLink" CssClass="AdList_Subject" runat="server" Text='<%# Eval("Subject") %>' Font-Bold='<%# Eval("Announce") %>' />
            <asp:Label ID="CommCnt" runat="server" CssClass="comCnt" Text='' />
            <img id="imgnew" runat="server" src="~/Img/inew.gif" alt="New" visible = "false" />
            <br />
            <asp:Label ID="SubTitle" runat="server" class="AdList_Subtitle" />
          </td>
          <td class="data" align="center">
           <%# Eval("PostBy") %>
          </td>
          <td class="data" align="center">
              <asp:Label ID="RegDate" runat="server" Text=""></asp:Label>
          </td>
          <td class="data" align="center">
            <%# Eval("ViewCount") %>
          </td>
        </tr>
      </ItemTemplate>
      <FooterTemplate>
      </table>
      </FooterTemplate>
    </asp:Repeater>
  
  <div class="cntrPnl">
 

      <asp:Button ID="btn_post" runat="server" CssClass="boxLnk toRite" Text="<%$ Resources: Resources, Gen.lbl_Add%>" />
    &nbsp;
    <uc1:FitPager ID="FitPager1" runat="server" RowsPerPage="20" OnPageNumberChanged="PageNumber_Changed" Visible="true" />
    &nbsp;
  </div>
  </div>
  <div>
  <epm:EntryPopup ID="EntryPopup" runat="server" />
</div>
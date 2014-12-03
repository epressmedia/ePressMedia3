<%@ Control Language="C#" AutoEventWireup="true" Inherits="Mobile_Common_MobileAdList1" Codebehind="MobileAdList1.ascx.cs" %>


    <asp:HiddenField ID="PageNumber" runat="server" />
    <asp:HiddenField ID="MaxPageNumber" runat="server" />
    <asp:HiddenField ID="CatId" runat="server" />
        <fieldset class="ui-grid-a">
    <div class="ui-block-b ddl_other">
        <asp:Repeater runat="server" ID="AdCatRepeator">
            <HeaderTemplate>
                <select name="AdCatSelector" id="AdCatSelector">
                    <option value="-1">다른 생활정보 보기...</option>
            </HeaderTemplate>
            <ItemTemplate>
                <option value='<%# Eval("Id") %>'>
                    <%#Eval("Label") %></option>
            </ItemTemplate>
            <FooterTemplate>
                </select>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</fieldset>
    <asp:Repeater ID="AdRepeater" runat="server" 
      onitemdatabound="AdRepeater_ItemDataBound">
      <HeaderTemplate>
      <ul data-role="listview" data-theme="d" data-inset="true" class="knn_list">
      <li data-role="list-divider"><asp:Label ID="listheader1" runat="server"></asp:Label></li>
      </HeaderTemplate>
      <ItemTemplate>
      <li>
      <table id="adlistTable">
       <tr>
      <td id="adlistImgCol">
       <a href="<%# string.Concat("classifieddetail.aspx?aid=", Eval("AdId")) %>"  rel=external>
      <asp:Image ID="Thumb" runat="server" width="70px" height="70px" /></a>
      </td>
      <td id="adlistContentCol">
      
       <p class="adtype"><asp:Literal ID="Literal2" runat="server" Text='<%# Eval("TypeLabel", "[{0}]") %>' Visible='<%# ((Container.DataItem as Knn.Classified.Ad).CategoryId == 1) %>' /></p>
      
      <a class="adlink" href="<%# string.Concat("classifieddetail.aspx?aid=", Eval("AdId")) %>"  rel=external>
      <%# Eval("Subject") %>
      </a>
      <p> Posted Date: <%# Eval("RegDate", "{0:MM/dd/yy}") %></br>
      </td>
      </tr>
      </table>


        </li>
      </ItemTemplate>
      <FooterTemplate>
      </ul>
      </FooterTemplate>
    </asp:Repeater>

    <fieldset class="ui-grid-a" id="navi_field">
    <div class="ui-block-b" id="div_prev"><a id="btn_prev" href="<%= string.Concat("list.aspx?CatId=",CatId.Value,"&page=", (int.Parse(PageNumber.Value)-1)  ) %>" rel="external" data-role="button" data-icon="arrow-l">Prev</a></div>
    <div class="ui-block-b" ><a id="btn_next" href="<%= string.Concat("list.aspx?CatId=",CatId.Value,"&page=", (int.Parse(PageNumber.Value)+1)  ) %>" rel="external" data-role="button" data-icon="arrow-r" data-iconpos="right">Next</a></div>
    </fieldset>

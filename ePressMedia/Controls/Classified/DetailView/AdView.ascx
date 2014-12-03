<%@ Control Language="C#" AutoEventWireup="true" Inherits="Classified_AdView" Codebehind="AdView.ascx.cs" %>


<%--<%@ Reference Control="~/Master/MasterPage_1141.master"%>--%>
<style>
    .AdView_image
    {
        max-width:100%;
        margin: 5px;;
    }
    .div_AdView_image
    {
        text-align: center;
    }
</style>



  <asp:HiddenField ID="UserName" runat="server" />

  <div class="thrdVw">
    <div class="inf">
      <span class="lbl2">Subject</span>
      <span class="ttl2"><asp:Literal ID="Subject" runat="server" /></span>
    </div>
    <div class="inf">
      <span class="lbl2">Name</span>
      <span class="data"><asp:Literal ID="PostBy" runat="server" /></span>
      <span class="lbl2">Date</span>
      <span class="data"><asp:Literal ID="PostDate" runat="server" /></span>
    </div>
    <div class="inf">
      <span class="lbl2">Email</span>
      <span class="data"><asp:Literal ID="Email" runat="server" /></span>
      <span class="lbl2">Phone</span>
      <span class="data"><asp:Literal ID="Phone" runat="server" /></span>
    </div>
    <div class="inf">
      <span class="lbl2">View</span>
      <span class="data"><asp:Literal ID="ViewCount" runat="server" /></span>
      <span class="lbl2">IP</span>
      <span class="data"><asp:Literal ID="IpAddr" runat="server" /></span>
    </div>
  
    <div id="exInfPnl" class="exinf" runat="server" visible="false">
      <div>
        <asp:Literal ID="ExInfo" runat="server" />
      </div>
      <div id="mapPnl" runat="server" class="cntrPnl" visible="false">
        <iframe runat="server" id="MapFrame" width="480" height="300" scrolling="no" frameborder="0" />
      </div>
    </div>

    <div class="clsPnl">
      <div class="cntrPnl">
        <iframe runat="server" id="PicFrame" width="480" height="380" scrolling="no" frameborder="0" />
      </div>
      <div class="adDesc">
        <asp:Literal ID="Desc" runat="server" />
      </div>
      
      <div class="div_AdView_image">
          <asp:Repeater ID="ImageRepeater" runat="server" OnItemDataBound="ImageRepeater_ItemDataBound">
          <ItemTemplate>
                    <div>
                <img id="adImage" runat="server" class="AdView_image" />
          </div>
          </ItemTemplate>

          </asp:Repeater>
      </div>
    </div>
  </div>
  

  <div class="cmdPnlR" style="margin-bottom:10px">
    <asp:Button ID="DelButton" runat="server" CssClass="boxLnk" Text="<%$ Resources: Resources, Gen.lbl_Delete %>"  />
    
                      <asp:Button ID="EditLink_popup" runat="server"  CssClass="boxLnk"
          Text="<%$ Resources: Resources, Gen.lbl_Edit %>" Visible="false" />
    <asp:HyperLink ID="ListLink" runat="server" CssClass="boxLnk" Text="<%$ Resources: Resources, Gen.lbl_List %>" />

    <div class="snsLnk">
                            <telerik:RadSocialShare runat="server" ID="socialShareNetworkShare" >
                        <MainButtons>
                            <telerik:RadSocialButton SocialNetType="ShareOnFacebook" />
                            <telerik:RadSocialButton SocialNetType="ShareOnTwitter" />
                            <telerik:RadSocialButton SocialNetType="GoogleBookmarks" />
                            <telerik:RadSocialButton SocialNetType="StumbleUpon" />
                        </MainButtons>
                    </telerik:RadSocialShare>
    </div>
  </div>

    <div>
<epm:EntryPopup ID="EntryPopupEdit" runat="server" />
        <epm:EntryPopup ID="DeletePopup" runat="server" />
        
  </div>


  
  
  
  
<%@ Control Language="C#" AutoEventWireup="true" Inherits="Mobile_Common_MobileAdDetail1" Codebehind="MobileAdDetail1.ascx.cs" %>
  
  
  


  
  <h1><asp:Literal ID="Subject" runat="server"  /></h1>
<asp:Repeater ID="ImageGallery" runat="server" 
    onitemdatabound="ImageGallery_ItemDataBound" >
<HeaderTemplate>
<ul data-role="listview" data-inset="true"  >
<li data-role="list-divider">Pictures</li>
  <li><p></p>
<div class="AdImage"></HeaderTemplate>
<ItemTemplate>

 <%--<img id="thumbnail" src=<%# Eval("ThumbSize") %>  data-large="<%# Eval("FullSize") %>"/>  --%>
    <asp:Image ID="thumbnail" runat="server" />
 
</ItemTemplate>
<FooterTemplate></div>
</li>
</ul></FooterTemplate>
</asp:Repeater>


<%--IP address is not needed to be displayed in the Mobile App but leave just in case --%>
<%--<asp:Label ID="IpAddr" runat="server" Text="" />--%>

<ul data-role="listview" data-inset="true"  >
<li data-role="list-divider">Description</li>
<li id="AdDescription" data-theme="d"><asp:Literal ID="Desc" runat="server" Text="" /> </li>
<li id="AdPostInfo" data-theme="d">
<table id ="PostInfoTable">
<tr>
<td id = "PostInfoTd">
Posted By<br/><asp:Literal ID="PostBy" runat="server" Text="" />
</td>
<td id = "PostInfoTd">
Posted Date<br/><asp:Literal ID="PostDate" runat="server" Text="" />
</td>   
<td id = "PostInfoTd">
Visits<br/><asp:Literal ID="ViewCount" runat="server" Text="" />
</td>
</tr>
</table>

</li>
</ul>

<a id="tel_link" runat="server" data-role="button" rel="external" >Tel : <asp:Literal ID="Phone" runat="server" Text="" /></a>
<a id = "mail_link" runat="server" data-role="button" rel="external">Email : <asp:Literal ID="Email" runat="server" Text="" /></a>

<!--Out of Scope for Phase1-->
<%-- <div class="snsLnk">
      <div id="fb-root"></div><script src="http://connect.facebook.net/en_US/all.js#xfbml=1"></script><fb:like href="" send="false" layout="button_count" width="450" show_faces="true" font="arial"></fb:like>
      <div class="twLnk">
        <a href="http://twitter.com/share" class="twitter-share-button" data-count="none">Tweet</a><script type="text/javascript" src="http://platform.twitter.com/widgets.js"></script>
      </div>
  </div>--%>


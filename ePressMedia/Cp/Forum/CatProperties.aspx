<%@ Page Title="" Language="C#"  AutoEventWireup="true" Inherits="Cp_Forum_ForumConfig" Codebehind="CatProperties.aspx.cs" %>
<%@ Register src="~/CP/Controls/Toolbox.ascx" tagname="Toolbox" tagprefix="uc" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/CP/Style/Cp.css" rel="stylesheet" type="text/css" />
    
   
</head>
<body>
    <form id="form1" runat="server">

      <h1>Forum Configuration - <asp:Literal ID="ForumName" runat="server" /></h1>
          <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
  </telerik:RadScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

  <div>
  <uc:toolbox id="toolbox1" runat="server" ButtonAvailable= "edit,save,cancel" ></uc:toolbox>
  </div>
  <table>
    <tr>
      <td class="label">Allow Private Thread</td>
      <td class="data">
        <asp:CheckBox ID="AllowPrivacy" runat="server" 
          Text="Allow to post a private thread only visiable to forum administrator." />
      </td>
    </tr>
    <tr>
      <td class="label">Private Forum</td>
      <td class="data">
        <asp:CheckBox ID="PrivateOnly" runat="server" Text="All threads will be posted as private" />
      </td>
    </tr>
    <tr>
      <td class="label">Allow Attachment</td>
      <td class="data">
        <asp:CheckBox ID="AllowAttach" runat="server" Text="Allow to upload and attach images and documents." />
      </td>
    </tr>
    <tr>
      <td class="label">Notify Post</td>
      <td class="data">
        <asp:CheckBox ID="NotifyPost" runat="server" Text="Notify a thread post event via email." />
      </td>
    </tr>
    <tr>
      <td class="label">Notify Email Address</td>
      <td class="data">
        <asp:TextBox ID="MailList" runat="server" Width="400px"></asp:TextBox><br />
        Seperate multiple emails in comma(,)
      </td>
    </tr>

  </table>
  </ContentTemplate>
  </asp:UpdatePanel>
  <div>
  <br />
  </div>

    </form>
</body>
</html>





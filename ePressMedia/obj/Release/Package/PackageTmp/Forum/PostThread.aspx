<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage_1131.master" AutoEventWireup="true" Inherits="Forum_PostThread" Codebehind="PostThread.aspx.cs" %>
<%@ MasterType VirtualPath="~/Master/MasterPage_1131.master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register TagPrefix="Upload" Namespace="Brettle.Web.NeatUpload" Assembly="Brettle.Web.NeatUpload" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <script type="text/javascript">
    function hideMessageBox() {
      var m = $find('MsgBoxMpe');
      if (m)
        m.hide();
    }
  </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
  <div class="forum_PostThread">
    <table class="postTbl">
      <tr runat="server" id="annRow" visible="false">
        <td class="label" style="width:100px;">
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources: Resources, Forum.lbl_Announcement %>"></asp:Literal> </td>
        <td>
          <asp:CheckBox ID="ChkAnnounce" runat="server" Text=" <%$ Resources: Resources, Forum.lbl_AnnouncementCheck %>" />
        </td>
      </tr>
      <tr runat="server" id="prvRow">
        <td class="label" style="width:100px;"><asp:Literal ID="Literal2" runat="server" Text="<%$ Resources: Resources, Forum.lbl_Private %>"></asp:Literal></td>
        <td>
          <asp:CheckBox ID="ChkPrivate" runat="server" Text=" <%$ Resources: Resources, Forum.lbl_PrivateCheck %>" />
        </td>
      </tr>
      <tr>
        <td class="label"><asp:Literal ID="Literal3" runat="server"  Text="<%$ Resources: Resources, Forum.lbl_Subject %>"></asp:Literal></td>
        <td>
          <asp:TextBox ID="Subject" runat="server" MaxLength="128" Width="400" />
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$ Resources: Resources,Forum.lbl_RequiredField %>"
              ControlToValidate="Subject" Display="Dynamic" />
        </td>
      </tr>
      <tr>
        <td class="label"><asp:Literal ID="Literal4" runat="server"  Text="<%$ Resources: Resources, Forum.lbl_Name %>"></asp:Literal></td>
        <td>
          <asp:TextBox ID="PostBy" runat="server" MaxLength="16" />
          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="<%$ Resources: Resources,Forum.lbl_RequiredField %>"
              ControlToValidate="PostBy" Display="Dynamic" />
        </td>
      </tr>
      <tr id="pwdRow" runat="server">
        <td class="label"><asp:Literal ID="Literal5" runat="server"  Text="<%$ Resources: Resources, Forum.lbl_Password %>"></asp:Literal></td>
        <td>
          <asp:TextBox ID="Password" runat="server" MaxLength="8" TextMode="Password" /><br />
            <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources: Resources, Forum.lbl_PasswordMsg %>"></asp:Literal>

          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="<%$ Resources: Resources,Forum.lbl_RequiredField %>"
              ControlToValidate="Password" Display="Dynamic" />
        </td>
      </tr>
      <tr>
        <td class="label"><asp:Literal ID="Literal6" runat="server" Text="<%$ Resources: Resources, Forum.lbl_Captcha %>"></asp:Literal></td>
        <td>
          <asp:TextBox ID="Captcha" runat="server" MaxLength="6" />
          <asp:Image id="CapImg" runat="server" ImageUrl="~/Controls/JpegCaptcha.aspx" AlternateText="" style="vertical-align:middle" />
          <br />
          <span class="ex">
              <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources: Resources, Forum.lbl_CaptchaMsg %>"></asp:Literal>
          </span>
          <asp:RequiredFieldValidator ID="CapReq" runat="server" ErrorMessage="<%$ Resources: Resources,Forum.lbl_RequiredField %>"
              ControlToValidate="Captcha" Display="Dynamic" />
          <asp:CompareValidator ID="CapCompValidator" runat="server" ErrorMessage="<%$ Resources: Resources,Captcha.ErrorMassage %>" 
            ControlToValidate="Captcha" Display="Dynamic"  />
        </td>
      </tr>
      <tr>
        <td class="label"><asp:Literal ID="Literal7" runat="server"  Text="<%$ Resources: Resources, Forum.lbl_Content %>"></asp:Literal></td>
        <td>
        <CKEditor:CKEditorControl ID="CkEditor" runat="server"  Toolbar="Source
        Undo|Redo|Bold|Italic|Underline|Strike|JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|Link|Table
      /
      NumberedList|BulletedList|-|Outdent|Indent|TextColor|BGColor
      /
      Styles|Format|Font|FontSize" 
            Width="100%" Height="300px">
        </CKEditor:CKEditorControl> 
        </td>
      </tr>
      <tr id="attRow" runat="server">
        <td class="label"><asp:Literal ID="Literal8" runat="server"  Text="<%$ Resources: Resources, Forum.lbl_Attachments %>"></asp:Literal></td>
        <td>
          <div class="upldPnl">
            <Upload:MultiFile ID="MultiFile1" runat="server" FileQueueControlID="imgListBox" UseFlashIfAvailable="True">
              <asp:Button ID="BrowseButton" runat="server" Text="<%$ Resources: Resources, Forum.btn_Attachments %>" />
            </Upload:MultiFile>
          </div>
          <Upload:ProgressBar ID="inlineProgressBar" runat="server" Inline="False" /> 
          
          <asp:RegularExpressionValidator ID="RegExpExt" ControlToValidate="MultiFile1" 
            ValidationExpression="(([^.;]*[.])+(jpg|gif|png|pdf|doc|xls|docx|xlsx|zip|JPG|GIF|PNG|PDF|DOC|XLS|DOCX|XLSX|ZIP); *)*(([^.;]*[.])+(jpg|gif|png|pdf|doc|xls|docx|xlsxlzip|JPG|GIF|PNG|PDF|DOC|XLS|DOCX|XLSX|ZIP))?$"
            Display="Dynamic" ErrorMessage="<%$ Resources: Resources, Forum.msg_Attachments %>" EnableClientScript="True" runat="server" />
          <div id="imgListBox">
          </div>
        
        </td>
      </tr>    
    </table>

    <div class="cntrPnl">
      <asp:LinkButton ID="PostLink" runat="server" CssClass="boxLnk" Text="<%$ Resources: Resources,Gen.lbl_Submit %>" 
        onclick="PostLink_Click" />
      <span class="colSep"></span>
      <asp:HyperLink ID="ListLink" runat="server" CssClass="boxLnk" Text="<%$ Resources: Resources,Gen.lbl_Cancel %>" />    
    </div>
    </div>
</asp:Content>


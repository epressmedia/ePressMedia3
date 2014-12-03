<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Entry.ascx.cs" Inherits="ePressMedia.Forum.Entry" %>
    

    <div class="ForumPost_Container" style="padding: 10px; background-color:White;">

            <div class="secClr">
            <asp:HiddenField ID="ImgCount" runat="server" />
        </div>
    <table class="postTbl" width="100%">
      <tr runat="server" id="annRow" visible="false">
        <td class="label" >
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources: Resources, Forum.lbl_Announcement %>"></asp:Literal> </td>
        <td class="data">
          <asp:CheckBox ID="ChkAnnounce" runat="server" Text=" <%$ Resources: Resources, Forum.lbl_AnnouncementCheck %>" />
        </td>
      </tr>
      <tr runat="server" id="prvRow">
        <td class="label" ><asp:Literal ID="Literal2" runat="server" Text="<%$ Resources: Resources, Forum.lbl_Private %>"></asp:Literal></td>
        <td class="data">
          <asp:CheckBox ID="ChkPrivate" runat="server" Text=" <%$ Resources: Resources, Forum.lbl_PrivateCheck %>" />
        </td>
      </tr>
      <tr>
        <td class="label"><asp:Literal ID="Literal3" runat="server"  Text="<%$ Resources: Resources, Forum.lbl_Subject %>"></asp:Literal></td>
        <td class="data">
          <asp:TextBox ID="Subject" runat="server" MaxLength="128" Width="100%"  />
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$ Resources: Resources,Forum.lbl_RequiredField %>"
              ControlToValidate="Subject" Display="Dynamic" ValidationGroup="Submit" />
        </td>
      </tr>
      <tr>
        <td class="label"><asp:Literal ID="Literal4" runat="server"  Text="<%$ Resources: Resources, Forum.lbl_Writer %>"></asp:Literal></td>
        <td class="data">
          <asp:TextBox ID="PostBy" runat="server" MaxLength="16" />
          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="<%$ Resources: Resources,Forum.lbl_RequiredField %>"
              ControlToValidate="PostBy" Display="Dynamic" ValidationGroup="Submit"/>
        </td>
      </tr>
      <tr id="pwdRow" runat="server">
        <td class="label"><asp:Literal ID="Literal5" runat="server"  Text="<%$ Resources: Resources, Forum.lbl_Password %>"></asp:Literal></td>
        <td class="data">
          <asp:TextBox ID="Password" runat="server" MaxLength="8" TextMode="Password" /><br />
            <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources: Resources, Forum.lbl_PasswordMsg %>"></asp:Literal>

          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="<%$ Resources: Resources,Forum.lbl_RequiredField %>"
              ControlToValidate="Password" Display="Dynamic"  ValidationGroup="Submit"/>
        </td>
      </tr>
      <tr id="CaptchaRow" runat="server">
        <td class="label"><asp:Literal ID="Literal6" runat="server" Text="<%$ Resources: Resources, Forum.lbl_Captcha %>"></asp:Literal></td>
        <td class="data">
          <asp:TextBox ID="Captcha" runat="server" MaxLength="6" />
          <asp:Image id="CapImg" runat="server" ImageUrl="~/Controls/JpegCaptcha.aspx" AlternateText="" style="vertical-align:middle" />
          <br />
          <span class="ex">
              <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources: Resources, Forum.lbl_CaptchaMsg %>"></asp:Literal>
          </span>
          <asp:RequiredFieldValidator ID="CapReq" runat="server" ErrorMessage="<%$ Resources: Resources,Forum.lbl_RequiredField %>"
              ControlToValidate="Captcha" Display="Dynamic" ValidationGroup="Submit" />
   <%--       <asp:CompareValidator ID="CapCompValidator" runat="server" ErrorMessage="<%$ Resources: Resources,Captcha.ErrorMassage %>" 
            ControlToValidate="Captcha" Display="Dynamic"  ValidationGroup="Submit" />--%>
        </td>
      </tr>
      <tr>
        <td class="label"><asp:Literal ID="Literal7" runat="server"  Text="<%$ Resources: Resources, Forum.lbl_Content %>"></asp:Literal></td>
        <td class="data">
        <telerik:RadEditor ID="ContentEditor" runat="server" Width="100%"  ToolbarMode="Default" ToolsFile="/Styles/ForumPost.xml" EditModes="Design">
        </telerik:RadEditor>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="<%$ Resources: Resources,Forum.lbl_RequiredField %>"
              ControlToValidate="ContentEditor" Display="Dynamic" ValidationGroup="Submit" />
        </td>
      </tr>
      <tr id="attRow" runat="server">
        <td class="label"><asp:Literal ID="Literal8" runat="server"  Text="<%$ Resources: Resources, Forum.lbl_Attachments %>"></asp:Literal></td>
        <td class="data">
        <asp:CheckBox ID="ChkDelFiles" runat="server" Text="<%$ Resources: Resources, Forum.lbl_AttachmentDelete %>" Visible = "false"/><br />
          <div class="upldPnl">
            <Upload:MultiFile ID="MultiFile1" runat="server" FileQueueControlID="imgListBox" UseFlashIfAvailable="True">
              <asp:Button ID="BrowseButton" runat="server" Text="<%$ Resources: Resources, Forum.btn_Attachments %>" />
            </Upload:MultiFile>
          </div>
          <Upload:ProgressBar ID="inlineProgressBar" runat="server" Inline="False" /> 
          
          <asp:RegularExpressionValidator ID="RegExpExt" ControlToValidate="MultiFile1" 
            
            Display="Dynamic" ErrorMessage="<%$ Resources: Resources, Forum.msg_Attachments %>" EnableClientScript="True" runat="server" ValidationGroup="Submit" />
          <div id="imgListBox">
          </div>
        
        </td>
      </tr>    
    </table>

    <div class="cntrPnl">

                <asp:Button ID="btn_Save" runat="server" Text="<%$ Resources: Resources,Gen.lbl_Submit %>" OnClick="btn_Save_Click"  ValidationGroup="Submit"/>
            <asp:Button ID="btn_Cancel" runat="server" Text="<%$ Resources: Resources,Gen.lbl_Cancel %>" OnClick="btn_Cancel_Click" />
 
    </div>
 

    </div>
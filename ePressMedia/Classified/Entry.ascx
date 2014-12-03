<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Entry.ascx.cs" Inherits="ePressMedia.Classified.Entry" %>
<div class="ClassifiedPost_Container" style="padding: 10px; background-color:White;">

<asp:HiddenField ID="CatId" runat="server" />
<asp:HiddenField ID="MaxWidth" runat="server" Value="560" />



  <table class="postTbl">
  <tr>
    <td class="label">작성자*</td>
    <td class="data">
      <asp:TextBox ID="PostBy" runat="server" MaxLength="60" /><br />
      <asp:RequiredFieldValidator ID="ReqPostBy" runat="server" ErrorMessage="작성자 명을 입력하세요"
        ControlToValidate="PostBy" Display="Dynamic" ValidationGroup = "Submit" />  
    </td>
  </tr>
  <tr>
    <td class="label">이메일</td>
    <td class="data">
      <asp:TextBox ID="Email" runat="server" CssClass="short" MaxLength="100" /><br />
      <asp:RegularExpressionValidator ID="RegExEmail1" runat="server" 
          ControlToValidate="Email" ErrorMessage="잘못된 이메일 형식입니다." 
          ValidationExpression="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" 
          Display="Dynamic" ValidationGroup = "Submit" ></asp:RegularExpressionValidator>
    </td>
  </tr>
  <tr>
    <td class="label">전화</td>
    <td class="data">
    <telerik:RadMaskedTextBox ID = "Phone" runat="server" Mask="###-###-####" DisplayMask="###-###-####"></telerik:RadMaskedTextBox>
    </td>
  </tr>

  <tr runat="server" id="pwdRow">
    <td class="label">비밀번호*</td>
    <td class="data">
      <asp:TextBox ID="Password1" runat="server" TextMode="Password" MaxLength="8" />
      <span class="inptSep"></span>
      비밀번호 확인
      <asp:TextBox ID="Password2" runat="server" TextMode="Password" MaxLength="8" /><br />
      <asp:CompareValidator ID="CompPass" runat="server" ErrorMessage="비밀번호가 일치하지 않습니다." 
        ControlToCompare="Password1" ControlToValidate="Password2" Display="Dynamic" />
      <asp:RequiredFieldValidator ID="ReqPass1" runat="server" ErrorMessage="비밀번호를 입력하세요.   "
        ControlToValidate="Password1" Display="Dynamic" ValidationGroup = "Submit"/>
      <asp:RequiredFieldValidator ID="ReqPass2" runat="server" ErrorMessage="비밀번호 재확인 값을 입력하세요"
        ControlToValidate="Password2" Display="Dynamic" ValidationGroup = "Submit"/>  
    </td>
  </tr>
  <tr>
    <td class="label">제목*</td>
    <td class="data">
            <asp:DropDownList ID="TypeList" runat="server" Width="120px">
        </asp:DropDownList>
      <asp:TextBox ID="Subject" runat="server" CssClass="medium" MaxLength="40" style="width:80%"/><br />
      <asp:RequiredFieldValidator ID="SubjReq" runat="server" ErrorMessage="제목을 입력하세요"
        ControlToValidate="Subject" Display="Dynamic" ValidationGroup="Submit"/>  

    </td>
  </tr>
        <tr>
        <td class="label"><asp:Literal ID="Literal7" runat="server"  Text="<%$ Resources: Resources, Forum.lbl_Content %>"></asp:Literal></td>
        <td class="data">
        <telerik:RadEditor ID="ContentEditor" runat="server" Width="97%" BorderColor="White" ToolbarMode="Default" ToolsFile="/Styles/ForumPost.xml" EditModes="Design"  Height="250px">
                                <CssFiles>
                            <telerik:EditorCssFile Value="~/Styles/EditorStyle.css" />
                        </CssFiles>
        </telerik:RadEditor>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="내용을 입력하세요"
        ControlToValidate="ContentEditor" Display="Dynamic" ValidationGroup = "Submit"/>
        </td>
      </tr>

  <tr>
  <td class="label">
  
  </td>
  <td class="data">
  <asp:Panel ID="UDFPanel" runat="server"></asp:Panel>
  </td>
  </tr>
  <tr>
    <td class="label">사진</td>
    <td class="data">
       <div>
           <asp:CheckBox ID="chk_del_current_image" runat="server"  Text=" 현재 이미지 삭제" Visible="false"/>
       </div>
      <div class="upldPnl">
        <Upload:MultiFile ID="MultiFile1" runat="server" FileQueueControlID="imgListBox" UseFlashIfAvailable="True" >
          <asp:Button ID="BrowseButton" runat="server" Text="업로드할 사진 선택..." />
        </Upload:MultiFile>
      </div>
      <Upload:ProgressBar ID="inlineProgressBar" runat="server" Inline="False" /> 
      <br />이미지 목록에서 첫번째 이미지가 대표 이미지로 지정됩니다.<br />
      <asp:RegularExpressionValidator ID="RegExpExt" ControlToValidate="MultiFile1" 
        ValidationExpression="(([^.;]*[.])+(jpg|gif|png|JPG|GIF|PNG); *)*(([^.;]*[.])+(jpg|gif|png|JPG|GIF|PNG))?$"
        Display="Dynamic" ErrorMessage="jpg, gif, png 파일만 허용됩니다." EnableClientScript="True" runat="server" ValidationGroup="Submit" />
      <div id="imgListBox" style="height:80px;">
      </div>
    </td>
  </tr>
  <tr runat="server" id="annRow" visible="false">
    <td class="label" style="width:100px;">공지사항</td>
    <td class="data">
      <asp:CheckBox ID="ChkAnnounce" runat="server" Text=" 공지사항인 경우에 체크하세요" /><br />
      공지사항은 글목록 최상단에 강조되어 표시됩니다.
    </td>
  </tr>
</table>

  <div class="cntrPnl">
                    <asp:Button ID="btn_Save" runat="server" Text="<%$ Resources: Resources,Gen.lbl_Submit %>" OnClick="btn_Save_Click"  ValidationGroup="Submit"/>
            <asp:Button ID="btn_Cancel" runat="server" Text="<%$ Resources: Resources,Gen.lbl_Cancel %>" OnClick="btn_Cancel_Click" />
  </div>


  </div>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThreadEntry.aspx.cs" Inherits="ePressMedia.Forum.ThreadEntry" %>

<%@ Register TagPrefix="Upload" Namespace="Brettle.Web.NeatUpload" Assembly="Brettle.Web.NeatUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style ="width:680px">
      <script type="text/javascript">
          function hideMessageBox() {
              var m = $find('MsgBoxMpe');
              if (m)
                  m.hide();
          }
  </script>

    <table class="postTbl" >
      <tr runat="server" id="annRow" visible="false">
        <td class="label" style="width:100px;">공지사항</td>
        <td>
          <asp:CheckBox ID="ChkAnnounce" runat="server" Text=" 공지사항인 경우에 체크하세요" /><br />
          공지사항은 글목록 최상단에 강조되어 표시되며 사용자 댓글이 방지됩니다.
        </td>
      </tr>
      <tr runat="server" id="prvRow">
        <td class="label" style="width:100px;">비밀글</td>
        <td>
          <asp:CheckBox ID="ChkPrivate" runat="server" Text=" 비밀글인 경우에 체크하세요" /><br />
          비밀번호를 아는 사람과 관리자만 글을 읽을 수 있습니다.
        </td>
      </tr>
      <tr>
        <td class="label">제목*</td>
        <td>
          <asp:TextBox ID="Subject" runat="server" MaxLength="128" Width="400" />
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<br />제목을 입력하세요"
              ControlToValidate="Subject" Display="Dynamic" />
        </td>
      </tr>
      <tr>
        <td class="label">작성자*</td>
        <td>
          <asp:TextBox ID="PostBy" runat="server" MaxLength="16" />
          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="<br />작성자를 입력하세요"
              ControlToValidate="PostBy" Display="Dynamic" />
        </td>
      </tr>
      <tr id="pwdRow" runat="server">
        <td class="label">비밀번호*</td>
        <td>
          <asp:TextBox ID="Password" runat="server" MaxLength="8" TextMode="Password" /><br />
          글 수정,삭제시에 필요합니다. (최대 8자)
          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="<br />비밀번호를 입력하세요"
              ControlToValidate="Password" Display="Dynamic" />
        </td>
      </tr>
      <tr>
        <td class="label">자동등록 방지*</td>
        <td>
          <asp:TextBox ID="Captcha" runat="server" MaxLength="6" />
          <asp:Image id="CapImg" runat="server" ImageUrl="~/Controls/JpegCaptcha.aspx" AlternateText="" style="vertical-align:middle" />
          <br />
          <span class="ex">오른쪽에 표시된 네자리 숫자를 입력해 주세요.</span>
          <asp:RequiredFieldValidator ID="CapReq" runat="server" ErrorMessage="<br />자동등록 방지 코드를 입력하세요"
              ControlToValidate="Captcha" Display="Dynamic" />
          <asp:CompareValidator ID="CapCompValidator" runat="server" ErrorMessage="<br />자동등록 방지코드가 맞지않습니다" 
            ControlToValidate="Captcha" Display="Dynamic"  />
        </td>
      </tr>
      <tr>
        <td class="label">내용</td>
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
        <td class="label">첨부파일</td>
        <td>
          <div class="upldPnl">
            <Upload:MultiFile ID="MultiFile1" runat="server" FileQueueControlID="imgListBox" UseFlashIfAvailable="True">
              <asp:Button ID="BrowseButton" runat="server" Text="첨부할 파일 선택..." />
            </Upload:MultiFile>
          </div>
          <Upload:ProgressBar ID="inlineProgressBar" runat="server" Inline="False" /> <%--Height="36px" Width="400px" />--%>
          
          <asp:RegularExpressionValidator ID="RegExpExt" ControlToValidate="MultiFile1" 
            ValidationExpression="(([^.;]*[.])+(jpg|gif|png|pdf|doc|xls|docx|xlsx|JPG|GIF|PNG|PDF|DOC|XLS|DOCX|XLSX); *)*(([^.;]*[.])+(jpg|gif|png|pdf|doc|xls|docx|xlsx|JPG|GIF|PNG|PDF|DOC|XLS|DOCX|XLSX))?$"
            Display="Dynamic" ErrorMessage="<br />jpg, gif, png, pdf, 워드(doc,docx), 엑셀(xls,xlsx) 파일만 허용됩니다." EnableClientScript="True" runat="server" />
          <div id="imgListBox">
          </div>
        
        </td>
      </tr>    
    </table>

    <div class="cntrPnl">
      <asp:LinkButton ID="PostLink" runat="server" CssClass="boxLnk" Text="등록" 
        onclick="PostLink_Click" />
      <span class="colSep"></span>
      <asp:HyperLink ID="ListLink" runat="server" CssClass="boxLnk" Text="취소" />    
    </div>
    </div>
    </form>
</body>
</html>

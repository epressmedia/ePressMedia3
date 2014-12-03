<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage_1131.master" AutoEventWireup="true" Inherits="Biz_EditBiz" Codebehind="EditBiz.aspx.cs" %>
<%@ Register TagPrefix="Upload" Namespace="Brettle.Web.NeatUpload" Assembly="Brettle.Web.NeatUpload" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <script src="../Scripts/Biz.js" type="text/javascript"></script>  
  <script type="text/javascript">

      function hideMessageBox() {
          var m = $find('MsgBoxMpe');
          if (m)
              m.hide();
      }

  </script>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LeftBarContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Content" Runat="Server">

    <asp:HiddenField ID="ImgCount" runat="server" />
    <asp:Panel ID="ErrorMsgBox" runat="server" CssClass="confirmBox" style="display:none">
      <asp:Label ID="ErrorMsg" runat="server" />
      <br /><br />
      <input id="OkButton" runat="server" type="button" value=" 확인 " onclick="javascript:hideMessageBox();" />
    </asp:Panel>
    
    <cc1:ModalPopupExtender ID="MsgBoxMpe" runat="server" DynamicServicePath="" 
      Enabled="True" TargetControlID="MsgBoxTarget" PopupControlID="ErrorMsgBox"
      BehaviorID="MsgBoxMpe" 
      BackgroundCssClass="modalBg"  />

    <asp:Button ID="MsgBoxTarget" runat="server" style="display:none" />

          <div class="pgTtl">
            * 업소 정보를 수정하세요. 관리자가 검토하여 승인 후 반영됩니다.
          </div>
          <br />
          <table class="post">
            <tr>
              <td class="label">비밀번호</td>
              <td class="data">
                <asp:TextBox ID="Password1" runat="server" TextMode="Password" />
                <span class="inptSep"></span>
                업소 등록시에 설정했던 비밀번호를 입력하세요.
                <asp:RequiredFieldValidator ID="ReqPass1" runat="server" ErrorMessage="<br />비밀번호를 입력하세요"
                  ControlToValidate="Password1" Display="Dynamic" />  
              </td>
            </tr>
          </table>

      
          <table class="post">
            <tr>
              <td class="label">업종</td>
              <td class="data">
                <asp:DropDownList ID="BizCatList" runat="server" />
                <asp:RequiredFieldValidator ID="ReqBizCat" runat="server" ErrorMessage="<br />업종을 선택하세요"
                  ControlToValidate="BizCatList" InitialValue="0" Display="Dynamic" />  
              </td>
            </tr>
            <tr>
              <td class="label">업소명 (한글)</td>
              <td class="data">
                <asp:TextBox ID="BizName" runat="server" CssClass="short" />
                <br />
                <span class="small">한글명이 없는 경우 이곳에 영문명을 입력하세요<asp:RequiredFieldValidator ID="ReqName" runat="server" ErrorMessage="<br />업소명을 입력하세요"
                  ControlToValidate="BizName" Display="Dynamic" />
                </span>
              </td>
            </tr>
            <tr>
              <td class="label">업소명 (영문)</td>
              <td class="data">
                <asp:TextBox ID="BizNameEng" runat="server" CssClass="short" />
                <br />
                <span class="small">영문명을 위에서 입력한 경우 이곳은 비워두세요</span>
              </td>
            </tr>
            <tr>
              <td class="label">업소 한줄소개</td>
              <td class="data">
                <asp:TextBox ID="ShortDesc" runat="server" CssClass="long" />
              </td>
            </tr>
          </table>
      

          <table class="post">
        <tr>
            <td class="label">
                State / ZIP Code
            </td>
            <td class="data">
                <asp:DropDownList ID="StateList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="StateList_SelectedIndexChanged">
                </asp:DropDownList>
                <span class="sep12"></span>도시(City)<asp:DropDownList ID="AreaList" runat="server" />
                <span class="inptSep"></span><br />ZIP
                <asp:TextBox ID="ZipCode" runat="server" MaxLength="10" />
                <asp:RegularExpressionValidator ID="RegExZip" runat="server" ControlToValidate="ZipCode"
                    ErrorMessage="<br />잘못된 Zip Code 형식입니다." ValidationExpression="^(\d{5}-\d{4}|\d{5}|\d{9})$|^([a-zA-Z]\d[a-zA-Z] \d[a-zA-Z]\d)$|^([a-zA-Z]\d[a-zA-Z]\d[a-zA-Z]\d)$"
                    Display="Dynamic" />
            </td>
        </tr>

          </table>


          <table class="post">
            <tr>
              <td class="label">Street Address</td>
              <td class="data">
                <asp:TextBox ID="Address" runat="server" CssClass="long" />
                <br />
                <span class="small">예) 23415 Hwy 99 Suite #A (&#39;.&#39; 및 &#39;,&#39; 는 입력하지 마세요)</span>
                <%--asp:RequiredFieldValidator ID="ReqAddr" runat="server" ErrorMessage="<br />거리 주소를 입력하세요"
                  ControlToValidate="Address" Display="Dynamic" /--%>  
              </td>
            </tr>
            <tr>
              <td class="label">업소 전화번호 1</td>
              <td class="data">
<telerik:RadMaskedTextBox ID = "Phone1" runat="server" Mask="###-###-####" DisplayMask="### -###-####"></telerik:RadMaskedTextBox>
                <br />
                <asp:RequiredFieldValidator ID="ReqPhone" runat="server" ErrorMessage="<br />업소 전화번호를 입력하세요"
                    ControlToValidate="Phone1" Display="Dynamic" />
              </td>
            </tr>
            <tr>
              <td class="label">업소 전화번호 2</td>
              <td class="data">
                <telerik:RadMaskedTextBox ID = "Phone2" runat="server" Mask="###-###-####" DisplayMask="###-###-####"></telerik:RadMaskedTextBox>
              </td>
            </tr>
            <tr>
              <td class="label">팩스번호</td>
              <td class="data">
                <telerik:RadMaskedTextBox ID = "Fax" runat="server" Mask="###-###-####" DisplayMask="###-###-####"></telerik:RadMaskedTextBox>
              </td>
            </tr>
            <tr>
              <td class="label">홈페이지</td>
              <td class="data">
                http://<asp:TextBox ID="Homepage" runat="server" CssClass="short" />
                <span class="small">예) www.ABC.com</span>
              </td>
            </tr>
            <tr>
              <td class="label">업소 이메일</td>
              <td class="data">
                <asp:TextBox ID="BizEmail" runat="server" CssClass="short" />
                <span class="small">예) ABC@ABC.com</span>
                <asp:RegularExpressionValidator ID="RegExEmail2" runat="server" 
                    ControlToValidate="BizEmail" ErrorMessage="<br />잘못된 이메일 형식입니다." 
                    ValidationExpression="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" 
                    Display="Dynamic"></asp:RegularExpressionValidator>
              </td>
            </tr>

            <tr>
              <td class="label">업소 상세 정보</td>
              <td class="data">
                <asp:TextBox ID="BizDescr" TextMode="MultiLine" Rows="10" runat="server" CssClass="long" type="text"/>
                
              </td>
            </tr>
            <tr>
              <td class="label">동영상정보</td>
              <td class="data">
                <asp:TextBox ID="VideoId" runat="server" CssClass="long" />
                <br />
                <span class="small">YouTube 동영상 재생시 브라우저 주소창에 표시되는 내용을 입력합니다.<br />
                  예) http://www.youtube.com/watch?v=13342
                </span>
              </td>
            </tr>

            <tr style="display:none">
              <td class="label">이미지 추가</td>
              <td class="data">
                <asp:CheckBox ID="ChkDelFiles" runat="server" Text="기존 이미지 삭제" /><br />
                <span class="guide">체크하면 기존 이미지를 삭제하고 새 이미지를 추가합니다.</span><br />
                <asp:CheckBox ID="ChkChgThumb" runat="server" Text="대표 이미지 변경" /><br />
                <span class="guide">체크하면 새로 추가하는 이미지 중 첫번째 이미지가 대표 이미지로 설정됩니다.</span>

                <div class="upldPnl">
                  <Upload:MultiFile ID="MultiFile1" runat="server" FileQueueControlID="imgListBox" UseFlashIfAvailable="True">
                    <asp:Button ID="BrowseButton" runat="server" Text="업로드할 사진 선택..." />
                  </Upload:MultiFile>
                </div>
                <Upload:ProgressBar ID="inlineProgressBar" runat="server" Inline="False" Triggers="PostButton" /> 
                <br />이미지 목록에서 첫번째 이미지가 대표 이미지로 지정됩니다.
                <asp:RegularExpressionValidator ID="RegExpExt" ControlToValidate="MultiFile1" 
                  ValidationExpression="(([^.;]*[.])+(jpg|gif|png|JPG|GIF|PNG); *)*(([^.;]*[.])+(jpg|gif|png|JPG|GIF|PNG))?$"
                  Display="Dynamic" ErrorMessage="<br />jpg, gif, png 파일만 허용됩니다." EnableClientScript="True" runat="server" />
                <div id="imgListBox">
                </div>
              </td>
            </tr>
          </table>
          <br />
          <div class="cntrPnl">
            <asp:Button ID="PostButton" runat="server" Text="저장하기" CssClass="flat" 
              onclick="PostButton_Click" />
            <span class="inptSep"></span>
            <asp:Button ID="CancelButton" runat="server" Text="수정 취소" CssClass="flat" 
              onclick="CancelButton_Click" CausesValidation="false" />          
          </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="RightBarContent" Runat="Server">
</asp:Content>


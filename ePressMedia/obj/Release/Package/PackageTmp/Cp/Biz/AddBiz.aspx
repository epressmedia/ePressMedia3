<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true" Inherits="Cp_AddBiz" Codebehind="AddBiz.aspx.cs" %>
<%@ Register TagPrefix="Upload" Namespace="Brettle.Web.NeatUpload" Assembly="Brettle.Web.NeatUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <link href="../Style/CP.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">

<style>
    .bizDetailInfo_text
{
    width:500px;
}
</style>
<h1>Add New Business Entity</h1>
          <br />


      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>      
          <table class="post">
            <tr>
              <td class="label">업종</td>
              <td class="data">
                <asp:DropDownList ID="BizCatList" runat="server" />
                <asp:RequiredFieldValidator ID="ReqBizCat" runat="server" ErrorMessage="<br />업종을 선택하세요"
                  ControlToValidate="BizCatList" InitialValue="0" Display="Dynamic" />  
              </td>
            </tr>
          </table>
        </ContentTemplate>
      </asp:UpdatePanel>

          <table class="post">
            <tr>
              <td class="label">업소명 (한글)</td>
              <td class="data">
                <asp:TextBox ID="BizNameKr" runat="server" CssClass="short" />
                <br />
                <span class="small">한글명이 없는 경우 이곳에 영문명을 입력하세요<asp:RequiredFieldValidator ID="ReqName" runat="server" ErrorMessage="<br />업소명을 입력하세요"
                  ControlToValidate="BizNameKr" Display="Dynamic" />
                </span>
              </td>
            </tr>
            <tr>
              <td class="label">업소명 (영문)</td>
              <td class="data">
                <asp:TextBox ID="BizNameEn" runat="server" CssClass="short" />
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
      
      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>      
          <table class="post">
            <tr>
              <td class="label">State / ZIP Code</td>
              <td class="data">
                <asp:DropDownList ID="StateList" runat="server" AutoPostBack="True" 
                  onselectedindexchanged="StateList_SelectedIndexChanged">
                </asp:DropDownList>
                      <span class="sep12"></span>
      도시(City)<asp:DropDownList ID="AreaList" runat="server" />
                <span class="inptSep"></span>
                ZIP
                <asp:TextBox ID="ZipCode" runat="server" MaxLength="10" />

                <asp:RegularExpressionValidator ID="RegExZip" runat="server" 
                    ControlToValidate="ZipCode" ErrorMessage="<br />잘못된 Zip Code 형식입니다." 
                    ValidationExpression="^(\d{5}-\d{4}|\d{5}|\d{9})$|^([a-zA-Z]\d[a-zA-Z] \d[a-zA-Z]\d)$|^([a-zA-Z]\d[a-zA-Z]\d[a-zA-Z]\d)$" 
                    Display="Dynamic" />
              </td>
            </tr>

          </table>
        </ContentTemplate>
      </asp:UpdatePanel>

          <table class="post">
            <tr>
              <td class="label">Street Address</td>
              <td class="data">
                <asp:TextBox ID="Address" runat="server" CssClass="long" />
                <br />
                <span class="small">예) 23415 Hwy 99 Suite #A (&#39;.&#39; 및 &#39;,&#39; 는 입력하지 마세요)</span>
 
              </td>
            </tr>
            <tr>
              <td class="label">업소 전화번호 1</td>
              <td class="data">
              <telerik:RadMaskedTextBox ID = "Phone1" runat="server" Mask="###-###-####" DisplayMask="###-###-####"></telerik:RadMaskedTextBox>

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
                http://<asp:TextBox ID="HomePage" runat="server" CssClass="short" />
                <span class="small">예) www.epressmedia.com</span>
              </td>
            </tr>
            <tr>
              <td class="label">이메일</td>
              <td class="data">
                <asp:TextBox ID="BizEmail" runat="server" CssClass="short" />
                <asp:RegularExpressionValidator ID="RegExEmail2" runat="server" 
                    ControlToValidate="BizEmail" ErrorMessage="<br />잘못된 이메일 형식입니다." 
                    ValidationExpression="^[\w\.\-]+@[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]{1,})*(\.[a-zA-Z]{2,3}){1,2}$" 
                    Display="Dynamic"></asp:RegularExpressionValidator>
              </td>
            </tr>
             <tr>
              <td class="label">업소 상세 정보</td>
              <td class="data">
                <asp:TextBox ID="BizDescr" TextMode="MultiLine" Rows="10" runat="server" CssClass="bizDetailInfo_text" type="text"/>
                
              </td>
            </tr>
            <tr>
               
              <td class="label">광고주</td>
              <td class="data">
                <asp:CheckBox ID="chk_adowner" runat="server" />
                
              </td>
            
            </tr>
             <tr>
               
              <td class="label">프리미엄 광고리스팅</td>
              <td class="data">
                <asp:CheckBox ID="chk_premium" runat="server" />
                
              </td>
            
            </tr>
              <td class="label">동영상정보</td>
              <td class="data">
                <asp:TextBox ID="VideoId" runat="server" CssClass="long" />
                <br />
                <span class="small">YouTube 동영상 재생시 브라우저 주소창에 표시되는 내용을 입력합니다.<br />
                  예) http://www.youtube.com/watch?v=13342
                </span>
              </td>
            </tr>
            <tr>
              <td class="label">이미지 등록</td>
              <td class="data">
                <div class="upldPnl">
                  <Upload:MultiFile ID="MultiFile1" runat="server" FileQueueControlID="imgListBox" UseFlashIfAvailable="True">
                    <asp:Button ID="BrowseButton" runat="server" Text="업로드할 사진 선택..." />
                  </Upload:MultiFile>
                </div>
                <Upload:ProgressBar ID="inlineProgressBar" runat="server" Inline="False" Triggers="PostButton" /> <%--Height="36px" Width="400px" />--%>
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
            <asp:Button ID="PostButton" runat="server" Text="등록" CssClass="flat" onclick="PostButton_Click" />
            <span class="inptSep"></span>
            <asp:Button ID="CancelButton" runat="server" Text="취소" CssClass="flat" onclick="CancelButton_Click" CausesValidation="false" />          
          </div>

</asp:Content>


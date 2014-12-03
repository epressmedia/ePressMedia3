<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true" Inherits="Cp_Biz_BizInfo" Codebehind="BizInfo.aspx.cs" %>
<%@ Register TagPrefix="Upload" Namespace="Brettle.Web.NeatUpload" Assembly="Brettle.Web.NeatUpload" %>
<%@ Register Src="~/CP/Controls/Toolbox.ascx" TagName="Toolbox" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
<style>
    .info_label
    {
        display: block;
font-size: 12px;
color: #3C3C3C;
    }
    .info_Text
    {
        font-size:16px;
        color:#929292;
        font-weight:bold;
        padding-left:8px;
    }
    .basic_info > div
    {
        float:left;
        margin:5px 45px 5px 5px;
    }
    .basic_info
    {
        margin: 10px 0 15px 0;
        float:left;
    }
    .NewValue
    {
        color:Red;
    }
    
    .BEChange_Change
    {
        margin:0 10px;
    }
    .BEChange_Apply, .BEChange_Reject
    {
        margin: 0 5px;
    }
    .BEChange_Text
    {
        min-width:200px;
        color:red
    }
    .BEChange_Approved
    {
        color: Blue !important;
        border-color:Blue;
    }
        .BEChange_Rejected
    {
        color: Red !important;
        border-color:Red;
    }
    
    
</style>
<script type="text/javascript">
function ApproveField(requestid) {
    var control = $(".BEChange_Text[requestid='" + requestid + "']");
    control.attr("status", "A");
    control.removeClass("BEChange_Rejected");
    control.addClass("BEChange_Approved");

    var r_current = $(".RejectList").val();
    $(".RejectList").val(r_current.replace(" " + requestid + ",", ""));

    var a_current = $(".ApproveList").val();
    var a_clearCurrent = a_current.replace(" " + requestid + ",", "");
    $(".ApproveList").val(a_clearCurrent + " " + requestid + ",");


}
function RejectField(requestid) {
    var control = $(".BEChange_Text[requestid='" + requestid + "']");
    control.attr("status", "R");
    control.addClass("BEChange_Rejected");
    control.removeClass("BEChange_Approved");

    var a_current = $(".ApproveList").val();
    $(".ApproveList").val(a_current.replace(" " + requestid + ",", ""));

    var r_current = $(".RejectList").val();
    var r_clearCurrent = r_current.replace(" " + requestid + ",", "");
    $(".RejectList").val(r_clearCurrent + " " + requestid + ",");



}
</script>
<h1>Business Entity Information</h1>

                <epm:BEChangeBox ID="aa" runat="server"></epm:BEChangeBox>

    <div>
        <uc:Toolbox ID="toolbox1" runat="server" ButtonAvailable="save,cancel,approve">
        </uc:Toolbox>

    </div>

<asp:HiddenField ID="ImgCount" runat="server" />
<div class="basic_info">
<div>
<asp:Label ID="lbl_created_date" runat="server" Text="Created Date" CssClass="info_label"></asp:Label>
<asp:Label ID="txt_created_date" runat="server" Text="" CssClass="info_Text"></asp:Label>
</div>
<div>
<asp:Label ID="lbl_created_by" runat="server" Text="Created By" CssClass="info_label"></asp:Label>
<asp:Label ID="txt_created_by" runat="server" Text="" CssClass="info_Text"></asp:Label>
</div>
<div>
<asp:Label ID="lbl_modified_date" runat="server" Text="Updated Date" CssClass="info_label"></asp:Label>
<asp:Label ID="txt_modified_date" runat="server" Text="" CssClass="info_Text"></asp:Label>
</div>
<div>
<asp:Label ID="lbl_modified_by" runat="server" Text="Updated By" CssClass="info_label"></asp:Label>
<asp:Label ID="txt_modified_by" runat="server" Text="" CssClass="info_Text"></asp:Label>
</div>
<div>
<asp:Label ID="lbl_status" runat="server" Text="Status" CssClass="info_label"></asp:Label>
<asp:Label ID="txt_status" runat="server" Text="" CssClass="info_Text"></asp:Label>
</div>
</div>
<div style="clear:both"></div>
<asp:UpdatePanel runat="server" ID="UpPanel2">
  <ContentTemplate>
    <table >
      <tr>
        <td class="label">비밀번호 수정</td>
        <td class="data">
          <asp:Literal ID="Password" runat="server" Visible="false" />
          새비밀번호
          <asp:TextBox ID="NewPwd" runat="server" length="8" MaxLength="8" />
          <asp:Button ID="PwdButton" runat="server" Text="Update" onclick="PwdButton_Click" />
          <asp:Label ID="PwdLabel" runat="server" Text="" />
        </td>    
      </tr>
    </table>
  </ContentTemplate>
</asp:UpdatePanel>
<table width="100%" style="margin-top:-2px;">
  <tr>
    <td class="label">업종</td>
    <td class="data">
                      <asp:DropDownList ID="BizCatList" runat="server" />
                      <epm:BEChangeBox  ID="epm_CategoryID" runat="server"></epm:BEChangeBox>
                <asp:RequiredFieldValidator ID="ReqBizCat" runat="server" ErrorMessage="<br />업종을 선택하세요"
                  ControlToValidate="BizCatList" InitialValue="0" Display="Dynamic" />  
                  
                  
    </td>    
  </tr>
  <tr>
    <td class="label">상호</td>
    <td class="data">
      <asp:TextBox ID="BizName" runat="server" Width="300px" />
      <epm:BEChangeBox  ID="epm_PrimaryName" runat="server"></epm:BEChangeBox>
      
    </td>    
  </tr>
  <tr>
    <td class="label">영문 상호</td>
    <td class="data">
      <asp:TextBox ID="BizNameEng" runat="server" Width="300px" />
      <epm:BEChangeBox  ID="epm_SecondaryName" runat="server"></epm:BEChangeBox>
    </td>    
  </tr>
  <tr>
    <td class="label">업소 한줄소개</td>
    <td class="data">
      <asp:TextBox ID="ShortDesc" runat="server" CssClass="long" Width="300px" />
      <epm:BEChangeBox  ID="epm_ShortDesc" runat="server"></epm:BEChangeBox>
    </td>
  </tr>
  <tr>
    <td class="label">전화번호1</td>
    <td class="data">
    <telerik:RadMaskedTextBox ID = "Phone1" runat="server" Mask="###-###-####" DisplayMask="###-###-####"></telerik:RadMaskedTextBox>
    <epm:BEChangeBox  ID="epm_Phone1" runat="server"></epm:BEChangeBox>
    </td>    
  </tr>
  <tr>
    <td class="label">전화번호2</td>
    <td class="data">
    <telerik:RadMaskedTextBox ID = "Phone2" runat="server" Mask="###-###-####" DisplayMask="###-###-####"></telerik:RadMaskedTextBox>
    <epm:BEChangeBox  ID="epm_Phone2" runat="server" Mask="###-###-####"></epm:BEChangeBox>
    </td>    
  </tr>
  <tr>
    <td class="label">팩스</td>
    <td class="data">
      <telerik:RadMaskedTextBox ID = "Fax" runat="server" Mask="###-###-####" DisplayMask="###-###-####"></telerik:RadMaskedTextBox>
      <epm:BEChangeBox  ID="epm_Fax" runat="server"></epm:BEChangeBox>
    </td>    
  </tr>
  <tr>
    <td class="label">Email</td>
    <td class="data">
      <asp:TextBox ID="BizEmail" runat="server" Width="208px" />
      <epm:BEChangeBox  ID="epm_Email" runat="server"></epm:BEChangeBox>
    </td>    
  </tr>
</table>
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>      
<table  style="margin-top:-2px;">  
  <tr>
    <td class="label">주(State/Province)</td>
    <td class="data">
      <asp:DropDownList ID="StateList" runat="server" AutoPostBack="True" 
        onselectedindexchanged="StateList_SelectedIndexChanged">
      </asp:DropDownList>
      <epm:BEChangeBox  ID="epm_State" runat="server"></epm:BEChangeBox>
</tr>
<tr>
    <td class="label">도시명(City)</td>
    <td class="data">
      <asp:DropDownList ID="AreaList" runat="server" />
      <epm:BEChangeBox  ID="epm_City" runat="server"></epm:BEChangeBox>
    </td>    
  </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
<table width="100%" style="margin-top:-2px;">
  <tr>
    <td class="label">주소</td>
    <td class="data">
      <asp:TextBox ID="Address" runat="server" Width="300px" />
      <epm:BEChangeBox  ID="epm_Address" runat="server"></epm:BEChangeBox>
    </td>
  </tr>
  <tr>
    <td class="label">ZIP/Postal Code</td>
    <td class="data">
      <asp:TextBox ID="ZipCode" runat="server" Width="300px" />
      <epm:BEChangeBox  ID="epm_ZipCode" runat="server"></epm:BEChangeBox>
      <asp:RegularExpressionValidator ID="RegExZip" runat="server" 
                    ControlToValidate="ZipCode" ErrorMessage="<br />잘못된 Zip Code 형식입니다." 
                    ValidationExpression="^(\d{5}-\d{4}|\d{5}|\d{9})$|^([a-zA-Z]\d[a-zA-Z] \d[a-zA-Z]\d)$|^([a-zA-Z]\d[a-zA-Z]\d[a-zA-Z]\d)$" 
                    Display="Dynamic" />
    </td>
  </tr>
  <tr>
    <td class="label">웹사이트</td>
    <td class="data">
      http://<asp:TextBox ID="Homepage" runat="server" Width="300px" />
      <epm:BEChangeBox  ID="epm_Website" runat="server"></epm:BEChangeBox>
    </td>
  </tr>
<tr>
  <td class="label">동영상정보</td>
  <td class="data">
    <asp:TextBox ID="VideoId" runat="server" Width="300px" />
    <epm:BEChangeBox  ID="epm_VideoURL" runat="server"></epm:BEChangeBox>
  </td>
</tr>
  <tr>
    <td class="label">광고주</td>
    <td class="data">
      <asp:CheckBox ID="ChkAdOwner" runat="server" Text=" 광고주이면 체크" />

    </td>
  </tr>
      <tr>
    <td class="label">프리미엄 광고 리스팅</td>
    <td class="data">
      <asp:CheckBox ID="Chk_Premium" runat="server" Text=" 프리미엄 광고 리스팅이면 체크" />

    </td>
  </tr>
  <tr>
    <td class="label">업소 상세 정보</td>
    <td class="data">
      <asp:TextBox ID="BizDescr" runat="server" TextMode="MultiLine" Rows="10" Width="400px" type="text"/> 
      <epm:BEChangeBox  ID="epm_Description" runat="server" TextMode="MultiLine" Rows="10" Width="400px"></epm:BEChangeBox>
    </td>
  </tr>
  <tr id = "listImage" runat="server" visible = "false">
   <td class="label">이미지</td>
   <td class="data">
   <div>
   <asp:Repeater ID = "imageRepeater" runat="server">
   <ItemTemplate>
   <div style="float:left;padding: 5px;">
   <asp:Image ID="bizImage" runat="server" ImageUrl=<%# Eval("ThumbnailPath") %> />
   </div>
   </ItemTemplate>
   </asp:Repeater>
   </div>
   <div style="clear:both"></div>
   </td>
  </tr>
  <tr>
    <td class="label">이미지 추가</td>
    <td class="data">
      <asp:CheckBox ID="ChkDelFiles" runat="server" Text="기존 이미지 삭제" Visible="false" /><br />
      <asp:CheckBox ID="ChkChgThumb" runat="server" Text="대표 이미지 변경" /><br />
      <span class="guide">체크하면 아래의 업로드 목록에서 첫번째 이미지가 대표 이미지로 설정됩니다.</span>

      <div class="upldPnl">
        <Upload:MultiFile ID="MultiFile1" runat="server" FileQueueControlID="imgListBox" UseFlashIfAvailable="True">
          <asp:Button ID="BrowseButton" runat="server" Text="업로드할 사진 선택..." />
        </Upload:MultiFile>
      </div>
      <Upload:ProgressBar ID="inlineProgressBar" runat="server" Inline="False" Triggers="PostButton" /> <%--Height="36px" Width="400px" />--%>
      <asp:RegularExpressionValidator ID="RegExpExt" ControlToValidate="MultiFile1" 
        ValidationExpression="(([^.;]*[.])+(jpg|gif|png|JPG|GIF|PNG); *)*(([^.;]*[.])+(jpg|gif|png|JPG|GIF|PNG))?$"
        Display="Dynamic" ErrorMessage="<br />jpg, gif, png 파일만 허용됩니다." EnableClientScript="True" runat="server" />
      <div id="imgListBox" style="width:400px;">
      </div>
    </td>
  </tr>

</table>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" >
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">



<div style="padding:5px">
<div style="font-size:16px; margin: 10px;font-weight: bold;">Business Entity Notes</div>
<div>
    <telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="True" 
        AllowSorting="True" CellSpacing="0" 
        DataSourceID="OpenAccessLinqDataSource1" GridLines="None" 
        AllowAutomaticDeletes="True" AllowAutomaticInserts="True" 
        AllowAutomaticUpdates="True" onitemdatabound="RadGrid1_ItemDataBound" 
        onitemcreated="RadGrid1_ItemCreated">
<MasterTableView AutoGenerateColumns="False" DataKeyNames="NoteId" 
            DataSourceID="OpenAccessLinqDataSource1" CommandItemDisplay="Top">

    <Columns>
                 <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                </telerik:GridEditCommandColumn>
        <telerik:GridBoundColumn DataField="NoteId" DataType="System.Int32" 
            FilterControlAltText="Filter NoteId column" HeaderText="NoteId" ReadOnly="True" 
            SortExpression="NoteId" UniqueName="NoteId" Visible="false">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="BusinessEntityId" DataType="System.Int32" 
            FilterControlAltText="Filter BusinessEntityId column" 
            HeaderText="BusinessEntityId" SortExpression="BusinessEntityId" 
            UniqueName="BusinessEntityId" Visible="false">
        </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Note" 
            FilterControlAltText="Filter Note column" HeaderText="Note" 
            SortExpression="Note" UniqueName="Note">
            <HeaderStyle Width="400px" />
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn
        DataField="CreatedDate"
        DataType="System.DateTime"
        HeaderText="Created Date"
        SortExpression="CreatedDate"
        DataFormatString="{0:MM/dd/yy}"
        UniqueName="CreatedDate" ReadOnly="true">
    </telerik:GridBoundColumn>
    <telerik:GridTemplateColumn UniqueName="CreatedByUser">
    <HeaderTemplate>
    <asp:Label ID="lbl_header" runat="server" Text="Created By"></asp:Label>
    </HeaderTemplate>
    <ItemTemplate>
   <asp:Label ID="lbl_createdby" runat="server" Text=""></asp:Label>
    </ItemTemplate>
    </telerik:GridTemplateColumn>
        <telerik:GridBoundColumn DataField="CreatedBy" DataType="System.Guid" 
            FilterControlAltText="Filter CreatedBy column" HeaderText="CreatedBy" 
            SortExpression="CreatedBy" UniqueName="CreatedBy" Visible="false" ReadOnly="true" >
        </telerik:GridBoundColumn>
        
        <telerik:GridBoundColumn
        DataField="ModifiedDate"
        DataType="System.DateTime"
        HeaderText="Modified Date"
        SortExpression="ModifiedDate"
        DataFormatString="{0:MM/dd/yy}"
        UniqueName="ModifiedDate" ReadOnly="true">
    </telerik:GridBoundColumn>
            <telerik:GridTemplateColumn UniqueName="ModifiedByUser">
    <HeaderTemplate>
    <asp:Label ID="lbl_header" runat="server" Text="Modified By"></asp:Label>
    </HeaderTemplate>
    <ItemTemplate>
   <asp:Label ID="lbl_modifiedby" runat="server" Text=""></asp:Label>
    </ItemTemplate>

    </telerik:GridTemplateColumn>
        <telerik:GridBoundColumn DataField="ModifiedBy" DataType="System.Guid" 
            FilterControlAltText="Filter ModifiedBy column" HeaderText="ModifiedBy" 
            SortExpression="ModifiedBy" UniqueName="ModifiedBy" Visible="false" ReadOnly="true">
        </telerik:GridBoundColumn>

               <telerik:GridButtonColumn Text="Delete" CommandName="Delete" ButtonType="ImageButton" />
    </Columns>

<EditFormSettings>
<EditColumn ButtonType="ImageButton"></EditColumn>
</EditFormSettings>
</MasterTableView>

<PagerStyle Mode="NextPrevAndNumeric" ></PagerStyle>

    </telerik:RadGrid>
    <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSource1"
        runat="server" ContextTypeName="EPM.Data.Model.EPMEntityModel" 
        EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" 
        OrderBy="CreatedDate desc" ResourceSetName="BusinessEntityNotes" 
        
        Where="BusinessEntityId == @BusinessEntityId &amp;&amp; Deleted == @Deleted" 
        onupdating="OpenAccessLinqDataSource1_Updating" 
        ondeleting="OpenAccessLinqDataSource1_Deleting" 
        oninserting="OpenAccessLinqDataSource1_Inserting">
        <WhereParameters>
            <asp:QueryStringParameter DefaultValue="" Name="BusinessEntityId" 
                QueryStringField="id" Type="Int32" />
            <asp:Parameter DefaultValue="false" Name="Deleted" Type="Boolean" />
        </WhereParameters>
    </telerik:OpenAccessLinqDataSource>
</div>
</div>
    </telerik:RadAjaxPanel>
<div class="cmdPnl">
     <asp:Button ID="PostButton" runat="server" Text="Save Changes" 
       onclick="SaveButton_Click"  />
       <input name="ApproveList" id="ApproveList" class="ApproveList" value="" type="hidden" runat="server" />
       <input name="RejectList" id="RejectList" class="RejectList" value="" type="hidden" runat="server" />

</div>
</asp:Content>



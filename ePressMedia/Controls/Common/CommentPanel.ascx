<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_CommentPanel" Codebehind="CommentPanel.ascx.cs" %>
<%@ Register src="~/Controls/Pager/FitPager.ascx" tagname="FitPager" tagprefix="uc1" %>



<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" MinDisplayTime="10"></telerik:RadAjaxLoadingPanel >

    <asp:HiddenField ID="CommType" runat="server" />




    <asp:HiddenField ID="CommentId" runat="server" Value="0" />



<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID ="RadAjaxLoadingPanel1" >
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="PostButton">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="commPnl" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>

    <div id ="commPnl" runat="server" class="commPnl" style="margin: 10px 0 10px 0">
    <div id="comment_header" runat="server" visible="false"  style="font-size: 26px;font-family: Helvetica;letter-spacing: -1px;color: #A0A0A0;">Comments</div>
      <p runat="server" visible="false">Comment <asp:Label ID="CommentCount" runat="server" CssClass="commCount" />개</p>
      <asp:Panel ID="CommentListPanel" runat="server" CssClass="commContent" Visible="false" >
        <dl>
          <asp:Repeater ID="CommentList" runat="server" onitemcommand="CommentList_ItemCommand" OnItemDataBound="CommentList_ItemDataBound">
            <ItemTemplate>
              <dt>
                <span class="pstDt">
                <%# Eval("PostDate", "{0:MM/dd HH:mm}") %> | 
                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("Id") %>' Text="삭제" CommandName="delete" />
                </span>
                <img runat="server" src="~/Img/comment2.gif" alt="" />
                <asp:Label runat="server" CssClass="pstInf" Text='<%# Eval("PostBy") %>' /> 
              </dt>
               <div id="commet_pw_div" runat="server" visible ="false">
                    비밀번호확인 <asp:TextBox ID="txt_comm_pw" runat="server"></asp:TextBox><asp:Button ID="btn_delete_confirm" runat="server" Text="확인" CommandArgument='<%# Eval("Id") %>'  CommandName="PasswordConfirm" />
               </div>
              <dd>
                <%# Eval("Comment") %>
              </dd>            
            </ItemTemplate>
          </asp:Repeater>
        </dl>
        <uc1:FitPager ID="FitPager1" runat="server" OnPageNumberChanged="PageNumberChanged" RowsPerPage="5" Visible="false" />
      </asp:Panel>
      <div class="commPost" id = "comment_post_panel" runat="server">
        <p runat="server" visible="true">
          <span class="lbl">자동입력방지</span><asp:TextBox ID="Captcha" MaxLength="6" runat="server"/>
          <asp:Image id="CapImg" runat="server" ImageUrl="~/Controls/JpegCaptcha.aspx" AlternateText="" />
        </p>
        <p>
          <span class="lbl">Name</span><asp:TextBox ID="PostBy" MaxLength="12" runat="server"/>
          <span class="colSep"></span>
          <span class="lbl">Password</span><asp:TextBox ID="Password" MaxLength="6" TextMode="Password" runat="server"/>
        </p>
        <p runat="server" visible="false">
          <span class="lbl">제목</span><asp:TextBox ID="Subject" runat="server" Width="420px" />
        </p>
        <p style="vertical-align:middle;position:relative;">
          <span class="lbl" style="height:64px;position:relative;bottom:24px;">Comments</span><asp:TextBox ID="Comment" runat="server" Width="440px" TextMode="MultiLine" Rows="4" />
          <asp:Button ID="PostButton" runat="server" Text=" Post " onclick="PostButton_Click" 
              style="height:64px;position:absolute; right:24px;" />
        </p>
      </div>  
    </div>

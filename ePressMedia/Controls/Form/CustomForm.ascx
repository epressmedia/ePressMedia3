<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomForm.ascx.cs" Inherits="ePressMedia.Controls.Form.CustomForm" %>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" MinDisplayTime="10"></telerik:RadAjaxLoadingPanel >

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID ="RadAjaxLoadingPanel1" >
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="btn_submit">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="CustomForm_Container" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>

<div id ="CustomForm_Container" runat="server" >
 <asp:Panel ID="UDF_Panel" runat="server"></asp:Panel>
   
            <telerik:RadCaptcha ID="RadCaptcha1" runat="server" ValidationGroup="SendEmail" EnableRefreshImage ="false"  Display="None"
                        ErrorMessage="<%$ Resources: Resources, Captcha.ErrorMassage %>" >
                    
                    </telerik:RadCaptcha>
          <asp:Button ID="btn_submit" runat="server" Text="" onclick="btn_submit_Click"  ValidationGroup = "SubmitValidation"/>
    <asp:Label ID="lbl_errorMessage" runat="server" Text=""></asp:Label>
                
     </div>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactForm.ascx.cs"  Inherits="ePressMedia.Controls.Common.ContactForm" %>
<div class="ContactForm_Container">

<%--    <asp:UpdatePanel ID="updatepanel1" runat="server">
    <ContentTemplate>
    --%>
    
<div class="ContactForm_Header">
<asp:Label ID="lbl_header" runat="server" Text=""></asp:Label>
</div>


<div class="ContactForm_FirstName" id = "ContactForm_FirstName" runat="server" visible="false">
<asp:Label ID="lbl_firstname" runat="server" Text="<%$ Resources: Resources, Account.lbl_FirstName %>"></asp:Label>
<asp:TextBox ID="txt_firstname" runat="server"></asp:TextBox>
</div>
<div class="ContactForm_LastName" id = "ContactForm_LastName" runat="server" visible="false">
<asp:Label ID="lbl_lastname" runat="server" Text="<%$ Resources: Resources, Account.lbl_LastName %>"></asp:Label>
<asp:TextBox ID="txt_lastname" runat="server"></asp:TextBox>
</div>
<div class="ContactForm_EmailAddress" id="ContactForm_EmailAddress" runat="server" visible="false">
<asp:Label ID="lbl_emailaddress" runat="server" Text="<%$ Resources: Resources, Account.lbl_EmailAddress %>"></asp:Label>
<asp:TextBox ID="txt_emailaddress" runat="server"></asp:TextBox>
</div>
<div class="ContactForm_PhoneNumber" id="ContactForm_PhoneNumber" runat="server" visible="false">
<asp:Label ID="lbl_phonenumber" runat="server" Text="<%$ Resources: Resources, Account.lbl_PhoneNumber %>"></asp:Label>
<asp:TextBox ID="txt_phonenumber" runat="server"></asp:TextBox>
</div>
<div class="ContactForm_Address" id = "ContactForm_Address" runat="server" visible="false">
<asp:Label ID="lbl_address" runat="server" Text="<%$ Resources: Resources, Account.lbl_Address1 %>"></asp:Label>
<asp:TextBox ID="txt_address" runat="server"></asp:TextBox>
</div>
<div class="ContactForm_City" id = "ContactForm_City" runat="server" visible="false">
<asp:Label ID="lbl_city" runat="server" Text="<%$ Resources: Resources, Account.lbl_City %>"></asp:Label>
<asp:TextBox ID="txt_city" runat="server"></asp:TextBox>
</div>
<div class="ContactForm_State" id="ContactForm_State" runat="server" visible="false">
<asp:Label ID="lbl_state" runat="server" Text="<%$ Resources: Resources, Account.lbl_State %>"></asp:Label>
<asp:TextBox ID="txt_state" runat="server"></asp:TextBox>
</div>
<div class="ContactForm_PostalCode" id="ContactForm_PostalCode" runat="server" visible="false">
<asp:Label ID="lbl_postalcode" runat="server" Text="<%$ Resources: Resources, Account.lbl_ZipCode %>"></asp:Label>
<asp:TextBox ID="txt_postalcode" runat="server"></asp:TextBox>
</div>
<div class="ContactForm_Comments" id = "ContactForm_Comments" runat="server" visible="false">
<asp:Label ID="lbl_comments" runat="server" Text="<%$ Resources: Resources, Account.lbl_Comment %>"></asp:Label>
<asp:TextBox ID="txt_comments" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
</div>
<div id="Error1"></div>
                <div>
                    
                    
                    
                       <telerik:RadCaptcha ID="RadCaptcha1" runat="server" ValidationGroup="SendEmail" EnableRefreshImage ="false"  Display="None"
                        ErrorMessage="<%$ Resources: Resources, Captcha.ErrorMassage %>" >
                    
                    </telerik:RadCaptcha>
                    
                    
                </div>
                <div>
                <asp:Button ID="btn_send" runat="server" Text="<%$ Resources: Resources, Gen.lbl_Send %>" onclick="btn_send_Click"  ValidationGroup = "SendEmail"/><asp:Label
                    ID="lbl_errorMessage" runat="server" Text=""></asp:Label>
                </div>
         
<%--    </ContentTemplate>
    </asp:UpdatePanel>
        --%>
</div>

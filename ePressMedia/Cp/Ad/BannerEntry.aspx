<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BannerEntry.aspx.cs" Inherits="ePressMedia.Cp.Ad.BannerEntry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Scripts/jquery-latest.js" type="text/javascript"></script>
    <link href="/CP/Style/Cp.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">
        function fileUploaded(sender, args) {
            //Truncate the name
            truncateName(args);

            //Display the picture
            var id = args.get_fileInfo().TempFileLocation;

            // $row.hide();
            $(".imageContainer").show();
            $(".imageContainer .banner_image").attr("src", getImageUrl(id));


        }
        function truncateName(args) {
            var $span = $telerik.$(".ruUploadProgress", args.get_row());
            var text = $span.text();

            if (text.length > 16) {
                var newString = text.substring(0, 16) + '...';
                $span.text(newString);
            }
        }
        function getImageUrl(id) {
            //var url = window.location.href;
            var handler = "/Article/StreamImage.ashx?path=/RadUploadTemp/" + id.substring(id.lastIndexOf("\\") + 1, id.length);
            return handler; // completeUrl
        }

        $(document).ready(function () {
            $('.rdo_start_immediate').click(function (event) {

                if ($('#<%=start_js_active.ClientID%>').text() == "") {
                    $('.txt_start_date').hide();
                    var myVal = document.getElementById('<%=val_start_date.ClientID%>');
                    ValidatorEnable(myVal, false);
                }

            });

            $('.rdo_start_specific').click(function (event) {
                if ($('#<%=start_js_active.ClientID%>').text() == "") {
                    $('.txt_start_date').show();
                    var myVal = document.getElementById('<%=val_start_date.ClientID%>');
                    ValidatorEnable(myVal, true);
                }

            });

            $('.rdo_no_expire').click(function (event) {

                $('.txt_end_date').hide();
                var myVal = document.getElementById('<%=val_end_date.ClientID%>');
                ValidatorEnable(myVal, false);

                var myVal2 = document.getElementById('<%=val_start_end_date.ClientID%>');
                ValidatorEnable(myVal2, false);
                var myVal3 = document.getElementById('<%=val_end_today_date.ClientID%>');
                ValidatorEnable(myVal3, false);
            });

            $('.rdo_end_specific').click(function (event) {

                $('.txt_end_date').show();
                var myVal = document.getElementById('<%=val_end_date.ClientID%>');
                ValidatorEnable(myVal, true);


                if ($('.txt_start_date').is(':visible')) {
                    var myVal2 = document.getElementById('<%=val_start_end_date.ClientID%>');
                    ValidatorEnable(myVal2, true);
                }
                else {
                    var myVal2 = document.getElementById('<%=val_end_today_date.ClientID%>');
                    ValidatorEnable(myVal2, true);
                }


            });


        });

        function CloseAndRebind(args) {

            GetRadWindow().close();
        }

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function validateCombo(source, args) {
            if (args.Value.length < 1) {
                args.IsValid = false;
            } else {
                args.IsValid = true;
            }
        }

    </script>
    <div>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <div id="div_advertiser_edit" runat="server" visible="false" class="gen_field">
            <asp:Label ID="Label11" runat="server" Text="Advertiser" CssClass="title"></asp:Label>
            <telerik:RadTextBox ID="txt_advertiser_name" runat="server" ReadOnly="true" CssClass="field">
            </telerik:RadTextBox>
        </div>
        <div id="div_advertiser_add" runat="server" class="gen_field">
            <asp:Label ID="Label12" runat="server" Text="Advertiser" CssClass="title"></asp:Label>
            <telerik:RadComboBox ID="cbo_businessEntity" runat="server" DropDownAutoWidth="Enabled"
                Height="150" EmptyMessage="Select" DataSourceID="BE_Source" DataTextField="PrimaryName"
                DataValueField="BusinessEntityId" EnableAutomaticLoadOnDemand="True" ItemsPerRequest="10"
                ShowMoreResultsBox="true" EnableVirtualScrolling="true" Filter="Contains" Width="90%">
            </telerik:RadComboBox>
            <asp:CustomValidator ID="CustomValidator1" ControlToValidate="cbo_businessEntity"
                runat="server" ValidateEmptyText="true" ClientValidationFunction="validateCombo"
                ValidationGroup="save" CssClass="error" ErrorMessage="Please select an advertiser">
            </asp:CustomValidator>
            <telerik:OpenAccessLinqDataSource ID="BE_Source" runat="server" ContextTypeName="EPM.Data.Model.EPMEntityModel"
                EntityTypeName="" ResourceSetName="BusinessEntities" Select="new (BusinessEntityId, PrimaryName)">
            </telerik:OpenAccessLinqDataSource>
        </div>
        <div class="gen_field">
            <asp:Label ID="Label9" runat="server" Text="Description" CssClass="title"></asp:Label>
            <telerik:RadTextBox ID="txt_description" runat="server" CssClass="field" Width="90%">
            </telerik:RadTextBox>
            <asp:RequiredFieldValidator ID="txt_description_validator" runat="server" CssClass="error"
                ErrorMessage="Description is required." ControlToValidate="txt_description" ValidationGroup="save"></asp:RequiredFieldValidator>
        </div>
        <div style="clear: both">
        </div>
        <div class="gen_field">
        <asp:Label ID="Label13" runat="server" Text="Start Date" CssClass="title"></asp:Label>
            <asp:RadioButton ID="rdo_start_Immediate" runat="server" Text="Start Immedidate"
                CssClass="rdo_start_immediate" GroupName="start_date" Checked="true" />
            <asp:RadioButton ID="rdo_start_specific" runat="server" Text="Specify Start Date"
                CssClass="rdo_start_specific" GroupName="start_date" />
            <telerik:RadDatePicker ID="txt_start_date" runat="server" CssClass="txt_start_date"
                Style="display: none">
            </telerik:RadDatePicker>
            <asp:RequiredFieldValidator ID="val_start_date" runat="server" CssClass="error" ErrorMessage="Start date must be entered"
                ControlToValidate="txt_start_date" ValidationGroup="save" Enabled="false"></asp:RequiredFieldValidator>
                <asp:Label ID="start_js_active" runat="server" CssClass="start_js_active" Style="display:none" />
        </div>
        <div class="gen_field">
        <asp:Label ID="Label14" runat="server" Text="End Date" CssClass="title"></asp:Label>
            <asp:RadioButton ID="rdo_no_expire" runat="server" Text="Don't Expire" GroupName="end_date"
                CssClass="rdo_no_expire" Checked="true" />
            <asp:RadioButton ID="rdo_end_specific" runat="server" Text="Specify End Date" GroupName="end_date"
                CssClass="rdo_end_specific" />
            <telerik:RadDatePicker ID="txt_end_date" runat="server" CssClass="txt_end_date" Style="display: none">
            </telerik:RadDatePicker>
            <asp:RequiredFieldValidator ID="val_end_date" runat="server" CssClass="error" ErrorMessage="End date must be entered"
                ControlToValidate="txt_end_date" ValidationGroup="save" Enabled="false"></asp:RequiredFieldValidator>
            <br />
            <asp:CompareValidator ID="val_start_end_date" runat="server" ControlToCompare="txt_start_date"
                CultureInvariantValues="true" Display="Dynamic" CssClass="error" EnableClientScript="true"
                ControlToValidate="txt_end_date" ErrorMessage="End Date must be greater than start date."
                Type="Date" ValidationGroup="save" Operator="GreaterThan" Enabled="false"></asp:CompareValidator>
            <br />
            <asp:CompareValidator ID="val_end_today_date" runat="server" ErrorMessage="End date must be greater than today"
                Operator="GreaterThan" ControlToValidate="txt_end_date" Type="Date" ValidationGroup="save"
                CssClass="error" Enabled="false" />
        </div>
        <div style="clear: both">
        </div>
        <div class="gen_field">
            <asp:Label ID="Label2" runat="server" Text="Media Type" CssClass="title"></asp:Label>
            <telerik:RadComboBox ID="ddl_meida_type" runat="server" Enabled="false">
            </telerik:RadComboBox>
        </div>
        <div style="clear: both">
        </div>
        <div class="gen_field">
            <asp:Label ID="Label1" runat="server" Text="Weight" CssClass="title"></asp:Label>
            <telerik:RadSlider ID="sl_weight" runat="server" LargeChange="4" SmallChange="1"
                MinimumValue="1" CssClass="weight_slider" MaximumValue="9" Width="400" Height="40"
                ItemType="Tick" TrackPosition="TopLeft">
            </telerik:RadSlider>
        </div>
        <div style="clear: both">
        </div>
        <div runat="server" id="ImagePanel">
            <div class="gen_field">
                <asp:Label ID="Label3" runat="server" Text="Banner Image" CssClass="title"></asp:Label>
                <telerik:RadAsyncUpload runat="server" ID="AsyncUpload1" MultipleFileSelection="Disabled"
                    OnClientFileUploaded="fileUploaded" TemporaryFolder="~/RadUploadTemp" TargetFolder="~/target"
                    AllowedFileExtensions="jpeg,jpg,gif,png,bmp" HttpHandlerUrl="~/Article/ImageHandler.ashx"
                    EnableInlineProgress="False" OnFileUploaded="RadAsyncUpload1_FileUploaded" PostbackTriggers="btn_save" />
                <asp:Label ID="lbl_path" runat="server" Text="" Visible="false"></asp:Label>
                <div class="imageContainer" id="imageContainer" runat="server">
                    <asp:Image ID="banner_image" CssClass="banner_image" runat="server" Width="200px" />
                </div>
            </div>
            <div style="clear: both">
            </div>
            <div class="gen_field">
                <asp:Label ID="Label4" runat="server" Text="Width" CssClass="title"></asp:Label><telerik:RadTextBox
                    ID="txt_width" runat="server">
                </telerik:RadTextBox>
            </div>
            <div class="gen_field">
                <asp:Label ID="Label5" runat="server" Text="Height" CssClass="title"></asp:Label><telerik:RadTextBox
                    ID="txt_height" runat="server">
                </telerik:RadTextBox>
            </div>
            <div class="gen_field">
                <asp:Label ID="Label6" runat="server" Text="Alt Text" CssClass="title"></asp:Label><telerik:RadTextBox
                    ID="txt_alt_text" runat="server">
                </telerik:RadTextBox>
            </div>
            <div class="gen_field">
                <asp:Label ID="Label7" runat="server" Text="Destination URL" CssClass="title"></asp:Label><telerik:RadTextBox
                    ID="txt_destination_url" runat="server" Width="90%">
                </telerik:RadTextBox>
            </div>
            <div class="gen_field">
                <asp:Label ID="Label8" runat="server" Text="Target" CssClass="title"></asp:Label>
                <telerik:RadComboBox ID="ddl_target_link" runat="server">
                </telerik:RadComboBox>
            </div>
        </div>
        <div style="clear: both">
        </div>
        <div id="div_active" runat="server" visible="false" class="gen_field">
            <asp:Label ID="Label10" runat="server" Text="Active" CssClass="title"></asp:Label>
            <asp:CheckBox ID="chk_active" runat="server" />
        </div>
        <div style="float: right">
            <asp:Button ID="btn_save" runat="server" Text="Save" OnClick="btn_save_Click" ValidationGroup="save" />
        </div>
    </div>
    </form>
</body>
</html>

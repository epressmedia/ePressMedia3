<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleThumbnail.aspx.cs" Inherits="ePressMedia.Article.ArticleThumbnail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery-latest.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function GetRadWindow() {
            var oWindow = null; if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog                
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)              
            return oWindow;
        }

        function CloseDataEntry() {

            GetRadWindow().close();
        } 

    </script>
</head>

<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <script type="text/javascript" language="javascript">

function SetUniqueRadioButton(strGroupName, current) {

$("input[name$='" + strGroupName + "']").attr('checked', false);
current.checked = true;
//alert(current.value);
$(".txt_url").val(current.value);
}
</script>
    <style>
        .thumb_item_div
        {
            border: 1px solid rgb(185, 185, 185);
padding: 5px;
margin: 5px;
float:left;
        }
        .thumb_item_div img 
        {
            max-height: 100px;
            max-width: 300px;
        }
        
        .txt_url
        {
         display:none;
        }
    </style>
    <div style="height:10px"></div>
    <epm:SlideDownPanel ID="slider" runat="server" Title="Thumbnail" Description="Select a Thumbnail to be used as primary in this article" 
                Orientation="Vertical" Enabled="true" Expanded="true">
    <div id="thumbnail_container" runat="server">
    <asp:Repeater ID="thumnail_repeater" runat="server" 
            onitemdatabound="thumnail_repeater_ItemDataBound">
    <ItemTemplate>
    <div class="thumb_item_div">
        <div class="thumb_image_div">
        <asp:Image ID="thumbnail_image" runat="server" CssClass="thumbnail_img"/>
        </div>
        <div style="text-align:center">
        <asp:RadioButton ID="thumbnail_radio" runat="server"/>
        </div>
    </div>
    </ItemTemplate></asp:Repeater>
    </div>
    <div style="clear:both">
    <asp:Label ID="lbl_msg" Text="Please select an image to generate a thumbmail" runat="server" Visible="false"></asp:Label>
    </div>
    <div style="clear:both">
    <telerik:RadButton ID="btn_ok" Text="OK" runat="server" OnClick="btn_OK_Click"></telerik:RadButton>
    <asp:TextBox ID="txt_url" runat="server" class="txt_url" Text="" ></asp:TextBox>
    </div>
    </epm:SlideDownPanel>
    </form>
    
</body>
</html>

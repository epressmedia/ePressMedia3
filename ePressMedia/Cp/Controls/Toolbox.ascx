<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Toolbox.ascx.cs" Inherits="ePressMedia.Cp.Controls.Toolbox" %>
<script type="text/javascript">
    function onClientToolbarButtonClicking(sender, args) {
        if  (args.get_item().get_text() == "Delete")
        {
        var agree = confirm("Are you sure you want to delete?");
        if (agree)
            return true;
        else
            return args.set_cancel(true);
         }

    }
</script>
            <telerik:RadToolBar id="RadToolBar1" runat="server" 
    EnableRoundedCorners="true" EnableShadows="true" Width="100%" 
    onbuttonclick="RadToolBar1_ButtonClick" OnClientButtonClicking="onClientToolbarButtonClicking" >
                <Items>
                    <telerik:RadToolBarButton ImageUrl="../Img/up.png" Text="Up" Visible="false"></telerik:RadToolBarButton>
                     <telerik:RadToolBarButton ImageUrl="../Img/down.png" Text="Down" Visible="false"></telerik:RadToolBarButton>
                     <telerik:RadToolBarButton ImageUrl="../Img/parent.png" Text="Move" Visible="false"></telerik:RadToolBarButton>
                     <telerik:RadToolBarButton ImageUrl="../Img/new.png" Text="Add" Visible="false"></telerik:RadToolBarButton>
                     <telerik:RadToolBarButton ImageUrl="../Img/edit.png" Text="Edit" Visible="false"></telerik:RadToolBarButton>
                     <telerik:RadToolBarButton ImageUrl="../Img/save.png" Text="Save" Visible="false"></telerik:RadToolBarButton>
                     <telerik:RadToolBarButton ImageUrl="../Img/save.png" Text="Save Changes" Visible="false"></telerik:RadToolBarButton>
                     <telerik:RadToolBarButton ImageUrl="../Img/delete.png" Text="Delete" Visible="false" ></telerik:RadToolBarButton>
                     <telerik:RadToolBarButton ImageUrl="../Img/parent.png" Text="Cancel" Visible="false"></telerik:RadToolBarButton>
                     <telerik:RadToolBarButton ImageUrl="../Img/approve.png" Text="Approve" Visible="false"></telerik:RadToolBarButton>

                    </Items>
              </telerik:RadToolBar>

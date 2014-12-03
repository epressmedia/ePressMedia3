<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageConfig.aspx.cs" Inherits="ePressMedia.Cp.Pages.PageConfig" ValidateRequest ="false" %>

  <%@ Register src="~/CP/Controls/PageBuilder.ascx" tagname="PageBuilder" tagprefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
        <script src="/Scripts/jquery-latest.js" type="text/javascript"></script>
    <script src="/cp/Scripts/Common.js" type="text/javascript"></script>
    <script src="/cp/Scripts/custom_jquery.js" type="text/javascript"></script>
    <link href="/CP/Style/Cp.css" rel="stylesheet" type="text/css" />
    
   <style type="text/css">
   html, body, form
{
    margin: 0;
    padding: 0;
    height: 100%;
}
</style>
</head>
<body >
    <form id="form1" runat="server">
    <div id="PageConfig_Container" style="width:980px;" runat="server">
      <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
  </telerik:RadScriptManager>
    <h1>
        Page Builder - <asp:Label ID="CatName" runat="server" Text=""></asp:Label></h1>
   

        <telerik:RadTabStrip ID="BuilderTab" runat="server" MultiPageID="BuilderPages"
        SelectedIndex="0" >
        <Tabs>
            <telerik:RadTab ID="tab_list" runat="server" Text="List View">
            </telerik:RadTab>
            <telerik:RadTab ID="tab_detail" runat="server" Text="Detail View">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="BuilderPages" runat="server"  SelectedIndex = "0">
        <telerik:RadPageView ID="BuilderListView"  runat="server" CssClass="PageBuilderList">
            <asp:Label ID="txt_List_View" runat="server" Text="List View" CssClass="PageBuilder_TabContent_Header"></asp:Label>
            <uc:PageBuilder ID ="PageBuilder" runat="server" />
        </telerik:RadPageView>
        <telerik:RadPageView ID="BuilderDetailView" runat="server" CssClass="PageBuilderList">
            <asp:Label ID="txt_Detail_View" runat="server" Text="Detail View " CssClass = "PageBuilder_TabContent_Header"></asp:Label>
            <uc:PageBuilder ID="PageBuilderDetail" runat="server"  UseForType="DetailView" class="PageBuilderDetail" />
        </telerik:RadPageView>
    </telerik:RadMultiPage>

</div>
</form>
</body>
</html>


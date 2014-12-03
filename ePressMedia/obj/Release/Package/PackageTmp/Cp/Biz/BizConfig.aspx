<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.Master" AutoEventWireup="true" CodeBehind="BizConfig.aspx.cs" Inherits="ePressMedia.Cp.Biz.BizConfig" %>
<%@ Register src="~/CP/Controls/PageBuilder.ascx" tagname="PageBuilder" tagprefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
<h1>        Page Builder - <asp:Label ID="CatName" runat="server" Text=""></asp:Label></h1>
        <div class="backLink">

</div>

    

        <telerik:RadTabStrip ID="BuilderTab" runat="server" MultiPageID="BuilderPages"
        SelectedIndex="0" >
        <Tabs>
            <telerik:RadTab runat="server" Text="List View">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Detail View">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="BuilderPages" runat="server"  SelectedIndex = "0">
        <telerik:RadPageView ID="BuilderListView"  runat="server" CssClass="PageBuilderList">
            <asp:Label ID="txt_List_View" runat="server" Text="List View" CssClass="PageBuilder_TabContent_Header"></asp:Label>
            <uc:PageBuilder ID ="PageBuilder" runat="server" ContentType = "Biz"/>
        </telerik:RadPageView>
        <telerik:RadPageView ID="BuilderDetailView" runat="server" CssClass="PageBuilderList">
            <asp:Label ID="txt_Detail_View" runat="server" Text="Detail View " CssClass = "PageBuilder_TabContent_Header"></asp:Label>
            <uc:PageBuilder ID="PageBuilderDetail" runat="server" ContentType="Biz" UseForType="DetailView" class="PageBuilderDetail" />
        </telerik:RadPageView>
    </telerik:RadMultiPage>

</asp:Content>

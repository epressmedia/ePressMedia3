<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true" Inherits="Cp_Biz_BizCategories" Codebehind="BizCategories.aspx.cs" %>
<%@ Register src="~/CP/Controls/Toolbox.ascx" tagname="Toolbox" tagprefix="uc" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <script type="text/javascript">

      function hideMessageBox() {
          var m = $find('MsgBoxMpe');
          if (m)
              m.hide();
      }

  </script> 

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
<style>
    .CatTreeView
    {
        border: 1px solid #DADADA;
        margin: 5px 5px 0 0;
        width: 45%;
        float:left;
    }
    .CatAddPanel
    {
        float:left;
        margin: 10px 0 0 10px;
    }
    .CatAddPanel span
    {
        display:block;
    }
    .CatAddPanel div
    {
        margin-bottom:10px;
    }
</style>
  <h1>Business Directory Category</h1>
  <uc:toolbox id="toolbox1" runat="server" ButtonAvailable= "add,cancel,edit,delete,save" ></uc:toolbox>
  <div class="CatTreeView">
  <telerik:RadTreeView runat="server" ID="CatTreeView"  DataTextField="CategoryName" DataValueField = "CategoryId"
    DataFieldID="CategoryId" DataFieldParentID="ParentCategoryId" 
          onnodeclick="CatTreeView_NodeClick" >
</telerik:RadTreeView>
</div>
<div id="CatEditPanel" runat="server" class="CatAddPanel" visible="false" >
<div >
<asp:HiddenField ID="lbl_CatId" runat="server" />
</div>
<div >
<asp:Label ID="Label1" runat="server" Text="Category Name"></asp:Label>
<asp:TextBox ID="txt_CatName" runat="server" Width="250px" ></asp:TextBox>
</div>
<div >
<asp:Label ID="Label2" runat="server" Text="Parent Category" ></asp:Label>
<asp:DropDownList ID="ddl_ParentCat" runat="server"  Width="250px"></asp:DropDownList>
</div>
<div>
    <asp:Button ID="btn_BEs" runat="server" Text="List Members" 
        onclick="btn_BEs_Click" />
</div>
</div>


</asp:Content>




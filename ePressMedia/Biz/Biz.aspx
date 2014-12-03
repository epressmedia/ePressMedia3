<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage_1131.master" AutoEventWireup="true" Inherits="Biz_Biz" Codebehind="Biz.aspx.cs" %>

<%@ Register 
    Assembly="AjaxControlToolkit" 
    Namespace="AjaxControlToolkit" 
    TagPrefix="ajaxToolkit" %>

<%@ Register src="BizList.ascx" tagname="BizList" tagprefix="uc1" %>
<%@ Register src="BizCatSearch.ascx" tagname="BizCatSearch" tagprefix="uc1" %>
<%@ Register src="BizRankBox.ascx" tagname="BizRankBox" tagprefix="uc1" %>
<%@ Register src="BizSearch.ascx" tagname="BizSearch" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Styles/VideoAd.css" rel="stylesheet" type="text/css" />
  <script src="../Scripts/jquery.cookie.js" type="text/javascript"></script>  
  <script src="../Scripts/Default.js" type="text/javascript"></script>  
  <script src="../Scripts/Biz.js" type="text/javascript"></script>  
  <script src="../Scripts/VideoAd.js" type="text/javascript"></script>
  <script type="text/javascript">
      $(document).ready(function () {
          $('div.art:even').css('backgroundColor', '#f7f7f7');
      });
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LeftBarContent" Runat="Server">

    
   <uc1:BizCatSearch ID="BizCatSearch1" runat="server" />
    <%--<uc1:BizRankBox ID="BizRankBox1" runat="server" />--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Content" Runat="Server">

  <div>
    <asp:Image ID="HeaderImg" runat="server" Visible="false" />
  </div>

  <uc1:BizSearch ID = "BizSearch1" runat="server" />
  <uc1:BizList ID="BizList1" runat="server" />

  
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="RightBarContent" Runat="Server">
</asp:Content>





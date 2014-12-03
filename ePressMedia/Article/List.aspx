<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage_1141.master" AutoEventWireup="true" Inherits="Article_Articles" Codebehind="List.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <script src="../Scripts/default.js" type="text/javascript"></script>  
    <script type="text/javascript">
       $(document).ready(function () {
           $('div.art:even').css('backgroundColor', '#f7f7f7');
       });
  </script>
  
    
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Menu" Runat="Server">

</asp:Content>

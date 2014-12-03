<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage_1111.master" AutoEventWireup="true" CodeBehind="Preview.aspx.cs" Inherits="ePressMedia.Cp.Pages.Preview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderScript" Runat="Server">
        <script src='<%= this.ResolveClientUrl("~/Scripts/jquery-latest.min.js") %>' type="text/javascript"></script>
        <script src='<%= this.ResolveClientUrl("~/Scripts/Common.js") %>' type="text/javascript"></script>
        <script src='<%= this.ResolveClientUrl("~/Scripts/Custom.js") %>' type="text/javascript"></script>
        <script src='<%= this.ResolveClientUrl("~/Scripts/jquery.cookie.js") %>' type="text/javascript"></script>
        <script src='<%= this.ResolveClientUrl("~/Scripts/Controls_Article.js") %>' type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('div.art:even').css('backgroundColor', '#f7f7f7');


            $(".PreviewContainer a").click(function () { return false; });

            $("div.Preview_Label").hide();

            $(".Preview_Op").mouseover(function () {

                $(this).children("div.PreviewContainer").css('opacity', '0.2');
                $(this).children("div.Preview_Label").show(); //.css('visibility', 'visible');
                if ($(this).attr("ref") == "red") {
                    $(this).children("div.PreviewContainer").css('outline', '2px solid red');
                    $(this).children("div.Preview_Label").css('color', 'red');
                }
                else {
                    $(this).children("div.PreviewContainer").css('outline', '2px solid blue');
                    $(this).children("div.Preview_Label").css('color', 'blue');
                }
                $(this).children("div.PreviewContainer").css('margin', '0px 2px');
            });

            $(".Preview_Op").mouseout(function () {
                $(this).children("div.PreviewContainer").css('opacity', '1.0');
                $(this).children("div.Preview_Label").hide(); //css('visibility', 'hidden');
                $(this).children("div.PreviewContainer").css('outline', 'none');
                $(this).children("div.PreviewContainer").css('margin', '0px 0px');
            });

        });

  </script>
  <style type="text/css">
              .Preview_Label
        {
            
font-size: 14px;
font-weight: bold;
z-index: 9999;
margin:2px 5px;

        }

        

        
      
  </style>
  
    
</asp:Content>
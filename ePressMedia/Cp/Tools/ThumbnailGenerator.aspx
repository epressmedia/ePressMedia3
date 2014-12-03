<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true" Inherits="Cp_Tools_ThumbnailGenerator" Codebehind="ThumbnailGenerator.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
<style>
    .processTypeArea
    {
        padding:5px;
        margin-bottom:10px;
        border: 1px solid black;
    }
</style>
<h1>Thumbnail Generator</h1>
<h2>This tool will read all articles which do not have a thumbnail but image in their HTML content and generate thumbnails.</h2>
<h3 style="font-weight:bold;color:red;">This tool will impact the application performace seriously.</h3>
<br />

<div class="processTypeArea">
<div>
    Row Range to Process: <asp:TextBox ID="txt_from" runat="server" Text = "1"></asp:TextBox>~<asp:TextBox ID="txt_to" runat="server" Text = "500"></asp:TextBox>
    <asp:RadioButton ID="rdb_article" runat="server" Text="Article" GroupName="type" Checked=true />
    <asp:RadioButton ID="rdb_forum" runat="server" Text="Forum" GroupName="type"/>
    <asp:RadioButton ID="rdb_biz" runat="server" Text="Biz" GroupName="type"/>
    </div>

    <div><asp:Button ID="btn_process" runat="server" Text="Process By Recent Contents" 
        onclick="btn_process_Click" />
        </div>
        </div>

            <div class="processTypeArea">
            <div>
    <asp:DropDownList ID=ddl_content_type runat="server" AutoPostBack="true" 
            onselectedindexchanged="ddl_content_type_SelectedIndexChanged">
            <asp:ListItem Text = "-- Select -- " Value ="0"></asp:ListItem>
        <asp:ListItem Text = "Article" Value ="article"></asp:ListItem>
    </asp:DropDownList>
<asp:DropDownList ID ="ddl_category" runat="server" AutoPostBack = "true"
                    onselectedindexchanged="ddl_category_SelectedIndexChanged"></asp:DropDownList>
                <asp:Label ID="lbl_catInfo" runat="server" Text=""></asp:Label>
</div>
    <div><asp:Button ID="btn_ProcessByCategory" runat="server" Text="Process By Category" 
        onclick="btn_ProcessByCategory_Click" />
    </div>
    </div>

    <br />
    <br />
    <asp:Label ID="lbl_log" runat="server" Text=""></asp:Label>


</asp:Content>


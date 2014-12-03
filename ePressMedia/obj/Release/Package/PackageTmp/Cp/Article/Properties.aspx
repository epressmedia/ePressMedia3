<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.Master" AutoEventWireup="true"
    CodeBehind="Properties.aspx.cs" Inherits="ePressMedia.Cp.Article.ArticleProperties" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <style>
        
    </style>
    <h1>
        <asp:Literal ID="CatName" runat="server" />
        Article Module Properties</h1>
    <%--  Upload Directory--%>
    <epm:SlideDownPanel ID="SlideDownPanel1" runat="server" Title="General Properties"
        Description="Gerneral properties being used in Article module" Orientation="Vertical"
        Enabled="true" Expanded="true">
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="sliderContainer">
                <div>
                    <span class="title">Maximum number of articles per category</span>
                    <telerik:RadNumericTextBox ID="txt_max_art" runat="server" ShowSpinButtons="true"
                        NumberFormat-DecimalDigits="0">
                    </telerik:RadNumericTextBox>
                </div>
                <div>
                    <span class="title">Maximum article image file size</span>
                    <telerik:RadNumericTextBox ID="txt_max_filesize" runat="server" ShowSpinButtons="true"
                        NumberFormat-DecimalDigits="0">
                    </telerik:RadNumericTextBox>
                    KB
                </div>
                <div>
                    <span class="title">Number of Hours to show New in Article</span>
                    <telerik:RadNumericTextBox ID="txt_hours_new" runat="server" ShowSpinButtons="true"
                        NumberFormat-DecimalDigits="0">
                    </telerik:RadNumericTextBox>
                    Hours
                </div>
                <div>
                    <asp:Button ID="btn_general_setting_save" runat="server" Text="Save" OnClick="btn_general_setting_save_Click" />
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </epm:SlideDownPanel>
    <epm:SlideDownPanel ID="uploaddirectory_slider" runat="server" Title="Upload Directory"
        Description="Specify upload directory where images will be saved for articles"
        Orientation="Vertical" Enabled="true" Expanded="true">
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel3" runat="server">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxPanel ID="RadAjaxPanel3" runat="server" LoadingPanelID="RadAjaxLoadingPanel3">
            <div class="sliderContainer">
                <div>
                    <span class="title">Image Upload Root</span>
                    <telerik:RadTextBox ID="txt_articleUploadeRoot" runat="server">
                    </telerik:RadTextBox>
                </div>
                <div style="display: none">
                    <span class="title">Thumbnail Image Root</span>
                    <telerik:RadTextBox ID="txt_articlethumbnailUploadeRoot" runat="server">
                    </telerik:RadTextBox>
                </div>
                <div>
                    <span class="title">&nbsp;</span>
                    <telerik:RadButton ID="btn_uploadRoot" runat="server" Text="Save" OnClick="btn_uploadRoot_Click">
                    </telerik:RadButton>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </epm:SlideDownPanel>
    <%--  Thumbnail--%>
    <epm:SlideDownPanel ID="slider" runat="server" Title="Thumbnail" Description="Specify thumbnail mode and dementions for article images"
        Orientation="Vertical" Enabled="true" Expanded="false" Visible="false">
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel2">
            <div class="sliderContainer">
                <div>
                    <span class="title">Thumbnail Width</span>
                    <telerik:RadTextBox ID="thumb_width" runat="server" InputType="Number">
                    </telerik:RadTextBox>&nbsp;px
                </div>
                <div>
                    <span class="title">Thumbnail Height</span>
                    <telerik:RadTextBox ID="thumb_height" runat="server" InputType="Number">
                    </telerik:RadTextBox>&nbsp;px
                </div>
                <div>
                    <span class="title">Background Color</span>
                    <telerik:RadTextBox ID="thumb_bg_color" runat="server">
                    </telerik:RadTextBox>
                </div>
                <div>
                    <span class="title">Thumbnail Mode</span>
                    <asp:DropDownList ID="ddl_thumbmode" runat="server">
                    </asp:DropDownList>
                </div>
                <div>
                    <span class="title">&nbsp;</span>
                    <telerik:RadButton ID="btn_thumbnail" runat="server" Text="Save" OnClick="btn_thumbnail_Click">
                    </telerik:RadButton>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </epm:SlideDownPanel>
    <epm:SlideDownPanel ID="SlideDownPanel2" runat="server" Title="Related Article Option"
        Description="Specify weights to be used to populate the related article" Orientation="Vertical"
        Enabled="true" Expanded="true">
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel4" runat="server">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxPanel ID="RadAjaxPanel4" runat="server" LoadingPanelID="RadAjaxLoadingPanel4">
            <div class="">
                <div>
                    <span class="title">Title Weight</span>
                    <telerik:RadSlider ID="sl_title" runat="server" LargeChange="4" SmallChange="1" MinimumValue="1" CssClass="weight_slider"
                        MaximumValue="9" Width="400" Height="40" ItemType="Tick" TrackPosition="Center">
                    </telerik:RadSlider>
                </div>
                <div>
                    <span class="title">Body Weight</span>
                    <telerik:RadSlider ID="sl_body" runat="server" LargeChange="4" SmallChange="1" MinimumValue="1" CssClass="weight_slider"
                        MaximumValue="9" Width="400" Height="40" ItemType="Tick" TrackPosition="TopLeft">
                    </telerik:RadSlider>
                </div>
                <div>
                    <span class="title">Tag Weight</span>
                    <telerik:RadSlider ID="sl_tag" runat="server" LargeChange="4" SmallChange="1" MinimumValue="1" CssClass="weight_slider"
                        MaximumValue="9" Width="400" Height="40" ItemType="Tick" TrackPosition="TopLeft">
                    </telerik:RadSlider>
                </div>
                <div>
                    <span class="title">View Count Weight</span>
                    <telerik:RadSlider ID="sl_count" runat="server" LargeChange="4" SmallChange="1" MinimumValue="1" CssClass="weight_slider"
                        MaximumValue="9" Width="400" Height="40" ItemType="Tick" TrackPosition="TopLeft">
                    </telerik:RadSlider>
                </div>
                <div>
                    <span class="title">&nbsp;</span>
                    <telerik:RadButton ID="btn_relatedArticle" runat="server" Text="Save" OnClick="btn_relatedArticle_Click">
                    </telerik:RadButton>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </epm:SlideDownPanel>
</asp:Content>

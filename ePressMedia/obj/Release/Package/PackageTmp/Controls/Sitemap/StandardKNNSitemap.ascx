<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_Sitemap_StandardKNNSitemap" Codebehind="StandardKNNSitemap.ascx.cs" %>

 <div> 
<telerik:RadSiteMap ID="RadSiteMap1" runat="server" MaxDataBindDepth="2">
        <LevelSettings>
            <telerik:SiteMapLevelSetting Level="0">
                <ListLayout AlignRows="true" />
                <NodeTemplate>
                <div class="StandardKNNSitemap_Header">
                <%# DataBinder.Eval(Container.DataItem, "label") %>
                </div>
                </NodeTemplate>

            </telerik:SiteMapLevelSetting>
        </LevelSettings>
</telerik:RadSiteMap>
</div>
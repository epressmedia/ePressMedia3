<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true" Inherits="Cp_Site_FileManager" Codebehind="FileManager.aspx.cs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register TagPrefix="Upload" Namespace="Brettle.Web.NeatUpload" Assembly="Brettle.Web.NeatUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

  <style type="text/css">
  .canvas { background: #eee; border:1px solid #dbdbdb; height:1%; overflow:hidden; }  
  /*.toolBar { padding-left:6px; border-top:1px solid #f7f7f7; border-bottom:1px solid #aaa; line-height:28pt; height:1%; overflow:hidden; }*/
  .btnSep { border-left:1px solid #aaa; border-right:1px solid #f7f7f7; }
  .filePane { width:auto; height:340px; margin:9px 9px 9px 9px; border:1px solid #dbdbdb; background-color:#fff; overflow:auto; } 
  .prvwBox { width:200px; height:200px; background-color:#fff; margin:9px auto 9px auto; border:1px solid #dbdbdb; }
  
  .pane, .toolBar { border-top:1px solid #f7f7f7; border-left:1px solid #f7f7f7; border-bottom:1px solid #aaa; border-right:1px solid #aaa; height:1%; overflow:hidden;background: #eee; }
  .toolBar { padding:6px; line-height:24pt; }
  
  #filePane { width:80%; height:360px; float:left; }
  #prvPane { width:220px; height:220px; text-align:center; }
  #infPane { width:220px; height:138px; }
  
  .clr { clear:both; }
  #upload { height:28px; overflow:hidden; }
  .list_box { height: 360px; width: 400px; overflow: auto; border: 1px solid #888; float: left; }
  .file_name, .file_name_alt { width: 200px; height: 20px; vertical-align: middle; padding: 0; }
  .file_name { background-color: #eee; }
  .file_list { width: 100%; }
  .file_list img { float: left; }
  .file_list a { color: #000; text-decoration: none; }
  .file_list a:hover { color: #0000ff; text-decoration: underline; }
  
  #UploadFileList { height: 120px; width: 480px; overflow: auto; border: 1px solid #dbdbdb; background-color:#fff; margin-top:-24px; margin-bottom:16px; line-height:14pt; }
  .ProgressBar { margin: 0px; border: 0px; padding: 0px; width: 100%; height: 32px; }
</style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">


  <h1>Site Files</h1>
 
  <div class="canvas">
  
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="toolBar">
      Current Location: <strong><asp:Label ID="CurFolder" runat="server" /></strong>
    </div>
    <div class="toolBar">
      <img src="../Img/parent.png" alt="" />
      <asp:LinkButton runat="server" ID="ToParent" Text=" Upper Folder" onclick="ToParent_Click" />
      &nbsp;<span class="btnSep"></span>&nbsp;
      <img src="../Img/refresh.png" alt="" />
      <asp:LinkButton runat="server" ID="Refresh" Text=" Refresh" onclick="Refresh_Click" />
      &nbsp;<span class="btnSep"></span>&nbsp;
      New Folder Name: <asp:TextBox ID="FolderName" runat="server" />&nbsp;
      <asp:Button runat="server" ID="NewFolderButton" Text="Add" onclick="NewFolderButton_Click" />
      <asp:HiddenField runat="server" ID="CurUrl" />
      <asp:HiddenField runat="server" ID="HomeUrl" />
    </div>
    <div>
      <div id="filePane" class="pane">
        <div class="filePane">
          <asp:DataList ID="DirList" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal"
              RepeatColumns="2" OnItemCommand="FileList_ItemCommand" CssClass="file_list">
            <ItemStyle CssClass="file_name_alt" />
            <ItemTemplate>
                  <asp:Image runat="server" ImageUrl="../Img/folder.png" ID="FileIcon" />
                  &nbsp;
                  <asp:LinkButton runat="server" ID="DirLink" CommandArgument='1'
                    Text='<%# Eval("Name") %>' />          
            </ItemTemplate>
          </asp:DataList>
    
          <asp:DataList ID="FileList" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal"
              RepeatColumns="2" OnItemCommand="FileList_ItemCommand" OnItemDataBound="FileList_ItemDataBound"
              CssClass="file_list">
            <ItemStyle CssClass="file_name_alt" />
            <ItemTemplate>
                  <asp:Image runat="server" ID="FileIcon" />
                  &nbsp;
                  <asp:LinkButton runat="server" ID="FileLink" CommandArgument='2'
                    Text='<%# Eval("Name") %>' />          
            </ItemTemplate>
          </asp:DataList>    
        </div>
      </div>
      
      <div style="float:left;">
        <div id="prvPane" class="pane">
          <div class="prvwBox">
            <asp:Image runat="server" ID="PreviewImage" Height="120px" Width="120px" Visible="false" />
          </div>
        
        </div>
        <div id="infPane" class="pane">
          <div style="padding:6px;line-height:16pt;">
          Selected File Info:
          <asp:Label ID="SelectedFileName" runat="server" />
          <asp:Label ID="SelectedSize" runat="server" /><br />
          URL:<br /> 
          <asp:TextBox ID="ImageURL" runat="server" Width="200px" />
          </div>
        </div>
      </div>
    </div>
    </ContentTemplate>
  </asp:UpdatePanel>
  <div style="clear:both"></div>
    <div class="toolBar">
      <div id="upload">
        <Upload:MultiFile ID="MultiFile1" runat="server" FileQueueControlID="UploadFileList" UseFlashIfAvailable="True">
          <asp:Button ID="Button1" runat="server" Text="Select Upload Files..." />
        </Upload:MultiFile>
      </div>
      <asp:RegularExpressionValidator id="RegularExpressionValidator2" 
            ControlToValidate="MultiFile1"
            ValidationExpression="(([^.;]*[.])+(jpg|gif|png|pdf|JPG|GIF|PNG|PDF); *)*(([^.;]*[.])+(jpg|gif|png|pdf|JPG|GIF|PNG|PDF))?$"
            Display="Static"
            ErrorMessage="Allow File Fomrat: jpg, gif, png, pdf"
            EnableClientScript="True" 
            runat="server"/>  
      <div id="UploadFileList">
  
      </div>
  
      <asp:Button ID="UploadButton" runat="server" Text="Upload" onclick="UploadButton_Click" />
    </div>
  </div>
  

    <Upload:ProgressBar ID="inlineProgressBar" runat="server" Inline="False" Triggers="UploadButton" />
  
  <br />
  <br />


</asp:Content>


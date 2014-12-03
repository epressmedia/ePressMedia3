<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Entry.ascx.cs" Inherits="ePressMedia.Article.ArticleEntryUC" %>
    <script src="/Scripts/tag-it.js" type="text/javascript"></script>
    <link href="/Styles/Tagit.css" rel="stylesheet" type="text/css" />
        <telerik:RadProgressManager runat="server" ID="RadProgressManager1" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function InsertSpan() {
                var editor = $find("<%= RadEditor1.ClientID %>"); //get a reference to the editor
                editor.pasteHtml('<span style="width:100px;border: 1px solid red;background-color: blue;color: white;">sample content</span>');
            }

            function InsertImage(filename) {

                var editor = $find("<%= RadEditor1.ClientID %>"); //get a reference to the editor
                //var panel = document.getElementById("mainPnl");
                //var width = editor.offsetWidth - 40;
                editor.pasteHtml('<img  src="' + filename + '" alt="" width="100%" />');
            }



            function fileUploaded(sender, args) {
                //Truncate the name
                truncateName(args);

                //Display the picture
                var id = args.get_fileInfo().TempFileLocation;

                // $row.hide();
                $(".imageContainer").show();
                $(".imageContainer").append("<img  width='100px' src='" + getImageUrl(id) + "' ref=" + args.get_fileInfo().NewFileName + " onclick=InsertImage('" + getImageUrl(id).replace(/\\/g, '\\\\') + "') />");


            }

            function fileUploadRemoving(sender, args) {
                var index = args.get_rowIndex();
                var totalCount = $telerik.$(".imageContainer img").size();
                index = totalCount - index;
                $telerik.$(".imageContainer img:eq(" + index + ")").remove();
            }


            function truncateName(args) {
                var $span = $telerik.$(".ruUploadProgress", args.get_row());
                var text = $span.text();

                if (text.length > 16) {
                    var newString = text.substring(0, 16) + '...';
                    $span.text(newString);
                }
            }

            function getImageUrl(id) {
                //var url = window.location.href;
                var handler = "/Article/StreamImage.ashx?path=/RadUploadTemp/" + id.substring(id.lastIndexOf("\\") + 1, id.length);
                return handler; // completeUrl
            }



            //<![CDATA[
            function validationFailed(sender, eventArgs) {
                $(".ErrorHodler").show();
                $(".ErrorHolder").append("<div>" + eventArgs.get_fileName() + "</div>").fadeIn("slow");
            }
            function selected(sender, args) {
                $telerik.$(args.get_row()).addClass("ruUploading");
            }


            function EditorOnClientLoad(editor, args) {
                var element = document.all ? editor.get_document().body : editor.get_document();
                var eventHandler = document.all ? "drop" : "dragstart";
                var selElem = editor.getSelectedElement();
                $telerik.addExternalHandler(element, eventHandler, function (e) {
                    $telerik.cancelRawEvent(e);
                    return false;
                });
            }

            $(function () {
                $('.tags').tagit({
                    //availableTags: sampleTags,
                    // This will make Tag-it submit a single form value, as a comma-delimited field.
                    allowSpaces: true,
                    singleField: true,
                    singleFieldNode: $('.tagnet')

                });

            });
            //]]>

        </script>
    </telerik:RadCodeBlock>

    <div class="articlePost_Container" style="padding: 10px; background-color:White;">
        <div class="secClr">
        </div>



        <table class="postTbl" width="100%">
             <tr>
                <td class="label">
                    <asp:Literal ID="Literal9" runat="server" Text='<%$ Resources: Resources, Article.lbl_Category %>'></asp:Literal>
                </td>
                <td class="data">
                    <asp:DropDownList ID="ddl_art_category" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Literal ID="Literal1" runat="server" Text='<%$ Resources: Resources, Article.lbl_Title %>'></asp:Literal>
                </td>
                <td class="data">
                    <asp:TextBox runat="server" ID="Subject" Width="100%" />
                    <asp:RequiredFieldValidator ID="SubjectReq" runat="server" ControlToValidate="Subject"
                        Display="Dynamic" ErrorMessage="<%$ Resources: Resources, Article.msg_PostRequiredField %>" ValidationGroup="PostLink" />
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Literal ID="Literal2" runat="server" Text='<%$ Resources: Resources, Article.lbl_SubTitle %>'></asp:Literal>
                </td>
                <td class="data">
                    <asp:TextBox runat="server" ID="SubTitle" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Literal ID="Literal3" runat="server" Text='<%$ Resources: Resources,  Article.lbl_PostedBy %>'></asp:Literal>
                </td>
                <td class="data">
                    <asp:TextBox runat="server" ID="Reporter" Width="200px" />
                    &nbsp;<asp:RequiredFieldValidator ID="ReporterReq" runat="server" ControlToValidate="Reporter"
                        Display="Dynamic" ErrorMessage="<%$ Resources: Resources, Article.msg_PostRequiredField %>" ValidationGroup="PostLink" />
                    <br />
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Literal ID="Literal4" runat="server" Text='<%$ Resources: Resources, Article.lbl_IssueDateTime %>'></asp:Literal>
                </td>
                <td class="data">
                    <telerik:RadDateTimePicker ID="IssueDatePicker" runat="server" Skin="Windows7" Culture="English">
                        <Calendar ID="Calendar4" runat="server" EnableKeyboardNavigation="true">
                        </Calendar>
                    </telerik:RadDateTimePicker>
                    <asp:RequiredFieldValidator ID="IssueDateReq" runat="server" ControlToValidate="IssueDatePicker"
                        Display="Dynamic" ErrorMessage="<%$ Resources: Resources, Article.msg_PostRequiredField %>" ValidationGroup="PostLink" />
                    <br />
                </td>
            </tr>

            <tr>
                <td class="label">
                    <asp:Literal ID="Literal5" runat="server" Text='<%$ Resources: Resources,  Article.lbl_SecondaryCategory %>'></asp:Literal>
                </td>
                <td class="data">
                    <asp:DropDownList ID="ddl_vc" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
                        <tr id="thumbnail_update" runat="server" visible = "false">
                <td class="label">
                    <asp:Literal ID="Literal6" runat="server" Text='<%$ Resources: Resources,  Article.lbl_UpdateThumbnail %>'></asp:Literal> 
                </td>
                <td class="data">
                    <asp:CheckBox ID="chk_update_thumbnail" runat="server" Text='<%$ Resources: Resources,  Article.lbl_UpdateThumbnailDescr %>' />
                </td>
            </tr>

                        <tr>
                <td class="label">
                    <asp:Literal ID="Literal7" runat="server" Text='<%$ Resources: Resources,  Article.lbl_UploadImages %>'></asp:Literal> 
                </td>
                <td class="data" style="text-align: center">
                    <div>
                        <telerik:RadAsyncUpload ID="RadAsyncUpload1" runat="server" HttpHandlerUrl="~/Article/ImageHandler.ashx"
                            TemporaryFolder="~/RadUploadTemp" TargetFolder="~/target" OnClientFileUploaded="fileUploaded" OnClientValidationFailed="validationFailed"
                            MultipleFileSelection="Automatic" AllowedFileExtensions="jpeg,jpg,gif,png,bmp"  OnClientFileSelected="selected"
                            EnableInlineProgress="False" UploadedFilesRendering="BelowFileInput" OnFileUploaded="RadAsyncUpload1_FileUploaded" PostbackTriggers="btn_Save">
                        </telerik:RadAsyncUpload>
                        <div class="ImageFileLimit">
                        <asp:Literal ID="ltl_upload_note" runat="server" ></asp:Literal>
                        </div>
                        <telerik:RadProgressArea ID="progressArea1" runat="server" />

     
     <div class="ErrorHolder" style="display:none">
     <div style="color:Red">
         <asp:Literal ID="Literal8" runat="server" Text='<%$ Resources: Resources,Article.msg_UploadImage  %>'></asp:Literal></div>
     </div>
     <div style="clear:both"></div>
     
                    </div>
                    <div class="imageContainer" style="display: none">
                        <asp:Label ID="lbl_imgContainer" runat="server" Text='<%$ Resources: Resources, Article.lbl_ImageClick %>'></asp:Label><br />
                    </div>
                    <div class="info">
                    </div>
                </td>
            </tr>
        </table>
                <div onmousedown="return false;">
            <epm:SlideDownPanel ID="SlideDownPanel2" runat="server" Title='<%$ Resources: Resources, Article.lbl_ContentEditor %>'
                Orientation="Vertical" Enabled="true" Expanded="true">
                                    <telerik:RadEditor ID="RadEditor1" runat="server" Height="500px" Width="97%" BorderColor="White" ToolsFile="~/Styles/ArticlePost.xml" OnClientLoad="EditorOnClientLoad">
                        <CssFiles>
                            <telerik:EditorCssFile Value="~/Styles/EditorStyle.css" />
                        </CssFiles>
                        <ImageManager UploadPaths="/Pics" EnableAsyncUpload="true" EnableImageEditor="true"
                            EnableThumbnailLinking="true" ViewMode="Thumbnails" ViewPaths="/Pics" />
                            <Modules>
                            <telerik:EditorModule Name="RadEditorStatistics" Enabled="true" Visible="true" />
                            </Modules>
                    </telerik:RadEditor>

                </epm:SlideDownPanel>
                </div>

        <div>
                    <epm:SlideDownPanel ID="SlideDownPanel1" runat="server" Title='<%$ Resources: Resources, Article.lbl_Tags %>' 
                Orientation="Vertical" Enabled="true" Expanded="false">

                    <input name="tags" id="myTagField" value="" class="tagnet" type="hidden" runat="server" />
                    <ul id="tags" runat="server" class="tags">
                    </ul>
                </epm:SlideDownPanel>
        </div>
        <div>
            <epm:SlideDownPanel ID="slider" runat="server" Title='<%$ Resources: Resources, Article.lbl_RelatedArticles %>' 
                Orientation="Vertical" Enabled="true" Expanded="false">
                <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RelatedArt_Search_Loading">
                    <epm:InformationBox ID="infoBox" runat="server" Title="Please enter a search keyword to find the related articles.">
                    </epm:InformationBox>
                    <div>
                        <telerik:RadTextBox ID="txt_Searchword" Text="" runat="server" ValidationGroup="related_search">
                        </telerik:RadTextBox>
                        <telerik:RadButton ID="btn_search" Text='<%$ Resources: Resources, Article.lbl_Search  %>' runat="server" OnClick="btn_search_Click" ValidationGroup="related_search">
                        </telerik:RadButton>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_Searchword" ErrorMessage="<%$ Resources: Resources, Article.msg_PostRequiredField %>" ValidationGroup="related_search"></asp:RequiredFieldValidator>
                    </div>
                    <div>
                        <telerik:RadListBox ID="lb_searchResult" runat="server" AllowTransfer="true" TransferToID="lb_selectedResult" AutoPostBackOnTransfer="true"
                            EnableDragAndDrop="true" Height="200px" Width="45%" OnItemDataBound="lb_searchResult_ItemDataBound"  OnTransferred="lb_searchResult_Transferred"> 
                            <ButtonSettings ShowTransferAll="false" VerticalAlign="Middle"></ButtonSettings>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container, "Value") %>
                                -
                                <%# DataBinder.Eval(Container, "Text") %>
                            </ItemTemplate>
                        </telerik:RadListBox>
                        <telerik:RadListBox ID="lb_selectedResult" runat="server" Height="200px" Width="45%">
                                               <ItemTemplate>
                                <%# DataBinder.Eval(Container, "Value") %>
                                -
                                <%# DataBinder.Eval(Container, "Text") %>
                            </ItemTemplate>
                        </telerik:RadListBox>
                    </div>
                </telerik:RadAjaxPanel>
                <telerik:RadAjaxLoadingPanel ID="RelatedArt_Search_Loading" runat="server">
                </telerik:RadAjaxLoadingPanel>
            </epm:SlideDownPanel>
        </div>
        <div class="cntrPnl">
            <asp:Button ID="btn_Save" runat="server" Text='<%$ Resources: Resources, Article.lbl_Save %>' OnClick="btn_Save_Click" ValidationGroup="PostLink" />
            <asp:Button ID="btn_Cancel" runat="server" Text='<%$ Resources: Resources,  Article.lbl_Cancel %>' OnClick="btn_Cancel_Click"  />
        </div>
         <div class="cntrPnl">
            &nbsp;</div>
    </div>
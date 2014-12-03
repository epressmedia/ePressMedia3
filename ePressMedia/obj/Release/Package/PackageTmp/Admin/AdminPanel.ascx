<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminPanel.ascx.cs" Inherits="ePressMedia.Pages.AdminPanel" %>


   <div class="Admin_EditMode_Wrap">
   <h2>Edit Mode</h2>
<div class="mode_selection">
<div><input id="chk_html" class="admin_chk_html" name = "chk_html" type="checkbox" /><a class="html_button">HTML</a>
</div><div>
<input id="chk_ascx" class="admin_chk_ascx" name = "chk_ascx" type="checkbox" /><a class="ascx_button">ASCX</a>
</div><div>
<input id="chk_image" class="admin_chk_image" name = "chk_image" type="checkbox" /><a class="image_button">IMAGE</a>
</div><div>
<input id="chk_adzone" class="admin_chk_adzone" name = "chk_adzone" type="checkbox" /><a class="adzone_button">Ad Zone</a>
</div>
</div>
</div>
<script type="text/javascript">
    $('.admin_chk_html').click(function () {
        var $this = $(this);
        // $this will contain a reference to the checkbox   
        if ($this.is(':checked')) {
            $("input[controltypefor='html']").show();
            $.cookie('admin_chk_html', true, { expires: 1, path:'/' });
        } else {
            $("input[controltypefor='html']").hide();
            $.cookie('admin_chk_html', false, { expires: 1, path: '/' });
        }
    });
    $('.admin_chk_ascx').click(function () {
        var $this = $(this);
        // $this will contain a reference to the checkbox   
        if ($this.is(':checked')) {
            $("input[controltypefor='ascx']").show();
            $.cookie('admin_chk_ascx', true, { expires: 1, path: '/' });
        } else {
            $("input[controltypefor='ascx']").hide();
            $.cookie('admin_chk_ascx', false, { expires: 1, path: '/' });
            // the checkbox was unchecked
        }
    });
    $('.admin_chk_image').click(function () {
        var $this = $(this);
        // $this will contain a reference to the checkbox   
        if ($this.is(':checked')) {
            $("input[controltypefor='image']").show();
            $.cookie('admin_chk_image', true, { expires: 1, path: '/' });
        } else {
            $("input[controltypefor='image']").hide();
            $.cookie('admin_chk_image', false, { expires: 1, path: '/' });
            // the checkbox was unchecked
        }
    });
    $('.admin_chk_adzone').click(function () {
        var $this = $(this);
        // $this will contain a reference to the checkbox   
        if ($this.is(':checked')) {
            $("input[controltypefor='adzone']").show();
            $.cookie('admin_chk_adzone', true, { expires: 1, path: '/' });
        } else {
            $("input[controltypefor='adzone']").hide();
            $.cookie('admin_chk_adzone', false, { expires: 1, path: '/' });
            // the checkbox was unchecked
        }
    });
    $(document).ready(function () {
        var admin_chk_html = $.cookie("admin_chk_html");
        var admin_chk_ascx = $.cookie("admin_chk_ascx");
        var admin_chk_image = $.cookie("admin_chk_image");
        var admin_chk_adzone = $.cookie("admin_chk_adzone");

        

        controlEditbuttons('admin_chk_html', 'html', admin_chk_html);
        controlEditbuttons('admin_chk_ascx', 'ascx', admin_chk_ascx);
        controlEditbuttons('admin_chk_image', 'image', admin_chk_image);
        controlEditbuttons('admin_chk_adzone', 'adzone', admin_chk_adzone);

    });

        function controlEditbuttons(name, type, checked) {

        if (checked == "true") {
            $("." + name+"").prop('checked', true);
            $("input[controltypefor='" + type + "']").show();
        }
        else {
            $("." + name + "").prop('checked', false);
            $("input[controltypefor='"+type+"']").hide();
        }
    }
</script>
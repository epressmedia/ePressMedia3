//#region qf-configurator-section
function slideConfig(uniqudId, dir) {
    if (!uniqudId) { return; }

    var opposite = (dir == 'Down' ? 'Up' : 'Down'),
        $cfgHeadContainer = $("#" + uniqudId).find("a.cfgHead"),
        $cfgContent = $("#" + uniqudId).find("div.cfgContent"),
        $cfgHiddenFieldContainer = $("#" + uniqudId).find("div.hiddenFieldContainer");

    $cfgContent.slideToggle(300, function () {
        $cfgHeadContainer.find(".cfgButton")
		    .removeClass("cfgUp")
		    .removeClass("cfgDown")
		    .addClass("cfg" + opposite);
    });

    $cfgHeadContainer[0].href = "javascript:slideConfig('" + uniqudId + "','" + opposite + "');";
    $("input[type='hidden']", $cfgHiddenFieldContainer).val(dir == "Down" ? "true" : "false");
}

//#endregion


function validateValue(source, args) {

    var targetID = $("#" + source.id+ "").attr("vid") + "_Container";
    if (args.Value == "") {
        args.IsValid = false;
        $("."+targetID+"").css("border", "1px solid red");
    }
    else {
        args.IsValid = true;
        //$("." + targetID + "").css("border", "");
    }
}  


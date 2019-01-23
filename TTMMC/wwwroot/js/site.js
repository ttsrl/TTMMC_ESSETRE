Array.prototype.contains = function (obj) {
    var i = this.length;
    while (i--) {
        if (this[i] === obj) {
            return true;
        }
    }
    return false;
};

Array.prototype.remove = function (obj) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] === obj) {
            this.splice(i, 1);
        }
    }
};

function openMenu(it) {
    $(it).find(".pnl").css("display", "inline-flex");
    $(it).find(".nmtlt").addClass("hover");
}

function closeMenu(it) {
    $(it).find(".pnl").css("display", "none");
    $(it).find(".nmtlt").removeClass("hover");
}

function loadingDialogShow() {
    $("#loadingDialogBkg").css("display", "inline");
    $("body").css("overflow", "hidden");
}

function loadingDialogHide() {
    $("#loadingDialogBkg").css("display", "none");
    $("body").css("overflow", "auto");
}

function alertDialog(title, msg) {
    var def = $.Deferred();
    var dialog = $("<div id='alertDialog'><div class='alertMsgDialog'><img class='alertIconDialog' src='/images/alertIcon.png' /><span>" + msg + "</span></div></div>");
    $(dialog).dialog({
        title: title,
        resizable: false,
        draggable: false,
        height: "auto",
        width: 360,
        modal: true,
        create: function (event) {
            $(event.target).parent().css({ 'position': "fixed" });
        },
        buttons: {
            "OK": function () {
                $(this).dialog("close");
                def.resolve();
            },
            "Annulla": function () {
                $(this).dialog("close");
                def.reject();
            }
        }
    });
    return def.promise();
}

function infoDialog(title, msg) {
    var dialog = $("<div id='infoDialog'><div class='infoMsgDialog'><img class='infoIconDialog' src='/images/infoIcon.png' /><span>" + msg + "</span></div></div>");
    $(dialog).dialog({
        title: title,
        resizable: false,
        draggable: false,
        height: "auto",
        width: 360,
        modal: true,
        create: function (event) {
            $(event.target).parent().css({ 'position': "fixed" });
        },
        buttons: {
            "OK": function () {
                $(this).dialog("close");
            }
        }
    });
}

function errorDialog(title, msg) {
    var dialog = $("<div id='errorDialog'><div class='errorMsgDialog'><img class='errorIconDialog' src='/images/errorIcon.png' /><span>" + msg + "</span></div></div>");
    $(dialog).dialog({
        title: title,
        resizable: false,
        draggable: false,
        height: "auto",
        width: 360,
        modal: true,
        create: function (event) {
            $(event.target).parent().css({ 'position': "fixed" });
        },
        buttons: {
            "OK": function () {
                $(this).dialog("close");
            }
        }
    });
}

function componentToHex(c) {
    var hex = c.toString(16);
    return hex.length === 1 ? "0" + hex : hex;
}

function rgbToHex(r, g, b) {
    return "#" + componentToHex(r) + componentToHex(g) + componentToHex(b);
}

function hexToRgb(hex) {
    var result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
    return result ? {
        r: parseInt(result[1], 16),
        g: parseInt(result[2], 16),
        b: parseInt(result[3], 16)
    } : null;
}

$(document).ready(function () {

    $("script.inject-json").each(function (i, e) {
        var name = $(e).attr("data-name") || "injectedJson";
        var json = $(e).html();
        if (name !== "injectedJson") {
            window[name] = JSON.parse(json);
        } else {
            if (!window[name]) {
                window[name] = new Array();
            }
            window[name].push(JSON.parse(json));
        }
    });

    $(".confirmDialog").click(function (e) {
        e.preventDefault();
        var url = $(this).attr("href");
        var title = $(this).attr("data-title") || "Conferma Azione";
        var msg = $(this).attr("data-msg");
        var target = $(this).attr("target");
        var dialog = $("<div id='dialog-form'>" + msg + "</div>");
        $(dialog).dialog({
            title: title,
            resizable: false,
            draggable: false,
            height: "auto",
            width: 400,
            modal: true,
            create: function (event) {
                $(event.target).parent().css({ 'position': "fixed" });
            },
            buttons: {
                "Conferma": function () {
                    $(this).dialog("close");
                    if (target === "_blank") {
                        var win = window.open(url, '_blank');
                        win.focus();
                    } else {
                        location.href = url;
                    }
                },
                "Annulla": function () {
                    $(this).dialog("close");
                }
            }
        });
    });

    $(".colorWindow").each(function () {
        $(this).minicolors({
            control: $(this).attr("data-control") || "hue",
            defaultValue: $(this).attr("data-defaultvalue") || "",
            format: $(this).attr("data-format") || "hex",
            keywords: $(this).attr("data-keywords") || "",
            inline: $(this).attr("data-inline") === "true",
            letterCase: $(this).attr("data-lettercase") || "lowercase",
            opacity: $(this).attr("data-opacity"),
            position: $(this).attr("data-position") || "bottom left",
            swatches: $(this).attr("data-swatches") ? $(this).attr("data-swatches").split("|") : [],
            change: function (value, opacity) {

                var val1 = $(this).attr("data-rewritergb") || "true";
                var val2 = $(this).attr("data-rewritehex") || "true";
                var rewritergb = val1.toLowerCase() === "true" ? true : false;
                var rewritehex = val2.toLowerCase() === "true" ? true : false;

                if (rewritergb) {
                    $(".colorRGB").val(hexToRgb(value).r + "," + hexToRgb(value).g + "," + hexToRgb(value).b);
                }
                if (rewritehex) {
                    $(".colorHEX").val(value);
                }
            }
        });
    });

});
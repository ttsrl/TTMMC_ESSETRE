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

});
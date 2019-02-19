$(document).ready(function () {

    jQuery.validator.addMethod("notEqual", function (value, element, param) {
            return this.optional(element) || (value !== param && value !== $(param).val());
    }, "Please specify a different (non-default) value");

    jQuery.validator.addMethod("passwordCheck", function (value, element, param) {
        if (this.optional(element)) {
            return true;
        } else if (!/[A-Z]/.test(value)) {
            return false;
        } else if (!/[a-z]/.test(value)) {
            return false;
        } else if (!/[0-9]/.test(value)) {
            return false;
        }
        return true;
    }, "");

    jQuery.validator.addMethod("populated", function (elmSelected, element, param) {
        if (param === true) {
            if (element.childElementCount > 0) {
                return true;
            }
        }
        return false;
    }, "");

    jQuery.validator.addMethod('filesize', function (value, element, param) {
        return this.optional(element) || element.files[0].size <= param;
    }, "");

    jQuery.validator.addMethod("extension", function (value, element, param) {
        param = typeof param === "string" ? param.replace(/,/g, '|') : "png|jpe?g|gif";
        return this.optional(element) || value.match(new RegExp(".(" + param + ")$", "i"));
    }, "");

});

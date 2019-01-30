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

    jQuery.validator.addMethod("radioYesRequired", function (value, element, param) {
        var v = $("input[name='" + param + "']:checked").val() || "1";
        if (v === "0") {
            return true;
        } else {
            if (element.value !== "") {
                return true;
            }
        }
        return false;
    }, "");

    jQuery.validator.addMethod("selectRequired", function (value, element, param) {
        var v = $("input[name='" + param + "']").val() || "";
        if (v === "0") {
            return true;
        } else {
            if (element.value !== "0") {
                return true;
            }
        }
        return false;
    }, "");

});

var validateNewMouldRules = {
    'code': { required: true },
    'client': { required: true },
    'mixture': { required: true },
    'description': { required: true },
    'location': { required: true },
    'master': { required: true },
    'image': { extension: "jpg|jpeg|png", filesize: 2097152 }
};
var validateNewMouldMessages = {
    'code': { required: "Inserisci un codice" },
    'client': { required: "Inserisci un cliente di default" },
    'mixture': { required: "Inserisci una miscela di default" },
    'description': { required: "Inserisci una descrizione" },
    'location': { required: "Inserisci la posizione dello stampo" },
    'master': { required: "Definisci un master" },
    'image': { extension:"Estensione file non supportata", filesize: "Dimensione file superiore a 2MB" }
};

var validateNewClientRules = {
    'name': { required: true },
    'vat': { required: true, digits: true },
    'fiscalCode': { required: true },
    'state': { required: true },
    'province': { required: true },
    'town': { required: true },
    'address': { required: true },
    'addressNumber': { required: true },
    'phone': { digits: true },
    'email': { email: true },
    'pec': { email: true }
};

var validateNewClientMessages = {
    'name': { required: "Inserisci un nome" },
    'vat': { required: "Inserisci una partita i.v.a", digits: "Partita i.v.a non valida" },
    'fiscalCode': { required: "Inserisci un codice fiscale" },
    'state': { required: "Inserisci uno stato" },
    'province': { required: "Inserisci una provincia" },
    'town': { required: "Inserisci un comune" },
    'address': { required: "Inserisci un'indirizzo" },
    'addressNumber': { required: "Inserisci un numero civico" },
    'phone': { digits: "Numero di telefono non valido" },
    'email': { email: "Email non valida" },
    'pec': { email: "Pec non valida" }
};

var validateEditClientRules = {
    'Name': { required: true },
    'vat': { required: true, digits: true },
    'FiscalCode': { required: true },
    'State': { required: true },
    'Province': { required: true },
    'Town': { required: true },
    'addressStreetMode': { required: true },
    'addressStreet': { required: true },
    'addressNumber': { required: true },
    'Address': { required: true },
    'Phone': { digits: true },
    'Email': { email: true },
    'pec': { email: true }
};

var validateEditClientMessages = {
    'Name': { required: "Inserisci un nome" },
    'vat': { required: "Inserisci una partita i.v.a", digits: "Partita i.v.a non valida" },
    'FiscalCode': { required: "Inserisci un codice fiscale" },
    'State': { required: "Inserisci uno stato" },
    'Province': { required: "Inserisci una provincia" },
    'Town': { required: "Inserisci un comune" },
    'addressStreetMode': { required: "" },
    'addressStreet': { required: "Inserisci un'indirizzo" },
    'addressNumber': { required: "Inserisci un numero civico" },
    'Address': { required: "" },
    'Phone': { digits: "Numero di telefono non valido" },
    'Email': { email: "Email non valida" },
    'pec': { email: "Pec non valida" }
};

var validateMaterialRules = {
    'name': { required: true }
};

var validateMaterialMessages = {
    'name': { required: "" }
};

var validateNewMixtureRules = {
    'name': { required: true, maxlength: 22 },
    'components': { populated: true }
};

var validateNewMasterRules = {
    'code': { required: true },
    'name': { required: true },
    'hexColor': { required: true }
};

var validateNewLayoutRules = {
    'client': { required: true },
    'mould': { required: true },
    'master': { required: true },
    'mixture': { required: true },
    'machine': { required: true },
    'quantity': { required: true, min: 1 },
    'minced': { radioYesRequired: "mincedCheck" },
    'packagingCount': { selectRequired: "packaging" },
    'start': { required: true }
};

var validateNewLayoutMessages = {
    'client': { required: "Selezionare un cliente" },
    'mould': { required: "Selezionare uno stampo" },
    'master': { required: "Selezionare un master" },
    'mixture': { required: "Selezionare una miscela" },
    'machine': { required: "Selezionare una macchina" },
    'quantity': { required: "Indicare la quantità", min: "Quantità minima da inserire 1" },
    'minced': { radioYesRequired: "Inserire un valoce per il macinato" },
    'packagingCount': { selectRequired: "Inserire una quantità di confezioni" },
    'start': { required: "Definire una data di inizio" }
};
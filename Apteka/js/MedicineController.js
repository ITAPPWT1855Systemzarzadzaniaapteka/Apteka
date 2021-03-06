﻿var sampleMedicinesData = [
    {
        id: 1,
        name: "Aspiryna",
        netto: 12.50
    },
    {
        id: 2,
        name: "Polopiryna",
        netto: 13.50
    },
    {
        id: 3,
        name: "ASdasdfd",
        netto: 11.50
    }
];
//function parseMedicines(arr) {
//    console.log('arr', medicines);
//    return _.map(medicines, function (med) {
//        return {
//            value: med.Nazwa,
//            data: med.Id_lek
//        };
//    })
//}

Date.prototype.toDateInputValue = (function () {
    var local = new Date(this);
    local.setMinutes(this.getMinutes() - this.getTimezoneOffset());
    return local.toJSON().slice(0, 10);
});

function parseWarehouses(arr) {
    return _.map(arr, function (warehouse) {
        return {
            value: warehouse.Nazwa.toLowerCase() + " " + warehouse.NIP,
            data: warehouse.Id_hurtownia
        };
    })
};

function parseMedicines(arr) {
    return _.map(arr, function (med) {
        return {
            value: med.Nazwa.toLowerCase() + "(" + med.Postac + " " + med.Opakowanie + ")",
            data: med.Id_lek
        };
    })
}


function addRow() {
    if ($("#product-table tr").length > 1 && !$("#product-table tr:last").find(".med-name").attr("disabled"))
        throw new Error("Przed dodaniem kolejnej pozycji zatwierdź nazwę istniejącej.")
    $("#product-table tr:last").find(".delete-row-button").hide();
    var rowId = $(".product-row").length;
    var namePrefix = "Products[" + rowId + "].";
    var newRow = $("<tr />", { "class": "product-row", "id": "product-row-" + rowId }).append([
            $("<input />", { "type": "hidden", "class": "med-id", "name": namePrefix + "Id", "readonly": "true" }),
            $("<td />").append($("<input />", { "type": "number", "class": "form-control lp", "value": rowId + 1, "readonly": "true", "step": "1", "tabindex": "-1" })),
            $("<td />").append($("<input />", { "type": "text", "name": namePrefix + "Name", "class": "form-control med-name" })),
            $("<td />").append($("<input />", { "type": "number", "name": namePrefix + "Quantity", "class": "form-control quantity", "step": "1" })),
            $("<td />").append($("<input />", { "type": "number", "name": namePrefix + "Price", "class": "form-control price", "step": "0.01" })),
            $("<td />").append($("<input />", { "type": "number", "name": namePrefix + "Netto", "class": "form-control netto", "readonly": "true", "step": "0.01", "tabindex": "-1" })),
            $("<td />").append($("<input />", { "type": "text", "name": namePrefix + "Vat", "class": "form-control vat", value: "23%" })),
            $("<td />").append($("<input />", { "type": "number", "name": namePrefix + "Brutto", "class": "form-control brutto", "readonly": "true", "step": "0.01", "tabindex": "-1" })),
            $("<td />").append($("<div />", { "class": "btn btn-danger delete-row-button" }).append($("<i />", { "class": "fa fa-remove" })))
    ]);
    $(newRow).find(".med-name").autocomplete({
        lookup: parseMedicines(sampleMedicinesData),
        minChars: 2,
        delay: 150,
        lookupLimit: 15,
        onSelect: function (selected) {
            var med = _.find(sampleMedicinesData, function (a) { return a.Id_lek === selected.data; });
            if (!med.Id_lek) throw new Error("Leku nie znaleziono.");
            var row = $(this).parents("tr");
            row.find(".price").val((med.netto || 0).toFixed(2));
            row.find(".quantity").val(1).trigger('input');
            row.find(".med-id").val(med.Id_lek);
            row.find(".med-name").attr("disabled", true);
            row.find(".med-name").autocomplete("dispose");
        }
    });
    $(newRow).find(".quantity, .price, .vat").on("input", function () {
        var row = $(this).parents("tr");
        var priceVal = row.find(".price").val();
        var quantVal = row.find(".quantity").val();
        var nettoVal = priceVal * quantVal;
        var bruttoVal = nettoVal * (100 + parseFloat(row.find(".vat").val())) / 100;
        row.find(".netto").val(nettoVal.toFixed(2));
        row.find(".brutto").val(bruttoVal.toFixed(2));

        var sumNetto = 0;
        var sumBrutto = 0;
        $(".product-row").each(function () {
            var row = $(this);
            sumNetto += +row.find(".netto").val();
            sumBrutto += +row.find(".brutto").val();
        });
        $("#netto").val(sumNetto.toFixed(2));
        $("#brutto").val(sumBrutto.toFixed(2));
    });
    $(newRow).find(".delete-row-button").on("click", function () {
        newRow.remove();
        $("#product-table tr:last").find(".delete-row-button").show();
    });
    $("#product-table tr:last").after(newRow);
    if ($("#product-table tr").length > 2) {
        console.log($("#product-table tr").length);
        $(newRow).find(".med-name").focus();
    }
};

$(document).ready(function () {
    $.get("/Invoice/GetMedicines", function (data) {
        sampleMedicinesData = data;
        addRow();
    });
    $(".datepicker").val(new Date().toDateInputValue());
    $.get("/Invoice/GetWarehouses", function (data) {
        sampleWarehousesData = data;
        $("#warehouse").autocomplete({
            lookup: parseWarehouses(sampleWarehousesData),
            onSelect: function (suggestion) {
                $("#warehouse-id").val(suggestion.data);
            }
        })
    });

    $("#add-row-button").click(addRow);
    $("#form").submit(function (e) {
        e.preventDefault();
        $("#form").find(".med-name").each(function () {
            console.log($(this).attr("disabled"))
            if (!$(this).attr("disabled"))
                throw new Error("Wypełnij wszystkie pola");
        });
        if (!$("#warehouse-id").val())
            throw new Error("Nie wybrano hurtowni");
        if (!$("#invoiceId").val())
            throw new Error("Nie wpisano numeru faktury");
        if ($("#form").find("tr").length < 2)
            throw new Error("Nie wybrano żadnych produktów");
        return this.submit();
    });
});

window.onerror = function (message) {
    $.toaster({ priority: 'danger', title: 'Błąd', message: message.replace("Uncaught Error: ", "") });
};


//var Apteka = angular.module('Apteka', []);
//Apteka.service('medicineService', function ($http) {
//    delete $http.defaults.headers.common['X-Requested-With'];

//    this.getAllWarehouses = function () {
//        $http({
//            method: 'GET',
//            url: '/Medicine/GetMedicines',
//        }).success(function (data) {
//            console.log(data);
//            callbackFunc(data);
//        }).error(function () {
//            alert("error");
//        });
//    }
//});

//Apteka.controller('medicine', ["$scope", "$http", function ($scope, $http) {
//    $('#filter').on('keyup', function () {
//        var text = $(this).val();
//        search(text);
//    });

//    function parseMedicines(arr) {
//        return _.map(medicines, function (med) {
//            return {
//                value: med.Nazwa,
//                data: med.Id_lek
//            };
//        })
//    }
//    $('#medicines').on('keyup', function () {
        
//        var searchtext = $(this).val();
//        $scope.error = "";
//        if (searchtext.length > 3) {
//            console.log('text is', searchtext);
//            var promise = $http.get("/Medicine/GetMedicines", { params: { text: searchtext } });
//            var onSuccess = function (response) {
//                $scope.medicines = response.data;
//                var txt = '';
//                if (response.data.length === 0) {
//                    $('#med').html("");
//                    txt = 'Nie znaleziono leku o tej nazwie';
//                }
//                for (var i = 0; i < response.data.length; i++) {
//                    txt += '<button type="button" onclick="setMed(' + response.data[i].Id_lek + ', \''+ response.data[i].Nazwa +'\')">';
//                    txt += response.data[i].Nazwa;
//                    txt += ',';
//                    txt += response.data[i].Postac;
//                    txt += ',';
//                    txt += response.data[i].Opakowanie;
//                    txt += ',';
//                    txt += response.data[i].Dawka;
//                    txt += '</button>';
//                }
//                $("#med").append('<ul>' + txt + '</ul>');
//            };
//            var onError = function (response) {
//                console.log(response);
//                $scope.error = "Problem: " + response.statusText;
//            }
//            promise.then(onSuccess, onError);
  
//        }
//    });

//    function search(search) {
//        $("#listagem tr:not(:contains('" + search + "'))").css("display", "none");
//        $("#listagem tr:contains('" + search + "')").css("display", "");
//    }

//}]);

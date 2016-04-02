﻿/* Sample Api Data */
var sampleMedicinesData = [
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
var sampleWarehousesData = [
    {
        id: 1,
        name: "Dudus",
        address: "Słonczena 13\n52-321 Wrocław",
        NIP: "000-000-00-00"
    },
    {
        id: 1,
        name: "Desert Eagle",
        address: "Wrocławska 14\n53-112 Wrocław",
        NIP: "854-456-55-11"
    }
];
var sampleSellerData = [
    {
        name: "Apteka",
        address1: "Apteczna 1",
        address2: "53-001 Wrocław",
        NIP: "123-456-78-90"
    }
]
/* End Sample Data */


function parseWarehouses(arr) {
    return _.map(arr, function (warehouse) {
        return {
            value: warehouse.name + " " + warehouse.NIP,
            data: warehouse.id
        };
    })
};

function parseMedicines(arr) {
    return _.map(arr, function (med) {
        return {
            value: med.name,
            data: med.id
        };
    })
}

$(function () {
    addRow();
    $("#warehouse-id").autocomplete({
        lookup: parseWarehouses(sampleWarehousesData)
    })
    $("#add-row-button").on("click", addRow);
})

function addRow() {
    var rowId = $(".product-row").length + 1;
    var newRow = $("<tr />", { "class": "product-row", "id": "product-row-" + rowId }).append([
            $("<td />").append($("<input />", { "type": "text", "class": "form-control lp", "value": rowId, "disabled": "true" })),
            $("<td />").append($("<input />", { "type": "text", "class": "form-control med-name", "ng-model": "facture.name" })),
            $("<td />").append($("<input />", { "type": "text", "class": "form-control quantity" })),
            $("<td />").append($("<input />", { "type": "text", "class": "form-control unit", "disabled": "true" })),
            $("<td />").append($("<input />", { "type": "text", "class": "form-control price", "disabled": "true" })),
            $("<td />").append($("<input />", { "type": "text", "class": "form-control netto", "disabled": "true" })),
            $("<td />").append($("<input />", { "type": "text", "class": "form-control vat", value: "23%", "disabled": "true" })),
            $("<td />").append($("<input />", { "type": "text", "class": "form-control brutto", "disabled": "true" }))
        ]);
    $(newRow).find(".med-name").autocomplete({
        lookup: parseMedicines(sampleMedicinesData),
        onSelect: function (selected) {
            var med = _.find(sampleMedicinesData, function (a) { return a.id === selected.data; });
            var row = $(this).parents("tr");
            row.find(".price").val(med.netto.toFixed(2));
            row.find(".quantity").val(1).trigger('input');
        }
    });
    $(newRow).find(".quantity").on("input", function () {
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
    $("#product-table tr:last").after(newRow);
};



var Apteka = angular.module('Apteka', []);
Apteka.controller('faktura', ["$scope", "$http", function ($scope, $http) {
    $scope.master = {};
    $scope.med = {};
    $scope.error = '';
    $scope.facture = { 'meds': []};
    $scope.create = function (facture) {
        $('#product-table tr').each(function () {
            var row = $(this);
            med = {};
            console.log('nowy row ', row);
            med['name'] = row.find('.med-name').val();
            med['quantity'] = row.find('.quantity').val();
            med['unit'] = row.find('.unit').val();
            med['price'] = row.find('.price').val();
            med['netto'] = row.find('.netto').val();
            med['brutto'] = row.find('.brutto').val();
            $scope.facture['meds'].push(med);
            console.log($scope.facture);
        });
        var promise = $http.post("/Faktura/Post");
        var onSuccess = function (response) {
            console.log(response);
        };
        var onError = function (reponse) {
            $scope.error = "Problem: " + reponse;
        }
        promise.then(onSuccess, onError);
    };

}]);

   
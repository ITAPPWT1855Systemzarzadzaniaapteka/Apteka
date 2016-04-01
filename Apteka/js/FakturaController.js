/* Sample Api Data */
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
var Apteka = angular.module('Apteka', []);

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
            $("<td />").append($("<input />", { "type": "text", "class": "form-control med-name" })),
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



/*Apteka.service('fakturaService', function ($http) {
    delete $http.defaults.headers.common['X-Requested-With'];

    this.getAllWarehouses = function () {
        $http({
            method: 'GET',
            url: '/Faktura/GetWarehouses',
        }).success(function (data) {
            callbackFunc(data);
        }).error(function () {
            alert("error");
        });
    }
});

Apteka.controller('faktura', function ($scope, fakturaService) {
    $scope.faktura = {};
    $scope.medicineCount = 0;
    $scope.medicines = [];
    $scope.showTipForMedicineName = false;
    $scope.netto = 100,00;
    $scope.medicineName = "";
  //  $scope.netto-unit = 0;
    
    $scope.displayMedicines = function () {
        if ($scope.medicineName.length >= 3) {
            $scope.showTipForMedicineName = true;
            $scope.medicines = ['one', 'two'];

        }
    }
    $scope.setMedicine = function (medicine) {
        $scope.medicineName = medicine;
    }
    $scope.auto = function() {
        $('.amount').on('keyup', function () {
            
            var amount = parseInt($(this).val());
            var netto = parseFloat($(this).prev('input').val());
            console.log('AMOUNT', amount, netto);
            if (!isNaN(netto) && !isNaN(amount)) {
                $scope.netto = $scope.netto + netto * amount;
                console.log('touched', $(this).val(), $scope.netto);
            }
            
        });
        $('.netto').on('keyup', function () {
            
            var netto = parseFloat($(this).val());
            var amount = parseInt($(this).closest('input').val());
            console.log('netto', netto, amount);
            if (!isNaN(netto) && !isNaN(amount)) {
                $scope.netto = $scope.netto + netto * amount;
                console.log('touched', $(this).val(), $scope.netto);
            }
            
        });
    }
    $scope.auto();
    $scope.getWarehouses = function () {
        $scope.GetWarehouses = fakturaService.getAllWarehouses(function (response) {
            var evidenceDate = response;
            console.log(response);
        });
    };

    $scope.addMedicine = function () {
        var medicinesHtml = $('.medicines').html();
        $scope.medicineCount++;
        $(medicinesHtml).find('input[id=medicine-name]').attr('id', 'medicine-' + $scope.medicineCount);
        $('.new-medicines').append(medicinesHtml);
        $scope.auto();
    }
   // $scope.getWarehouses();

});*/
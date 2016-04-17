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


function parseWarehouses(arr) {
    return _.map(arr, function (warehouse) {
        return {
            value: warehouse.name + " " + warehouse.NIP,
            data: warehouse.id
        };
    })
};

function getMedicines() {
    var medicines = [];
    $.get({
        url: "/Medicine/GetMedicines",
        data: { text: 'aspi'  } ,
        dataType: "json"
    }).done(function (data) {
        console.log("Data Loaded: ", data);
        medicines = data;
    });;
    return medicines;
}
function parseMedicines(arr) {
 //   arr = getMedicines();
    return _.map(arr, function (med) {
        return {
            value: med.Nazwa,
            data: med.Id_lek
        };
    })
}

$(function () {
    //sampleMedicinesData = searchMedicines();
    addRow();
//    $(".datepicker").val(new Date().toDateInputValue());
   
    $("#warehouse").autocomplete({
        lookup: parseWarehouses(sampleWarehousesData),
        onSelect: function (suggestion) {
            $("#warehouse-id").val(suggestion.data);
        }
    })
    $("#add-row-button").on("click", addRow);
$("#datepicker").datepicker({ });
})
function searchMedicines() {
    var medicines = [];
    console.log('search');
    $.get({ url: "/Medicine/GetMedicines", data: { text: 'alne' } }).done(function (response) {
        medicines = response;
        //var txt = '';
        console.log('resp', response);
        //if (response.length === 0) {
        //    row.find('.med-name').html("");
     
        //}
        //for (var i = 0; i < response.length; i++) {
        //    txt += '<button type="button" onclick="setMed(' + response[i].Id_lek + ', \'' + response[i].Nazwa + '\')">';
        //    txt += response[i].Nazwa;
        //    txt += ',';
        //    txt += response[i].Postac;
        //    txt += ',';
        //    txt += response[i].Opakowanie;
        //    txt += ',';
        //    txt += response[i].Dawka;
        //    txt += '</button>';
        //}
        //console.log('row find', row.find('.med-name'));
        //row.find('.med-name').append('<ul>' + txt + '</ul>');
    });
    return medicines;
}
function setMed(rowid, id, name) {
    console.log('ser med');
    $('.product-row').find('#med-' + rowid).val(id);
    console.log('found', $('.product-row').find('#med-' + id).val());
    $('#medname-' + rowid).val(name);
    $('.product-row ul').html("");
    $('.alert-danger').html("");

}
function addRow() {
    var rowId = $(".product-row").length;
    //  var namePrefix = "Products["+rowId+"].";
    var namePrefix = "model.Operacja[" + rowId + "].";
    var newRow = $("<tr />", { "class": "product-row", "id": "product-row-" + rowId }).append([
        $("<td />").append($("<input />", { "type": "number", "class": "form-control lp", "value": rowId, "readonly": "true"})),
        //$("<td />").append($("<input />", { "type": "number", "class": "form-control lp", "value": rowId, "id":"med-"+rowId, "readonly": "true", "step": "1", "name": "model.Operacja[" + rowId + "].Lek.Id_Lek" })),
            $("<td />").append($("<input />", { "type": "text", "name": namePrefix+"Id_lek", "class": "form-control med-name", "id":"medname-"+rowId })),
            $("<td />").append($("<input />", { "type": "number", "name": namePrefix + "Przychod", "class": "form-control quantity", "step": "1" })),
      //      $("<td />").append($("<input />", { "type": "text", "name": namePrefix + "Unit", "class": "form-control unit"})),
            $("<td />").append($("<input />", { "type": "number", "name": namePrefix + "Price", "class": "form-control price", "step": "0.01" })),
            $("<td />").append($("<input />", { "type": "number", "name": namePrefix + "Netto", "class": "form-control netto", "readonly": "true", "step": "0.01" })),
            $("<td />").append($("<input />", { "type": "text", "name": namePrefix + "Vat", "class": "form-control vat", value: "23%"})),
            $("<td />").append($("<input />", { "type": "number", "name": namePrefix + "Brutto", "class": "form-control brutto", "readonly": "true", "step": "0.01" }))
    ]);
    var lala = document.getElementById('med-' + rowId);
    $(newRow).find(".med-name").autocomplete({
    lookup: parseMedicines(sampleMedicinesData),
        onSelect: function (selected) {
            var med = _.find(sampleMedicinesData, function (a) { return a.id === selected.data; });
            var row = $(this).parents("tr");
            row.find(".price").val(med.netto);
            row.find(".quantity").val(1).trigger('input');
        }
    });
    $(newRow).find(".price, .vat").on("input", function () {
        var row = $(this).parents("tr");
        var priceVal = row.find(".price").val();
        var quantVal = row.find(".quantity").val();
        var nettoVal = priceVal * quantVal;
        var bruttoVal = nettoVal * (100 + parseFloat(row.find(".vat").val())) / 100;
        row.find(".netto").val(nettoVal);
        row.find(".brutto").val(bruttoVal);

        var sumNetto = 0;
        var sumBrutto = 0;
        $(".product-row").each(function () {
            var row = $(this);
            sumNetto += row.find(".netto").val().replace(',','.');
            sumBrutto += row.find(".brutto").val();
        });
        $("#netto").val(sumNetto.replace(',', '.'));
        $("#brutto").val(sumBrutto);
    });
    $("#product-table tr:last").after(newRow);

    $(newRow).find('.med-name').on('keyup', function () {
        var row = $(this).parents("tr");
         var searchtext = $(newRow).find('.med-name').val();
      //  if (searchtext.length > 3) {
         var txt = '';
         $(newRow+' ul').html("");
         $.get({ url: "/Medicine/GetMedicines", data: { text: searchtext } }).done(function (response) {
             if (response.length === 0) {
                 $(newRow+ 'ul').html("");
                 txt = 'Nie znaleziono leku o tej nazwie';
             }
             for (var i = 0; i < response.length; i++) {
                 txt += '<button type="button" onclick="setMed('+ rowId + ', ' + response[i].Id_lek + ', \'' + response[i].Nazwa + '\')">';
                 txt += response[i].Nazwa;
                 txt += ',';
                 txt += response[i].Postac;
                 txt += ',';
                 txt += response[i].Opakowanie;
                 txt += ',';
                 txt += response[i].Dawka;
                 txt += 'Podaj id:';
                 txt += response[i].Id_lek;
                 txt += '</button>';
             }
             console.log('rowid', rowId);
             $(newRow).append('<ul>' + txt + '</ul>');
         });
            //$('#medicine-id').val(id);
            //$('#medicines').val(name);
            //$('.med-name ul').html("");
            //$('.alert-danger').html("");
     //   }
        })
};




var Apteka = angular.module('Apteka', []);
Apteka.controller('invoice', ["$scope", "$http", function ($scope, $http) {



    $('.med-name').on('keyup', function () {
        var row = $(this).parents("tr");
        var searchtext = $(this).val();
        $scope.error = "";
        if (searchtext.length > 3) {
            var promise = $http.get("/Medicine/GetMedicines", { params: { text: searchtext } });
            var onSuccess = function (response) {
                $scope.medicines = response.data;
                var txt = '';
                if (response.data.length === 0) {
                    row.find('.med-name').html("");
                    txt = 'Nie znaleziono leku o tej nazwie';
                }
                for (var i = 0; i < response.data.length; i++) {
                    txt += '<button type="button" onclick="setMed(' + response.data[i].Id_lek + ', \''+ response.data[i].Nazwa +'\')">';
                    txt += response.data[i].Nazwa;
                    txt += ',';
                    txt += response.data[i].Postac;
                    txt += ',';
                    txt += response.data[i].Opakowanie;
                    txt += ',';
                    txt += response.data[i].Dawka;
                    txt += '</button>';
                }
                row.find('.med-name').append('<ul>' + txt + '</ul>');
            };
            var onError = function (response) {
                console.log(response);
                $scope.error = "Problem: " + response.statusText;
            }
            promise.then(onSuccess, onError);
  
        }
    })

    $scope.error = '';
    
    $scope.master = {};
    $scope.med = {};
    
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
         var mis= $scope.facture.Netto;
        $scope.facture.Netto = 12.0;
        $scope.facture.Netto = 15.5;
   //     $scope.facture['Operacja'] = 
      //  $scope.facture['Netto'] = parseFloat(String(mis).replace(',', '.'));
      //  $scope.facture['Brutto']= $scope.facture['Brutto'].replace(',', '.');
        console.log($scope.facture)
        console.log(JSON.stringify($scope.facture));
        var promise = $http.post("/Invoice/Create",  { params: { text: JSON.stringify($scope.facture) }} );
        var onSuccess = function (response) {
            console.log(response);
        };
        var onError = function (reponse) {
            $scope.error = "Problem: " + reponse;
        }
        promise.then(onSuccess, onError);
    };
    
}]);

   
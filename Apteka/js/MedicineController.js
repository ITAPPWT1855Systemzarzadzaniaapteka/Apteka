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
//function parseMedicines(arr) {
//    console.log('arr', medicines);
//    return _.map(medicines, function (med) {
//        return {
//            value: med.Nazwa,
//            data: med.Id_lek
//        };
//    })
//}
$(function () {

    $("#listagem").tablesorter();
    
})
function setMed(id, name) {
    $('#medicine-id').val(id);
    $('#medicines').val(name);
    $('#med').html("");
}
var Apteka = angular.module('Apteka', []);
Apteka.service('medicineService', function ($http) {
    delete $http.defaults.headers.common['X-Requested-With'];

    this.getAllWarehouses = function () {
        $http({
            method: 'GET',
            url: '/Medicine/GetMedicines',
        }).success(function (data) {
            console.log(data);
            callbackFunc(data);
        }).error(function () {
            alert("error");
        });
    }
});

Apteka.controller('medicine', ["$scope", "$http", function ($scope, $http) {
    $('#filter').on('keyup', function () {
        var text = $(this).val();
        search(text);
    });

    function parseMedicines(arr) {
        return _.map(medicines, function (med) {
            return {
                value: med.Nazwa,
                data: med.Id_lek
            };
        })
    }
    $('#medicines').on('keyup', function () {
        var searchtext = $(this).val();
        if (searchtext.length > 0) {
            var promise = $http.get("/Medicine/GetMedicines", { params: { text: searchtext } });
            var onSuccess = function (response) {
                $scope.medicines = response.data;
                var txt = '';
                for (var i = 0; i < response.data.length; i++) {
                    txt += '<button type="button" onclick="setMed(' + response.data[i].Id_lek + ', \''+ response.data[i].Nazwa +'\')">';
                    txt += response.data[i].Nazwa;
                    txt += ',';
                    txt += response.data[i].Postac;
                    txt += ',';
                    txt += response.data[i].Opakowanie;
                    txt += '</button>';
                }
                $("#med").append('<ul>' + txt + '</ul>');
            };
            var onError = function (reponse) {
                $scope.error = "Problem: " + reponse;
            }
            promise.then(onSuccess, onError);
  
        }
    });

    function search(search) {
        $("#listagem tr:not(:contains('" + search + "'))").css("display", "none");
        $("#listagem tr:contains('" + search + "')").css("display", "");
    }

}]);

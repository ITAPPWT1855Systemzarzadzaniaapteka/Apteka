﻿$(function () {

    $("#listagem").tablesorter();
    function search(search) {

        $("#listagem tr:not(:contains('" + search + "'))").css("display", "none");
        $("#listagem tr:contains('" + search + "')").css("display", "");
    }
    $('#filter').on('keyup', function () {
        var text = $(this).val();
        console.log(text);
        search(text);
    });


});
var Apteka = angular.module('Apteka', []);
Apteka.controller('checkinvoicecontent', ["$scope", "$http", function ($scope, $http) { }]);
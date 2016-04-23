
var Apteka = angular.module('Apteka', []);
Apteka.controller('warehouse', ["$scope", "$http", function ($scope, $http) {

    $('#filter').on('keyup', function () {
        var text = $(this).val();
        search(text);
    });
    function search(search) {
        $("#listagem tr:not(:contains('" + search + "'))").css("display", "none");
        $("#listagem tr:contains('" + search + "')").css("display", "");
    }

}]);
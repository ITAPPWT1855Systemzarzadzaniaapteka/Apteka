
var Apteka = angular.module('Apteka', []);


Apteka.service('fakturaService', function ($http) {
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

});
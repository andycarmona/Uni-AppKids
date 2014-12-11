var SpicyController = function ($scope, $http, $window, userService) {
    $scope.spice = 'very';

    $scope.chiliSpicy = function () {
        $scope.spice = 'chili';
    };

    $scope.jalapenoSpicy = function () {
        $scope.spice = 'jalapeño';
    };
}

SpicyController.$inject = ['$scope', '$http', '$window', 'userService'];

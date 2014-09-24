appRoot.controller('AboutController', function($scope, $location, $resource) {
    $scope.oneAtATime = true;
    //default: all countrys
    $scope.country = 'ddd';
    //example data
    $scope.listcity = [{
        name: 'Madrid',
        country: '3'
    }, {
        name: 'Paris',
        country: '2'
    }, {
        name: 'Lyon',
        country: '2'
    }, {
        name: 'Zurich',
        country: '1'
    }];

    //set a new selection
    $scope.setCountry = function (cid) {
        $scope.country = cid;
    }

})
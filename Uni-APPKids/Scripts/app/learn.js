angular.module('main')
    .controller('LearnController', ['$scope', '$location', '$resource', function ($scope, $location, $resource) {
    var dictionaryResource = $resource('/api/dictionary', {}, { update: { method: 'PUT' } });

    $scope.dictionaryList = [];

    dictionaryResource.query(function (data) {
        $scope.dictionaryList = [];
        angular.forEach(data, function (dictionaryData) {
            $scope.dictionaryList.push(dictionaryData);
        });
    });

    $scope.dictionary = 'ddd';
        //example data
    $scope.listdictionary = {
        data: [{
            id: 'id1',
            name: 'name1'
        }, {
            id: 'id2',
            name: 'name2'
        }]
    };

    $scope.oneAtATime = true;

    $scope.groups = [
      {
          title: 'Dynamic Group Header - 1',
          content: 'Dynamic Group Body - 1'
      },
      {
          title: 'Dynamic Group Header - 2',
          content: 'Dynamic Group Body - 2'
      }
    ];

    $scope.items = ['Item 1', 'Item 2', 'Item 3'];

    $scope.addItem = function () {
        var newItemNo = $scope.items.length + 1;
        $scope.items.push('Item ' + newItemNo);
    };

    $scope.status = {
        isFirstOpen: true,
        isFirstDisabled: false
    };

        //set a new selection
    $scope.setDictionary = function (cid) {
        $scope.dictionary = cid;
    }

}]);
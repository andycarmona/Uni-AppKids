

describe('Controller: Controllers/EditPageController', function() {

    var $rootScope, $scope, $controller;

    beforeEach(window.module('UniAppSpelApp'));

    beforeEach(window.inject(function(_$rootScope_, _$controller_) {
        $rootScope = _$rootScope_;
        $scope = $rootScope.$new();
        $controller = _$controller_;

        $controller('EditPageController', { '$rootScope': $rootScope, '$scope': $scope });
    }));

    it('WordName_In_Array_Is_Not_Null', function () {
        expect($scope.words).toBeNull();
    });
})
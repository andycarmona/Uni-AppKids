UniAppSpelApp.service("userService", function ($http) {
    var userDictionary= {};
    this.GetUsers = function () {
        return this.users = ['John', 'James', 'Jake'];
    }
    
    this.GetRequest = function (aUrl) {
        var request = $http({
            method: "get",
            url: aUrl
        });
        return (request.then(handleSuccess, handleError));

    }


    this.PostRequest = function (aUrl) {
        var request = $http({
            method: "post",
            url: aUrl,
            datatype:"json"
        });
        return (request.then(handleSuccess, handleError));

    }

    function handleError(response) {

        if (!angular.isObject(response.data) || !response.data.message)
        {
            
            return (response.data);

        }

   
        return (response.data.message);

    }


    function handleSuccess(response) {

        return (response.data);

    }
});
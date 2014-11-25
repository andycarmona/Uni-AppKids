UniAppSpelApp.service("userService", function ($http) {
    var userDictionary= {};
    this.GetUsers = function () {
        return this.users = ['John', 'James', 'Jake'];
    }
    
    this.GetRequest = function (aUrl) {
        var request = $http({
            method: "get",
            url: aUrl
            
            //params: {
            //    action: "get"
            //}
        });
        return (request.then(handleSuccess, handleError));

    }


    this.PostRequest = function (aUrl) {
        var request = $http({
            method: "post",
            url: aUrl,
            datatyp:"json"

            //params: {
            //    action: "get"
            //}
        });
        return (request.then(handleSuccess, handleError));

    }

    function handleError(response) {

        if (!angular.isObject(response.data) || !response.data.message)
        {
            
            return (true);

        }

   
        return (response.data.message);

    }


    function handleSuccess(response) {

        return (response.data);

    }
});
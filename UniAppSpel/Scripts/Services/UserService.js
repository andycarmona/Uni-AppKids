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
        return (request);

    }


    this.PostRequest = function (aUrl) {
        var request = $http({
            method: "post",
            url: aUrl,
            datatype:"json"
        });
        return (request);

    }
});
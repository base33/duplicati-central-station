angular.element(document.getElementsByTagName('head')).append(angular.element('<base href="' + window.location.pathname + '" />'));

var centralstation = angular.module("centralstation", ["ngRoute", "chart.js"]).run(["$route", function ($route) {
    $route.reload();
}]);

centralstation.config(function ($routeProvider, $locationProvider, ChartJsProvider) {
    $routeProvider
     .when('/', {
         templateUrl: '/views/dashboard.html',
         controller: 'DashboardController'
     })
    .when('/instances/add', {
        templateUrl: '/views/instance_add.html',
        controller: 'InstanceAddController'
    })
    .when('/instances', {
        templateUrl: '/views/instance_list.html',
        controller: 'InstanceListController'
    })
    .when('/instances/detailed/:id', {
        templateUrl: '/views/instance_detailed.html',
        controller: 'InstanceDetailedController'
    })
    .when('/instances/detailed/:id/backup/:reportid', {
        templateUrl: '/views/instance_detailed_backup.html',
        controller: 'InstanceDetailedBackupController'
    });


    $locationProvider.html5Mode(false);

    // Configure all charts
    ChartJsProvider.setOptions({
        colors: ['#97BBCD', '#DCDCDC', '#F7464A', '#46BFBD', '#FDB45C', '#949FB1', '#4D5360']
    });
    // Configure all doughnut charts
    ChartJsProvider.setOptions('doughnut', {
        cutoutPercentage: 60
    });
    ChartJsProvider.setOptions('bubble', {
        tooltips: { enabled: false }
    });
});

centralstation.controller('MainController', function ($scope, $route, $routeParams, $location) {
    $scope.$route = $route;
    $scope.$location = $location;
    $scope.$routeParams = $routeParams;
});

centralstation.controller('DashboardController', function ($scope, $routeParams, $http) {
    $scope.name = 'DashboardController';
    $scope.params = $routeParams;

    $http.get("/api/backups/latest").then((response) => {
        $scope.backups = response.data;

        $scope.totalFlagged = 0;
        $scope.totalSuccess = 0;

        for (var i = 0; i < $scope.backups.length; i++) {
            if ($scope.backups[i].ShouldBeFlagged) {
                $scope.totalFlagged++;
            }
            else {
                $scope.totalSuccess++;
            }
        }
    });
});

centralstation.controller('InstanceAddController', function ($scope, $routeParams, $http, $location) {
    $scope.name = 'InstanceAddController';
    $scope.params = $routeParams;

    $scope.submit = function () {
        if ($scope.saving)
            return;

        $scope.saving = true;
        $http.post("/api/instances", $scope.instance).then(function () {
            $location.path("/");
        });
    }
});

centralstation.controller('InstanceListController', function ($scope, $routeParams, $http, $location) {
    $scope.name = 'InstanceListController';
    $scope.params = $routeParams;



    $scope.delete = function (instance) {
        $http.delete("/api/instances/" + instance.Id).then(function () {
            load();
        });
    }

    function load() {
        $http.get("/api/instances").then(function (response) {
            $scope.instances = response.data;

            for (var i = 0; i < $scope.instances.length; i++) {
                $scope.instances[i]["NotificationUrl"] = $location.protocol() + "://" + $location.host() + ":" + $location.port() + "/api/reports/" + $scope.instances[i].Id;
            }
        });
    }

    load();
});

centralstation.controller('InstanceDetailedController', function ($scope, $routeParams, $http, $location) {
    $scope.name = 'InstanceDetailedController';
    $scope.params = $routeParams;

    function load() {
        $http.get("/api/backups/" + $routeParams.id).then(function (response) {
            $scope.instance = response.data;
            BuildFileGrowthGraph($scope.instance.PreviousReports)
        });
    }

    load();


    function BuildFileGrowthGraph(data) {
        $scope.graph = {};
        $scope.graph.labels = [];
        $scope.graph.series = ['Size in KB'];
        $scope.graph.data = [
          []
        ];
        $scope.graph.onClick = function (points, evt) {
            console.log(points, evt);
        };
        $scope.graph.onHover = function (points) {
            if (points.length > 0) {
                console.log('Point', points[0].value);
            } else {
                console.log('No point');
            }
        };

        $scope.graph.options = {
            scales: {
                yAxes: [
                  {
                      id: 'y-axis-1',
                      type: 'linear',
                      display: true,
                      position: 'left'
                  }
                ]
            },
            height: 100
        };

        for (var i = data.length - 1; i >= 0; i--) {
            $scope.graph.labels.push(formatDate(data[i].BeginDate));
            $scope.graph.data[0].push(data[i].SizeOfExaminedFiles);
        }
    }

    function formatDate(string) {
        var d = new Date(string);
        return d.getFullYear() + "/" +
            ("00" + (d.getMonth() + 1)).slice(-2) + "/" +
                ("00" + d.getDate()).slice(-2) + " " +
                ("00" + d.getHours()).slice(-2) + ":" +
                ("00" + d.getMinutes()).slice(-2) + ":" +
                ("00" + d.getSeconds()).slice(-2);
    }

});

centralstation.controller('InstanceDetailedBackupController', function ($scope, $routeParams, $http, $location) {
    $scope.name = 'InstanceDetailedController';
    $scope.params = $routeParams;

    function load() {
        $http.get("/api/backups/" + $routeParams.id + "/backup/" + $routeParams.reportid).then(function (response) {
            $scope.backup = response.data;
        });
    }

    load();

});
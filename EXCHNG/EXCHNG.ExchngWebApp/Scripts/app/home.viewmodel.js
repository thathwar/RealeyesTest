function HomeViewModel(app, dataModel) {
    var self = this;
    self.errorMessage = ko.observable("");
    self.currencies = ko.observableArray();
    self.fromCurrency = ko.observable("SELECT");
    self.toCurrency = ko.observable("SELECT");
    self.graphCurrency = ko.observable("SELECT");
    self.fromValue = ko.observable(0);
    self.toValue = ko.observable(0);
    self.conversionValue = ko.observable(0.0);
    self.exchangeTextVisible = ko.observable(false);
    self.computing = ko.observable(false);
    self.loading = ko.observable(false);
    self.errorMessageGraph = ko.observable("");
    self.graphContainer = ko.observable("");

    self.fromCurrency.subscribe(function (out) {
        self.errorMessage("");
        if (out == "SELECT") {
            self.exchangeTextVisible(false);
            self.toValue(0);
            return;
        }

        compute();
    });

    self.toCurrency.subscribe(function (out) {
        self.errorMessage("");
        if (out == "SELECT") {
            self.exchangeTextVisible(false);
            self.toValue(0);
            return;
        }

        compute();
    });

    self.onFromValueChange = function (data, e) {
        if ((e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 96 && e.keyCode <= 105) || e.keyCode == 8) {
            self.errorMessage("");
            compute();
        }
    };


    self.graphCurrency.subscribe(function (out) {
        self.errorMessageGraph("");
        if (out == "SELECT") {
            clearGraphContainer();
            return;
        }

        self.loading(true);
        clearGraphContainer();
        buildGraphContainer();

        //call to server to get graph data
        dataModel.getHistoricalGraphData({ currency: self.graphCurrency() })
        .done(function (output) {
            if (output.errorMessage == null) {
                //Draw rickshaw graph
                var graph = new Rickshaw.Graph({
                    element: document.getElementById("chart"),
                    width: 400,
                    height: 100,
                    renderer: 'line',
                    series: [
                        {
                            color: "#6060c0",
                            data: output.graphDatas,
                            name: output.currency
                        }
                    ]
                });

                graph.render();

                var hoverDetail = new Rickshaw.Graph.HoverDetail({
                    graph: graph
                });

                var legend = new Rickshaw.Graph.Legend({
                    graph: graph,
                    element: document.getElementById('legend')

                });

                var shelving = new Rickshaw.Graph.Behavior.Series.Toggle({
                    graph: graph,
                    legend: legend
                });

                var axes = new Rickshaw.Graph.Axis.Time({
                    graph: graph,
                });
                axes.render();
            }
            else {
                self.errorMessageGraph(output.errorMessage);
            }

            self.loading(false);
        })
         .fail(function (output) {
             self.loading(false);
             self.errorMessageGraph(output.message);
         });
    });

    //call to server compute method
    function compute() {
        if (self.fromCurrency() != "SELECT" && self.toCurrency() != "SELECT") {
            self.computing(true);
            dataModel.compute({
                fromCurrency: self.fromCurrency(),
                toCurrency: self.toCurrency(),
                fromValue: $("#fromValue").val()
            })
                .done(function (output) {
                    if (output.errorMessage == null) {
                        self.toValue(parseFloat(output.toValue).toFixed(3));
                        self.conversionValue(parseFloat(output.conversionValue).toFixed(3));
                        self.exchangeTextVisible(true);
                    } else {
                        self.errorMessage(output.errorMessage);
                        self.exchangeTextVisible(false);
                    }
                    self.computing(false);
                })
                .fail(function (output) {
                    self.computing(false);
                    self.errorMessage(output.message);
                    self.exchangeTextVisible(false);
                });
        }
    };

    function clearGraphContainer() {
        self.graphContainer("");
    }

    function buildGraphContainer() {
        self.graphContainer('<div id="chart_container"><div id="chart"></div><div id="legend_container"><div id="smoother" title="Smoothing"></div><div id="legend"></div></div><div id="slider"></div></div>');
    }

    return self;
}

app.addViewModel({
    name: "Home",
    bindingMemberName: "home",
    factory: HomeViewModel
});

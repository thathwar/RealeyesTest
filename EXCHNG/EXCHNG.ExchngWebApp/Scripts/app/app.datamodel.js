function AppDataModel() {
    var self = this;
    self.computeUrl = "/api/Exchange/Compute";
    self.getHistoricalGraphDataUrl = "/api/Exchange/GetHistoricalGraphData";

    // Operations
    self.compute = function (data) {
        return $.ajax({
            method: 'post',
            data: data,
            url: self.computeUrl,
        });
    };

    self.getHistoricalGraphData = function (data) {
        return $.ajax({
            method: 'post',
            data: data,
            url: self.getHistoricalGraphDataUrl,
        });
    };
}

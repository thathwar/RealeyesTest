function HomeViewModel(app, dataModel) {
    var self = this;
    self.errorMessage = ko.observable("");
    self.currencies = ko.observableArray();
    self.fromCurrency = ko.observable("SELECT");
    self.toCurrency = ko.observable("SELECT");
    self.conversionValue = ko.observable(0);

    return self;
}

app.addViewModel({
    name: "Home",
    bindingMemberName: "home",
    factory: HomeViewModel
});

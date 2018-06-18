window.mb.designSettings = {

    templates: {
        mainSettings: "#settingsTpl",
    },

    $selectTemplateType: '#selectTemplateType',
    $selectSubTemplateDesign: "#selectSubTemplateDesign",
    $selectCurrency: "#selectCurrency",
    $saveDesignSettings: ".saveDesignSettings",

    id: null,
    subTemplates: null,
    templateTypes: null,

    templateType: null,
    subTemplateDesign: null,
    subTemplateSettings: null,
    currency: null,
    currencies: null,

    loadDesignSettings: function (slideId, callbackFun) {
        var self = this;
        window.mb.httpWrapper.get({
            url: '/Slide/GetDesignSettings/?slideId=' + slideId ,
            success: function (response) {

                // Register all sub templates
                window.mb.templateManager.registerTemplate("SingleColumn", "SingleColumnTmpl");
                window.mb.templateManager.registerTemplate("TwoColumn", "TwoColumnTmpl");
                window.mb.templateManager.registerTemplate("ThreeColumn", "ThreeColumnTmpl");

                if (response !== "error") {

                    self.Id = response.Id;
                    self.templateTypes = response.TemplateTypeOptions;
                    self.templateType = response.TemplateType || response.TemplateTypeOptions[0].Id;

                    self.subTemplates = response.SubTemplates;
                    self.subTemplate = response.SubTemplate || response.SubTemplates[0].Id;

                    self.currency = response.Currency;
                    self.currencies = response.CurrencyOptions;

                    // Set template settings
                    window.mb.templateManager.settings = response.TemplateSettings || response.DefaultTemplateSettings[0];
                    window.mb.templateManager.listOfDefaultSettings = response.DefaultTemplateSettings;

                    if (callbackFun) {
                        callbackFun();
                    }
                }
            },
            error: function () { }
        });
    },

    initialiseSelectOptions: function () {
        var self = this;


        //Render main templates
        self.clearSelectElement(self.$selectTemplateType);
        for (var i = 0; i < self.templateTypes.length; i++) {
            var type = self.templateTypes[i];
            self.appendTemplateTypeToSelectElement(type);
        }

        // Render sub templates
        self.clearSelectElement(self.$selectSubTemplateDesign);
        for (var j = 0; j < self.subTemplates.length; j++) {
            var sub = self.subTemplates[j];
            if (sub.ParentTemplateId === self.templateType) {
                self.appendSubDesignTemplate(sub);
            }
        }

        // Render currencies
        self.renderCurrencies();

        // Render template settings
        window.mb.templateManager.showTemplateSettings();
    },

    clearSelectElement: function (eleId) {
        $(eleId).empty();
    },

    renderCurrencies: function() {
        var self = this;
        self.clearSelectElement(self.$selectCurrency);

        var select = $(self.$selectCurrency);

        for (var i = 0; i < self.currencies.length; i++) {
            var cur = self.currencies[i];

            var option = '<option value="' + cur.Value + '"';
            if (cur.Value === self.currency) {
                option += ' selected ';
            }
            option += ('>' + cur.Text + '</option>');

            select.append(option);
        }
    },

    appendTemplateTypeToSelectElement: function (template) {
        var self = this;

        var option = '<option value="' + template.Id + '"';
        if (template.Selected) {
            option += ' selected ';
        }
        option += ('>' + template.Name + '</option>');

        var select = $(self.$selectTemplateType);
        select.append(option);
    },

    appendSubDesignTemplate: function (subTemplate) {
        var self = this;

        var option = '<option value="' + subTemplate.Id + '"';
        if (subTemplate.Selected) {
            option += ' selected ';
        }
        option += ('>' + subTemplate.Name + '</option>');
        var select = $(self.$selectSubTemplateDesign);
        select.append(option);
    },

    displayChildSubDesignTemplates: function (parentId) {
        var self = this;
        var sublist = [];
        for (var i = 0; i < self.subTemplates.length; i++) {
            if (self.subTemplates[i].ParentTemplateId === parentId) {
                sublist.push(self.subTemplates[i]);
            }
        }

        self.clearSelectElement(self.$selectSubTemplateDesign);
        if (sublist.length) {
            for (var j = 0; j < sublist.length; j++) {
                self.appendSubDesignTemplate(sublist[j]);
            }
        }
    },

    showSettings: function (slideId) {
        var self = this;
        self.loadDesignSettings(slideId, function () {

            $(self.templates.mainSettings).modal({
                show: true,
                keyboard: true,
                focus: true
            });

            self.initialiseSelectOptions();


            $(self.templates.mainSettings).on('shown.bs.modal',
            function (e) {
                $(self.templates.mainSettings).find(self.$selectTemplateType).on('change', function () {

                    var val = $(this).val();
                    self.displayChildSubDesignTemplates(val);

                    var selectedTemp = null;
                    for (var i = 0; i < self.templateTypes.length; i++) {
                        if (self.templateTypes[i].Id == val) {
                            selectedTemp = self.templateTypes[i];
                            break;
                        }
                    }

                    if (selectedTemp) {
                        window.mb.templateManager.showSelectedTemplate(selectedTemp.HtmlTemplateId);
                    }
                });

                $(self.templates.mainSettings).find(self.$saveDesignSettings).on('click', function () {
                    self.saveDesignSettings();
                });
            });

            $(self.templates.mainSettings).on('hide.bs.modal',
                function (e) {
                    $(self.templates.mainSettings).off();
                    $(self.templates.mainSettings).find(self.$selectTemplateType).off();
                    $(self.templates.mainSettings).find(self.$selectSubTemplateDesign).off();
                    $(self.templates.mainSettings).find(self.$saveDesignSettings).off();
                });
        });
        
    },

    saveDesignSettings: function() {
        var self = this;

        window.mb.templateManager.updateSettings();
        self.currency = $(self.$selectCurrency).val();
        self.templateType = $(self.$selectTemplateType).val();
        self.subTemplate = $(self.$selectSubTemplateDesign).val();

        var url = '/Slide/';
        var isUrlValid = true;

        switch (self.templateType) {
            case "1":
                url += "SaveSingleColDesignSettings";
                break;
            case "2":
                url += "SaveTwoColDesignSettings";
                break;
            case "3":
                url += "SaveThreeColDesignSettings";
                break;
            default:
                isUrlValid = false;
                break;
        }

        if (isUrlValid) {

            window.mb.httpWrapper.post({
                url: url,
                data: {
                    Id: self.Id || window.newguid(),
                    SlideId: window.mb.slideId,
                    Currency: self.currency,
                    TemplateType: self.templateType,
                    SubTemplate: self.subTemplate,
                    TemplateSettingsValues: window.mb.templateManager.settings
                },
                success: function(response) {
                    if (response && response.Success) {
                        $(self.templates.mainSettings).modal("hide");
                    }
                },
                error: function() {}
            });
        }
    }
}
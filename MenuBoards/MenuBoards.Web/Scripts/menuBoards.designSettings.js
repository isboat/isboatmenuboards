window.menuBoards.designSettings = {

    templates: {
        mainSettings: "#settingsTpl",
    },

    $selectTemplateType: '#selectTemplateType',
    $selectSubTemplateDesign: "#selectSubTemplateDesign",
    $selectCurrency: "#selectCurrency",
    $saveDesignSettings: ".saveDesignSettings",

    subTemplates: null,
    templateTypes: null,

    templateType: null,
    subTemplateDesign: null,
    subTemplateSettings: null,
    currency: null,
    currencies: null,

    loadDesignSettings: function () {
        var self = this;
        window.menuBoards.httpWrapper.get({
            url: '/Slide/GetDesignSettings',
            success: function (response) {

                if (response && response !== "error") {
                    self.templateTypes = response.TemplateTypeOptions;
                    self.subTemplates = response.SubTemplates;
                    self.templateType = response.TemplateType;
                    self.selectedSubTemplate = response.SelectedSubTemplate;
                    self.currency = response.Currency;
                    self.currencies = response.CurrencyOptions;

                    // Set sub template settings
                    window.menuBoards.subTemplateSettings.settings = response.SubTemplateSettings;
                }
            },
            error: function () { }
        });

        // Also load default sub template settings
        window.menuBoards.subTemplateSettings.loadDefaultSubTemplateSettings();
    },

    initialiseSelectOptions: function () {
        var self = this;

        self.clearSelectElement(self.$selectTemplateType);

        //Render main templates
        for (var i = 0; i < self.templateTypes.length; i++) {
            var type = self.templateTypes[i];
            type.Selected = type.Value === self.templateType;
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

        // Render sub template settings
        window.menuBoards.subTemplateSettings.showDefaultTemplate();
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

        var option = '<option value="' + template.Value + '"';
        if (template.Selected) {
            option += ' selected ';
        }
        option += ('>' + template.Text + '</option>');

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

    showSettings: function () {
        var self = this;

        $(self.templates.mainSettings).modal({
            show: true,
            keyboard: true,
            focus: true
        });

        self.initialiseSelectOptions();

        $(self.templates.mainSettings).on('shown.bs.modal',
            function (e) {
                $(self.templates.mainSettings).find(self.$selectTemplateType).on('change', function () {

                    self.displayChildSubDesignTemplates($(this).val());
                });

                $(self.$selectSubTemplateDesign).on('change', function () {
                    var subTemplateId = $(this).val();
                    var foundSubTemplate = null;

                    if (subTemplateId) {
                        for (var i = 0; i < self.subTemplates.length; i++) {
                            if (self.subTemplates[i].Id == subTemplateId) {
                                foundSubTemplate = self.subTemplates[i];
                                break;
                            }
                        }

                        if (foundSubTemplate && foundSubTemplate.HtmlTemplateId) {
                            window.menuBoards.subTemplateSettings.appendTemplate(foundSubTemplate
                                .HtmlTemplateId);
                        }
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
    },

    saveDesignSettings: function() {
        var self = this;

        window.menuBoards.subTemplateSettings.setSettings();
        self.currency = $(self.$selectCurrency).val();
        self.templateType = $(self.$selectTemplateType).val();
        self.selectedSubTemplate = $(self.$selectSubTemplateDesign).val();
    }
}
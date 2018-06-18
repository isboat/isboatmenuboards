window.mb.templateManager = {
    settings: null,
    currentTemplate: null,
    listOfDefaultSettings: null,
    templateListIndexer: {},
    
    registerTemplate: function (templateName, eleIdentifier) {
        var self = this;

        var templ = {

            settings: null,

            render: function(settings) {
                this.settings = settings;

                var template = document.getElementById(eleIdentifier).innerHTML;

                var $subTplSection = $(".designSettingsForm .subTplSettings");
                $subTplSection.empty();

                $subTplSection.html(template);

                window.rivets.bind($(".designSettingsForm .subTplSettings"), { settings: this.settings });
            }
        };

        self.templateListIndexer[templateName] = templ;
    },

    showTemplateSettings: function () {
        var self = this;
        if (self.settings) {
            self.renderTemplate(self.settings.HtmlTemplateId, self.settings);
        } else {
            var first = self.listOfDefaultSettings[0];
            if (first) {
                self.showSelectedTemplate(first.HtmlTemplateId);
            }
        }
    },

    showSelectedTemplate: function (tmpId) {
        var self = this;
        if (self.listOfDefaultSettings) {
            var settingsToRender = null;

            for (var i = 0; i < self.listOfDefaultSettings.length; i++) {
                if (self.listOfDefaultSettings[i].HtmlTemplateId == tmpId) {
                    settingsToRender = self.listOfDefaultSettings[i];
                    break;
                }
            }

            if (settingsToRender) {
                self.renderTemplate(tmpId, settingsToRender);
            }
        }
    },

    renderTemplate: function (tmpId, values) {

        this.currentTemplate = tmpId;
        this.templateListIndexer[this.currentTemplate].render(values);
    },

    updateSettings: function() {
        this.settings = this.templateListIndexer[this.currentTemplate].settings;
    }
}